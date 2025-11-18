using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

public partial class LocationEditorViewModel : ObservableObject
{
    [ObservableProperty]
    private string locationName = "New Location";

    [ObservableProperty]
    private string locationKey = "";

    [ObservableProperty]
    private string shortDescription = "";

    [ObservableProperty]
    private string longDescription = "";

    [ObservableProperty]
    private bool hasViewFromHere;

    [ObservableProperty]
    private string viewFromHereDescription = "";

    [ObservableProperty]
    private bool isHidden;

    [ObservableProperty]
    private bool isLibraryItem;

    [ObservableProperty]
    private string currentTab = "description";

    [ObservableProperty]
    private string statusMessage = "Ready";

    public ObservableCollection<DirectionViewModel> Directions { get; } = new();
    public ObservableCollection<PropertyViewModel> Properties { get; } = new();

    public LocationEditorViewModel()
    {
        LoadLocation();
    }

    private void LoadLocation()
    {
        // TODO: Load location from service
        LocationKey = GenerateKey();
        InitializeDirections();
        InitializeProperties();
    }

    private string GenerateKey()
    {
        return "Location" + Guid.NewGuid().ToString("N").Substring(0, 8);
    }

    private void InitializeDirections()
    {
        // Initialize with common directions
        var directions = new[] { "North", "South", "East", "West", "Up", "Down", "In", "Out" };
        // TODO: Add actual direction initialization
    }

    private void InitializeProperties()
    {
        // TODO: Load available properties from adventure
    }

    [RelayCommand]
    private void SelectTab(string tabName)
    {
        CurrentTab = tabName;
    }

    [RelayCommand]
    private async Task FormatText()
    {
        // TODO: Show formatting toolbar
        await Shell.Current.DisplayAlert("Format", "Text formatting options", "OK");
    }

    [RelayCommand]
    private async Task InsertFunction()
    {
        // TODO: Show function picker
        await Shell.Current.DisplayAlert("Functions", "Available text functions", "OK");
    }

    [RelayCommand]
    private void AddDirection()
    {
        // TODO: Add new direction
        StatusMessage = "Direction added";
    }

    [RelayCommand]
    private async Task Cancel()
    {
        var result = await Shell.Current.DisplayAlert(
            "Cancel",
            "Discard changes?",
            "Yes", "No");

        if (result)
        {
            await Shell.Current.GoToAsync("..");
        }
    }

    [RelayCommand]
    private void Apply()
    {
        // TODO: Save changes without closing
        StatusMessage = "Changes applied";
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        // TODO: Save location
        StatusMessage = "Location saved";
        await Task.Delay(500);
        await Shell.Current.GoToAsync("..");
    }
}

public partial class DirectionViewModel : ObservableObject
{
    [ObservableProperty]
    private string direction = "";

    [ObservableProperty]
    private object? targetLocation;

    public ObservableCollection<string> AvailableDirections { get; } = new()
    {
        "North", "South", "East", "West",
        "Northeast", "Northwest", "Southeast", "Southwest",
        "Up", "Down", "In", "Out"
    };

    public ObservableCollection<object> AvailableLocations { get; } = new();
}

public partial class PropertyViewModel : ObservableObject
{
    [ObservableProperty]
    private string propertyName = "";

    [ObservableProperty]
    private string propertyDescription = "";

    [ObservableProperty]
    private bool isEnabled;

    [ObservableProperty]
    private string stringValue = "";

    [ObservableProperty]
    private string stateValue = "";

    [ObservableProperty]
    private bool boolValue;

    [ObservableProperty]
    private bool isStringType;

    [ObservableProperty]
    private bool isStateType;

    [ObservableProperty]
    private bool isBoolType;

    public ObservableCollection<string> StateOptions { get; } = new();
}
