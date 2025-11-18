using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class PropertyListPage : ContentPage
{
    private readonly AdventureService _adventureService;

    public PropertyListPage()
    {
        InitializeComponent();
        _adventureService = AdventureService.Instance;
        LoadProperties();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadProperties();
    }

    private void LoadProperties()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        PropertiesCollection.ItemsSource = adventure.Properties.Values.OrderBy(p => p.Name).ToList();
    }

    private async void OnPropertyTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is Property property)
        {
            await Navigation.PushAsync(new PropertyEditorPage(property));
        }
    }

    private async void OnAddProperty(object sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
        {
            await DisplayAlert("Error", "No adventure loaded.", "OK");
            return;
        }

        var newProperty = new Property
        {
            Key = Guid.NewGuid().ToString(),
            Name = "New Property",
            Type = PropertyType.SelectionOnly
        };

        adventure.Properties[newProperty.Key] = newProperty;
        await Navigation.PushAsync(new PropertyEditorPage(newProperty));
    }
}
