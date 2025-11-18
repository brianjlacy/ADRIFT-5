using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

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
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null) return;

            // Load available tasks
            AvailableTasks.Clear();
            foreach (var task in adventure.Tasks.Values)
            {
                AvailableTasks.Add(new TaskItemViewModel
                {
                    Key = task.Key,
                    Command = task.Commands.Any() ? task.Commands.First().Command : task.Name
                });
            }

            if (!string.IsNullOrEmpty(HintKey) && adventure.Hints.TryGetValue(HintKey, out var existingHint))
            {
                // Edit mode - load existing hint
                IsEditMode = true;
                PageTitle = "Edit Hint";

                Question = existingHint.Question;

                // Set selected task
                if (!string.IsNullOrEmpty(existingHint.RelatedTaskKey))
                {
                    SelectedTask = AvailableTasks.FirstOrDefault(t => t.Key == existingHint.RelatedTaskKey);
                }

                // Load hints
                Hints.Clear();
                foreach (var hint in existingHint.Hints.OrderBy(h => h.Order))
                {
                    Hints.Add(new HintItemViewModel
                    {
                        Text = hint.Text,
                        Order = hint.Order
                    });
                }
            }
            else
            {
                // New hint mode
                HintKey = "hint_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                IsEditMode = false;
                PageTitle = "New Hint";
                Question = "";
                SelectedTask = null;
                Hints.Clear();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to initialize: {ex.Message}", "OK");
        }
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
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
                return;
            }

            if (string.IsNullOrEmpty(HintKey))
            {
                HintKey = "hint_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            }

            var hint = new Hint
            {
                Key = HintKey,
                Question = Question.Trim(),
                RelatedTaskKey = SelectedTask?.Key,
                LastModified = DateTime.Now
            };

            // Save hints
            hint.Hints.Clear();
            foreach (var hintVm in Hints)
            {
                if (!string.IsNullOrWhiteSpace(hintVm.Text))
                {
                    hint.Hints.Add(new HintText
                    {
                        Order = hintVm.Order,
                        Text = hintVm.Text.Trim()
                    });
                }
            }

            // Add or update in adventure
            if (adventure.Hints.ContainsKey(HintKey))
            {
                adventure.Hints[HintKey] = hint;
            }
            else
            {
                adventure.Hints.Add(HintKey, hint);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to save hint: {ex.Message}", "OK");
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
