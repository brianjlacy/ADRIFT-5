using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public MainViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        LoadAdventure();
    }

    [ObservableProperty]
    private string adventureTitle = "New Adventure";

    [ObservableProperty]
    private string author = "";

    [ObservableProperty]
    private string introduction = "";

    [ObservableProperty]
    private string winningText = "";

    [ObservableProperty]
    private string version = "5.0.36.6";

    [ObservableProperty]
    private int locationCount;

    [ObservableProperty]
    private int objectCount;

    [ObservableProperty]
    private int taskCount;

    [ObservableProperty]
    private int characterCount;

    [ObservableProperty]
    private int eventCount;

    [ObservableProperty]
    private int variableCount;

    private void LoadAdventure()
    {
        // TODO: Load adventure from service
        UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        // TODO: Get actual counts from adventure
        LocationCount = 0;
        ObjectCount = 0;
        TaskCount = 0;
        CharacterCount = 0;
        EventCount = 0;
        VariableCount = 0;
    }

    [RelayCommand]
    private async Task NewLocation()
    {
        await Shell.Current.GoToAsync("locationeditor");
    }

    [RelayCommand]
    private async Task NewObject()
    {
        await Shell.Current.GoToAsync("objecteditor");
    }

    [RelayCommand]
    private async Task NewTask()
    {
        await Shell.Current.GoToAsync("taskeditor");
    }

    [RelayCommand]
    private async Task NewCharacter()
    {
        await Shell.Current.GoToAsync("charactereditor");
    }

    [RelayCommand]
    private async Task TestAdventure()
    {
        // TODO: Launch runner with current adventure
        await Shell.Current.DisplayAlert("Test", "Testing adventure...", "OK");
    }

    [RelayCommand]
    private async Task ViewMap()
    {
        await Shell.Current.GoToAsync("//map");
    }
}
