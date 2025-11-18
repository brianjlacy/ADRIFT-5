using ADRIFT.Core.Engine;
using ADRIFT.Core.Models;
using System.Text.Json;

namespace ADRIFT.Runner.Pages;

public partial class GamePage : ContentPage
{
    private GameEngine? _engine;
    private readonly List<string> _commandHistory = new();
    private int _historyIndex = -1;
    private Adventure? _currentAdventure;

    public GamePage()
    {
        InitializeComponent();
    }

    public GamePage(Adventure adventure) : this()
    {
        _currentAdventure = adventure;
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

    private async void OnSaveGame(object? sender, EventArgs e)
    {
        if (_engine == null)
        {
            await DisplayAlert("Error", "No game in progress.", "OK");
            return;
        }

        try
        {
            // Prompt for save name
            var saveName = await DisplayPromptAsync(
                "Save Game",
                "Enter a name for this save:",
                initialValue: $"Save_{DateTime.Now:yyyyMMdd_HHmmss}");

            if (string.IsNullOrWhiteSpace(saveName))
                return;

            // Get saves directory
            var savesPath = Path.Combine(FileSystem.AppDataDirectory, "Saves");
            if (!Directory.Exists(savesPath))
                Directory.CreateDirectory(savesPath);

            var saveFilePath = Path.Combine(savesPath, $"{saveName}.sav");

            // Save game state
            var savedState = _engine.SaveGame();
            var json = JsonSerializer.Serialize(savedState, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(saveFilePath, json);

            await DisplayAlert("Success", $"Game saved as '{saveName}'", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save game: {ex.Message}", "OK");
        }
    }

    private async void OnLoadGame(object? sender, EventArgs e)
    {
        if (_engine == null)
        {
            await DisplayAlert("Error", "No game in progress.", "OK");
            return;
        }

        try
        {
            // Get saves directory
            var savesPath = Path.Combine(FileSystem.AppDataDirectory, "Saves");
            if (!Directory.Exists(savesPath))
            {
                await DisplayAlert("Info", "No saved games found.", "OK");
                return;
            }

            // Get list of save files
            var saveFiles = Directory.GetFiles(savesPath, "*.sav");
            if (saveFiles.Length == 0)
            {
                await DisplayAlert("Info", "No saved games found.", "OK");
                return;
            }

            // Show list of saves
            var saveNames = saveFiles.Select(Path.GetFileNameWithoutExtension).ToArray();
            var choice = await DisplayActionSheet(
                "Load Game",
                "Cancel",
                null,
                saveNames);

            if (choice == null || choice == "Cancel")
                return;

            // Load selected save
            var saveFilePath = Path.Combine(savesPath, $"{choice}.sav");
            var json = await File.ReadAllTextAsync(saveFilePath);
            var savedState = JsonSerializer.Deserialize<GameState>(json);

            if (savedState != null)
            {
                _engine.RestoreGame(savedState);

                // Clear and re-display
                OutputLabel.Text = "";
                AppendOutput($"Game loaded from '{choice}'\n\n");

                // Show current location
                _engine.ProcessCommand("look");
                AppendOutput(_engine.State.GetAndClearOutput());
                AppendOutput("\n> ");

                CommandEntry.IsEnabled = true;
                await DisplayAlert("Success", "Game loaded successfully.", "OK");
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load game: {ex.Message}", "OK");
        }
    }

    private async void OnRestartGame(object? sender, EventArgs e)
    {
        if (_currentAdventure == null)
        {
            await DisplayAlert("Error", "Cannot restart game.", "OK");
            return;
        }

        var result = await DisplayAlert(
            "Restart Game",
            "Are you sure you want to restart? All progress will be lost.",
            "Restart", "Cancel");

        if (result)
        {
            // Clear output
            OutputLabel.Text = "";

            // Restart the game
            StartAdventure(_currentAdventure);
        }
    }
}
