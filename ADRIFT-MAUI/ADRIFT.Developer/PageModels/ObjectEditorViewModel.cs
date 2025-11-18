using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

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
        // Load available locations, objects, and characters
        // TODO: Load from adventure service
        AvailableLocations.Add(new LocationItemViewModel { Key = "loc1", Name = "Starting Room" });
        AvailableLocations.Add(new LocationItemViewModel { Key = "loc2", Name = "Forest Path" });

        AvailableContainers.Add(new ObjectItemViewModel { Key = "obj1", FullName = "Wooden Chest" });
        AvailableContainers.Add(new ObjectItemViewModel { Key = "obj2", FullName = "Leather Bag" });

        AvailableCharacters.Add(new CharacterItemViewModel { Key = "char1", Name = "Player" });
        AvailableCharacters.Add(new CharacterItemViewModel { Key = "char2", Name = "Guard" });

        if (!string.IsNullOrEmpty(ObjectKey))
        {
            // Edit mode - load existing object
            IsEditMode = true;
            PageTitle = "Edit Object";

            // TODO: Load object from adventure service
            // For now, using sample data
            Article = "a";
            Prefix = "rusty";
            Name = "sword";
            Aliases = "blade, weapon";
            ShortDescription = "A rusty old sword";
            LongDescription = "This is an ancient sword covered in rust. Despite its age, it still looks functional.";
            SelectedLocation = AvailableLocations.FirstOrDefault();
            SelectedSize = "Normal";
            Weight = "2.5";
            IsWearable = true;
        }
        else
        {
            // New object mode
            IsEditMode = false;
            PageTitle = "New Object";
            Article = "a";
            Name = "";
            Weight = "1.0";
            SelectedSize = "Normal";
        }

        UpdateFullObjectName();
        await Task.CompletedTask;
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
        // TODO: Save object to adventure service
        await Task.Delay(100);

        if (string.IsNullOrEmpty(ObjectKey))
        {
            ObjectKey = "obj_" + Guid.NewGuid().ToString("N")[..8];
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
