using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class ALRListPage : ContentPage
{
    private readonly AdventureService _adventureService;

    public ALRListPage()
    {
        InitializeComponent();
        _adventureService = AdventureService.Instance;
        LoadALRs();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadALRs();
    }

    private void LoadALRs()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        ALRsCollection.ItemsSource = adventure.ALRs.Values.OrderBy(a => a.Order).ToList();
    }

    private async void OnALRTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is ALR alr)
        {
            await Navigation.PushAsync(new ALREditorPage(alr));
        }
    }

    private async void OnAddALR(object sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
        {
            await DisplayAlert("Error", "No adventure loaded.", "OK");
            return;
        }

        var newALR = new ALR
        {
            Key = Guid.NewGuid().ToString(),
            OldText = "text to replace",
            NewText = new Description { Text = "replacement text" },
            Order = 100
        };

        adventure.ALRs[newALR.Key] = newALR;
        await Navigation.PushAsync(new ALREditorPage(newALR));
    }
}
