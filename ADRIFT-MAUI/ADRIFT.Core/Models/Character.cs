namespace ADRIFT.Core.Models;

/// <summary>
/// Represents a character (NPC or player) in the adventure
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Character : AdriftItem
{
    // Core identity
    public string Name { get; set; } = string.Empty; // Proper name
    public string Descriptor { get; set; } = string.Empty; // Generic descriptor (e.g., "the guard")
    public string Prefix { get; set; } = string.Empty; // Article (a, an, the)
    public List<string> Aliases { get; set; } = new(); // Alternative names

    // Description
    public Description Description { get; set; } = new(); // Main description (with alternates)

    // Character type
    public CharacterType CharacterType { get; set; } = CharacterType.NonPlayer;

    // Gender
    public Gender Gender { get; set; } = Gender.Unknown;

    // Perspective (for player character)
    public Perspective Perspective { get; set; } = Perspective.SecondPerson;

    // Location
    public CharacterLocationType LocationType { get; set; } = CharacterLocationType.AtLocation;
    public string? LocationKey { get; set; } // For AtLocation
    public string? CharacterKey { get; set; } // For OnCharacter/InCharacter
    public string? ObjectKey { get; set; } // For OnSurface/InContainer

    // Position
    public CharacterPosition Position { get; set; } = CharacterPosition.Standing;

    // Known to (which characters know this character)
    public List<string> KnownTo { get; set; } = new();

    // "Is here" description (when character is in a location)
    public Description? IsHereDescription { get; set; }

    // Walk routes
    public List<Walk> Walks { get; set; } = new();

    // Conversation topics
    public Dictionary<string, Topic> Topics { get; set; } = new();
    public string GeneralGreeting { get; set; } = string.Empty;
    public string UnknownTopicResponse { get; set; } = string.Empty;

    // Battle system (optional)
    public int Strength { get; set; }
    public int Stamina { get; set; }
    public int Defense { get; set; }
    public int Accuracy { get; set; }

    // Properties (integrated with property system)
    public Dictionary<string, Property> Properties { get; set; } = new();

    // Tracking what this character has seen
    public HashSet<string> SeenLocationKeys { get; set; } = new();
    public HashSet<string> SeenObjectKeys { get; set; } = new();
    public HashSet<string> SeenCharacterKeys { get; set; } = new();

    // Helper properties
    public string ProperName => !string.IsNullOrEmpty(Name) ? Name : Descriptor;
    public string FullName => string.Join(" ", new[] { Prefix, Name }.Where(s => !string.IsNullOrWhiteSpace(s)));

    public override string DisplayName => ProperName;
    public override string ItemType => "Character";
}

/// <summary>
/// Player vs NPC
/// </summary>
public enum CharacterType
{
    Player,
    NonPlayer
}

/// <summary>
/// Character gender (affects pronouns)
/// </summary>
public enum Gender
{
    Male,
    Female,
    Unknown
}

/// <summary>
/// Narrative perspective
/// </summary>
public enum Perspective
{
    FirstPerson, // I, me, my
    SecondPerson, // you, your
    ThirdPerson // he/she/they, him/her/them
}

/// <summary>
/// Where character is located
/// </summary>
public enum CharacterLocationType
{
    AtLocation,
    Hidden,
    OnCharacter,
    InCharacter,
    OnSurface,
    InContainer
}

/// <summary>
/// Character position
/// </summary>
public enum CharacterPosition
{
    Standing,
    Sitting,
    Lying
}

/// <summary>
/// Character walk (movement pattern)
/// </summary>
public class Walk
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Description { get; set; } = string.Empty;
    public List<WalkStep> Steps { get; set; } = new();
    public bool Loop { get; set; } // Repeat walk
    public bool StartActive { get; set; } // Start immediately
    public WalkStatus Status { get; set; } = WalkStatus.NotStarted;
    public int CurrentStep { get; set; } // Current step index
}

public enum WalkStatus
{
    NotStarted,
    Active,
    Paused,
    Finished
}

/// <summary>
/// Single step in a walk
/// </summary>
public class WalkStep
{
    public int StepNumber { get; set; }
    public string LocationKey { get; set; } = string.Empty;
    public DirectionEnum Direction { get; set; } = DirectionEnum.North;
    public int DelayTurns { get; set; } // Delay before this step
}

/// <summary>
/// Conversation topic
/// </summary>
public class Topic
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string TopicName { get; set; } = string.Empty;
    public List<string> Keywords { get; set; } = new(); // Keywords that trigger this topic
    public Description Introduction { get; set; } = new(); // Shown first time
    public Description Response { get; set; } = new(); // Shown on ask/tell
    public bool IsCommand { get; set; } // Execute as command vs conversation
    public bool Stay { get; set; } // Stay in conversation after
    public string? ParentKey { get; set; } // Parent topic for tree structure
    public List<string> SubTopicKeys { get; set; } = new(); // Child topics
}
