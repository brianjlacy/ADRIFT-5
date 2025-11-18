using ADRIFT.Core.Models;

namespace ADRIFT.Engine.GameEngine;

/// <summary>
/// Manages the current state of a running game
/// Tracks player location, object locations, task completion, variables, etc.
/// </summary>
public class GameState
{
    public Adventure Adventure { get; }
    public string CurrentLocationKey { get; set; } = string.Empty;
    public int Turns { get; set; } = 0;
    public int Score { get; set; } = 0;
    public List<string> OutputBuffer { get; } = new();

    // Track dynamic state
    public Dictionary<string, string> ObjectLocations { get; } = new(); // ObjectKey -> LocationKey
    public Dictionary<string, string> CharacterLocations { get; } = new(); // CharacterKey -> LocationKey
    public HashSet<string> CompletedTasks { get; } = new();
    public Dictionary<string, HashSet<string>> DisplayedDescriptions { get; } = new(); // ItemKey -> Set of description IDs
    public Dictionary<string, bool> EventStates { get; } = new(); // EventKey -> IsRunning

    public GameState(Adventure adventure)
    {
        Adventure = adventure;
        Initialize();
    }

    /// <summary>
    /// Initializes the game state from the adventure
    /// </summary>
    private void Initialize()
    {
        // Set starting location
        CurrentLocationKey = Adventure.StartLocationKey;
        if (string.IsNullOrEmpty(CurrentLocationKey) && Adventure.Locations.Count > 0)
        {
            CurrentLocationKey = Adventure.Locations.Keys.First();
        }

        // Initialize object locations
        foreach (var obj in Adventure.Objects.Values)
        {
            // TODO: Parse initial location from object properties
            // For now, place all objects at first location
            if (Adventure.Locations.Count > 0)
            {
                ObjectLocations[obj.Key] = Adventure.Locations.Keys.First();
            }
        }

        // Initialize character locations
        foreach (var character in Adventure.Characters.Values)
        {
            // TODO: Parse initial location from character properties
            if (Adventure.Locations.Count > 0)
            {
                CharacterLocations[character.Key] = Adventure.Locations.Keys.First();
            }
        }

        // Initialize events
        foreach (var evt in Adventure.Events.Values)
        {
            EventStates[evt.Key] = false;
        }
    }

    /// <summary>
    /// Gets the current location
    /// </summary>
    public Core.Models.Location? GetCurrentLocation()
    {
        if (Adventure.Locations.TryGetValue(CurrentLocationKey, out var location))
        {
            return location;
        }
        return null;
    }

    /// <summary>
    /// Moves the player to a new location
    /// </summary>
    public bool MoveToLocation(string locationKey)
    {
        if (Adventure.Locations.ContainsKey(locationKey))
        {
            CurrentLocationKey = locationKey;
            return true;
        }
        return false;
    }

    /// <summary>
    /// Gets all objects at a specific location
    /// </summary>
    public List<AdriftObject> GetObjectsAtLocation(string locationKey)
    {
        var objects = new List<AdriftObject>();

        foreach (var kvp in ObjectLocations)
        {
            if (kvp.Value == locationKey && Adventure.Objects.TryGetValue(kvp.Key, out var obj))
            {
                objects.Add(obj);
            }
        }

        return objects;
    }

    /// <summary>
    /// Gets all characters at a specific location
    /// </summary>
    public List<Character> GetCharactersAtLocation(string locationKey)
    {
        var characters = new List<Character>();

        foreach (var kvp in CharacterLocations)
        {
            if (kvp.Value == locationKey && Adventure.Characters.TryGetValue(kvp.Key, out var character))
            {
                characters.Add(character);
            }
        }

        return characters;
    }

    /// <summary>
    /// Moves an object to a new location
    /// </summary>
    public void MoveObject(string objectKey, string locationKey)
    {
        ObjectLocations[objectKey] = locationKey;
    }

    /// <summary>
    /// Moves a character to a new location
    /// </summary>
    public void MoveCharacter(string characterKey, string locationKey)
    {
        CharacterLocations[characterKey] = locationKey;
    }

    /// <summary>
    /// Marks a task as completed
    /// </summary>
    public void CompleteTask(string taskKey)
    {
        CompletedTasks.Add(taskKey);
    }

    /// <summary>
    /// Checks if a task is completed
    /// </summary>
    public bool IsTaskCompleted(string taskKey)
    {
        return CompletedTasks.Contains(taskKey);
    }

    /// <summary>
    /// Marks a description as displayed
    /// </summary>
    public void MarkDescriptionDisplayed(string itemKey, string descriptionId)
    {
        if (!DisplayedDescriptions.ContainsKey(itemKey))
        {
            DisplayedDescriptions[itemKey] = new HashSet<string>();
        }
        DisplayedDescriptions[itemKey].Add(descriptionId);
    }

    /// <summary>
    /// Checks if a description has been displayed
    /// </summary>
    public bool HasDescriptionBeenDisplayed(string itemKey, string descriptionId)
    {
        return DisplayedDescriptions.ContainsKey(itemKey) &&
               DisplayedDescriptions[itemKey].Contains(descriptionId);
    }

    /// <summary>
    /// Adds text to the output buffer
    /// </summary>
    public void AddOutput(string text)
    {
        if (!string.IsNullOrWhiteSpace(text))
        {
            OutputBuffer.Add(text);
        }
    }

    /// <summary>
    /// Gets and clears the output buffer
    /// </summary>
    public string GetOutput()
    {
        var output = string.Join("\n\n", OutputBuffer);
        OutputBuffer.Clear();
        return output;
    }

    /// <summary>
    /// Creates a deep copy of the current state (for save games)
    /// </summary>
    public GameState Clone()
    {
        var clone = new GameState(Adventure)
        {
            CurrentLocationKey = this.CurrentLocationKey,
            Turns = this.Turns,
            Score = this.Score
        };

        foreach (var kvp in ObjectLocations)
            clone.ObjectLocations[kvp.Key] = kvp.Value;

        foreach (var kvp in CharacterLocations)
            clone.CharacterLocations[kvp.Key] = kvp.Value;

        foreach (var task in CompletedTasks)
            clone.CompletedTasks.Add(task);

        foreach (var kvp in EventStates)
            clone.EventStates[kvp.Key] = kvp.Value;

        return clone;
    }
}
