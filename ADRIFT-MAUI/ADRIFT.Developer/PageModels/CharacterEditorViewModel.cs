using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

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

            if (!string.IsNullOrEmpty(CharacterKey) && adventure.Characters.TryGetValue(CharacterKey, out var existingChar))
            {
                // Edit mode - load existing character
                IsEditMode = true;
                PageTitle = "Edit Character";

                Prefix = existingChar.Prefix;
                Name = existingChar.Name;
                Aliases = string.Join(", ", existingChar.Aliases);
                Description = existingChar.Description;
                SelectedCharacterType = existingChar.Type.ToString();
                PersonalityTraits = existingChar.PersonalityTraits;

                if (!string.IsNullOrEmpty(existingChar.InitialLocationKey))
                {
                    SelectedLocation = AvailableLocations.FirstOrDefault(l => l.Key == existingChar.InitialLocationKey);
                }

                CanMove = existingChar.CanMove;
                FollowsPlayer = existingChar.FollowsPlayer;
                GeneralGreeting = existingChar.Greeting;

                // Load inventory
                InventoryItems.Clear();
                foreach (var objKey in existingChar.InventoryKeys)
                {
                    if (adventure.Objects.TryGetValue(objKey, out var obj))
                    {
                        InventoryItems.Add(new InventoryItemViewModel
                        {
                            ObjectName = obj.FullName,
                            ObjectKey = objKey
                        });
                    }
                }
            }
            else
            {
                // New character mode
                CharacterKey = "char_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                IsEditMode = false;
                PageTitle = "New Character";
                Name = "";
                SelectedCharacterType = "NPC";
            }

            UpdateFullCharacterName();
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to initialize: {ex.Message}", "OK");
        }
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
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
                return;
            }

            if (string.IsNullOrEmpty(CharacterKey))
            {
                CharacterKey = "char_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            }

            var character = new Character
            {
                Key = CharacterKey,
                Prefix = Prefix,
                Name = Name,
                Description = Description,
                InitialLocationKey = SelectedLocation?.Key,
                CanMove = CanMove,
                FollowsPlayer = FollowsPlayer,
                Greeting = GeneralGreeting,
                PersonalityTraits = PersonalityTraits,
                LastModified = DateTime.Now
            };

            // Parse aliases
            if (!string.IsNullOrWhiteSpace(Aliases))
            {
                character.Aliases.Clear();
                foreach (var alias in Aliases.Split(',', StringSplitOptions.RemoveEmptyEntries))
                {
                    character.Aliases.Add(alias.Trim());
                }
            }

            // Parse character type
            if (Enum.TryParse<CharacterType>(SelectedCharacterType, out var charType))
            {
                character.Type = charType;
            }

            // Save inventory
            character.InventoryKeys.Clear();
            foreach (var item in InventoryItems)
            {
                if (!string.IsNullOrEmpty(item.ObjectKey))
                {
                    character.InventoryKeys.Add(item.ObjectKey);
                }
            }

            // Add or update in adventure
            if (adventure.Characters.ContainsKey(CharacterKey))
            {
                adventure.Characters[CharacterKey] = character;
            }
            else
            {
                adventure.Characters.Add(CharacterKey, character);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to save character: {ex.Message}", "OK");
        }
    }
}

public partial class InventoryItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string objectName = "";

    [ObservableProperty]
    private string objectKey = "";
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
