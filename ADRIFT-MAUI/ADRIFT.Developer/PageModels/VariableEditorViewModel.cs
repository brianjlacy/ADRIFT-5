using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

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
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null) return;

            if (!string.IsNullOrEmpty(VariableKey) && adventure.Variables.TryGetValue(VariableKey, out var existingVariable))
            {
                // Edit mode - load existing variable
                IsEditMode = true;
                PageTitle = "Edit Variable";

                VariableName = existingVariable.Name;
                SelectedVariableType = existingVariable.Type.ToString();
                InitialValue = existingVariable.InitialValue;
                CurrentValue = existingVariable.CurrentValue;
                Description = existingVariable.Description;
            }
            else
            {
                // New variable mode
                VariableKey = "var_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                IsEditMode = false;
                PageTitle = "New Variable";
                VariableName = "";
                SelectedVariableType = "Integer";
                InitialValue = "0";
                CurrentValue = "0";
                Description = "";
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to initialize: {ex.Message}", "OK");
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
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
                return;
            }

            if (string.IsNullOrEmpty(VariableKey))
            {
                VariableKey = "var_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            }

            var variable = new Variable
            {
                Key = VariableKey,
                Name = VariableName,
                InitialValue = InitialValue,
                CurrentValue = CurrentValue,
                Description = Description,
                LastModified = DateTime.Now
            };

            // Parse variable type
            if (Enum.TryParse<VariableType>(SelectedVariableType, out var varType))
            {
                variable.Type = varType;
            }

            // Add or update in adventure
            if (adventure.Variables.ContainsKey(VariableKey))
            {
                adventure.Variables[VariableKey] = variable;
            }
            else
            {
                adventure.Variables.Add(VariableKey, variable);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to save variable: {ex.Message}", "OK");
        }
    }
}
