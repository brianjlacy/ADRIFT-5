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
    private HintManager? _hintManager;

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
        _hintManager = new HintManager(adventure, _engine.State);

        // Display introduction
        var intro = _engine.GetIntroduction();
        AppendOutput(intro);
        AppendOutput("\n> ");

        // Update status bar
        UpdateStatusBar();

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

        // Update status bar
        UpdateStatusBar();

        // Clear input
        CommandEntry.Text = string.Empty;

        // Scroll to bottom
        MainThread.BeginInvokeOnMainThread(async () =>
        {
            await Task.Delay(100);
            await OutputScroll.ScrollToAsync(0, OutputLabel.Height, false);
        });
    }

    private void UpdateStatusBar()
    {
        if (_engine == null)
            return;

        ScoreLabel.Text = $"Score: {_engine.State.Score}";
        TurnsLabel.Text = $"Turns: {_engine.State.TurnCount}";

        var location = _engine.State.GetCurrentLocation();
        if (location != null)
        {
            LocationLabel.Text = location.ShortDescription;
        }
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

            // Reset hint tracking
            _hintManager?.Reset();
        }
    }

    private async void OnShowHints(object? sender, EventArgs e)
    {
        if (_engine == null || _hintManager == null)
        {
            await DisplayAlert("Error", "No game in progress.", "OK");
            return;
        }

        try
        {
            var availableHints = _hintManager.GetAvailableHints();

            if (availableHints.Count == 0)
            {
                await DisplayAlert("Hints", "No hints are currently available.", "OK");
                return;
            }

            // Show list of hint questions
            var hintQuestions = availableHints.Select(h => h.Question).ToArray();
            var choice = await DisplayActionSheet(
                "Select a hint:",
                "Cancel",
                null,
                hintQuestions);

            if (choice == null || choice == "Cancel")
                return;

            // Find the selected hint
            var selectedHint = availableHints.FirstOrDefault(h => h.Question == choice);
            if (selectedHint == null)
                return;

            // Show hint levels progressively
            await ShowHintProgression(selectedHint);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to display hints: {ex.Message}", "OK");
        }
    }

    private async Task ShowHintProgression(Hint hint)
    {
        if (_hintManager == null)
            return;

        var currentLevel = _hintManager.GetCurrentLevel(hint.Key);

        // Determine what to show next
        string message;
        string[] buttons;

        if (currentLevel == 0)
        {
            // Show subtle hint with option to see more
            message = $"[Subtle Hint]\n{hint.SubtleHint}";
            buttons = new[] { "Show Medium Hint", "That's Enough" };
        }
        else if (currentLevel == 1)
        {
            // Show medium hint with option to see spoiler
            message = $"[Medium Hint]\n{hint.MediumHint}";
            buttons = new[] { "Show Spoiler", "That's Enough" };
        }
        else if (currentLevel == 2)
        {
            // Show spoiler
            message = $"[Spoiler]\n{hint.SpoilerHint}";
            buttons = new[] { "OK" };
        }
        else
        {
            // All hints shown
            message = $"[All Hints Shown]\n\nSubtle: {hint.SubtleHint}\n\nMedium: {hint.MediumHint}\n\nSpoiler: {hint.SpoilerHint}";
            buttons = new[] { "OK" };
        }

        var choice = await DisplayActionSheet(
            hint.Question,
            null,
            null,
            buttons);

        if (choice == "Show Medium Hint" || choice == "Show Spoiler")
        {
            // Advance to next level
            _hintManager.GetNextHintLevel(hint.Key);

            // Show next level
            await ShowHintProgression(hint);
        }
    }

    private void OnHistoryUp(object? sender, EventArgs e)
    {
        if (_commandHistory.Count == 0)
            return;

        if (_historyIndex > 0)
        {
            _historyIndex--;
            CommandEntry.Text = _commandHistory[_historyIndex];
            CommandEntry.CursorPosition = CommandEntry.Text.Length;
        }
    }

    private void OnHistoryDown(object? sender, EventArgs e)
    {
        if (_commandHistory.Count == 0)
            return;

        if (_historyIndex < _commandHistory.Count - 1)
        {
            _historyIndex++;
            CommandEntry.Text = _commandHistory[_historyIndex];
            CommandEntry.CursorPosition = CommandEntry.Text.Length;
        }
        else if (_historyIndex == _commandHistory.Count - 1)
        {
            // Go past last history item = clear entry
            _historyIndex = _commandHistory.Count;
            CommandEntry.Text = "";
        }
    }

    private async void OnExportTranscript(object? sender, EventArgs e)
    {
        if (_engine == null || string.IsNullOrEmpty(OutputLabel.Text))
        {
            await DisplayAlert("Error", "No transcript to export.", "OK");
            return;
        }

        try
        {
            // Prompt for filename
            var filename = await DisplayPromptAsync(
                "Export Transcript",
                "Enter filename (without extension):",
                initialValue: $"Transcript_{DateTime.Now:yyyyMMdd_HHmmss}");

            if (string.IsNullOrWhiteSpace(filename))
                return;

            // Get documents directory
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var adriftPath = Path.Combine(documentsPath, "ADRIFT Transcripts");

            if (!Directory.Exists(adriftPath))
                Directory.CreateDirectory(adriftPath);

            var filePath = Path.Combine(adriftPath, $"{filename}.txt");

            // Create transcript with metadata
            var transcript = $"ADRIFT 5 Runner - Game Transcript\n";
            transcript += $"Adventure: {_currentAdventure?.Title ?? "Unknown"}\n";
            transcript += $"Author: {_currentAdventure?.Author ?? "Unknown"}\n";
            transcript += $"Date: {DateTime.Now:yyyy-MM-dd HH:mm:ss}\n";
            transcript += $"Final Score: {_engine.State.Score}\n";
            transcript += $"Turns Taken: {_engine.State.TurnCount}\n";
            transcript += $"\n{new string('=', 60)}\n\n";
            transcript += OutputLabel.Text;

            await File.WriteAllTextAsync(filePath, transcript);

            await DisplayAlert("Success",
                $"Transcript exported to:\n{filePath}",
                "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to export transcript: {ex.Message}", "OK");
        }
    }
}
