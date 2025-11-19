using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Main game engine for running ADRIFT adventures
/// </summary>
public class GameEngine
{
    private readonly Adventure _adventure;
    private readonly CommandParser _parser;
    private GameState _state = null!;

    public GameState State => _state;
    public Adventure Adventure => _adventure;

    public GameEngine(Adventure adventure)
    {
        _adventure = adventure;

        // Add system tasks if not already present
        SystemTaskGenerator.AddSystemTasks(_adventure);

        _parser = new CommandParser(adventure);
        Initialize();
    }

    /// <summary>
    /// Initialize or restart the game
    /// </summary>
    public void Initialize()
    {
        _state = GameState.Initialize(_adventure);
    }

    /// <summary>
    /// Process a player command and return the response
    /// </summary>
    public string ProcessCommand(string input)
    {
        if (string.IsNullOrWhiteSpace(input))
            return "Please enter a command.";

        _state.LastCommand = input;
        _state.OutputBuffer.Clear();

        // Special commands
        if (HandleSpecialCommands(input))
        {
            return _state.GetAndClearOutput();
        }

        // Parse the command
        var matches = _parser.ParseCommand(input, _state);

        if (matches.Count == 0)
        {
            _state.AddOutput("I don't understand that command.");
            return _state.GetAndClearOutput();
        }

        // Execute the best matching task
        var bestMatch = matches.First();
        ExecuteTask(bestMatch);

        // Increment turn counter
        _state.TurnCount++;

        // Process character movements (walk routes and following)
        ProcessCharacterMovement();

        // Process turn-based events
        ProcessTurnBasedEvents();

        // Check triggered events
        ProcessTriggeredEvents();

        // Check win/lose conditions
        CheckGameEnd();

        return _state.GetAndClearOutput();
    }

    private bool HandleSpecialCommands(string input)
    {
        var command = input.Trim().ToLower();

        switch (command)
        {
            case "look":
            case "l":
                DescribeLocation();
                return true;

            case "inventory":
            case "inv":
            case "i":
                ShowInventory();
                return true;

            case "score":
                ShowScore();
                return true;

            case "help":
                ShowHelp();
                return true;

            case "quit":
            case "q":
                _state.AddOutput("Thanks for playing!");
                return true;

            default:
                // Check for directional movement
                if (TryMove(command))
                    return true;
                return false;
        }
    }

    private bool TryMove(string direction)
    {
        var directionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "n", "North" }, { "north", "North" },
            { "s", "South" }, { "south", "South" },
            { "e", "East" }, { "east", "East" },
            { "w", "West" }, { "west", "West" },
            { "ne", "Northeast" }, { "northeast", "Northeast" },
            { "nw", "Northwest" }, { "northwest", "Northwest" },
            { "se", "Southeast" }, { "southeast", "Southeast" },
            { "sw", "Southwest" }, { "southwest", "Southwest" },
            { "u", "Up" }, { "up", "Up" },
            { "d", "Down" }, { "down", "Down" },
            { "in", "In" }, { "out", "Out" }
        };

        if (!directionMap.TryGetValue(direction, out var normalizedDirection))
            return false;

        var currentLocation = _state.GetCurrentLocation();
        if (currentLocation == null)
            return false;

        var directionExit = currentLocation.Directions.FirstOrDefault(d =>
            d.DirectionName.Equals(normalizedDirection, StringComparison.OrdinalIgnoreCase));

        if (directionExit == null)
        {
            _state.AddOutput("You can't go that way.");
            return true;
        }

        // Check restrictions
        if (directionExit.HasRestriction && !string.IsNullOrEmpty(directionExit.RestrictionDescription))
        {
            _state.AddOutput(directionExit.RestrictionDescription);
            return true;
        }

        // Move to new location
        _state.CurrentLocationKey = directionExit.DestinationKey;
        DescribeLocation();
        return true;
    }

    private void DescribeLocation()
    {
        var location = _state.GetCurrentLocation();
        if (location == null)
        {
            _state.AddOutput("You are nowhere.");
            return;
        }

        _state.AddOutput($"**{location.ShortDescription}**");
        _state.AddOutput(location.LongDescription);

        // List objects
        var objects = _state.GetObjectsAtCurrentLocation();
        if (objects.Count > 0)
        {
            var objectList = string.Join(", ", objects.Select(o => o.FullName));
            _state.AddOutput($"\nYou can see: {objectList}");
        }

        // List characters
        var characters = _state.GetCharactersAtCurrentLocation();
        if (characters.Count > 0)
        {
            foreach (var character in characters)
            {
                _state.AddOutput($"\n{character.FullName} is here. {character.Description}");
            }
        }

        // List exits
        if (location.Directions.Count > 0)
        {
            var exits = string.Join(", ", location.Directions.Select(d => d.DirectionName.ToLower()));
            _state.AddOutput($"\nObvious exits: {exits}");
        }
    }

    private void ShowInventory()
    {
        if (_state.Inventory.Count == 0)
        {
            _state.AddOutput("You are not carrying anything.");
            return;
        }

        _state.AddOutput("You are carrying:");
        foreach (var obj in _state.Inventory)
        {
            _state.AddOutput($"  - {obj.FullName}");
        }
    }

    private void ShowScore()
    {
        _state.AddOutput($"Score: {_state.Score} out of {_adventure.MaxScore}");
        _state.AddOutput($"Turns: {_state.TurnCount}");
    }

    private void ShowHelp()
    {
        _state.AddOutput("Available commands:");
        _state.AddOutput("  LOOK (L) - Look around");
        _state.AddOutput("  INVENTORY (I) - Check what you're carrying");
        _state.AddOutput("  SCORE - Check your score");
        _state.AddOutput("  HELP - Show this help");
        _state.AddOutput("  QUIT (Q) - Quit the game");
        _state.AddOutput("\nMovement: N, S, E, W, NE, NW, SE, SW, U, D, IN, OUT");
        _state.AddOutput("\nYou can also try commands like:");
        _state.AddOutput("  TAKE <object>, DROP <object>, EXAMINE <object>");
        _state.AddOutput("  OPEN <object>, CLOSE <object>, USE <object>");
        _state.AddOutput("  TALK TO <character>, GIVE <object> TO <character>");
    }

    private void ExecuteTask(TaskMatch match)
    {
        var task = match.Task;

        // Check if task is already completed and not repeatable
        if (_state.IsTaskCompleted(task.Key) && !task.IsRepeatable)
        {
            _state.AddOutput("You've already done that.");
            return;
        }

        // Check restrictions
        if (!EvaluateRestrictions(task, match.Parameters))
        {
            // Output failure message if available
            if (!string.IsNullOrEmpty(task.FailureMessage))
            {
                _state.AddOutput(FormatText(task.FailureMessage, match.Parameters));
            }
            else
            {
                _state.AddOutput("You can't do that right now.");
            }
            return;
        }

        // Execute success actions
        ExecuteActions(task.SuccessActions, match.Parameters);

        // Update pronoun references for parser
        _parser.UpdateLastReferences(match.Parameters);

        // Output completion text
        if (!string.IsNullOrEmpty(task.CompletionText))
        {
            _state.AddOutput(FormatText(task.CompletionText, match.Parameters));
        }
        else if (!string.IsNullOrEmpty(task.SuccessMessage))
        {
            _state.AddOutput(FormatText(task.SuccessMessage, match.Parameters));
        }
        else
        {
            _state.AddOutput("Done.");
        }

        // Award score
        if (task.ScoreValue > 0)
        {
            _state.Score += task.ScoreValue;
        }

        // Mark as completed
        if (!task.IsRepeatable)
        {
            _state.CompleteTask(task.Key);
        }
    }

    private bool EvaluateRestrictions(Core.Models.Task task, Dictionary<string, string> parameters)
    {
        var evaluator = new RestrictionEvaluator(_adventure, _state);
        return evaluator.EvaluateTaskRestrictions(task, parameters);
    }

    private void ExecuteActions(List<TaskAction> actions, Dictionary<string, string> parameters)
    {
        foreach (var action in actions.OrderBy(a => a.Order))
        {
            ExecuteAction(action, parameters);
        }
    }

    private void ExecuteAction(TaskAction action, Dictionary<string, string> parameters)
    {
        switch (action.ActionType.ToLower())
        {
            case "moveobject":
                if (action.Parameters.TryGetValue("Object", out var objectKey))
                {
                    var resolvedObjectKey = ResolveParameter(objectKey, parameters);
                    var destination = action.Parameters.GetValueOrDefault("Destination", "");

                    if (destination.Equals("Inventory", StringComparison.OrdinalIgnoreCase))
                    {
                        _state.AddToInventory(resolvedObjectKey);
                    }
                    else if (destination.Equals("CurrentLocation", StringComparison.OrdinalIgnoreCase))
                    {
                        _state.MoveObject(resolvedObjectKey, _state.CurrentLocationKey);
                    }
                    else if (destination.StartsWith("%") && destination.EndsWith("%"))
                    {
                        // Resolve parameter (e.g., %character%)
                        var resolvedDest = ResolveParameter(destination, parameters);
                        _state.MoveObject(resolvedObjectKey, resolvedDest);
                    }
                    else
                    {
                        _state.MoveObject(resolvedObjectKey, destination);
                    }
                }
                break;

            case "setobjectproperty":
                // Set a property on an object (e.g., IsOpen, IsLocked)
                if (action.Parameters.TryGetValue("Object", out var objKey) &&
                    action.Parameters.TryGetValue("Property", out var property) &&
                    action.Parameters.TryGetValue("Value", out var value))
                {
                    var stateKey = $"{objKey}.{property}";
                    _state.ObjectStates[stateKey] = value.Equals("true", StringComparison.OrdinalIgnoreCase);
                }
                break;

            case "setvariable":
                if (action.Parameters.TryGetValue("Variable", out var varKey) &&
                    action.Parameters.TryGetValue("Value", out var varValue))
                {
                    _state.SetVariableValue(varKey, varValue);
                }
                break;

            case "addscore":
                if (action.Parameters.TryGetValue("Amount", out var amount) &&
                    int.TryParse(amount, out var scoreAmount))
                {
                    _state.Score += scoreAmount;
                }
                break;

            case "outputtext":
                if (action.Parameters.TryGetValue("Text", out var text))
                {
                    _state.AddOutput(FormatText(text, parameters));
                }
                break;

            case "moveplayer":
                if (action.Parameters.TryGetValue("Location", out var locationKey))
                {
                    _state.CurrentLocationKey = locationKey;
                }
                break;

            case "removeobject":
                // Remove an object from the game (e.g., consumed, destroyed)
                if (action.Parameters.TryGetValue("Object", out var removeObjKey))
                {
                    var resolvedKey = ResolveParameter(removeObjKey, parameters);
                    _state.RemoveFromInventory(resolvedKey);
                    _state.ObjectLocations.Remove(resolvedKey);
                }
                break;

            case "showconversation":
                // Show character conversation/greeting
                if (action.Parameters.TryGetValue("Character", out var charKey))
                {
                    var resolvedCharKey = ResolveParameter(charKey, parameters);
                    var conversationMgr = new ConversationManager(_adventure, _state);
                    var response = conversationMgr.ProcessConversation(resolvedCharKey);
                    _state.AddOutput(response);
                }
                break;

            case "askabout":
                // Ask character about a topic
                if (action.Parameters.TryGetValue("Character", out var askCharKey) &&
                    action.Parameters.TryGetValue("Topic", out var askTopic))
                {
                    var resolvedCharKey = ResolveParameter(askCharKey, parameters);
                    var resolvedTopic = ResolveParameter(askTopic, parameters);
                    var conversationMgr = new ConversationManager(_adventure, _state);
                    var response = conversationMgr.AskAbout(resolvedCharKey, resolvedTopic);
                    _state.AddOutput(response);
                }
                break;

            case "tellabout":
                // Tell character about a topic
                if (action.Parameters.TryGetValue("Character", out var tellCharKey) &&
                    action.Parameters.TryGetValue("Topic", out var tellTopic))
                {
                    var resolvedCharKey = ResolveParameter(tellCharKey, parameters);
                    var resolvedTopic = ResolveParameter(tellTopic, parameters);
                    var conversationMgr = new ConversationManager(_adventure, _state);
                    var response = conversationMgr.TellAbout(resolvedCharKey, resolvedTopic);
                    _state.AddOutput(response);
                }
                break;

            case "removerestriction":
                // Remove a restriction from a direction (for secret passages, etc.)
                if (action.Parameters.TryGetValue("Location", out var locKey) &&
                    action.Parameters.TryGetValue("Direction", out var directionName))
                {
                    if (_adventure.Locations.TryGetValue(locKey, out var location))
                    {
                        var direction = location.Directions.FirstOrDefault(d =>
                            d.DirectionName.Equals(directionName, StringComparison.OrdinalIgnoreCase));
                        if (direction != null)
                        {
                            direction.RestrictionDescription = null;
                        }
                    }
                }
                break;

            case "endgame":
                // End the game with win/lose/neutral status
                var endType = action.Parameters.GetValueOrDefault("Type", "win");
                switch (endType.ToLower())
                {
                    case "win":
                        _state.GameWon = true;
                        var winText = action.Parameters.GetValueOrDefault("Text", "");
                        if (!string.IsNullOrEmpty(winText))
                        {
                            _state.AddOutput(FormatText(winText, parameters));
                        }
                        else if (!string.IsNullOrEmpty(_adventure.WinText.Text))
                        {
                            _state.AddOutput(FormatText(_adventure.WinText.Text, parameters));
                        }
                        else
                        {
                            _state.AddOutput("\n**Congratulations! You have won the game!**");
                        }
                        break;

                    case "lose":
                        _state.GameLost = true;
                        var loseText = action.Parameters.GetValueOrDefault("Text", "");
                        if (!string.IsNullOrEmpty(loseText))
                        {
                            _state.AddOutput(FormatText(loseText, parameters));
                        }
                        else if (!string.IsNullOrEmpty(_adventure.LoseText.Text))
                        {
                            _state.AddOutput(FormatText(_adventure.LoseText.Text, parameters));
                        }
                        else
                        {
                            _state.AddOutput("\n**Game Over. You have lost.**");
                        }
                        break;

                    case "neutral":
                    default:
                        _state.GameEnded = true;
                        var neutralText = action.Parameters.GetValueOrDefault("Text", "");
                        if (!string.IsNullOrEmpty(neutralText))
                        {
                            _state.AddOutput(FormatText(neutralText, parameters));
                        }
                        else
                        {
                            _state.AddOutput("\n**The game has ended.**");
                        }
                        break;
                }
                break;

            case "movecharacter":
                // Move a character to a location
                if (action.Parameters.TryGetValue("Character", out var moveCharKey) &&
                    action.Parameters.TryGetValue("Location", out var moveCharLoc))
                {
                    var resolvedCharKey = ResolveParameter(moveCharKey, parameters);
                    var resolvedLoc = ResolveParameter(moveCharLoc, parameters);
                    _state.MoveCharacter(resolvedCharKey, resolvedLoc);
                }
                break;

            case "executetask":
                // Execute another task
                if (action.Parameters.TryGetValue("Task", out var execTaskKey))
                {
                    var resolvedTaskKey = ResolveParameter(execTaskKey, parameters);
                    var task = _adventure.Tasks.GetValueOrDefault(resolvedTaskKey);
                    if (task != null)
                    {
                        ExecuteTask(task, new CommandMatch { Parameters = parameters });
                    }
                }
                break;

            case "setproperty":
                // Set a custom property value
                if (action.Parameters.TryGetValue("Property", out var propKey) &&
                    action.Parameters.TryGetValue("Value", out var propValue))
                {
                    var resolvedValue = ResolveParameter(propValue, parameters);
                    _state.SetPropertyValue(propKey, resolvedValue);
                }
                break;

            case "time":
                // Advance game time
                if (action.Parameters.TryGetValue("Minutes", out var minutes) &&
                    int.TryParse(minutes, out var minutesToAdd))
                {
                    _state.TimeElapsed += minutesToAdd;
                }
                break;
        }
    }

    private void ProcessCharacterMovement()
    {
        var movementManager = new CharacterMovementManager(_adventure, _state);
        movementManager.ProcessTurn();
    }

    private void ProcessTurnBasedEvents()
    {
        foreach (var evt in _adventure.Events.Values)
        {
            if (evt.Type != EventType.TimeBased)
                continue;

            if (_state.TriggeredEvents.GetValueOrDefault(evt.Key, false))
                continue;

            // Increment turn counter
            if (!_state.EventTurnCounters.ContainsKey(evt.Key))
                _state.EventTurnCounters[evt.Key] = 0;

            _state.EventTurnCounters[evt.Key]++;

            // Check if delay has elapsed
            if (evt.Trigger == TriggerType.AfterTime && _state.EventTurnCounters[evt.Key] >= evt.DelayTurns)
            {
                TriggerEvent(evt);

                // Check if it repeats
                if (evt.RepeatTurns > 0)
                {
                    _state.EventTurnCounters[evt.Key] = 0; // Reset counter
                }
                else
                {
                    _state.TriggeredEvents[evt.Key] = true; // Mark as triggered
                }
            }
        }
    }

    private void ProcessTriggeredEvents()
    {
        foreach (var evt in _adventure.Events.Values)
        {
            if (evt.Type != EventType.Triggered)
                continue;

            if (_state.TriggeredEvents.GetValueOrDefault(evt.Key, false))
                continue;

            // Check if conditions are met
            if (EvaluateEventConditions(evt))
            {
                TriggerEvent(evt);
                _state.TriggeredEvents[evt.Key] = true;
            }
        }
    }

    private bool EvaluateEventConditions(Event evt)
    {
        var evaluator = new RestrictionEvaluator(_adventure, _state);
        return evaluator.EvaluateEventConditions(evt);
    }

    private void TriggerEvent(Event evt)
    {
        // Output event text
        if (!string.IsNullOrEmpty(evt.OutputText))
        {
            _state.AddOutput(evt.OutputText);
        }

        // Execute event actions
        foreach (var action in evt.Actions.OrderBy(a => a.Order))
        {
            ExecuteAction(new TaskAction
            {
                ActionType = action.ActionType,
                Parameters = action.Parameters
            }, new Dictionary<string, string>());
        }
    }

    private void CheckGameEnd()
    {
        // Check if game already ended through action
        if (_state.GameWon || _state.GameLost || _state.GameEnded)
            return;

        // Check if max score is reached (one win condition)
        if (_state.Score >= _adventure.MaxScore && _adventure.MaxScore > 0)
        {
            _state.GameWon = true;
            if (!string.IsNullOrEmpty(_adventure.WinText.Text))
            {
                _state.AddOutput("\n" + FormatText(_adventure.WinText.Text, new Dictionary<string, string>()));
            }
            else
            {
                _state.AddOutput("\n**Congratulations! You have won the game!**");
            }
        }
    }

    private string FormatText(string text, Dictionary<string, string> parameters)
    {
        var formatter = new TextFormatter(_adventure, _state);
        return formatter.Format(text, parameters);
    }

    private string ResolveParameter(string value, Dictionary<string, string> parameters)
    {
        // If value starts with %, resolve from parameters
        if (value.StartsWith("%") && value.EndsWith("%"))
        {
            var paramName = value.Trim('%');
            if (parameters.TryGetValue(paramName, out var resolved))
            {
                return resolved;
            }
        }

        return value;
    }

    /// <summary>
    /// Get the introduction text for starting the game
    /// </summary>
    public string GetIntroduction()
    {
        var intro = new List<string>();

        if (!string.IsNullOrEmpty(_adventure.Title))
        {
            intro.Add($"**{_adventure.Title}**");
        }

        if (!string.IsNullOrEmpty(_adventure.Author))
        {
            intro.Add($"by {_adventure.Author}");
        }

        if (!string.IsNullOrEmpty(_adventure.Introduction))
        {
            intro.Add("\n" + _adventure.Introduction);
        }

        intro.Add("\n");

        // Describe starting location
        _state.OutputBuffer.Clear();
        DescribeLocation();
        intro.Add(_state.GetAndClearOutput());

        return string.Join("\n", intro);
    }

    /// <summary>
    /// Save the current game state
    /// </summary>
    public GameState SaveGame()
    {
        return _state.Clone();
    }

    /// <summary>
    /// Restore a saved game state
    /// </summary>
    public void RestoreGame(GameState savedState)
    {
        _state = savedState.Clone();
    }
}
