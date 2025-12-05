using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.PageModels;

public partial class WalkEditorViewModel : ObservableObject
{
    private Walk? _walk;

    public WalkEditorViewModel()
    {
        Steps = new ObservableCollection<WalkStepItemViewModel>();
        Steps.CollectionChanged += (s, e) => UpdateStepCount();
    }

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private bool loop = false;

    [ObservableProperty]
    private bool startActive = false;

    [ObservableProperty]
    private string status = "NotStarted";

    [ObservableProperty]
    private ObservableCollection<WalkStepItemViewModel> steps;

    [ObservableProperty]
    private string stepCountText = "(0 steps)";

    /// <summary>
    /// Load walk data into the editor
    /// </summary>
    public void LoadWalk(Walk walk)
    {
        _walk = walk;

        Description = walk.Description;
        Loop = walk.Loop;
        StartActive = walk.StartActive;
        Status = walk.Status.ToString();

        // Load steps
        Steps.Clear();
        foreach (var step in walk.Steps.OrderBy(s => s.StepNumber))
        {
            Steps.Add(new WalkStepItemViewModel
            {
                StepNumber = step.StepNumber,
                LocationKey = step.LocationKey,
                Direction = step.Direction.ToString(),
                DelayTurns = step.DelayTurns
            });
        }

        UpdateStepCount();
    }

    /// <summary>
    /// Save walk data back to the model
    /// </summary>
    private void SaveToModel()
    {
        if (_walk == null) return;

        _walk.Description = Description;
        _walk.Loop = Loop;
        _walk.StartActive = StartActive;

        if (Enum.TryParse<WalkStatus>(Status, out var walkStatus))
        {
            _walk.Status = walkStatus;
        }

        // Save steps
        _walk.Steps.Clear();
        for (int i = 0; i < Steps.Count; i++)
        {
            var stepVM = Steps[i];
            _walk.Steps.Add(new WalkStep
            {
                StepNumber = i + 1,
                LocationKey = stepVM.LocationKey,
                Direction = Enum.TryParse<DirectionEnum>(stepVM.Direction, out var dir) ? dir : DirectionEnum.North,
                DelayTurns = stepVM.DelayTurns
            });
        }
    }

    private void UpdateStepCount()
    {
        var count = Steps.Count;
        StepCountText = $"({count} {(count == 1 ? "step" : "steps")})";

        // Renumber steps
        for (int i = 0; i < Steps.Count; i++)
        {
            Steps[i].StepNumber = i + 1;
        }
    }

    [RelayCommand]
    private async Task AddStep()
    {
        // Create a new step and prompt user to edit it
        var newStep = new WalkStepItemViewModel
        {
            StepNumber = Steps.Count + 1,
            LocationKey = "location_key",
            Direction = "North",
            DelayTurns = 0
        };

        Steps.Add(newStep);

        // TODO: In a real implementation, this would open a step detail editor
        // For now, we just add a placeholder
        await Task.CompletedTask;
    }

    [RelayCommand]
    private void RemoveStep(WalkStepItemViewModel step)
    {
        Steps.Remove(step);
        UpdateStepCount();
    }

    [RelayCommand]
    private void MoveStepUp(WalkStepItemViewModel step)
    {
        var index = Steps.IndexOf(step);
        if (index > 0)
        {
            Steps.Move(index, index - 1);
            UpdateStepCount();
        }
    }

    [RelayCommand]
    private void MoveStepDown(WalkStepItemViewModel step)
    {
        var index = Steps.IndexOf(step);
        if (index < Steps.Count - 1)
        {
            Steps.Move(index, index + 1);
            UpdateStepCount();
        }
    }

    [RelayCommand]
    private async Task EditStep(WalkStepItemViewModel step)
    {
        // TODO: Open a detail editor for the step
        // For now, show a simple prompt
        await Application.Current!.MainPage!.DisplayAlert(
            "Edit Step",
            $"Step {step.StepNumber}: Edit LocationKey, Direction, and DelayTurns in a detail dialog (not yet implemented)",
            "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        SaveToModel();
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private Task Apply()
    {
        SaveToModel();
        return Task.CompletedTask;
    }

    [RelayCommand]
    private async Task Cancel()
    {
        var result = await Application.Current!.MainPage!.DisplayAlert(
            "Cancel",
            "Discard changes to this walk route?",
            "Yes", "No");

        if (result)
        {
            await Shell.Current.GoToAsync("..");
        }
    }
}

/// <summary>
/// ViewModel for a single walk step
/// </summary>
public partial class WalkStepItemViewModel : ObservableObject
{
    [ObservableProperty]
    private int stepNumber = 1;

    [ObservableProperty]
    private string locationKey = "";

    [ObservableProperty]
    private string direction = "North";

    [ObservableProperty]
    private int delayTurns = 0;
}
