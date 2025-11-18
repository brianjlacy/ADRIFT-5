using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ADRIFT.ViewModels;
public partial class EventListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    public EventListViewModel(IAdventureService adventureService) { _adventureService = adventureService; FilteredEvents = new(); }
    [ObservableProperty] private ObservableCollection<EventItemViewModel> filteredEvents;
    [ObservableProperty] private string searchText = "";
    public async Task LoadEventsAsync() { var events = await _adventureService.GetEventsAsync(); FilteredEvents.Clear(); foreach (var e in events) FilteredEvents.Add(new(e)); }
    [RelayCommand] private async Task AddEvent() => await Shell.Current.GoToAsync("eventeditor");
    [RelayCommand] private async Task EditEvent(EventItemViewModel e) => await Shell.Current.GoToAsync($"eventeditor?key={e.Key}");
}
public partial class EventItemViewModel : ObservableObject
{
    public EventItemViewModel(object e) { Key = "evt" + Guid.NewGuid().ToString("N")[..8]; Name = "Event"; Description = "Description"; Timing = "Turn-based"; }
    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private string timing = "";
}
