namespace ADRIFT.Core.Models;

/// <summary>
/// Represents a task/command in the adventure
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Task : AdriftItem
{
    // Core identity
    public string Name { get; set; } = string.Empty;
    public Description Description { get; set; } = new();

    // Task type
    public TaskType Type { get; set; } = TaskType.General;

    // Priority (0-99999, higher = more priority)
    public int Priority { get; set; } = 5000;

    // Commands (multiple command patterns possible)
    public List<TaskCommand> Commands { get; set; } = new();

    // Restrictions (all must pass for task to execute)
    public List<Restriction> Restrictions { get; set; } = new();

    // Actions (execute on success)
    public List<Action> Actions { get; set; } = new();

    // Success and Failure actions (simplified action system)
    public List<TaskAction> SuccessActions { get; set; } = new();
    public List<TaskAction> FailureActions { get; set; } = new();

    // Output messages
    public Description CompletionMessage { get; set; } = new(); // Shown on success
    public Description FailureMessage { get; set; } = new(); // Shown on restriction failure

    // Task control
    public bool IsRepeatable { get; set; } // Can be done multiple times
    public bool Completed { get; set; } // Has been completed

    // Scoring
    public int ScoreValue { get; set; } // Points awarded on completion

    // For specific tasks
    public SpecificOverrideType SpecificOverride { get; set; } = SpecificOverrideType.Override;
    public List<TaskSpecific> Specifics { get; set; } = new(); // What this specific task applies to

    // For system tasks
    public bool RunImmediately { get; set; } // Execute at game start

    // Location trigger
    public bool IsLocationTrigger { get; set; } // Execute when entering location
    public string? TriggerLocationKey { get; set; } // Location that triggers this task

    // References (for command parsing)
    public List<TaskReference> References { get; set; } = new();

    // Task execution tracking
    public int TimesCompleted { get; set; } // How many times completed
    public int TurnLastCompleted { get; set; } // Turn number when last completed

    // Properties (integrated with property system)
    public Dictionary<string, Property> Properties { get; set; } = new();

    public override string DisplayName => Name;
    public override string ItemType => "Task";
}

/// <summary>
/// Task type
/// </summary>
public enum TaskType
{
    /// <summary>
    /// General task (applies to any matching command)
    /// </summary>
    General,

    /// <summary>
    /// System task (built-in commands like LOOK, INVENTORY, etc.)
    /// </summary>
    System,

    /// <summary>
    /// Specific task (applies to specific objects/characters)
    /// </summary>
    Specific
}

/// <summary>
/// How specific tasks override general tasks
/// </summary>
public enum SpecificOverrideType
{
    /// <summary>
    /// Run before general task
    /// </summary>
    Before,

    /// <summary>
    /// Replace general task
    /// </summary>
    Override,

    /// <summary>
    /// Run after general task
    /// </summary>
    After
}

/// <summary>
/// Command pattern for a task
/// </summary>
public class TaskCommand
{
    /// <summary>
    /// Command pattern (e.g., "take #object#", "give #object# to #character#")
    /// </summary>
    public string Pattern { get; set; } = string.Empty;

    /// <summary>
    /// Which references are optional in this command
    /// </summary>
    public List<int> OptionalReferences { get; set; } = new();
}

/// <summary>
/// What a specific task applies to
/// </summary>
public class TaskSpecific
{
    /// <summary>
    /// Type of specific
    /// </summary>
    public SpecificType Type { get; set; } = SpecificType.Object;

    /// <summary>
    /// Object key (if Type = Object)
    /// </summary>
    public string? ObjectKey { get; set; }

    /// <summary>
    /// Character key (if Type = Character)
    /// </summary>
    public string? CharacterKey { get; set; }

    /// <summary>
    /// Direction (if Type = Direction)
    /// </summary>
    public DirectionEnum? Direction { get; set; }

    /// <summary>
    /// Object group key (if Type = ObjectGroup)
    /// </summary>
    public string? ObjectGroupKey { get; set; }

    /// <summary>
    /// Character group key (if Type = CharacterGroup)
    /// </summary>
    public string? CharacterGroupKey { get; set; }

    /// <summary>
    /// Text pattern (if Type = Text)
    /// </summary>
    public string? TextPattern { get; set; }
}

public enum SpecificType
{
    Object,
    Character,
    Direction,
    Location,
    ObjectGroup,
    CharacterGroup,
    Text,
    Number
}

/// <summary>
/// Reference in a command pattern
/// </summary>
public class TaskReference
{
    /// <summary>
    /// Reference number (1-5)
    /// </summary>
    public int ReferenceNumber { get; set; } = 1;

    /// <summary>
    /// Type of reference
    /// </summary>
    public ReferenceType Type { get; set; } = ReferenceType.Object;

    /// <summary>
    /// Is this reference optional in the command?
    /// </summary>
    public bool IsOptional { get; set; }

    /// <summary>
    /// Specific items this reference can match (if not any)
    /// </summary>
    public List<string> RestrictToKeys { get; set; } = new();
}

public enum ReferenceType
{
    Object,
    Character,
    Number,
    Text,
    Direction,
    Location,
    Item // Any item (object, character, or location)
}
