using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.ViewModels;

[QueryProperty(nameof(VariableKey), "key")]
public partial class VariableEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public VariableEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        VariableTypes = new ObservableCollection<string> { "Integer", "Text", "Boolean" };
        SelectedVariableType = "Integer";
        UpdateKeyboardAndHint();
    }

    [ObservableProperty]
    private string variableKey = "";

    [ObservableProperty]
    private string pageTitle = "New Variable";

    [ObservableProperty]
    private string variableName = "";

    [ObservableProperty]
    private ObservableCollection<string> variableTypes;

    [ObservableProperty]
    private string selectedVariableType = "Integer";

    [ObservableProperty]
    private string initialValue = "0";

    [ObservableProperty]
    private string currentValue = "0";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private bool isEditMode = false;

    [ObservableProperty]
    private Keyboard valueKeyboard = Keyboard.Numeric;

    [ObservableProperty]
    private string valueHint = "Enter an integer value (e.g., 0, 42, -10)";

    partial void OnSelectedVariableTypeChanged(string value)
    {
        UpdateKeyboardAndHint();

        // Set appropriate default value when type changes
        if (string.IsNullOrEmpty(InitialValue) ||
            (value == "Integer" && InitialValue == "") ||
            (value == "Text" && (InitialValue == "0" || InitialValue == "False")) ||
            (value == "Boolean" && InitialValue != "True" && InitialValue != "False"))
        {
            InitialValue = value switch
            {
                "Integer" => "0",
                "Boolean" => "False",
                _ => ""
            };
        }
    }

    private void UpdateKeyboardAndHint()
    {
        switch (SelectedVariableType)
        {
            case "Integer":
                ValueKeyboard = Keyboard.Numeric;
                ValueHint = "Enter an integer value (e.g., 0, 42, -10)";
                break;
            case "Boolean":
                ValueKeyboard = Keyboard.Default;
                ValueHint = "Enter True or False";
                break;
            case "Text":
            default:
                ValueKeyboard = Keyboard.Default;
                ValueHint = "Enter any text value";
                break;
        }
    }

    public async Task InitializeAsync()
    {
        if (!string.IsNullOrEmpty(VariableKey))
        {
            // Edit mode - load existing variable
            IsEditMode = true;
            PageTitle = "Edit Variable";

            // TODO: Load variable from adventure service
            // For now, using sample data
            VariableName = "Sample Variable";
            SelectedVariableType = "Integer";
            InitialValue = "10";
            CurrentValue = "15";
            Description = "This is a sample variable for demonstration";
        }
        else
        {
            // New variable mode
            IsEditMode = false;
            PageTitle = "New Variable";
            VariableName = "";
            SelectedVariableType = "Integer";
            InitialValue = "0";
            CurrentValue = "0";
            Description = "";
        }

        await Task.CompletedTask;
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
        await SaveVariable();

        await Shell.Current.DisplayAlert("Success", "Variable changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveVariable();
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private void ResetToInitial()
    {
        CurrentValue = InitialValue;
    }

    private bool ValidateInput()
    {
        // Validate variable name
        if (string.IsNullOrWhiteSpace(VariableName))
        {
            Shell.Current.DisplayAlert("Validation Error", "Variable name is required.", "OK");
            return false;
        }

        // Validate initial value based on type
        if (SelectedVariableType == "Integer")
        {
            if (!int.TryParse(InitialValue, out _))
            {
                Shell.Current.DisplayAlert("Validation Error", "Initial value must be a valid integer.", "OK");
                return false;
            }
        }
        else if (SelectedVariableType == "Boolean")
        {
            if (InitialValue != "True" && InitialValue != "False")
            {
                Shell.Current.DisplayAlert("Validation Error", "Initial value must be True or False.", "OK");
                return false;
            }
        }

        return true;
    }

    private async Task SaveVariable()
    {
        // TODO: Save variable to adventure service
        // For now, just a placeholder
        await Task.Delay(100);

        // If this was a new variable, generate a key
        if (string.IsNullOrEmpty(VariableKey))
        {
            VariableKey = "var_" + Guid.NewGuid().ToString("N")[..8];
        }
    }
}
