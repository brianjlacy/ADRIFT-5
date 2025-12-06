using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class GamePage : ContentPage
{
    private readonly GameViewModel _viewModel;

    public GamePage(GameViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;

        // Auto-scroll to bottom when new output is added
        _viewModel.OutputLines.CollectionChanged += (s, e) =>
        {
            Dispatcher.Dispatch(async () =>
            {
                await Task.Delay(100); // Small delay for layout
                await OutputScrollView.ScrollToAsync(0, double.MaxValue, false);
            });
        };
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        CommandEntry.Focus();
    }

    private async void OnMenuClicked(object? sender, EventArgs e)
    {
        var action = await DisplayActionSheet("Game Menu", "Cancel", null,
            "New Game", "Save Game", "Load Game", "Restart", "About");

        switch (action)
        {
            case "New Game":
                _viewModel.StartNewGameCommand.Execute(null);
                break;

            case "Save Game":
                await DisplayAlert("Save Game", "Save functionality not yet implemented.", "OK");
                break;

            case "Load Game":
                await DisplayAlert("Load Game", "Load functionality not yet implemented.", "OK");
                break;

            case "Restart":
                var confirm = await DisplayAlert("Restart", "Restart the current adventure?", "Yes", "No");
                if (confirm)
                {
                    _viewModel.StartNewGameCommand.Execute(null);
                }
                break;

            case "About":
                var adventure = (Application.Current as App)?.Services.GetService<IAdventureService>()?.CurrentAdventure;
                var message = adventure != null
                    ? $"{adventure.Title}\nby {adventure.Author}\n\n{adventure.Description}"
                    : "No adventure loaded.";
                await DisplayAlert("About", message, "OK");
                break;
        }
    }
}
