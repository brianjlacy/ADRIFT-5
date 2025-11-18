using ADRIFT.Core.Engine;
using ADRIFT.Core.Models;

namespace ADRIFT.Runner.Pages;

public partial class GamePage : ContentPage
{
    private GameEngine? _engine;
    private readonly List<string> _commandHistory = new();
    private int _historyIndex = -1;

    public GamePage()
    {
        InitializeComponent();
    }

    public GamePage(Adventure adventure) : this()
    {
        StartAdventure(adventure);
    }

    private void StartAdventure(Adventure adventure)
    {
        _engine = new GameEngine(adventure);

        // Display introduction
        var intro = _engine.GetIntroduction();
        AppendOutput(intro);
        AppendOutput("\n> ");

        CommandEntry.Focus();
    }

    private void OnCommandEntered(object? sender, EventArgs e)
    {
        if (_engine == null)
            return;

        var command = CommandEntry.Text?.Trim();
        if (string.IsNullOrWhiteSpace(command))
            return;

        // Add to history
        _commandHistory.Add(command);
        _historyIndex = _commandHistory.Count;

        // Echo command
        AppendOutput(command);

        // Process command
        var response = _engine.ProcessCommand(command);

        // Display response
        if (!string.IsNullOrWhiteSpace(response))
        {
            AppendOutput("\n" + response);
        }

        // Check game end
        if (_engine.State.GameWon || _engine.State.GameLost)
        {
            AppendOutput("\n\n[Game Over]");
            CommandEntry.IsEnabled = false;
        }
        else
        {
            AppendOutput("\n> ");
        }

        // Clear input
        CommandEntry.Text = string.Empty;

        // Scroll to bottom
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(100);
            await OutputScroll.ScrollToAsync(0, OutputLabel.Height, false);
        });
    }

    private void AppendOutput(string text)
    {
        if (string.IsNullOrEmpty(OutputLabel.Text))
        {
            OutputLabel.Text = text;
        }
        else
        {
            OutputLabel.Text += text;
        }
    }

    protected override bool OnBackButtonPressed()
    {
        // Confirm before leaving game
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            var result = await DisplayAlert("Exit Game",
                "Are you sure you want to exit? Unsaved progress will be lost.",
                "Exit", "Cancel");

            if (result)
            {
                await Navigation.PopAsync();
            }
        });

        return true; // Prevent default back behavior
    }
}
