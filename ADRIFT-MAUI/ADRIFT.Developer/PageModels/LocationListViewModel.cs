using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

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

    private void FilterLocations()
    {
        // TODO: Implement filtering based on SearchText
        // For now, just reload
        _ = LoadLocationsAsync();
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
            // TODO: Implement delete functionality
            FilteredLocations.Remove(location);
            LocationCount = FilteredLocations.Count;
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
            // TODO: Implement sorting
            await Shell.Current.DisplayAlert("Sort", $"Sorting by: {action}", "OK");
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
            // TODO: Implement filtering
            await Shell.Current.DisplayAlert("Filter", $"Filter: {action}", "OK");
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

    public LocationItemViewModel(object location) : this()
    {
        // TODO: Map from clsLocation to view model properties
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
