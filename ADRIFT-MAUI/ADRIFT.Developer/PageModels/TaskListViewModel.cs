using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

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

    [RelayCommand]
    private async Task AddTask() => await Shell.Current.GoToAsync("taskeditor");

    [RelayCommand]
    private async Task EditTask(TaskItemViewModel task) => await Shell.Current.GoToAsync($"taskeditor?key={task.Key}");

    [RelayCommand]
    private async Task DeleteTask(TaskItemViewModel task)
    {
        if (await Shell.Current.DisplayAlert("Delete", $"Delete '{task.Command}'?", "Delete", "Cancel"))
        {
            FilteredTasks.Remove(task);
            TaskCount = FilteredTasks.Count;
        }
    }

    [RelayCommand]
    private async Task Refresh() => await LoadTasksAsync();

    [RelayCommand]
    private Task Filter() => Shell.Current.DisplayActionSheet("Filter", "Cancel", null, "All Tasks", "System", "General", "High Priority");
}

public partial class TaskItemViewModel : ObservableObject
{
    public TaskItemViewModel()
    {
        Key = "task" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Command = "Sample Task";
        Description = "Task description";
        TaskType = "General";
        Priority = "Normal";
    }

    public TaskItemViewModel(object task) : this()
    {
        // TODO: Map from task to view model properties
    }

    [ObservableProperty] private string key = "";
    [ObservableProperty] private string command = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private string taskType = "";
    [ObservableProperty] private string priority = "";
}
