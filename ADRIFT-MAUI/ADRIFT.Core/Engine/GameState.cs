using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Represents the current state of a game in progress
/// </summary>
public class GameState
{
    public Adventure Adventure { get; set; } = null!;

    // Player state
    public string CurrentLocationKey { get; set; } = string.Empty;
    public List<AdriftObject> Inventory { get; set; } = new();
    public int Score { get; set; }
    public int TurnCount { get; set; }
    public bool GameWon { get; set; }
    public bool GameLost { get; set; }
    public bool GameEnded { get; set; }

    // Entity locations and states
    public Dictionary<string, string> ObjectLocations { get; set; } = new(); // ObjectKey -> LocationKey
    public Dictionary<string, string> CharacterLocations { get; set; } = new(); // CharacterKey -> LocationKey
    public Dictionary<string, bool> CompletedTasks { get; set; } = new(); // TaskKey -> Completed
    public Dictionary<string, bool> TriggeredEvents { get; set; } = new(); // EventKey -> Triggered
    public Dictionary<string, int> EventTurnCounters { get; set; } = new(); // EventKey -> Turn counter

    // Variable values (runtime)
    public Dictionary<string, string> VariableValues { get; set; } = new(); // VariableKey -> Current value

    // Property values (runtime)
    public Dictionary<string, string> PropertyValues { get; set; } = new(); // PropertyKey -> Current value

    // Object states
    public Dictionary<string, bool> ObjectStates { get; set; } = new(); // ObjectKey.PropertyName -> Value

    // Character walk state
    public Dictionary<string, CharacterWalkState> CharacterWalkStates { get; set; } = new(); // CharacterKey -> Walk state

    // Output buffer
    public List<string> OutputBuffer { get; set; } = new();
    public string LastCommand { get; set; } = string.Empty;

    // Player attributes (for character-based games)
    public string PlayerName { get; set; } = "Adventurer";
    public Dictionary<string, object> PlayerAttributes { get; set; } = new();

    // Time tracking (for time-based games)
    public int TimeElapsed { get; set; } = 0; // Minutes elapsed

    /// <summary>
    /// Initialize game state from an adventure
    /// </summary>
    public static GameState Initialize(Adventure adventure)
    {
        var state = new GameState
        {
            Adventure = adventure,
            TurnCount = 0,
            Score = 0
        };

        // Find starting location (first non-library location or first location)
        var startLocation = adventure.Locations.Values
            .FirstOrDefault(l => !l.IsLibrary)
            ?? adventure.Locations.Values.FirstOrDefault();

        if (startLocation != null)
        {
            state.CurrentLocationKey = startLocation.Key;
        }

        // Initialize object locations
        foreach (var obj in adventure.Objects.Values)
        {
            if (!string.IsNullOrEmpty(obj.LocationKey))
            {
                state.ObjectLocations[obj.Key] = obj.LocationKey;
            }
        }

        // Initialize character locations and walk states
        foreach (var character in adventure.Characters.Values)
        {
            if (!string.IsNullOrEmpty(character.InitialLocationKey))
            {
                state.CharacterLocations[character.Key] = character.InitialLocationKey;
            }

            // Initialize walk state if character has a walk route
            if (character.HasWalkRoute && character.WalkSteps.Count > 0)
            {
                var walkStep = character.WalkSteps.OrderBy(s => s.StepNumber).FirstOrDefault();
                state.CharacterWalkStates[character.Key] = new CharacterWalkState
                {
                    CurrentStepIndex = 0,
                    TurnsUntilNextMove = walkStep?.DelayTurns ?? 0,
                    IsActive = true
                };
            }
        }

        // Initialize variables
        foreach (var variable in adventure.Variables.Values)
        {
            state.VariableValues[variable.Key] = variable.InitialValue;
        }

        // Initialize completed tasks
        foreach (var task in adventure.Tasks.Values)
        {
            state.CompletedTasks[task.Key] = false;
        }

        // Initialize triggered events
        foreach (var evt in adventure.Events.Values)
        {
            state.TriggeredEvents[evt.Key] = false;
            if (evt.Type == EventType.TimeBased)
            {
                state.EventTurnCounters[evt.Key] = 0;
            }
        }

        return state;
    }

    /// <summary>
    /// Get the current location object
    /// </summary>
    public Location? GetCurrentLocation()
    {
        if (string.IsNullOrEmpty(CurrentLocationKey))
            return null;

        Adventure.Locations.TryGetValue(CurrentLocationKey, out var location);
        return location;
    }

    /// <summary>
    /// Get all objects at the current location
    /// </summary>
    public List<AdriftObject> GetObjectsAtCurrentLocation()
    {
        var objects = new List<AdriftObject>();

        foreach (var obj in Adventure.Objects.Values)
        {
            if (ObjectLocations.TryGetValue(obj.Key, out var locationKey) && locationKey == CurrentLocationKey)
            {
                objects.Add(obj);
            }
            else if (obj.LocationType == ObjectLocation.AtLocation && obj.LocationKey == CurrentLocationKey)
            {
                objects.Add(obj);
            }
        }

        return objects;
    }

    /// <summary>
    /// Get all characters at the current location
    /// </summary>
    public List<Character> GetCharactersAtCurrentLocation()
    {
        var characters = new List<Character>();

        foreach (var character in Adventure.Characters.Values)
        {
            if (CharacterLocations.TryGetValue(character.Key, out var locationKey) && locationKey == CurrentLocationKey)
            {
                characters.Add(character);
            }
        }

        return characters;
    }

    /// <summary>
    /// Move an object to a new location
    /// </summary>
    public void MoveObject(string objectKey, string destinationKey)
    {
        ObjectLocations[objectKey] = destinationKey;
    }

    /// <summary>
    /// Add an object to inventory
    /// </summary>
    public void AddToInventory(string objectKey)
    {
        if (Adventure.Objects.TryGetValue(objectKey, out var obj))
        {
            if (!Inventory.Any(o => o.Key == objectKey))
            {
                Inventory.Add(obj);
                ObjectLocations[objectKey] = "Inventory";
            }
        }
    }

    /// <summary>
    /// Remove an object from inventory
    /// </summary>
    public void RemoveFromInventory(string objectKey)
    {
        var obj = Inventory.FirstOrDefault(o => o.Key == objectKey);
        if (obj != null)
        {
            Inventory.Remove(obj);
            ObjectLocations.Remove(objectKey);
        }
    }

    /// <summary>
    /// Check if player has an object
    /// </summary>
    public bool HasObject(string objectKey)
    {
        return Inventory.Any(o => o.Key == objectKey);
    }

    /// <summary>
    /// Get a variable value
    /// </summary>
    public string GetVariableValue(string variableKey)
    {
        return VariableValues.GetValueOrDefault(variableKey, string.Empty);
    }

    /// <summary>
    /// Set a variable value
    /// </summary>
    public void SetVariableValue(string variableKey, string value)
    {
        VariableValues[variableKey] = value;
    }

    public string GetPropertyValue(string propertyKey)
    {
        return PropertyValues.GetValueOrDefault(propertyKey, string.Empty);
    }

    public void SetPropertyValue(string propertyKey, string value)
    {
        PropertyValues[propertyKey] = value;
    }

    /// <summary>
    /// Mark a task as completed
    /// </summary>
    public void CompleteTask(string taskKey)
    {
        CompletedTasks[taskKey] = true;
    }

    /// <summary>
    /// Check if a task has been completed
    /// </summary>
    public bool IsTaskCompleted(string taskKey)
    {
        return CompletedTasks.GetValueOrDefault(taskKey, false);
    }

    /// <summary>
    /// Get the current location of a character
    /// </summary>
    public string GetCharacterLocation(string characterKey)
    {
        return CharacterLocations.GetValueOrDefault(characterKey, string.Empty);
    }

    /// <summary>
    /// Get the current location of an object
    /// </summary>
    public string GetObjectLocation(string objectKey)
    {
        return ObjectLocations.GetValueOrDefault(objectKey, string.Empty);
    }

    /// <summary>
    /// Move a character to a new location
    /// </summary>
    public void MoveCharacter(string characterKey, string destinationKey)
    {
        CharacterLocations[characterKey] = destinationKey;
    }

    /// <summary>
    /// Add text to output buffer
    /// </summary>
    public void AddOutput(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            OutputBuffer.Add(text);
        }
    }

    /// <summary>
    /// Get and clear output buffer
    /// </summary>
    public string GetAndClearOutput()
    {
        var output = string.Join("\n\n", OutputBuffer);
        OutputBuffer.Clear();
        return output;
    }

    /// <summary>
    /// Clone the current state (for save/restore)
    /// </summary>
    public GameState Clone()
    {
        return new GameState
        {
            Adventure = this.Adventure, // Reference only, don't clone
            CurrentLocationKey = this.CurrentLocationKey,
            Inventory = new List<AdriftObject>(this.Inventory),
            Score = this.Score,
            TurnCount = this.TurnCount,
            GameWon = this.GameWon,
            GameLost = this.GameLost,
            ObjectLocations = new Dictionary<string, string>(this.ObjectLocations),
            CharacterLocations = new Dictionary<string, string>(this.CharacterLocations),
            CompletedTasks = new Dictionary<string, bool>(this.CompletedTasks),
            TriggeredEvents = new Dictionary<string, bool>(this.TriggeredEvents),
            EventTurnCounters = new Dictionary<string, int>(this.EventTurnCounters),
            VariableValues = new Dictionary<string, string>(this.VariableValues),
            ObjectStates = new Dictionary<string, bool>(this.ObjectStates),
            CharacterWalkStates = new Dictionary<string, CharacterWalkState>(
                this.CharacterWalkStates.ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Clone())),
            PlayerName = this.PlayerName,
            PlayerAttributes = new Dictionary<string, object>(this.PlayerAttributes),
            TimeElapsed = this.TimeElapsed,
            LastCommand = this.LastCommand
        };
    }
}

/// <summary>
/// Tracks the walk state for a character with a walk route
/// </summary>
public class CharacterWalkState
{
    public int CurrentStepIndex { get; set; }
    public int TurnsUntilNextMove { get; set; }
    public bool IsActive { get; set; }

    public CharacterWalkState Clone()
    {
        return new CharacterWalkState
        {
            CurrentStepIndex = this.CurrentStepIndex,
            TurnsUntilNextMove = this.TurnsUntilNextMove,
            IsActive = this.IsActive
        };
    }
}
