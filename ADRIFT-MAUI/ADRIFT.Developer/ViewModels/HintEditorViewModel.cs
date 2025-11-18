using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

[QueryProperty(nameof(HintKey), "key")]
public partial class HintEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public HintEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        Hints = new ObservableCollection<HintItemViewModel>();
        Hints.CollectionChanged += (s, e) => UpdateHintCounts();
        AvailableTasks = new ObservableCollection<TaskItemViewModel>();
    }

    [ObservableProperty]
    private string hintKey = "";

    [ObservableProperty]
    private string pageTitle = "New Hint";

    [ObservableProperty]
    private string question = "";

    [ObservableProperty]
    private ObservableCollection<TaskItemViewModel> availableTasks;

    [ObservableProperty]
    private TaskItemViewModel? selectedTask;

    [ObservableProperty]
    private string newHintText = "";

    [ObservableProperty]
    private ObservableCollection<HintItemViewModel> hints;

    [ObservableProperty]
    private int hintCount = 0;

    [ObservableProperty]
    private bool hasHints = false;

    [ObservableProperty]
    private bool hasNoHints = true;

    [ObservableProperty]
    private string hintCountText = "0 hints";

    [ObservableProperty]
    private bool isEditMode = false;

    private void UpdateHintCounts()
    {
        HintCount = Hints.Count;
        HasHints = HintCount > 0;
        HasNoHints = HintCount == 0;
        HintCountText = $"{HintCount} {(HintCount == 1 ? "hint" : "hints")}";

        // Update order numbers
        for (int i = 0; i < Hints.Count; i++)
        {
            Hints[i].Order = i + 1;
        }
    }

    public async Task InitializeAsync()
    {
        // Load available tasks
        // TODO: Load from adventure service
        AvailableTasks.Add(new TaskItemViewModel { Key = "task1", Name = "Open the door" });
        AvailableTasks.Add(new TaskItemViewModel { Key = "task2", Name = "Find the key" });
        AvailableTasks.Add(new TaskItemViewModel { Key = "task3", Name = "Talk to the guard" });

        if (!string.IsNullOrEmpty(HintKey))
        {
            // Edit mode - load existing hint
            IsEditMode = true;
            PageTitle = "Edit Hint";

            // TODO: Load hint from adventure service
            // For now, using sample data
            Question = "How do I get past the guard?";
            SelectedTask = AvailableTasks.FirstOrDefault();

            Hints.Add(new HintItemViewModel { Text = "Try talking to the guard" });
            Hints.Add(new HintItemViewModel { Text = "Maybe the guard wants something?" });
            Hints.Add(new HintItemViewModel { Text = "Check your inventory for items" });
        }
        else
        {
            // New hint mode
            IsEditMode = false;
            PageTitle = "New Hint";
            Question = "";
            SelectedTask = null;
            Hints.Clear();
        }

        await Task.CompletedTask;
    }

    [RelayCommand]
    private void AddHint()
    {
        if (string.IsNullOrWhiteSpace(NewHintText))
            return;

        Hints.Add(new HintItemViewModel { Text = NewHintText.Trim() });
        NewHintText = ""; // Clear the entry field
    }

    [RelayCommand]
    private void DeleteHint(HintItemViewModel hint)
    {
        if (hint != null)
        {
            Hints.Remove(hint);
        }
    }

    [RelayCommand]
    private void MoveHintUp(HintItemViewModel hint)
    {
        if (hint == null)
            return;

        var index = Hints.IndexOf(hint);
        if (index > 0)
        {
            Hints.Move(index, index - 1);
            UpdateHintCounts(); // Refresh order numbers
        }
    }

    [RelayCommand]
    private void MoveHintDown(HintItemViewModel hint)
    {
        if (hint == null)
            return;

        var index = Hints.IndexOf(hint);
        if (index < Hints.Count - 1)
        {
            Hints.Move(index, index + 1);
            UpdateHintCounts(); // Refresh order numbers
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

        // Save changes but stay on page
        await SaveHint();

        await Shell.Current.DisplayAlert("Success", "Hint changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveHint();
        await Shell.Current.GoToAsync("..");
    }

    private bool ValidateInput()
    {
        // Validate question
        if (string.IsNullOrWhiteSpace(Question))
        {
            Shell.Current.DisplayAlert("Validation Error", "Question is required.", "OK");
            return false;
        }

        // At least one hint should be added
        if (Hints.Count == 0)
        {
            Shell.Current.DisplayAlert("Validation Error", "Please add at least one hint.", "OK");
            return false;
        }

        return true;
    }

    private async Task SaveHint()
    {
        // TODO: Save hint to adventure service
        // For now, just a placeholder
        await Task.Delay(100);

        // If this was a new hint, generate a key
        if (string.IsNullOrEmpty(HintKey))
        {
            HintKey = "hint_" + Guid.NewGuid().ToString("N")[..8];
        }
    }
}

public partial class HintItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string text = "";

    [ObservableProperty]
    private int order = 1;
}

public partial class TaskItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string key = "";

    [ObservableProperty]
    private string name = "";
}
