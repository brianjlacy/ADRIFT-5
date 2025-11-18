using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class LocationListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public LocationListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredLocations = new ObservableCollection<LocationItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<LocationItemViewModel> filteredLocations;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int locationCount;

    [ObservableProperty]
    private bool isRefreshing;

    partial void OnSearchTextChanged(string value)
    {
        FilterLocations();
    }

    public async Task LoadLocationsAsync()
    {
        try
        {
            IsRefreshing = true;

            var locations = await _adventureService.GetLocationsAsync();

            FilteredLocations.Clear();
            foreach (var location in locations)
            {
                FilteredLocations.Add(new LocationItemViewModel(location));
            }

            LocationCount = FilteredLocations.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async void FilterLocations()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadLocationsAsync();
            return;
        }

        try
        {
            var allLocations = await _adventureService.GetLocationsAsync();
            var filtered = allLocations.Where(l =>
                l.ShortDescription.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                l.LongDescription.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                l.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredLocations.Clear();
            foreach (var location in filtered)
            {
                FilteredLocations.Add(new LocationItemViewModel(location));
            }

            LocationCount = FilteredLocations.Count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter locations: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddLocation()
    {
        await Shell.Current.GoToAsync("locationeditor");
    }

    [RelayCommand]
    private async Task EditLocation(LocationItemViewModel location)
    {
        await Shell.Current.GoToAsync($"locationeditor?key={location.Key}");
    }

    [RelayCommand]
    private async Task DeleteLocation(LocationItemViewModel location)
    {
        var result = await Shell.Current.DisplayAlert(
            "Delete Location",
            $"Are you sure you want to delete '{location.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Locations.ContainsKey(location.Key))
                {
                    adventure.Locations.Remove(location.Key);
                    FilteredLocations.Remove(location);
                    LocationCount = FilteredLocations.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete location: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadLocationsAsync();
    }

    [RelayCommand]
    private async Task Sort()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Sort By",
            "Cancel",
            null,
            "Name (A-Z)", "Name (Z-A)", "Recently Modified", "Key");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Name (A-Z)" => FilteredLocations.OrderBy(l => l.Name).ToList(),
                "Name (Z-A)" => FilteredLocations.OrderByDescending(l => l.Name).ToList(),
                "Recently Modified" => FilteredLocations.OrderByDescending(l => l.Key).ToList(),
                "Key" => FilteredLocations.OrderBy(l => l.Key).ToList(),
                _ => FilteredLocations.ToList()
            };

            FilteredLocations.Clear();
            foreach (var item in sorted)
            {
                FilteredLocations.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet(
            "Filter",
            "Cancel",
            null,
            "All Locations", "Hidden Only", "Visible Only", "Library Only");

        if (action != null && action != "Cancel")
        {
            try
            {
                var allLocations = await _adventureService.GetLocationsAsync();
                IEnumerable<AdriftLocation> filtered = action switch
                {
                    "Hidden Only" => allLocations.Where(l => l.HideOnMap),
                    "Visible Only" => allLocations.Where(l => !l.HideOnMap),
                    "Library Only" => allLocations.Where(l => l.IsLibrary),
                    _ => allLocations
                };

                FilteredLocations.Clear();
                foreach (var location in filtered)
                {
                    FilteredLocations.Add(new LocationItemViewModel(location));
                }

                LocationCount = FilteredLocations.Count;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter locations: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterLocations();
        await Task.CompletedTask;
    }
}

// ViewModel for individual location items in the list
public partial class LocationItemViewModel : ObservableObject
{
    public LocationItemViewModel()
    {
        Key = "loc" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Name = "Sample Location";
        ShortDescription = "A sample location";
        ExitCount = 0;
        IsHidden = false;
        IsLibrary = false;
    }

    public LocationItemViewModel(AdriftLocation location) : this()
    {
        Key = location.Key;
        Name = location.ShortDescription;
        ShortDescription = location.ShortDescription;
        ExitCount = location.Directions.Count;
        IsHidden = location.HideOnMap;
        IsLibrary = location.IsLibrary;
    }

    [ObservableProperty]
    private string key = "";

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string shortDescription = "";

    [ObservableProperty]
    private int exitCount;

    [ObservableProperty]
    private bool isHidden;

    [ObservableProperty]
    private bool isLibrary;
}
