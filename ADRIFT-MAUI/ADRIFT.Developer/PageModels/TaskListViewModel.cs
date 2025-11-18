using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class TaskListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public TaskListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredTasks = new ObservableCollection<TaskItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<TaskItemViewModel> filteredTasks;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int taskCount;

    [ObservableProperty]
    private bool isRefreshing;

    partial void OnSearchTextChanged(string value)
    {
        FilterTasks();
    }

    public async Task LoadTasksAsync()
    {
        try
        {
            IsRefreshing = true;
            var tasks = await _adventureService.GetTasksAsync();
            FilteredTasks.Clear();
            foreach (var task in tasks)
            {
                FilteredTasks.Add(new TaskItemViewModel(task));
            }
            TaskCount = FilteredTasks.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async void FilterTasks()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadTasksAsync();
            return;
        }

        try
        {
            var allTasks = await _adventureService.GetTasksAsync();
            var filtered = allTasks.Where(t =>
                t.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                t.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredTasks.Clear();
            foreach (var task in filtered)
            {
                FilteredTasks.Add(new TaskItemViewModel(task));
            }

            TaskCount = FilteredTasks.Count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter tasks: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddTask() => await Shell.Current.GoToAsync("taskeditor");

    [RelayCommand]
    private async Task EditTask(TaskItemViewModel task) => await Shell.Current.GoToAsync($"taskeditor?key={task.Key}");

    [RelayCommand]
    private async Task DeleteTask(TaskItemViewModel task)
    {
        if (await Shell.Current.DisplayAlert("Delete", $"Delete '{task.Command}'?", "Delete", "Cancel"))
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Tasks.ContainsKey(task.Key))
                {
                    adventure.Tasks.Remove(task.Key);
                    FilteredTasks.Remove(task);
                    TaskCount = FilteredTasks.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete task: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Refresh() => await LoadTasksAsync();

    [RelayCommand]
    private async Task Sort()
    {
        var action = await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Command (A-Z)", "Command (Z-A)", "Priority", "Type");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Command (A-Z)" => FilteredTasks.OrderBy(t => t.Command).ToList(),
                "Command (Z-A)" => FilteredTasks.OrderByDescending(t => t.Command).ToList(),
                "Priority" => FilteredTasks.OrderBy(t => t.Priority).ToList(),
                "Type" => FilteredTasks.OrderBy(t => t.TaskType).ToList(),
                _ => FilteredTasks.ToList()
            };

            FilteredTasks.Clear();
            foreach (var item in sorted)
            {
                FilteredTasks.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Tasks", "System", "General", "Specific", "Repeatable");

        if (action != null && action != "Cancel")
        {
            try
            {
                var allTasks = await _adventureService.GetTasksAsync();
                IEnumerable<Core.Models.Task> filtered = action switch
                {
                    "System" => allTasks.Where(t => t.Type == TaskType.System),
                    "General" => allTasks.Where(t => t.Type == TaskType.General),
                    "Specific" => allTasks.Where(t => t.Type == TaskType.Specific),
                    "Repeatable" => allTasks.Where(t => t.IsRepeatable),
                    _ => allTasks
                };

                FilteredTasks.Clear();
                foreach (var task in filtered)
                {
                    FilteredTasks.Add(new TaskItemViewModel(task));
                }

                TaskCount = FilteredTasks.Count;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter tasks: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterTasks();
        await Task.CompletedTask;
    }
}

public partial class TaskItemViewModel : ObservableObject
{
    public TaskItemViewModel()
    {
        Key = "task" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Command = "Sample Task";
        Description = "Task description";
        TaskType = "General";
        Priority = "5";
    }

    public TaskItemViewModel(Core.Models.Task task) : this()
    {
        Key = task.Key;
        Command = task.Commands.Any() ? task.Commands.First().Command : task.Name;
        Description = task.Description;
        TaskType = task.Type.ToString();
        Priority = task.Priority.ToString();
    }

    [ObservableProperty] private string key = "";
    [ObservableProperty] private string command = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private string taskType = "";
    [ObservableProperty] private string priority = "";
}
