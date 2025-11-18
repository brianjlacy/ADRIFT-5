namespace ADRIFT.Core.Models;

/// <summary>
/// Main adventure container
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Adventure
{
    // Basic metadata
    public string Title { get; set; } = "New Adventure";
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = "1.0";

    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;

    // Collections - All game items
    public Dictionary<string, Location> Locations { get; set; } = new();
    public Dictionary<string, AdriftObject> Objects { get; set; } = new();
    public Dictionary<string, Character> Characters { get; set; } = new();
    public Dictionary<string, Task> Tasks { get; set; } = new();
    public Dictionary<string, Event> Events { get; set; } = new();
    public Dictionary<string, Variable> Variables { get; set; } = new();
    public Dictionary<string, Synonym> Synonyms { get; set; } = new();
    public Dictionary<string, Group> Groups { get; set; } = new();
    public Dictionary<string, Hint> Hints { get; set; } = new();
    public Dictionary<string, Property> Properties { get; set; } = new();
    public Dictionary<string, ALR> ALRs { get; set; } = new();
    public Dictionary<string, UserFunction> UserFunctions { get; set; } = new();
    public Dictionary<string, Sound> Sounds { get; set; } = new();
    public Dictionary<string, Graphic> Graphics { get; set; } = new();
    public Dictionary<string, MediaResource> MediaResources { get; set; } = new();

    // Macros (may be global or per-adventure)
    public List<Macro> Macros { get; set; } = new();

    // Map
    public Map Map { get; set; } = new();

    // Game settings
    public string PlayerKey { get; set; } = string.Empty;
    public string StartLocationKey { get; set; } = string.Empty;
    public int MaxScore { get; set; }

    /// <summary>
    /// Use time-based system instead of turn-based
    /// </summary>
    public bool UseTime { get; set; }
    public Description IntroductionText { get; set; } = new();
    public Description WinText { get; set; } = new();
    public Description LoseText { get; set; } = new();

    // Display settings
    public bool ShowExits { get; set; } = true;
    public bool Use8PointCompass { get; set; } = false; // Show NE, SE, SW, NW directions

    // Task execution mode
    public TaskExecutionMode TaskExecutionMode { get; set; } = TaskExecutionMode.HighestPriorityTask;

    // Perspective
    public Perspective DefaultPerspective { get; set; } = Perspective.SecondPerson;

    // Battle system
    public bool EnableBattleSystem { get; set; }

    // Custom fonts and colors
    public string? CustomFontName { get; set; }
    public int? CustomFontSize { get; set; }
    public string? BackgroundColor { get; set; }
    public string? ForegroundColor { get; set; }
    public string? LinkColor { get; set; }

    // User status box (custom status display)
    public string? UserStatusBox { get; set; }

    // Babel Treaty metadata (IF standard)
    public string? IFID { get; set; } // Interactive Fiction ID
    public byte[]? CoverArt { get; set; }
    public string? Genre { get; set; }
    public string? Group { get; set; }
    public string? ForgivenessLevel { get; set; }
    public DateTime? FirstPublished { get; set; }
    public string? Language { get; set; }

    // Library settings
    public bool IsLibrary { get; set; }
    public List<string> LibraryKeys { get; set; } = new(); // Keys of items from libraries

    // File metadata
    public string Filename { get; set; } = string.Empty;
    public string FullPath { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public bool IsPasswordProtected => !string.IsNullOrEmpty(Password);

    // Version tracking
    public double ADRIFTVersion { get; set; } = 5.000036; // ADRIFT 5.0.36

    // Custom direction names (for localization)
    public Dictionary<DirectionEnum, string>? CustomDirectionNames { get; set; }

    // Custom "not understood" message
    public string? NotUnderstoodMessage { get; set; }

    // Item counts
    public int TotalItems => Locations.Count + Objects.Count + Characters.Count +
                             Tasks.Count + Events.Count + Variables.Count +
                             Synonyms.Count + Groups.Count + Hints.Count +
                             Properties.Count + ALRs.Count + UserFunctions.Count +
                             Sounds.Count;

    /// <summary>
    /// Get item by key (searches all collections)
    /// </summary>
    public AdriftItem? GetItem(string key)
    {
        if (Locations.TryGetValue(key, out var location)) return location;
        if (Objects.TryGetValue(key, out var obj)) return obj;
        if (Characters.TryGetValue(key, out var character)) return character;
        if (Tasks.TryGetValue(key, out var task)) return task;
        if (Events.TryGetValue(key, out var evt)) return evt;
        if (Variables.TryGetValue(key, out var variable)) return variable;
        if (Synonyms.TryGetValue(key, out var synonym)) return synonym;
        if (Groups.TryGetValue(key, out var group)) return group;
        if (Hints.TryGetValue(key, out var hint)) return hint;
        if (Properties.TryGetValue(key, out var property)) return property;
        if (ALRs.TryGetValue(key, out var alr)) return alr;
        if (UserFunctions.TryGetValue(key, out var userFunc)) return userFunc;
        if (Sounds.TryGetValue(key, out var sound)) return sound;
        return null;
    }
}

/// <summary>
/// Task execution mode
/// </summary>
public enum TaskExecutionMode
{
    /// <summary>
    /// Execute highest priority matching task
    /// </summary>
    HighestPriorityTask,

    /// <summary>
    /// Execute highest priority task that passes restrictions (ADRIFT 4 compatibility)
    /// </summary>
    HighestPriorityPassingTask
}
