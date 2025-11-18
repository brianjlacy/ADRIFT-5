using ADRIFT.Core.Models;
using ADRIFT.Engine.Expressions;
using ADRIFT.Engine.Parser;

namespace ADRIFT.Engine.GameEngine;

/// <summary>
/// Main ADRIFT game engine
/// Handles game loop, command processing, and state management
/// </summary>
public class AdriftEngine
{
    public GameState State { get; private set; }
    private readonly CommandParser _parser;
    private readonly ExpressionEvaluator _evaluator;

    public event EventHandler<string>? OutputGenerated;
    public event EventHandler<GameState>? StateChanged;

    public AdriftEngine(Adventure adventure)
    {
        State = new GameState(adventure);
        _parser = new CommandParser(adventure);
        _evaluator = new ExpressionEvaluator(adventure);
    }

    /// <summary>
    /// Starts a new game
    /// </summary>
    public string StartGame()
    {
        State.AddOutput(State.Adventure.IntroductionText);

        // Display starting location
        DisplayCurrentLocation();

        return State.GetOutput();
    }

    /// <summary>
    /// Processes a player command
    /// </summary>
    public string ProcessCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return string.Empty;

        State.Turns++;

        // Try to parse as a special command first
        if (ProcessSpecialCommand(command))
        {
            return State.GetOutput();
        }

        // Parse the command
        var matches = _parser.ParseCommand(command);

        if (matches.Count == 0)
        {
            State.AddOutput("I don't understand that command.");
            return State.GetOutput();
        }

        // Execute the best matching task
        var bestMatch = matches[0];
        ExecuteTask(bestMatch.Task, bestMatch.Command);

        // Process any triggered events
        ProcessEvents();

        return State.GetOutput();
    }

    /// <summary>
    /// Processes special commands (look, inventory, quit, etc.)
    /// </summary>
    private bool ProcessSpecialCommand(string command)
    {
        command = command.Trim().ToLowerInvariant();

        switch (command)
        {
            case "look":
            case "l":
                DisplayCurrentLocation();
                return true;

            case "inventory":
            case "inv":
            case "i":
                DisplayInventory();
                return true;

            case "score":
                State.AddOutput($"You have scored {State.Score} out of a possible {State.Adventure.MaxScore} points.");
                return true;

            case "help":
                DisplayHelp();
                return true;

            case "quit":
            case "q":
                State.AddOutput("Thanks for playing!");
                return true;

            default:
                // Check for movement commands
                return ProcessMovementCommand(command);
        }
    }

    /// <summary>
    /// Processes movement commands (north, south, n, s, etc.)
    /// </summary>
    private bool ProcessMovementCommand(string command)
    {
        var location = State.GetCurrentLocation();
        if (location == null)
            return false;

        // Map command to direction
        var direction = command switch
        {
            "n" or "north" => "North",
            "s" or "south" => "South",
            "e" or "east" => "East",
            "w" or "west" => "West",
            "ne" or "northeast" => "NorthEast",
            "nw" or "northwest" => "NorthWest",
            "se" or "southeast" => "SouthEast",
            "sw" or "southwest" => "SouthWest",
            "u" or "up" => "Up",
            "d" or "down" => "Down",
            "in" => "In",
            "out" => "Out",
            _ => null
        };

        if (direction == null)
            return false;

        // Find the exit in that direction
        var exit = location.Directions.FirstOrDefault(d =>
            d.DirectionName.Equals(direction, StringComparison.OrdinalIgnoreCase));

        if (exit != null)
        {
            // Check restrictions
            // TODO: Evaluate restrictions

            // Move to the new location
            if (State.MoveToLocation(exit.DestinationKey))
            {
                DisplayCurrentLocation();
                return true;
            }
        }

        State.AddOutput($"You can't go {direction.ToLowerInvariant()} from here.");
        return true;
    }

    /// <summary>
    /// Executes a task
    /// </summary>
    private void ExecuteTask(ADRIFT.Core.Models.Task task, string command)
    {
        // Check if task is repeatable
        if (!task.IsRepeatable && State.IsTaskCompleted(task.Key))
        {
            State.AddOutput("You've already done that.");
            return;
        }

        // TODO: Check task restrictions

        // Execute task actions
        // For now, just display the success message
        if (!string.IsNullOrWhiteSpace(task.SuccessMessage))
        {
            State.AddOutput(task.SuccessMessage);
        }
        else
        {
            State.AddOutput("Done.");
        }

        // Mark as completed
        State.CompleteTask(task.Key);

        // Add score
        if (task.ScoreValue > 0)
        {
            State.Score += task.ScoreValue;
            State.AddOutput($"[Your score has increased by {task.ScoreValue} points.]");
        }

        // TODO: Execute task actions (move objects, change variables, etc.)

        StateChanged?.Invoke(this, State);
    }

    /// <summary>
    /// Displays the current location
    /// </summary>
    private void DisplayCurrentLocation()
    {
        var location = State.GetCurrentLocation();
        if (location == null)
        {
            State.AddOutput("You are nowhere.");
            return;
        }

        // Display location name
        State.AddOutput($"**{location.ShortDescription}**");

        // Display long description
        if (!string.IsNullOrWhiteSpace(location.LongDescription))
        {
            State.AddOutput(location.LongDescription);
        }

        // Display objects
        var objects = State.GetObjectsAtLocation(location.Key);
        if (objects.Count > 0)
        {
            var objectNames = objects.Select(o => $"a {o.Name}").ToList();
            State.AddOutput($"You can see {FormatList(objectNames)} here.");
        }

        // Display characters
        var characters = State.GetCharactersAtLocation(location.Key);
        if (characters.Count > 0)
        {
            var characterNames = characters.Select(c => c.Name).ToList();
            State.AddOutput($"{FormatList(characterNames, capitalize: true)} {(characters.Count == 1 ? "is" : "are")} here.");
        }

        // Display visible exits
        if (location.Directions.Count > 0)
        {
            var exits = location.Directions.Select(d => d.DirectionName.ToLowerInvariant()).ToList();
            State.AddOutput($"Obvious exits: {FormatList(exits, useOr: false)}.");
        }
    }

    /// <summary>
    /// Displays inventory
    /// </summary>
    private void DisplayInventory()
    {
        // TODO: Implement inventory tracking
        State.AddOutput("You are not carrying anything.");
    }

    /// <summary>
    /// Displays help text
    /// </summary>
    private void DisplayHelp()
    {
        State.AddOutput(@"Available commands:
- Movement: north, south, east, west, up, down, etc. (or n, s, e, w, u, d)
- look (l) - Look around
- inventory (i) - Check your inventory
- examine <object> - Examine an object
- take <object> - Take an object
- drop <object> - Drop an object
- score - Check your score
- help - Display this help
- quit (q) - Quit the game

You can also try standard adventure game commands like 'open door', 'talk to guard', etc.");
    }

    /// <summary>
    /// Processes events (time-based, triggered, etc.)
    /// </summary>
    private void ProcessEvents()
    {
        // TODO: Implement event processing
        // Check for time-based events
        // Check for condition-triggered events
    }

    /// <summary>
    /// Formats a list of items with proper grammar
    /// </summary>
    private string FormatList(List<string> items, bool useOr = false, bool capitalize = false)
    {
        if (items.Count == 0)
            return string.Empty;

        if (items.Count == 1)
        {
            var item = items[0];
            return capitalize ? char.ToUpper(item[0]) + item.Substring(1) : item;
        }

        var separator = useOr ? " or " : " and ";
        var last = items[^1];
        var others = string.Join(", ", items.Take(items.Count - 1));
        var result = $"{others}{separator}{last}";

        return capitalize ? char.ToUpper(result[0]) + result.Substring(1) : result;
    }

    /// <summary>
    /// Saves the current game state
    /// </summary>
    public GameState SaveGame()
    {
        return State.Clone();
    }

    /// <summary>
    /// Restores a saved game state
    /// </summary>
    public void RestoreGame(GameState savedState)
    {
        State = savedState;
        State.AddOutput("Game restored.");
        DisplayCurrentLocation();
        StateChanged?.Invoke(this, State);
    }
}
