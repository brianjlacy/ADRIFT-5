using ADRIFT.Developer.Pages;

namespace ADRIFT.Developer;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        // Register routes for navigation
        Routing.RegisterRoute("locationeditor", typeof(LocationEditorPage));
        Routing.RegisterRoute("objecteditor", typeof(ObjectEditorPage));
        Routing.RegisterRoute("taskeditor", typeof(TaskEditorPage));
        Routing.RegisterRoute("charactereditor", typeof(CharacterEditorPage));
        Routing.RegisterRoute("eventeditor", typeof(EventEditorPage));
        Routing.RegisterRoute("variableeditor", typeof(VariableEditorPage));
        Routing.RegisterRoute("synonymeditor", typeof(SynonymEditorPage));
        Routing.RegisterRoute("groupeditor", typeof(GroupEditorPage));
        Routing.RegisterRoute("hinteditor", typeof(HintEditorPage));
        Routing.RegisterRoute("settings", typeof(SettingsPage));
    }

    private async void OnNewAdventure(object sender, EventArgs e)
    {
        var result = await DisplayAlert("New Adventure",
            "Create a new adventure? Any unsaved changes will be lost.",
            "Yes", "No");

        if (result)
        {
            // TODO: Create new adventure
            await DisplayAlert("Info", "New adventure created", "OK");
        }
    }

    private async void OnOpenAdventure(object sender, EventArgs e)
    {
        // TODO: Show file picker
        await DisplayAlert("Info", "Open adventure dialog", "OK");
    }

    private async void OnSaveAdventure(object sender, EventArgs e)
    {
        // TODO: Save current adventure
        await DisplayAlert("Info", "Adventure saved", "OK");
    }

    private async void OnSettings(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("settings");
    }

    private async void OnAbout(object sender, EventArgs e)
    {
        await DisplayAlert("About ADRIFT 5",
            "ADRIFT 5.0.36.6\n" +
            "Adventure Development & Runner - Interactive Fiction Toolkit\n\n" +
            "Copyright © Campbell Wild 1998-2025\n\n" +
            "Converted to .NET MAUI from VB.NET/WinForms",
            "OK");
    }
}
