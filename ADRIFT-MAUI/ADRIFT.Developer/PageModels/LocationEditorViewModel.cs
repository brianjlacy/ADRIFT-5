using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

[QueryProperty(nameof(LocationKey), "key")]
public partial class LocationEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    private bool _isEditMode;
    private AdriftLocation? _originalLocation;

    public LocationEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        LocationKey = GenerateKey();
    }

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

    partial void OnLocationKeyChanged(string value)
    {
        if (!string.IsNullOrEmpty(value) && value != _originalLocation?.Key)
        {
            LoadLocation(value);
        }
    }

    private async void LoadLocation(string key)
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure != null && adventure.Locations.TryGetValue(key, out var location))
            {
                _isEditMode = true;
                _originalLocation = location;

                LocationKey = location.Key;
                LocationName = location.ShortDescription;
                ShortDescription = location.ShortDescription;
                LongDescription = location.LongDescription;
                IsHidden = location.HideOnMap;
                IsLibraryItem = location.IsLibrary;

                // Load directions
                Directions.Clear();
                var allLocations = adventure.Locations.Values.ToList();
                foreach (var dir in location.Directions)
                {
                    var dirVm = new DirectionViewModel(allLocations)
                    {
                        Direction = dir.DirectionName,
                        TargetLocationKey = dir.DestinationKey,
                        RestrictionDescription = dir.RestrictionDescription ?? ""
                    };
                    Directions.Add(dirVm);
                }

                StatusMessage = "Location loaded";
            }
            else
            {
                _isEditMode = false;
                StatusMessage = "Creating new location";
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to load location: {ex.Message}", "OK");
        }
    }

    private string GenerateKey()
    {
        return "Location" + Guid.NewGuid().ToString("N").Substring(0, 8);
    }

    [RelayCommand]
    private void SelectTab(string tabName)
    {
        CurrentTab = tabName;
    }

    [RelayCommand]
    private async Task FormatText()
    {
        await Shell.Current.DisplayAlert("Format", "Text formatting options: Bold, Italic, Underline, Color", "OK");
    }

    [RelayCommand]
    private async Task InsertFunction()
    {
        await Shell.Current.DisplayAlert("Functions", "Available functions: %CharacterName%, %ObjectName%, %Variable%", "OK");
    }

    [RelayCommand]
    private void AddDirection()
    {
        var adventure = _adventureService.CurrentAdventure;
        var allLocations = adventure?.Locations.Values.ToList() ?? new List<AdriftLocation>();

        var newDirection = new DirectionViewModel(allLocations)
        {
            Direction = "North"
        };
        Directions.Add(newDirection);
        StatusMessage = "Direction added";
    }

    [RelayCommand]
    private void RemoveDirection(DirectionViewModel direction)
    {
        Directions.Remove(direction);
        StatusMessage = "Direction removed";
    }

    [RelayCommand]
    private async Task EditDirectionRestrictions(DirectionViewModel direction)
    {
        var result = await Shell.Current.DisplayPromptAsync(
            "Direction Restriction",
            "Enter restriction description:",
            initialValue: direction.RestrictionDescription,
            maxLength: 500);

        if (result != null)
        {
            direction.RestrictionDescription = result;
            StatusMessage = "Restriction updated";
        }
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
        SaveLocation(false);
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (SaveLocation(true))
        {
            await Task.Delay(300);
            await Shell.Current.GoToAsync("..");
        }
    }

    private bool SaveLocation(bool showMessage)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(ShortDescription))
            {
                StatusMessage = "Short description is required";
                return false;
            }

            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                StatusMessage = "No adventure loaded";
                return false;
            }

            var location = new AdriftLocation
            {
                Key = LocationKey,
                ShortDescription = ShortDescription,
                LongDescription = LongDescription,
                HideOnMap = IsHidden,
                IsLibrary = IsLibraryItem,
                LastModified = DateTime.Now
            };

            // Save directions
            location.Directions.Clear();
            foreach (var dirVm in Directions)
            {
                if (!string.IsNullOrEmpty(dirVm.TargetLocationKey))
                {
                    var direction = new Direction
                    {
                        DirectionName = dirVm.Direction,
                        DestinationKey = dirVm.TargetLocationKey,
                        RestrictionDescription = string.IsNullOrWhiteSpace(dirVm.RestrictionDescription)
                            ? null
                            : dirVm.RestrictionDescription
                    };
                    location.Directions.Add(direction);
                }
            }

            // Add or update in adventure
            if (adventure.Locations.ContainsKey(LocationKey))
            {
                adventure.Locations[LocationKey] = location;
                StatusMessage = showMessage ? "Location updated" : "Changes applied";
            }
            else
            {
                adventure.Locations.Add(LocationKey, location);
                _isEditMode = true;
                StatusMessage = showMessage ? "Location created" : "Changes applied";
            }

            return true;
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error: {ex.Message}";
            return false;
        }
    }
}

public partial class DirectionViewModel : ObservableObject
{
    public DirectionViewModel(List<AdriftLocation> availableLocations)
    {
        AvailableLocations.Clear();
        foreach (var loc in availableLocations)
        {
            AvailableLocations.Add(new LocationOption
            {
                Key = loc.Key,
                Name = loc.ShortDescription
            });
        }
    }

    [ObservableProperty]
    private string direction = "";

    [ObservableProperty]
    private string targetLocationKey = "";

    [ObservableProperty]
    private string restrictionDescription = "";

    public ObservableCollection<string> AvailableDirections { get; } = new()
    {
        "North", "South", "East", "West",
        "Northeast", "Northwest", "Southeast", "Southwest",
        "Up", "Down", "In", "Out",
        "NorthEast", "NorthWest", "SouthEast", "SouthWest"
    };

    public ObservableCollection<LocationOption> AvailableLocations { get; } = new();
}

public class LocationOption
{
    public string Key { get; set; } = "";
    public string Name { get; set; } = "";
    public string DisplayText => $"{Name} ({Key})";
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
