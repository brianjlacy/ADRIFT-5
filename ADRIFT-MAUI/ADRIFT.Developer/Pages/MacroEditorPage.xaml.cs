using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class MacroEditorPage : ContentPage
{
    private readonly Macro _macro;
    private readonly AdventureService _adventureService;

    public MacroEditorPage(Macro macro)
    {
        InitializeComponent();
        _macro = macro;
        _adventureService = AdventureService.Instance;

        LoadMacro();
    }

    private void LoadMacro()
    {
        NameEntry.Text = _macro.Name;
        CommandEntry.Text = _macro.Command;
        KeyBindingEntry.Text = _macro.KeyBinding;
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        try
        {
            _macro.Name = NameEntry.Text?.Trim() ?? "Unnamed Macro";
            _macro.Command = CommandEntry.Text?.Trim() ?? "";
            _macro.KeyBinding = KeyBindingEntry.Text?.Trim() ?? "";

            await DisplayAlert("Success", "Macro saved successfully.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save macro: {ex.Message}", "OK");
        }
    }

    private async void OnDelete(object? sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        var result = await DisplayAlert(
            "Delete Macro",
            $"Are you sure you want to delete '{_macro.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            adventure.Macros.Remove(_macro);
            await DisplayAlert("Success", "Macro deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}
