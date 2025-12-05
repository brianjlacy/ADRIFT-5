using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class MacroListPage : ContentPage
{
    private readonly AdventureService _adventureService;

    public MacroListPage()
    {
        InitializeComponent();
        _adventureService = AdventureService.Instance;
        LoadMacros();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadMacros();
    }

    private void LoadMacros()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        MacrosCollection.ItemsSource = adventure.Macros.OrderBy(m => m.Name).ToList();
    }

    private async void OnMacroTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Macro macro)
        {
            await Navigation.PushAsync(new MacroEditorPage(macro));
        }
    }

    private async void OnAddMacro(object sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
        {
            await DisplayAlert("Error", "No adventure loaded.", "OK");
            return;
        }

        var newMacro = new Macro
        {
            Name = "New Macro",
            Command = "look",
            KeyBinding = ""
        };

        adventure.Macros.Add(newMacro);
        await Navigation.PushAsync(new MacroEditorPage(newMacro));
    }
}
