using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class EventListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public EventListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredEvents = new ObservableCollection<EventItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<EventItemViewModel> filteredEvents;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int eventCount;

    [ObservableProperty]
    private bool isRefreshing;

    partial void OnSearchTextChanged(string value)
    {
        FilterEvents();
    }

    public async Task LoadEventsAsync()
    {
        try
        {
            IsRefreshing = true;
            var events = await _adventureService.GetEventsAsync();

            FilteredEvents.Clear();
            foreach (var evt in events)
            {
                FilteredEvents.Add(new EventItemViewModel(evt));
            }

            EventCount = FilteredEvents.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async void FilterEvents()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadEventsAsync();
            return;
        }

        try
        {
            var allEvents = await _adventureService.GetEventsAsync();
            var filtered = allEvents.Where(e =>
                e.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                e.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                e.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredEvents.Clear();
            foreach (var evt in filtered)
            {
                FilteredEvents.Add(new EventItemViewModel(evt));
            }

            EventCount = FilteredEvents.Count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter events: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddEvent() => await Shell.Current.GoToAsync("eventeditor");

    [RelayCommand]
    private async Task EditEvent(EventItemViewModel evt) =>
        await Shell.Current.GoToAsync($"eventeditor?key={evt.Key}");

    [RelayCommand]
    private async Task DeleteEvent(EventItemViewModel evt)
    {
        var result = await Shell.Current.DisplayAlert(
            "Delete Event",
            $"Are you sure you want to delete '{evt.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Events.ContainsKey(evt.Key))
                {
                    adventure.Events.Remove(evt.Key);
                    FilteredEvents.Remove(evt);
                    EventCount = FilteredEvents.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete event: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Refresh() => await LoadEventsAsync();

    [RelayCommand]
    private async Task Sort()
    {
        var action = await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Name (A-Z)", "Name (Z-A)", "Type", "Timing");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Name (A-Z)" => FilteredEvents.OrderBy(e => e.Name).ToList(),
                "Name (Z-A)" => FilteredEvents.OrderByDescending(e => e.Name).ToList(),
                "Type" => FilteredEvents.OrderBy(e => e.EventType).ToList(),
                "Timing" => FilteredEvents.OrderBy(e => e.Timing).ToList(),
                _ => FilteredEvents.ToList()
            };

            FilteredEvents.Clear();
            foreach (var item in sorted)
            {
                FilteredEvents.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Events", "Time-Based", "Triggered", "Repeating");

        if (action != null && action != "Cancel")
        {
            try
            {
                var allEvents = await _adventureService.GetEventsAsync();
                IEnumerable<Event> filtered = action switch
                {
                    "Time-Based" => allEvents.Where(e => e.Type == EventType.TimeBased),
                    "Triggered" => allEvents.Where(e => e.Type == EventType.Triggered),
                    "Repeating" => allEvents.Where(e => e.Type == EventType.Repeating),
                    _ => allEvents
                };

                FilteredEvents.Clear();
                foreach (var evt in filtered)
                {
                    FilteredEvents.Add(new EventItemViewModel(evt));
                }

                EventCount = FilteredEvents.Count;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter events: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterEvents();
        await Task.CompletedTask;
    }
}

public partial class EventItemViewModel : ObservableObject
{
    public EventItemViewModel()
    {
        Key = "evt" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Name = "Sample Event";
        Description = "A sample event";
        EventType = "Triggered";
        Timing = "Immediate";
    }

    public EventItemViewModel(Event evt) : this()
    {
        Key = evt.Key;
        Name = evt.Name;
        Description = evt.Description;
        EventType = evt.Type.ToString();
        Timing = evt.Trigger.ToString();
    }

    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private string eventType = "";
    [ObservableProperty] private string timing = "";
}
