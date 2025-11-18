using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.ViewModels;

public partial class TaskEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public TaskEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
    }

    [ObservableProperty]
    private string taskKey = "";

    [ObservableProperty]
    private string taskName = "";

    [ObservableProperty]
    private string description = "";

    [RelayCommand]
    private async System.Threading.Tasks.Task SaveTask()
    {
        // TODO: Implement task saving
        await System.Threading.Tasks.Task.CompletedTask;
    }
}
