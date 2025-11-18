namespace ADRIFT.Engine.FileIO;

/// <summary>
/// Intermediate representation of an ADRIFT adventure loaded from a TAF file
/// This format bridges the gap between the TAF XML format and the simplified MAUI models
/// </summary>
public class AdventureData
{
    // File information
    public string FileName { get; set; } = "";
    public string FilePath { get; set; } = "";

    // Metadata
    public double Version { get; set; } = 5.0;
    public string Title { get; set; } = "Untitled Adventure";
    public string Author { get; set; } = "Unknown";
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    public DateTime Created { get; set; } = DateTime.Now;

    // Display settings
    public string FontName { get; set; } = "Arial";
    public int FontSize { get; set; } = 12;
    public int BackgroundColor { get; set; } = -1;
    public int InputColor { get; set; } = -1;
    public int OutputColor { get; set; } = -1;
    public int LinkColor { get; set; } = -1;

    // Game settings
    public bool ShowFirstRoom { get; set; } = true;
    public bool ShowExits { get; set; } = true;
    public bool EnableMenu { get; set; } = true;
    public bool EnableDebugger { get; set; } = true;

    // Descriptions
    public string IntroductionText { get; set; } = "";
    public string WinningText { get; set; } = "";

    // Collections of game elements (stored as raw XML nodes for now)
    public List<LocationData> Locations { get; set; } = new();
    public List<ObjectData> Objects { get; set; } = new();
    public List<TaskData> Tasks { get; set; } = new();
    public List<CharacterData> Characters { get; set; } = new();
    public List<EventData> Events { get; set; } = new();
    public List<VariableData> Variables { get; set; } = new();
    public List<GroupData> Groups { get; set; } = new();
    public List<HintData> Hints { get; set; } = new();
    public List<SynonymData> Synonyms { get; set; } = new();
    public List<PropertyData> Properties { get; set; } = new();
}

/// <summary>
/// Location data from TAF file
/// </summary>
public class LocationData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string ShortDescription { get; set; } = "";
    public string LongDescription { get; set; } = "";
    public string ViewFromHereDescription { get; set; } = "";
    public bool HideOnMap { get; set; } = false;
    public bool IsLibrary { get; set; } = false;
    public DateTime LastUpdated { get; set; } = DateTime.Now;
    public List<DirectionData> Directions { get; set; } = new();
    public Dictionary<string, string> Properties { get; set; } = new();
}

/// <summary>
/// Direction/Movement data
/// </summary>
public class DirectionData
{
    public string Direction { get; set; } = "North";
    public string DestinationKey { get; set; } = "";
    public List<string> Restrictions { get; set; } = new();
}

/// <summary>
/// Object data from TAF file
/// </summary>
public class ObjectData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Article { get; set; } = "a";
    public string Prefix { get; set; } = "";
    public string Name { get; set; } = "object";
    public List<string> Aliases { get; set; } = new();
    public string Description { get; set; } = "";
    public bool IsLibrary { get; set; } = false;
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // Location
    public string LocationType { get; set; } = "AtLocation"; // AtLocation, InsideObject, HeldByCharacter, Hidden
    public string LocationKey { get; set; } = "";

    // Properties
    public Dictionary<string, string> Properties { get; set; } = new();

    // Physical properties
    public int Size { get; set; } = 5; // ObjectSize enum
    public int Weight { get; set; } = 5; // ObjectWeight enum
    public bool IsStatic { get; set; } = false;
    public bool IsContainer { get; set; } = false;
    public bool IsSurface { get; set; } = false;
    public bool IsWearable { get; set; } = false;
    public bool IsEdible { get; set; } = false;
    public bool IsLightSource { get; set; } = false;
    public bool IsReadable { get; set; } = false;

    // Container properties
    public int Capacity { get; set; } = 0;
    public bool IsOpenable { get; set; } = false;
    public bool IsLockable { get; set; } = false;
    public bool IsOpen { get; set; } = true;
    public bool IsLocked { get; set; } = false;

    // Readable text
    public string ReadableText { get; set; } = "";
}

/// <summary>
/// Task data from TAF file
/// </summary>
public class TaskData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Description { get; set; } = "";
    public int Priority { get; set; } = 50;
    public string TaskType { get; set; } = "General"; // General, Specific
    public bool IsLibrary { get; set; } = false;
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // Commands
    public List<string> Commands { get; set; } = new();

    // References
    public List<string> ReferencedObjects { get; set; } = new();
    public List<string> ReferencedCharacters { get; set; } = new();
    public List<string> ReferencedLocations { get; set; } = new();

    // Restrictions
    public List<string> Restrictions { get; set; } = new();

    // Actions
    public List<ActionData> Actions { get; set; } = new();

    // Messages
    public string CompletionMessage { get; set; } = "";
    public string FailureMessage { get; set; } = "";

    // Scoring
    public int Score { get; set; } = 0;
    public bool RepeatableRestriction { get; set; } = false;
}

/// <summary>
/// Action data
/// </summary>
public class ActionData
{
    public string Type { get; set; } = "Message";
    public string Message { get; set; } = "";
    public Dictionary<string, string> Parameters { get; set; } = new();
}

/// <summary>
/// Character data from TAF file
/// </summary>
public class CharacterData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "character";
    public string Gender { get; set; } = "Male"; // Male, Female, Unknown
    public string Description { get; set; } = "";
    public bool IsLibrary { get; set; } = false;
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // Location
    public string LocationType { get; set; } = "AtLocation";
    public string LocationKey { get; set; } = "";

    // Properties
    public Dictionary<string, string> Properties { get; set; } = new();

    // Inventory
    public List<string> Inventory { get; set; } = new();

    // Walk route
    public List<WalkStepData> WalkRoute { get; set; } = new();

    // Conversation
    public string GeneralGreeting { get; set; } = "";
    public List<TopicData> Topics { get; set; } = new();
}

/// <summary>
/// Walk step data
/// </summary>
public class WalkStepData
{
    public string LocationKey { get; set; } = "";
    public int Turns { get; set; } = 1;
}

/// <summary>
/// Conversation topic data
/// </summary>
public class TopicData
{
    public string Keyword { get; set; } = "";
    public string Response { get; set; } = "";
}

/// <summary>
/// Event data from TAF file
/// </summary>
public class EventData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Description { get; set; } = "";
    public string EventType { get; set; } = "Immediate"; // Immediate, AfterTime, OnCondition
    public bool IsLibrary { get; set; } = false;
    public DateTime LastUpdated { get; set; } = DateTime.Now;

    // Timing
    public int DelayTurns { get; set; } = 0;
    public int RepeatInterval { get; set; } = 0;
    public bool IsRepeating { get; set; } = false;

    // Conditions
    public List<string> TriggerConditions { get; set; } = new();

    // Actions
    public List<ActionData> Actions { get; set; } = new();

    // Parent/child relationships
    public string ParentEventKey { get; set; } = "";
    public List<string> SubEventKeys { get; set; } = new();
}

/// <summary>
/// Variable data from TAF file
/// </summary>
public class VariableData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "variable";
    public string Type { get; set; } = "Integer"; // Integer, Text, Boolean
    public string InitialValue { get; set; } = "0";
    public int IntValue { get; set; } = 0;
    public string StringValue { get; set; } = "";
    public bool BoolValue { get; set; } = false;
}

/// <summary>
/// Group data from TAF file
/// </summary>
public class GroupData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "group";
    public string GroupType { get; set; } = "Objects"; // Objects, Locations, Characters
    public List<string> Members { get; set; } = new();
}

/// <summary>
/// Hint data from TAF file
/// </summary>
public class HintData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Question { get; set; } = "";
    public List<string> HintTexts { get; set; } = new();
    public List<string> Restrictions { get; set; } = new();
}

/// <summary>
/// Synonym data from TAF file
/// </summary>
public class SynonymData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string CommonName { get; set; } = "";
    public List<string> Synonyms { get; set; } = new();
}

/// <summary>
/// Property data from TAF file
/// </summary>
public class PropertyData
{
    public string Key { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = "property";
    public string Description { get; set; } = "";
    public string PropertyOf { get; set; } = "Objects"; // Objects, Locations, Characters
    public string PropertyType { get; set; } = "Text"; // Text, Integer, StateList, ValueList
    public List<string> States { get; set; } = new();
    public string InitialState { get; set; } = "";
}
