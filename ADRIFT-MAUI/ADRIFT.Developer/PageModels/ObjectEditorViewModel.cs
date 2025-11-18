using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

[QueryProperty(nameof(ObjectKey), "key")]
public partial class ObjectEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public ObjectEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;

        // Initialize collections
        AvailableLocations = new ObservableCollection<LocationItemViewModel>();
        AvailableContainers = new ObservableCollection<ObjectItemViewModel>();
        AvailableCharacters = new ObservableCollection<CharacterItemViewModel>();
        CustomProperties = new ObservableCollection<CustomPropertyViewModel>();

        // Initialize options
        LocationTypes = new ObservableCollection<string>
        {
            "At Location",
            "Inside Object",
            "Held by Character",
            "Worn by Character",
            "Hidden at Location"
        };

        SizeOptions = new ObservableCollection<string>
        {
            "Tiny", "Small", "Normal", "Large", "Huge"
        };

        SelectedLocationType = "At Location";
        SelectedSize = "Normal";

        // Set default tab
        SelectTab("Description");
    }

    [ObservableProperty]
    private string objectKey = "";

    [ObservableProperty]
    private string pageTitle = "New Object";

    [ObservableProperty]
    private string objectSummary = "";

    [ObservableProperty]
    private bool isEditMode = false;

    // Tab visibility
    [ObservableProperty]
    private string selectedTab = "Description";

    [ObservableProperty]
    private bool isDescriptionTabVisible = true;

    [ObservableProperty]
    private bool isLocationTabVisible = false;

    [ObservableProperty]
    private bool isPropertiesTabVisible = false;

    [ObservableProperty]
    private bool isAdvancedTabVisible = false;

    [ObservableProperty]
    private Color descriptionTabColor = Color.FromArgb("#512BD4");

    [ObservableProperty]
    private Color locationTabColor = Color.FromArgb("#808080");

    [ObservableProperty]
    private Color propertiesTabColor = Color.FromArgb("#808080");

    [ObservableProperty]
    private Color advancedTabColor = Color.FromArgb("#808080");

    // Description tab properties
    [ObservableProperty]
    private string article = "a";

    [ObservableProperty]
    private string prefix = "";

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string fullObjectName = "a [object]";

    [ObservableProperty]
    private string aliases = "";

    [ObservableProperty]
    private string shortDescription = "";

    [ObservableProperty]
    private string longDescription = "";

    // Location tab properties
    [ObservableProperty]
    private ObservableCollection<LocationItemViewModel> availableLocations;

    [ObservableProperty]
    private LocationItemViewModel? selectedLocation;

    [ObservableProperty]
    private ObservableCollection<string> locationTypes;

    [ObservableProperty]
    private string selectedLocationType = "At Location";

    [ObservableProperty]
    private ObservableCollection<ObjectItemViewModel> availableContainers;

    [ObservableProperty]
    private ObjectItemViewModel? selectedContainer;

    [ObservableProperty]
    private ObservableCollection<CharacterItemViewModel> availableCharacters;

    [ObservableProperty]
    private CharacterItemViewModel? selectedCharacter;

    [ObservableProperty]
    private bool isInsideObject = false;

    [ObservableProperty]
    private bool isHeldByCharacter = false;

    // Properties tab
    [ObservableProperty]
    private ObservableCollection<string> sizeOptions;

    [ObservableProperty]
    private string selectedSize = "Normal";

    [ObservableProperty]
    private string weight = "1.0";

    [ObservableProperty]
    private bool isStatic = false;

    [ObservableProperty]
    private bool isContainer = false;

    [ObservableProperty]
    private bool isSurface = false;

    [ObservableProperty]
    private bool isWearable = false;

    [ObservableProperty]
    private bool isEdible = false;

    [ObservableProperty]
    private string capacity = "10";

    [ObservableProperty]
    private bool isOpenable = false;

    [ObservableProperty]
    private bool isLockable = false;

    // Advanced tab
    [ObservableProperty]
    private ObservableCollection<CustomPropertyViewModel> customProperties;

    [ObservableProperty]
    private bool hasCustomProperties = false;

    [ObservableProperty]
    private bool isLightSource = false;

    [ObservableProperty]
    private bool isReadable = false;

    [ObservableProperty]
    private string readingText = "";

    partial void OnArticleChanged(string value) => UpdateFullObjectName();
    partial void OnPrefixChanged(string value) => UpdateFullObjectName();
    partial void OnNameChanged(string value) => UpdateFullObjectName();

    partial void OnSelectedLocationTypeChanged(string value)
    {
        IsInsideObject = value == "Inside Object";
        IsHeldByCharacter = value == "Held by Character" || value == "Worn by Character";
    }

    partial void OnCustomPropertiesChanged(ObservableCollection<CustomPropertyViewModel> value)
    {
        HasCustomProperties = value?.Count > 0;
    }

    private void UpdateFullObjectName()
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(Article)) parts.Add(Article);
        if (!string.IsNullOrWhiteSpace(Prefix)) parts.Add(Prefix);
        if (!string.IsNullOrWhiteSpace(Name))
            parts.Add(Name);
        else
            parts.Add("[object]");

        FullObjectName = string.Join(" ", parts);
        UpdateObjectSummary();
    }

    private void UpdateObjectSummary()
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            ObjectSummary = $"{FullObjectName} - {SelectedSize}, {Weight} kg";
        }
        else
        {
            ObjectSummary = "Configure object properties";
        }
    }

    [RelayCommand]
    private void SelectTab(string tabName)
    {
        SelectedTab = tabName;

        var activeColor = Color.FromArgb("#512BD4");
        var inactiveColor = Color.FromArgb("#808080");

        IsDescriptionTabVisible = tabName == "Description";
        IsLocationTabVisible = tabName == "Location";
        IsPropertiesTabVisible = tabName == "Properties";
        IsAdvancedTabVisible = tabName == "Advanced";

        DescriptionTabColor = IsDescriptionTabVisible ? activeColor : inactiveColor;
        LocationTabColor = IsLocationTabVisible ? activeColor : inactiveColor;
        PropertiesTabColor = IsPropertiesTabVisible ? activeColor : inactiveColor;
        AdvancedTabColor = IsAdvancedTabVisible ? activeColor : inactiveColor;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null) return;

            // Load available locations
            AvailableLocations.Clear();
            foreach (var loc in adventure.Locations.Values)
            {
                AvailableLocations.Add(new LocationItemViewModel(loc));
            }

            // Load available containers
            AvailableContainers.Clear();
            foreach (var obj in adventure.Objects.Values.Where(o => o.IsContainer))
            {
                AvailableContainers.Add(new ObjectItemViewModel(obj));
            }

            // Load available characters
            AvailableCharacters.Clear();
            foreach (var ch in adventure.Characters.Values)
            {
                AvailableCharacters.Add(new CharacterItemViewModel(ch));
            }

            if (!string.IsNullOrEmpty(ObjectKey) && adventure.Objects.TryGetValue(ObjectKey, out var existingObj))
            {
                // Edit mode - load existing object
                IsEditMode = true;
                PageTitle = "Edit Object";

                Article = existingObj.Article;
                Prefix = existingObj.Prefix;
                Name = existingObj.Name;
                Aliases = string.Join(", ", existingObj.Aliases);
                ShortDescription = existingObj.ShortDescription;
                LongDescription = existingObj.LongDescription;

                if (!string.IsNullOrEmpty(existingObj.LocationKey))
                {
                    SelectedLocation = AvailableLocations.FirstOrDefault(l => l.Key == existingObj.LocationKey);
                }

                SelectedSize = existingObj.Size.ToString();
                Weight = existingObj.Weight.ToString();
                IsStatic = existingObj.IsStatic;
                IsContainer = existingObj.IsContainer;
                IsSurface = existingObj.IsSurface;
                IsWearable = existingObj.IsWearable;
            }
            else
            {
                // New object mode
                ObjectKey = "obj_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                IsEditMode = false;
                PageTitle = "New Object";
                Article = "a";
                Name = "";
                Weight = "1.0";
                SelectedSize = "Normal";
            }

            UpdateFullObjectName();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to initialize: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddCustomProperty()
    {
        // TODO: Show dialog to add custom property
        CustomProperties.Add(new CustomPropertyViewModel
        {
            PropertyName = "Sample Property",
            PropertyValue = "Sample Value"
        });
        HasCustomProperties = true;
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveCustomProperty(CustomPropertyViewModel property)
    {
        if (property != null)
        {
            CustomProperties.Remove(property);
            HasCustomProperties = CustomProperties.Count > 0;
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Apply()
    {
        if (!ValidateInput())
            return;

        await SaveObject();
        await Shell.Current.DisplayAlert("Success", "Object changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveObject();
        await Shell.Current.GoToAsync("..");
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            Shell.Current.DisplayAlert("Validation Error", "Object name is required.", "OK");
            return false;
        }

        if (!double.TryParse(Weight, out var weightValue) || weightValue < 0)
        {
            Shell.Current.DisplayAlert("Validation Error", "Weight must be a positive number.", "OK");
            return false;
        }

        if (IsContainer && (!int.TryParse(Capacity, out var capacityValue) || capacityValue < 0))
        {
            Shell.Current.DisplayAlert("Validation Error", "Capacity must be a positive integer.", "OK");
            return false;
        }

        return true;
    }

    private async Task SaveObject()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
                return;
            }

            if (string.IsNullOrEmpty(ObjectKey))
            {
                ObjectKey = "obj_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            }

            var obj = new AdriftObject
            {
                Key = ObjectKey,
                Article = Article,
                Prefix = Prefix,
                Name = Name,
                ShortDescription = ShortDescription,
                LongDescription = LongDescription,
                LocationKey = SelectedLocation?.Key,
                IsStatic = IsStatic,
                IsContainer = IsContainer,
                IsSurface = IsSurface,
                IsWearable = IsWearable,
                LastModified = DateTime.Now
            };

            // Parse aliases
            if (!string.IsNullOrWhiteSpace(Aliases))
            {
                obj.Aliases.Clear();
                foreach (var alias in Aliases.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    obj.Aliases.Add(alias.Trim());
                }
            }

            // Parse size
            if (Enum.TryParse<ObjectSize>(SelectedSize, out var size))
            {
                obj.Size = size;
            }

            // Parse weight
            if (double.TryParse(Weight, out var weight))
            {
                obj.Weight = weight;
            }

            // Add or update in adventure
            if (adventure.Objects.ContainsKey(ObjectKey))
            {
                adventure.Objects[ObjectKey] = obj;
            }
            else
            {
                adventure.Objects.Add(ObjectKey, obj);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to save object: {ex.Message}", "OK");
        }
    }
}

public partial class CustomPropertyViewModel : ObservableObject
{
    [ObservableProperty]
    private string propertyName = "";

    [ObservableProperty]
    private string propertyValue = "";

    public string DisplayText => $"{PropertyName}: {PropertyValue}";
}
