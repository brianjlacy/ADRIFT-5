using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

[QueryProperty(nameof(EventKey), "key")]
public partial class EventEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public EventEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;

        // Initialize collections
        AvailableParentEvents = new ObservableCollection<EventItemViewModel>();
        TriggerConditions = new ObservableCollection<ConditionViewModel>();
        EventActions = new ObservableCollection<EventActionViewModel>();

        EventTypes = new ObservableCollection<string> { "Time-based", "Triggered", "Repeating" };
        TriggerTypes = new ObservableCollection<string> { "After Time", "On Condition", "Immediate" };

        SelectedEventType = "Triggered";
        SelectedTriggerType = "On Condition";

        // Set default tab
        SelectTab("Description");

        // Wire up collection changed events
        TriggerConditions.CollectionChanged += (s, e) => UpdateConditionCount();
        EventActions.CollectionChanged += (s, e) => UpdateActionCount();
    }

    [ObservableProperty]
    private string eventKey = "";

    [ObservableProperty]
    private string pageTitle = "New Event";

    [ObservableProperty]
    private string eventSummary = "";

    [ObservableProperty]
    private bool isEditMode = false;

    // Tab visibility
    [ObservableProperty]
    private string selectedTab = "Description";

    [ObservableProperty]
    private bool isDescriptionTabVisible = true;

    [ObservableProperty]
    private bool isWhenTabVisible = false;

    [ObservableProperty]
    private bool isActionsTabVisible = false;

    [ObservableProperty]
    private Color descriptionTabColor = Color.FromArgb("#512BD4");

    [ObservableProperty]
    private Color whenTabColor = Color.FromArgb("#808080");

    [ObservableProperty]
    private Color actionsTabColor = Color.FromArgb("#808080");

    // Description tab properties
    [ObservableProperty]
    private string eventName = "";

    [ObservableProperty]
    private ObservableCollection<string> eventTypes;

    [ObservableProperty]
    private string selectedEventType = "Triggered";

    [ObservableProperty]
    private string description = "";

    // When tab properties
    [ObservableProperty]
    private ObservableCollection<string> triggerTypes;

    [ObservableProperty]
    private string selectedTriggerType = "On Condition";

    [ObservableProperty]
    private bool isTimeBased = false;

    [ObservableProperty]
    private bool isConditionBased = true;

    [ObservableProperty]
    private string delayTurns = "0";

    [ObservableProperty]
    private string repeatTurns = "0";

    [ObservableProperty]
    private ObservableCollection<ConditionViewModel> triggerConditions;

    [ObservableProperty]
    private bool hasConditions = false;

    [ObservableProperty]
    private bool hasNoConditions = true;

    [ObservableProperty]
    private ObservableCollection<EventItemViewModel> availableParentEvents;

    [ObservableProperty]
    private EventItemViewModel? selectedParentEvent;

    // Actions tab properties
    [ObservableProperty]
    private ObservableCollection<EventActionViewModel> eventActions;

    [ObservableProperty]
    private bool hasActions = false;

    [ObservableProperty]
    private bool hasNoActions = true;

    [ObservableProperty]
    private string actionCountText = "(0 actions)";

    [ObservableProperty]
    private string outputText = "";

    partial void OnEventNameChanged(string value) => UpdateEventSummary();
    partial void OnSelectedEventTypeChanged(string value) => UpdateEventSummary();

    partial void OnSelectedTriggerTypeChanged(string value)
    {
        IsTimeBased = value == "After Time";
        IsConditionBased = value == "On Condition";
    }

    private void UpdateEventSummary()
    {
        if (!string.IsNullOrWhiteSpace(EventName))
        {
            EventSummary = $"{EventName} - {SelectedEventType}";
        }
        else
        {
            EventSummary = "Configure event properties";
        }
    }

    private void UpdateConditionCount()
    {
        var count = TriggerConditions.Count;
        HasConditions = count > 0;
        HasNoConditions = count == 0;
    }

    private void UpdateActionCount()
    {
        var count = EventActions.Count;
        HasActions = count > 0;
        HasNoActions = count == 0;
        ActionCountText = $"({count} {(count == 1 ? "action" : "actions")})";

        // Update order numbers
        for (int i = 0; i < EventActions.Count; i++)
        {
            EventActions[i].Order = i + 1;
        }
    }

    [RelayCommand]
    private void SelectTab(string tabName)
    {
        SelectedTab = tabName;

        var activeColor = Color.FromArgb("#512BD4");
        var inactiveColor = Color.FromArgb("#808080");

        IsDescriptionTabVisible = tabName == "Description";
        IsWhenTabVisible = tabName == "When";
        IsActionsTabVisible = tabName == "Actions";

        DescriptionTabColor = IsDescriptionTabVisible ? activeColor : inactiveColor;
        WhenTabColor = IsWhenTabVisible ? activeColor : inactiveColor;
        ActionsTabColor = IsActionsTabVisible ? activeColor : inactiveColor;
    }

    public async Task InitializeAsync()
    {
        // Load available parent events
        // TODO: Load from adventure service
        AvailableParentEvents.Add(new EventItemViewModel { Key = "evt1", Name = "Timer Event" });
        AvailableParentEvents.Add(new EventItemViewModel { Key = "evt2", Name = "Location Change" });

        if (!string.IsNullOrEmpty(EventKey))
        {
            // Edit mode - load existing event
            IsEditMode = true;
            PageTitle = "Edit Event";

            // TODO: Load event from adventure service
            // For now, using sample data
            EventName = "Castle Gate Opens";
            SelectedEventType = "Triggered";
            Description = "The castle gate slowly creaks open when the guard is given the password.";
            SelectedTriggerType = "On Condition";
            OutputText = "The heavy castle gate swings open with a loud creak.";

            // Sample conditions
            TriggerConditions.Add(new ConditionViewModel
            {
                Description = "Player has said password to guard"
            });

            // Sample actions
            EventActions.Add(new EventActionViewModel
            {
                ActionType = "Move Object",
                Description = "Move 'castle gate' to 'open' state"
            });
            EventActions.Add(new EventActionViewModel
            {
                ActionType = "Display Message",
                Description = "Show gate opening message"
            });
        }
        else
        {
            // New event mode
            IsEditMode = false;
            PageTitle = "New Event";
            EventName = "";
            DelayTurns = "0";
            RepeatTurns = "0";
        }

        UpdateEventSummary();
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task AddCondition()
    {
        // TODO: Show dialog to configure condition
        TriggerConditions.Add(new ConditionViewModel
        {
            Description = "New condition"
        });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveCondition(ConditionViewModel condition)
    {
        if (condition != null)
        {
            TriggerConditions.Remove(condition);
        }
    }

    [RelayCommand]
    private async Task AddAction()
    {
        // TODO: Show dialog to configure action
        EventActions.Add(new EventActionViewModel
        {
            ActionType = "Display Message",
            Description = "Show message to player",
            Order = EventActions.Count + 1
        });
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveAction(EventActionViewModel action)
    {
        if (action != null)
        {
            EventActions.Remove(action);
        }
    }

    [RelayCommand]
    private void MoveActionUp(EventActionViewModel action)
    {
        if (action == null)
            return;

        var index = EventActions.IndexOf(action);
        if (index > 0)
        {
            EventActions.Move(index, index - 1);
            UpdateActionCount(); // Refresh order numbers
        }
    }

    [RelayCommand]
    private void MoveActionDown(EventActionViewModel action)
    {
        if (action == null)
            return;

        var index = EventActions.IndexOf(action);
        if (index < EventActions.Count - 1)
        {
            EventActions.Move(index, index + 1);
            UpdateActionCount(); // Refresh order numbers
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

        await SaveEvent();
        await Shell.Current.DisplayAlert("Success", "Event changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveEvent();
        await Shell.Current.GoToAsync("..");
    }

    private bool ValidateInput()
    {
        if (string.IsNullOrWhiteSpace(EventName))
        {
            Shell.Current.DisplayAlert("Validation Error", "Event name is required.", "OK");
            return false;
        }

        if (IsTimeBased)
        {
            if (!int.TryParse(DelayTurns, out var delay) || delay < 0)
            {
                Shell.Current.DisplayAlert("Validation Error", "Delay must be a positive integer.", "OK");
                return false;
            }

            if (!int.TryParse(RepeatTurns, out var repeat) || repeat < 0)
            {
                Shell.Current.DisplayAlert("Validation Error", "Repeat interval must be a positive integer.", "OK");
                return false;
            }
        }

        return true;
    }

    private async Task SaveEvent()
    {
        // TODO: Save event to adventure service
        await Task.Delay(100);

        if (string.IsNullOrEmpty(EventKey))
        {
            EventKey = "evt_" + Guid.NewGuid().ToString("N")[..8];
        }
    }
}

public partial class EventItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string key = "";

    [ObservableProperty]
    private string name = "";
}

public partial class ConditionViewModel : ObservableObject
{
    [ObservableProperty]
    private string description = "";
}

public partial class EventActionViewModel : ObservableObject
{
    [ObservableProperty]
    private int order = 1;

    [ObservableProperty]
    private string actionType = "";

    [ObservableProperty]
    private string description = "";
}
