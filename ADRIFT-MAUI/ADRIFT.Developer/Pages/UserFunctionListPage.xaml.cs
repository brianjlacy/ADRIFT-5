using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class UserFunctionListPage : ContentPage
{
    private readonly AdventureService _adventureService;

    public UserFunctionListPage()
    {
        InitializeComponent();
        _adventureService = AdventureService.Instance;
        LoadFunctions();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadFunctions();
    }

    private void LoadFunctions()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        FunctionsCollection.ItemsSource = adventure.UserFunctions.Values.OrderBy(f => f.Name).ToList();
    }

    private async void OnFunctionTapped(object sender, TappedEventArgs e)
    {
        if (sender is Frame frame && frame.BindingContext is UserFunction function)
        {
            await Navigation.PushAsync(new UserFunctionEditorPage(function));
        }
    }

    private async void OnAddFunction(object sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
        {
            await DisplayAlert("Error", "No adventure loaded.", "OK");
            return;
        }

        var newFunction = new UserFunction
        {
            Key = Guid.NewGuid().ToString(),
            Name = "NewFunction",
            Description = "Custom function",
            Output = new Description { Text = "" }
        };

        adventure.UserFunctions[newFunction.Key] = newFunction;
        await Navigation.PushAsync(new UserFunctionEditorPage(newFunction));
    }
}
