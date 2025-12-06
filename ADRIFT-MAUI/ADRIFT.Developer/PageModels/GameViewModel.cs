using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class GameViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    [ObservableProperty]
    private string currentCommand = "";

    [ObservableProperty]
    private string currentLocation = "Unknown";

    [ObservableProperty]
    private int currentScore = 0;

    [ObservableProperty]
    private int currentTurn = 0;

    [ObservableProperty]
    private string statusMessage = "Ready to play";

    [ObservableProperty]
    private bool isGameRunning;

    public ObservableCollection<GameOutputLine> OutputLines { get; } = new();
    public ObservableCollection<string> CommandHistory { get; } = new();

    private int _historyIndex = -1;

    public GameViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
    }

    [RelayCommand]
    private void StartNewGame()
    {
        OutputLines.Clear();
        CommandHistory.Clear();
        CurrentTurn = 0;
        CurrentScore = 0;
        IsGameRunning = true;

        var adventure = _adventureService.CurrentAdventure;
        if (adventure != null)
        {
            AddOutput($"Welcome to {adventure.Title}", OutputLineType.Title);
            AddOutput($"by {adventure.Author}", OutputLineType.Normal);
            AddOutput("", OutputLineType.Normal);
            AddOutput("Type 'help' for commands, 'quit' to exit.", OutputLineType.Normal);
            AddOutput("", OutputLineType.Normal);

            // Show initial location
            ShowCurrentLocation();
        }
        else
        {
            AddOutput("No adventure loaded.", OutputLineType.Error);
        }
    }

    [RelayCommand]
    private void ProcessCommand()
    {
        if (string.IsNullOrWhiteSpace(CurrentCommand))
            return;

        var command = CurrentCommand.Trim();
        AddOutput($"> {command}", OutputLineType.Command);

        // Add to history
        CommandHistory.Insert(0, command);
        if (CommandHistory.Count > 50)
        {
            CommandHistory.RemoveAt(CommandHistory.Count - 1);
        }
        _historyIndex = -1;

        // Process the command
        ProcessGameCommand(command);

        CurrentCommand = "";
    }

    private void ProcessGameCommand(string command)
    {
        var lowerCommand = command.ToLower();

        // Built-in commands
        switch (lowerCommand)
        {
            case "help":
            case "?":
                ShowHelp();
                break;

            case "look":
            case "l":
                ShowCurrentLocation();
                break;

            case "inventory":
            case "i":
                ShowInventory();
                break;

            case "score":
                AddOutput($"Your score is {CurrentScore}.", OutputLineType.Normal);
                break;

            case "quit":
            case "exit":
                QuitGame();
                break;

            default:
                // Try to parse as game command
                ParseAdventureCommand(command);
                break;
        }

        CurrentTurn++;
    }

    private void ShowHelp()
    {
        AddOutput("Available commands:", OutputLineType.Normal);
        AddOutput("  LOOK (L) - Look around", OutputLineType.Normal);
        AddOutput("  INVENTORY (I) - Check inventory", OutputLineType.Normal);
        AddOutput("  SCORE - Check your score", OutputLineType.Normal);
        AddOutput("  HELP (?) - Show this help", OutputLineType.Normal);
        AddOutput("  QUIT - Exit the game", OutputLineType.Normal);
        AddOutput("", OutputLineType.Normal);
        AddOutput("Type any command to interact with the adventure.", OutputLineType.Normal);
    }

    private void ShowCurrentLocation()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null || adventure.Locations.Count == 0)
        {
            AddOutput("No location available.", OutputLineType.Error);
            return;
        }

        // Get first location for now (would need player position tracking)
        var location = adventure.Locations.Values.FirstOrDefault();
        if (location != null)
        {
            CurrentLocation = location.ShortDescription;
            AddOutput(location.ShortDescription, OutputLineType.LocationName);
            AddOutput(location.LongDescription.GetText(), OutputLineType.Normal);
            AddOutput("", OutputLineType.Normal);

            // Show exits
            var exits = location.Exits.Where(e => e.Exists).ToList();
            if (exits.Any())
            {
                var exitText = "Exits: " + string.Join(", ", exits.Select(e => e.Direction.ToString()));
                AddOutput(exitText, OutputLineType.Normal);
            }
        }
    }

    private void ShowInventory()
    {
        AddOutput("You are carrying nothing.", OutputLineType.Normal);
        AddOutput("", OutputLineType.Normal);
    }

    private void ParseAdventureCommand(string command)
    {
        // Placeholder for actual adventure command parsing
        AddOutput("I don't understand that command.", OutputLineType.Error);
    }

    private void QuitGame()
    {
        AddOutput("Thanks for playing!", OutputLineType.Normal);
        IsGameRunning = false;
    }

    public void AddOutput(string text, OutputLineType type = OutputLineType.Normal)
    {
        OutputLines.Add(new GameOutputLine
        {
            Text = text,
            Type = type,
            Timestamp = DateTime.Now
        });
    }

    [RelayCommand]
    private void PreviousCommand()
    {
        if (CommandHistory.Count == 0) return;

        _historyIndex++;
        if (_historyIndex >= CommandHistory.Count)
        {
            _historyIndex = CommandHistory.Count - 1;
        }

        CurrentCommand = CommandHistory[_historyIndex];
    }

    [RelayCommand]
    private void NextCommand()
    {
        if (CommandHistory.Count == 0) return;

        _historyIndex--;
        if (_historyIndex < 0)
        {
            _historyIndex = -1;
            CurrentCommand = "";
        }
        else
        {
            CurrentCommand = CommandHistory[_historyIndex];
        }
    }
}

public partial class GameOutputLine : ObservableObject
{
    [ObservableProperty]
    private string text = "";

    [ObservableProperty]
    private OutputLineType type = OutputLineType.Normal;

    [ObservableProperty]
    private DateTime timestamp = DateTime.Now;

    public Color TextColor => Type switch
    {
        OutputLineType.Command => Colors.Blue,
        OutputLineType.Error => Colors.Red,
        OutputLineType.Title => Colors.DarkGreen,
        OutputLineType.LocationName => Colors.DarkBlue,
        OutputLineType.System => Colors.Gray,
        _ => Colors.Black
    };

    public FontAttributes FontAttribute => Type switch
    {
        OutputLineType.Title => FontAttributes.Bold,
        OutputLineType.LocationName => FontAttributes.Bold,
        OutputLineType.Command => FontAttributes.Italic,
        _ => FontAttributes.None
    };
}

public enum OutputLineType
{
    Normal,
    Command,
    Error,
    Title,
    LocationName,
    System
}
