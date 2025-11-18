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
        Commands = new ObservableCollection<TaskCommand>();
        TaskKey = GenerateKey();
    }

    [ObservableProperty]
    private string taskKey = "";

    [ObservableProperty]
    private string taskName = "New Task";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private string completionText = "Task completed.";

    [ObservableProperty]
    private int priority = 5;

    [ObservableProperty]
    private bool isRepeatable;

    [ObservableProperty]
    private bool isLibrary;

    [ObservableProperty]
    private string selectedTaskType = "Specific";

    [ObservableProperty]
    private string statusMessage = "Ready";

    public ObservableCollection<TaskCommand> Commands { get; }

    public ObservableCollection<string> TaskTypes { get; } = new()
    {
        "System",
        "General",
        "Specific"
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

                TaskKey = task.Key;
                TaskName = task.Name;
                Description = task.Description;
                CompletionText = task.CompletionText;
                Priority = task.Priority;
                IsRepeatable = task.IsRepeatable;
                IsLibrary = task.IsLibrary;
                SelectedTaskType = task.Type.ToString();

                // Load commands
                Commands.Clear();
                foreach (var cmd in task.Commands)
                {
                    Commands.Add(new TaskCommand
                    {
                        Command = cmd.Command,
                        IsRegex = false
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

    [RelayCommand]
    private async System.Threading.Tasks.Task AddCommand()
    {
        var command = await Shell.Current.DisplayPromptAsync(
            "Add Command",
            "Enter command text:",
            placeholder: "e.g., 'get ball', 'take %object%'");

        if (!string.IsNullOrEmpty(command))
        {
            Commands.Add(new TaskCommand { Command = command, IsRegex = false });
            StatusMessage = "Command added";
        }
    }

    [RelayCommand]
    private void RemoveCommand(TaskCommand command)
    {
        Commands.Remove(command);
        StatusMessage = "Command removed";
    }

    [RelayCommand]
    private async System.Threading.Tasks.Task EditCommand(TaskCommand command)
    {
        var result = await Shell.Current.DisplayPromptAsync(
            "Edit Command",
            "Edit command text:",
            initialValue: command.Command);

        if (result != null)
        {
            command.Command = result;
            StatusMessage = "Command updated";
        }
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
                Description = Description,
                CompletionText = CompletionText,
                Priority = Priority,
                IsRepeatable = IsRepeatable,
                IsLibrary = IsLibrary,
                LastModified = DateTime.Now
            };

            // Parse task type
            if (Enum.TryParse<TaskType>(SelectedTaskType, out var taskType))
            {
                task.Type = taskType;
            }

            // Save commands
            task.Commands.Clear();
            foreach (var cmdVm in Commands)
            {
                task.Commands.Add(new CommandPattern
                {
                    Command = cmdVm.Command
                });
            }

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

public partial class TaskCommand : ObservableObject
{
    [ObservableProperty]
    private string command = "";

    [ObservableProperty]
    private bool isRegex;
}
