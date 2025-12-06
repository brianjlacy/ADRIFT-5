using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

[QueryProperty(nameof(TaskKey), "key")]
public partial class TaskEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    private bool _isEditMode;
    private Core.Models.Task? _originalTask;

    public TaskEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        Commands = new ObservableCollection<TaskCommandViewModel>();
        Restrictions = new ObservableCollection<RestrictionViewModel>();
        SuccessActions = new ObservableCollection<TaskActionViewModel>();
        FailureActions = new ObservableCollection<TaskActionViewModel>();
        Specifics = new ObservableCollection<TaskSpecificViewModel>();
        References = new ObservableCollection<TaskReferenceViewModel>();
        TaskKey = GenerateKey();
    }

    // GENERAL TAB
    [ObservableProperty]
    private string taskKey = "";

    [ObservableProperty]
    private string taskName = "New Task";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private string selectedTaskType = "General";

    [ObservableProperty]
    private int priority = 5000;

    // COMMANDS TAB
    public ObservableCollection<TaskCommandViewModel> Commands { get; }

    // RESTRICTIONS TAB
    public ObservableCollection<RestrictionViewModel> Restrictions { get; }

    // ACTIONS TAB
    [ObservableProperty]
    private string completionMessage = "Task completed.";

    [ObservableProperty]
    private string failureMessage = "";

    public ObservableCollection<TaskActionViewModel> SuccessActions { get; }
    public ObservableCollection<TaskActionViewModel> FailureActions { get; }

    // ADVANCED TAB
    [ObservableProperty]
    private bool isRepeatable;

    [ObservableProperty]
    private int scoreValue;

    [ObservableProperty]
    private bool runImmediately;

    [ObservableProperty]
    private bool isLocationTrigger;

    [ObservableProperty]
    private string triggerLocationKey = "";

    [ObservableProperty]
    private string selectedSpecificOverride = "Override";

    public ObservableCollection<TaskSpecificViewModel> Specifics { get; }
    public ObservableCollection<TaskReferenceViewModel> References { get; }

    // UI State
    [ObservableProperty]
    private string statusMessage = "Ready";

    [ObservableProperty]
    private bool isLibrary;

    public ObservableCollection<string> TaskTypes { get; } = new()
    {
        "General",
        "System",
        "Specific"
    };

    public ObservableCollection<string> SpecificOverrideTypes { get; } = new()
    {
        "Before",
        "Override",
        "After"
    };

    partial void OnTaskKeyChanged(string value)
    {
        if (!string.IsNullOrEmpty(value) && value != _originalTask?.Key)
        {
            LoadTask(value);
        }
    }

    private async void LoadTask(string key)
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure != null && adventure.Tasks.TryGetValue(key, out var task))
            {
                _isEditMode = true;
                _originalTask = task;

                // General
                TaskKey = task.Key;
                TaskName = task.Name;
                Description = task.Description.ToString();
                SelectedTaskType = task.Type.ToString();
                Priority = task.Priority;

                // Commands
                Commands.Clear();
                foreach (var cmd in task.Commands)
                {
                    Commands.Add(new TaskCommandViewModel
                    {
                        Pattern = cmd.Pattern
                    });
                }

                // Restrictions
                Restrictions.Clear();
                foreach (var restriction in task.Restrictions)
                {
                    Restrictions.Add(new RestrictionViewModel
                    {
                        Description = restriction.ToString()
                    });
                }

                // Actions
                CompletionMessage = task.CompletionMessage.ToString();
                FailureMessage = task.FailureMessage.ToString();

                SuccessActions.Clear();
                foreach (var action in task.SuccessActions)
                {
                    SuccessActions.Add(new TaskActionViewModel
                    {
                        Description = action.ToString()
                    });
                }

                FailureActions.Clear();
                foreach (var action in task.FailureActions)
                {
                    FailureActions.Add(new TaskActionViewModel
                    {
                        Description = action.ToString()
                    });
                }

                // Advanced
                IsRepeatable = task.IsRepeatable;
                ScoreValue = task.ScoreValue;
                RunImmediately = task.RunImmediately;
                IsLocationTrigger = task.IsLocationTrigger;
                TriggerLocationKey = task.TriggerLocationKey ?? "";
                SelectedSpecificOverride = task.SpecificOverride.ToString();

                Specifics.Clear();
                foreach (var specific in task.Specifics)
                {
                    Specifics.Add(new TaskSpecificViewModel
                    {
                        Type = specific.Type.ToString(),
                        Details = specific.ObjectKey ?? specific.CharacterKey ?? specific.TextPattern ?? ""
                    });
                }

                References.Clear();
                foreach (var reference in task.References)
                {
                    References.Add(new TaskReferenceViewModel
                    {
                        ReferenceNumber = reference.ReferenceNumber,
                        Type = reference.Type.ToString(),
                        IsOptional = reference.IsOptional
                    });
                }

                StatusMessage = "Task loaded";
            }
            else
            {
                _isEditMode = false;
                StatusMessage = "Creating new task";
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to load task: {ex.Message}", "OK");
        }
    }

    private string GenerateKey()
    {
        return "Task" + Guid.NewGuid().ToString("N").Substring(0, 8);
    }

    // COMMAND COMMANDS
    [RelayCommand]
    private async System.Threading.Tasks.Task AddCommand()
    {
        var pattern = await Shell.Current.DisplayPromptAsync(
            "Add Command",
            "Enter command pattern:",
            placeholder: "e.g., 'take #object#', 'give #object1# to #character2#'");

        if (!string.IsNullOrEmpty(pattern))
        {
            Commands.Add(new TaskCommandViewModel { Pattern = pattern });
            StatusMessage = "Command added";
        }
    }

    [RelayCommand]
    private void RemoveCommand(TaskCommandViewModel command)
    {
        Commands.Remove(command);
        StatusMessage = "Command removed";
    }

    [RelayCommand]
    private async System.Threading.Tasks.Task EditCommand(TaskCommandViewModel command)
    {
        var result = await Shell.Current.DisplayPromptAsync(
            "Edit Command",
            "Edit command pattern:",
            initialValue: command.Pattern);

        if (result != null)
        {
            command.Pattern = result;
            StatusMessage = "Command updated";
        }
    }

    // RESTRICTION COMMANDS
    [RelayCommand]
    private async System.Threading.Tasks.Task AddRestriction()
    {
        Restrictions.Add(new RestrictionViewModel { Description = "New Restriction" });
        StatusMessage = "Restriction added";
        await System.Threading.Tasks.Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveRestriction(RestrictionViewModel restriction)
    {
        Restrictions.Remove(restriction);
        StatusMessage = "Restriction removed";
    }

    // ACTION COMMANDS
    [RelayCommand]
    private async System.Threading.Tasks.Task AddSuccessAction()
    {
        SuccessActions.Add(new TaskActionViewModel { Description = "New Success Action" });
        StatusMessage = "Success action added";
        await System.Threading.Tasks.Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveSuccessAction(TaskActionViewModel action)
    {
        SuccessActions.Remove(action);
        StatusMessage = "Success action removed";
    }

    [RelayCommand]
    private async System.Threading.Tasks.Task AddFailureAction()
    {
        FailureActions.Add(new TaskActionViewModel { Description = "New Failure Action" });
        StatusMessage = "Failure action added";
        await System.Threading.Tasks.Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveFailureAction(TaskActionViewModel action)
    {
        FailureActions.Remove(action);
        StatusMessage = "Failure action removed";
    }

    // SPECIFIC COMMANDS
    [RelayCommand]
    private async System.Threading.Tasks.Task AddSpecific()
    {
        Specifics.Add(new TaskSpecificViewModel { Type = "Object", Details = "" });
        StatusMessage = "Specific added";
        await System.Threading.Tasks.Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveSpecific(TaskSpecificViewModel specific)
    {
        Specifics.Remove(specific);
        StatusMessage = "Specific removed";
    }

    // REFERENCE COMMANDS
    [RelayCommand]
    private async System.Threading.Tasks.Task AddReference()
    {
        var nextNum = References.Count + 1;
        References.Add(new TaskReferenceViewModel
        {
            ReferenceNumber = nextNum,
            Type = "Object",
            IsOptional = false
        });
        StatusMessage = "Reference added";
        await System.Threading.Tasks.Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveReference(TaskReferenceViewModel reference)
    {
        References.Remove(reference);
        StatusMessage = "Reference removed";
    }

    [RelayCommand]
    private async System.Threading.Tasks.Task Cancel()
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
        SaveTask(false);
    }

    [RelayCommand]
    private async System.Threading.Tasks.Task SaveAndClose()
    {
        if (SaveTask(true))
        {
            await System.Threading.Tasks.Task.Delay(300);
            await Shell.Current.GoToAsync("..");
        }
    }

    private bool SaveTask(bool showMessage)
    {
        try
        {
            if (string.IsNullOrWhiteSpace(TaskName))
            {
                StatusMessage = "Task name is required";
                return false;
            }

            if (Commands.Count == 0)
            {
                StatusMessage = "At least one command is required";
                return false;
            }

            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                StatusMessage = "No adventure loaded";
                return false;
            }

            var task = new Core.Models.Task
            {
                Key = TaskKey,
                Name = TaskName,
                Priority = Priority,
                IsRepeatable = IsRepeatable,
                IsLibrary = IsLibrary,
                LastModified = DateTime.Now,
                ScoreValue = ScoreValue,
                RunImmediately = RunImmediately,
                IsLocationTrigger = IsLocationTrigger,
                TriggerLocationKey = string.IsNullOrWhiteSpace(TriggerLocationKey) ? null : TriggerLocationKey
            };

            // Set description
            task.Description = new Description { Text = Description };

            // Parse task type
            if (Enum.TryParse<TaskType>(SelectedTaskType, out var taskType))
            {
                task.Type = taskType;
            }

            // Parse specific override
            if (Enum.TryParse<SpecificOverrideType>(SelectedSpecificOverride, out var overrideType))
            {
                task.SpecificOverride = overrideType;
            }

            // Save completion and failure messages
            task.CompletionMessage = new Description { Text = CompletionMessage };
            task.FailureMessage = new Description { Text = FailureMessage };

            // Save commands
            task.Commands.Clear();
            foreach (var cmdVm in Commands)
            {
                task.Commands.Add(new Core.Models.TaskCommand
                {
                    Pattern = cmdVm.Pattern
                });
            }

            // Note: Restrictions, Actions, Specifics, and References are placeholders
            // Full implementation would require proper builders/editors

            // Add or update in adventure
            if (adventure.Tasks.ContainsKey(TaskKey))
            {
                adventure.Tasks[TaskKey] = task;
                StatusMessage = showMessage ? "Task updated" : "Changes applied";
            }
            else
            {
                adventure.Tasks.Add(TaskKey, task);
                _isEditMode = true;
                StatusMessage = showMessage ? "Task created" : "Changes applied";
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

// VIEWMODEL CLASSES FOR COLLECTIONS

public partial class TaskCommandViewModel : ObservableObject
{
    [ObservableProperty]
    private string pattern = "";
}

public partial class RestrictionViewModel : ObservableObject
{
    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private string type = "Must";
}

public partial class TaskActionViewModel : ObservableObject
{
    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private string actionType = "SetProperty";
}

public partial class TaskSpecificViewModel : ObservableObject
{
    [ObservableProperty]
    private string type = "Object";

    [ObservableProperty]
    private string details = "";
}

public partial class TaskReferenceViewModel : ObservableObject
{
    [ObservableProperty]
    private int referenceNumber = 1;

    [ObservableProperty]
    private string type = "Object";

    [ObservableProperty]
    private bool isOptional;
}
