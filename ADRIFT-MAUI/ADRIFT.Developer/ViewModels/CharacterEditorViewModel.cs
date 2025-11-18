using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

[QueryProperty(nameof(CharacterKey), "key")]
public partial class CharacterEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public CharacterEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;

        // Initialize collections
        AvailableLocations = new ObservableCollection<LocationItemViewModel>();
        InventoryItems = new ObservableCollection<InventoryItemViewModel>();
        WalkSteps = new ObservableCollection<WalkStepViewModel>();
        ConversationTopics = new ObservableCollection<ConversationTopicViewModel>();

        CharacterTypes = new ObservableCollection<string> { "NPC", "Companion", "Enemy", "Merchant", "Guard" };
        SelectedCharacterType = "NPC";

        // Set default tab
        SelectTab("Description");

        // Wire up collection changed events
        InventoryItems.CollectionChanged += (s, e) => UpdateInventoryCount();
        WalkSteps.CollectionChanged += (s, e) => UpdateWalkStepCount();
        ConversationTopics.CollectionChanged += (s, e) => UpdateTopicCount();
    }

    [ObservableProperty]
    private string characterKey = "";

    [ObservableProperty]
    private string pageTitle = "New Character";

    [ObservableProperty]
    private string characterSummary = "";

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
    private bool isWalkTabVisible = false;

    [ObservableProperty]
    private bool isTopicsTabVisible = false;

    [ObservableProperty]
    private Color descriptionTabColor = Color.FromArgb("#512BD4");

    [ObservableProperty]
    private Color locationTabColor = Color.FromArgb("#808080");

    [ObservableProperty]
    private Color walkTabColor = Color.FromArgb("#808080");

    [ObservableProperty]
    private Color topicsTabColor = Color.FromArgb("#808080");

    // Description tab properties
    [ObservableProperty]
    private string prefix = "";

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string fullCharacterName = "[character]";

    [ObservableProperty]
    private string aliases = "";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private ObservableCollection<string> characterTypes;

    [ObservableProperty]
    private string selectedCharacterType = "NPC";

    [ObservableProperty]
    private string personalityTraits = "";

    // Location tab properties
    [ObservableProperty]
    private ObservableCollection<LocationItemViewModel> availableLocations;

    [ObservableProperty]
    private LocationItemViewModel? selectedLocation;

    [ObservableProperty]
    private bool canMove = true;

    [ObservableProperty]
    private bool followsPlayer = false;

    [ObservableProperty]
    private ObservableCollection<InventoryItemViewModel> inventoryItems;

    [ObservableProperty]
    private bool hasInventory = false;

    [ObservableProperty]
    private string inventoryCountText = "(0 items)";

    // Walk tab properties
    [ObservableProperty]
    private bool hasWalkRoute = false;

    [ObservableProperty]
    private ObservableCollection<WalkStepViewModel> walkSteps;

    [ObservableProperty]
    private bool hasWalkSteps = false;

    [ObservableProperty]
    private string walkStepCountText = "(0 steps)";

    [ObservableProperty]
    private bool walkLoops = false;

    // Topics tab properties
    [ObservableProperty]
    private ObservableCollection<ConversationTopicViewModel> conversationTopics;

    [ObservableProperty]
    private bool hasTopics = false;

    [ObservableProperty]
    private bool hasNoTopics = true;

    [ObservableProperty]
    private string generalGreeting = "";

    partial void OnPrefixChanged(string value) => UpdateFullCharacterName();
    partial void OnNameChanged(string value) => UpdateFullCharacterName();

    private void UpdateFullCharacterName()
    {
        var parts = new List<string>();
        if (!string.IsNullOrWhiteSpace(Prefix)) parts.Add(Prefix);
        if (!string.IsNullOrWhiteSpace(Name))
            parts.Add(Name);
        else
            parts.Add("[character]");

        FullCharacterName = string.Join(" ", parts);
        UpdateCharacterSummary();
    }

    private void UpdateCharacterSummary()
    {
        if (!string.IsNullOrWhiteSpace(Name))
        {
            CharacterSummary = $"{FullCharacterName} - {SelectedCharacterType}";
        }
        else
        {
            CharacterSummary = "Configure character properties";
        }
    }

    private void UpdateInventoryCount()
    {
        var count = InventoryItems.Count;
        HasInventory = count > 0;
        InventoryCountText = $"({count} {(count == 1 ? "item" : "items")})";
    }

    private void UpdateWalkStepCount()
    {
        var count = WalkSteps.Count;
        HasWalkSteps = count > 0;
        WalkStepCountText = $"({count} {(count == 1 ? "step" : "steps")})";

        // Update step numbers
        for (int i = 0; i < WalkSteps.Count; i++)
        {
            WalkSteps[i].StepNumber = i + 1;
        }
    }

    private void UpdateTopicCount()
    {
        var count = ConversationTopics.Count;
        HasTopics = count > 0;
        HasNoTopics = count == 0;
    }

    [RelayCommand]
    private void SelectTab(string tabName)
    {
        SelectedTab = tabName;

        var activeColor = Color.FromArgb("#512BD4");
        var inactiveColor = Color.FromArgb("#808080");

        IsDescriptionTabVisible = tabName == "Description";
        IsLocationTabVisible = tabName == "Location";
        IsWalkTabVisible = tabName == "Walk";
        IsTopicsTabVisible = tabName == "Topics";

        DescriptionTabColor = IsDescriptionTabVisible ? activeColor : inactiveColor;
        LocationTabColor = IsLocationTabVisible ? activeColor : inactiveColor;
        WalkTabColor = IsWalkTabVisible ? activeColor : inactiveColor;
        TopicsTabColor = IsTopicsTabVisible ? activeColor : inactiveColor;
    }

    public async Task InitializeAsync()
    {
        // Load available locations
        // TODO: Load from adventure service
        AvailableLocations.Add(new LocationItemViewModel { Key = "loc1", Name = "Town Square" });
        AvailableLocations.Add(new LocationItemViewModel { Key = "loc2", Name = "Castle Gate" });
        AvailableLocations.Add(new LocationItemViewModel { Key = "loc3", Name = "Market" });

        if (!string.IsNullOrEmpty(CharacterKey))
        {
            // Edit mode - load existing character
            IsEditMode = true;
            PageTitle = "Edit Character";

            // TODO: Load character from adventure service
            // For now, using sample data
            Prefix = "Captain";
            Name = "Redbeard";
            Aliases = "captain, sailor, redbeard";
            Description = "A grizzled sea captain with a magnificent red beard and a weathered face that tells countless tales of adventure.";
            SelectedCharacterType = "NPC";
            PersonalityTraits = "brave, gruff, loyal";
            SelectedLocation = AvailableLocations.FirstOrDefault();
            GeneralGreeting = "Ahoy there, landlubber! What brings ye to these parts?";

            // Sample inventory
            InventoryItems.Add(new InventoryItemViewModel { ObjectName = "Compass" });
            InventoryItems.Add(new InventoryItemViewModel { ObjectName = "Map" });

            // Sample conversation topics
            ConversationTopics.Add(new ConversationTopicViewModel
            {
                TopicName = "Ship",
                Response = "Aye, me ship is the finest vessel on the seven seas!"
            });
        }
        else
        {
            // New character mode
            IsEditMode = false;
            PageTitle = "New Character";
            Name = "";
            SelectedCharacterType = "NPC";
        }

        UpdateFullCharacterName();
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task AddInventoryItem()
    {
        // TODO: Show dialog to select object
        InventoryItems.Add(new InventoryItemViewModel { ObjectName = "New Item" });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveInventoryItem(InventoryItemViewModel item)
    {
        if (item != null)
        {
            InventoryItems.Remove(item);
        }
    }

    [RelayCommand]
    private async Task AddWalkStep()
    {
        // TODO: Show dialog to configure walk step
        WalkSteps.Add(new WalkStepViewModel
        {
            Description = "Walk to location",
            StepNumber = WalkSteps.Count + 1
        });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveWalkStep(WalkStepViewModel step)
    {
        if (step != null)
        {
            WalkSteps.Remove(step);
        }
    }

    [RelayCommand]
    private async Task AddTopic()
    {
        // TODO: Show dialog to add conversation topic
        ConversationTopics.Add(new ConversationTopicViewModel
        {
            TopicName = "New Topic",
            Response = "Character's response..."
        });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveTopic(ConversationTopicViewModel topic)
    {
        if (topic != null)
        {
            ConversationTopics.Remove(topic);
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

        await SaveCharacter();
        await Shell.Current.DisplayAlert("Success", "Character changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveCharacter();
        await Shell.Current.GoToAsync("..");
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            Shell.Current.DisplayAlert("Validation Error", "Character name is required.", "OK");
            return false;
        }

        return true;
    }

    private async Task SaveCharacter()
    {
        // TODO: Save character to adventure service
        await Task.Delay(100);

        if (string.IsNullOrEmpty(CharacterKey))
        {
            CharacterKey = "char_" + Guid.NewGuid().ToString("N")[..8];
        }
    }
}

public partial class InventoryItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string objectName = "";
}

public partial class WalkStepViewModel : ObservableObject
{
    [ObservableProperty]
    private int stepNumber = 1;

    [ObservableProperty]
    private string description = "";
}

public partial class ConversationTopicViewModel : ObservableObject
{
    [ObservableProperty]
    private string topicName = "";

    [ObservableProperty]
    private string response = "";
}
