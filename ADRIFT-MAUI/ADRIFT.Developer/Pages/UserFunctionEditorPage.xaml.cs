using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class UserFunctionEditorPage : ContentPage
{
    private readonly UserFunction _function;
    private readonly AdventureService _adventureService;

    public UserFunctionEditorPage(UserFunction function)
    {
        InitializeComponent();
        _function = function;
        _adventureService = AdventureService.Instance;

        LoadFunction();
    }

    private void LoadFunction()
    {
        NameEntry.Text = _function.Name;
        DescriptionEditor.Text = _function.Description;
        OutputEditor.Text = _function.Output.Text;

        // Load arguments
        var argumentLines = _function.Arguments
            .OrderBy(a => a.ArgumentNumber)
            .Select(a => $"{a.Name}|{a.Type}")
            .ToList();
        ArgumentsEditor.Text = string.Join("\n", argumentLines);

        UpdateUsageLabel();
        NameEntry.TextChanged += (s, e) => UpdateUsageLabel();
        ArgumentsEditor.TextChanged += (s, e) => UpdateUsageLabel();
    }

    private void UpdateUsageLabel()
    {
        var args = ArgumentsEditor.Text?
            .Split('\n', StringSplitOptions.RemoveEmptyEntries)
            .Select(l => l.Split('|')[0].Trim())
            .ToList() ?? new List<string>();

        var argsText = args.Count > 0 ? ":" + string.Join(":", args) : "";
        UsageLabel.Text = $"{{{NameEntry.Text?.Trim() ?? "FunctionName"}{argsText}}}";
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        try
        {
            _function.Name = NameEntry.Text?.Trim() ?? "Unnamed";
            _function.Description = DescriptionEditor.Text?.Trim() ?? "";
            _function.Output.Text = OutputEditor.Text?.Trim() ?? "";

            // Parse arguments
            _function.Arguments.Clear();
            var lines = ArgumentsEditor.Text?
                .Split('\n', StringSplitOptions.RemoveEmptyEntries) ?? Array.Empty<string>();

            for (int i = 0; i < lines.Length; i++)
            {
                var parts = lines[i].Split('|', StringSplitOptions.TrimEntries);
                if (parts.Length >= 2)
                {
                    var argType = Enum.TryParse<FunctionArgumentType>(parts[1], out var type)
                        ? type
                        : FunctionArgumentType.Text;

                    _function.Arguments.Add(new FunctionArgument
                    {
                        ArgumentNumber = i + 1,
                        Name = parts[0],
                        Type = argType
                    });
                }
            }

            await DisplayAlert("Success", "Function saved successfully.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save function: {ex.Message}", "OK");
        }
    }

    private async void OnDelete(object? sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        var result = await DisplayAlert(
            "Delete Function",
            $"Are you sure you want to delete '{_function.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            adventure.UserFunctions.Remove(_function.Key);
            await DisplayAlert("Success", "Function deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}
