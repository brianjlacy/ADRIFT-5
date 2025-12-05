namespace ADRIFT.Core.Models;

/// <summary>
/// Action to execute when a task succeeds (or event triggers)
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Action
{
    /// <summary>
    /// Type of action
    /// </summary>
    public ActionType Type { get; set; } = ActionType.DisplayMessage;

    /// <summary>
    /// For MoveObject actions
    /// </summary>
    public MoveObjectAction? MoveObject { get; set; }

    /// <summary>
    /// For AddRemoveObject actions
    /// </summary>
    public AddRemoveObjectAction? AddRemoveObject { get; set; }

    /// <summary>
    /// For MoveCharacter actions
    /// </summary>
    public MoveCharacterAction? MoveCharacter { get; set; }

    /// <summary>
    /// For AddRemoveLocation actions
    /// </summary>
    public AddRemoveLocationAction? AddRemoveLocation { get; set; }

    /// <summary>
    /// For SetProperties actions
    /// </summary>
    public SetPropertiesAction? SetProperties { get; set; }

    /// <summary>
    /// For SetVariable actions
    /// </summary>
    public SetVariableAction? SetVariable { get; set; }

    /// <summary>
    /// For SetTasks actions
    /// </summary>
    public SetTasksAction? SetTasks { get; set; }

    /// <summary>
    /// For ExecuteTask actions
    /// </summary>
    public ExecuteTaskAction? ExecuteTask { get; set; }

    /// <summary>
    /// For Conversation actions
    /// </summary>
    public ConversationAction? Conversation { get; set; }

    /// <summary>
    /// For EndGame actions
    /// </summary>
    public EndGameAction? EndGame { get; set; }

    /// <summary>
    /// For Time actions
    /// </summary>
    public TimeAction? Time { get; set; }

    /// <summary>
    /// For Score actions
    /// </summary>
    public ScoreAction? Score { get; set; }

    /// <summary>
    /// For DisplayMessage actions
    /// </summary>
    public DisplayMessageAction? DisplayMessage { get; set; }
}

public enum ActionType
{
    MoveObject,
    AddRemoveObject,
    MoveCharacter,
    AddRemoveLocation,
    SetProperties,
    SetVariable,
    SetTasks,
    ExecuteTask,
    Conversation,
    EndGame,
    Time,
    Score,
    DisplayMessage
}

/// <summary>
/// Move an object to a new location
/// </summary>
public class MoveObjectAction
{
    /// <summary>
    /// Which object(s) to move
    /// </summary>
    public MoveObjectWhat What { get; set; } = MoveObjectWhat.Object;

    /// <summary>
    /// Specific object key (if What = Object)
    /// </summary>
    public string? ObjectKey { get; set; }

    /// <summary>
    /// Object group key (if What = ObjectsInGroup)
    /// </summary>
    public string? ObjectGroupKey { get; set; }

    /// <summary>
    /// Where to move the object(s)
    /// </summary>
    public MoveObjectTo To { get; set; } = MoveObjectTo.ToLocation;

    /// <summary>
    /// Destination location key (if To = ToLocation)
    /// </summary>
    public string? LocationKey { get; set; }

    /// <summary>
    /// Destination location group key (if To = ToLocationGroup)
    /// </summary>
    public string? LocationGroupKey { get; set; }

    /// <summary>
    /// Destination object key (if To = InsideObject or OntoObject)
    /// </summary>
    public string? DestinationObjectKey { get; set; }

    /// <summary>
    /// Destination character key (if To = HeldByCharacter or WornByCharacter)
    /// </summary>
    public string? DestinationCharacterKey { get; set; }
}

public enum MoveObjectWhat
{
    Object,
    ObjectsInGroup,
    ObjectsAtLocation,
    ObjectsHeldByCharacter,
    ObjectsWornByCharacter,
    ObjectsInsideObject,
    ObjectsOnObject,
    ReferencedObject
}

public enum MoveObjectTo
{
    ToLocation,
    ToLocationGroup,
    InsideObject,
    OntoObject,
    HeldByCharacter,
    WornByCharacter,
    ToPartOfCharacter,
    ToPartOfObject,
    ToPresence,
    ToNowhere
}

/// <summary>
/// Add or remove objects from the game
/// </summary>
public class AddRemoveObjectAction
{
    /// <summary>
    /// Add (true) or remove (false)
    /// </summary>
    public bool Add { get; set; } = true;

    /// <summary>
    /// Object key
    /// </summary>
    public string ObjectKey { get; set; } = string.Empty;
}

/// <summary>
/// Move a character to a new location
/// </summary>
public class MoveCharacterAction
{
    /// <summary>
    /// Which character(s) to move
    /// </summary>
    public MoveCharacterWho Who { get; set; } = MoveCharacterWho.Character;

    /// <summary>
    /// Specific character key (if Who = Character)
    /// </summary>
    public string? CharacterKey { get; set; }

    /// <summary>
    /// Character group key (if Who = CharactersInGroup)
    /// </summary>
    public string? CharacterGroupKey { get; set; }

    /// <summary>
    /// Where to move the character(s)
    /// </summary>
    public MoveCharacterTo To { get; set; } = MoveCharacterTo.ToLocation;

    /// <summary>
    /// Destination location key (if To = ToLocation)
    /// </summary>
    public string? LocationKey { get; set; }

    /// <summary>
    /// Destination location group key (if To = ToLocationGroup)
    /// </summary>
    public string? LocationGroupKey { get; set; }

    /// <summary>
    /// Destination object key (if To = InsideObject or OntoObject)
    /// </summary>
    public string? DestinationObjectKey { get; set; }

    /// <summary>
    /// Destination character key (if To = InCharacter or OnCharacter)
    /// </summary>
    public string? DestinationCharacterKey { get; set; }
}

public enum MoveCharacterWho
{
    Character,
    CharactersInGroup,
    CharactersAtLocation,
    ReferencedCharacter
}

public enum MoveCharacterTo
{
    ToLocation,
    ToLocationGroup,
    InsideObject,
    OntoObject,
    InCharacter,
    OnCharacter,
    ToSameLocationAsObject,
    ToPresence
}

/// <summary>
/// Add or remove locations from the game
/// </summary>
public class AddRemoveLocationAction
{
    /// <summary>
    /// Add (true) or remove (false)
    /// </summary>
    public bool Add { get; set; } = true;

    /// <summary>
    /// Location key
    /// </summary>
    public string LocationKey { get; set; } = string.Empty;
}

/// <summary>
/// Set property values
/// </summary>
public class SetPropertiesAction
{
    /// <summary>
    /// Item key (object/character/location)
    /// </summary>
    public string ItemKey { get; set; } = string.Empty;

    /// <summary>
    /// Property key
    /// </summary>
    public string PropertyKey { get; set; } = string.Empty;

    /// <summary>
    /// New value for the property
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// For state-based properties
    /// </summary>
    public string? StateName { get; set; }
}

/// <summary>
/// Set variable value
/// </summary>
public class SetVariableAction
{
    /// <summary>
    /// Variable key
    /// </summary>
    public string VariableKey { get; set; } = string.Empty;

    /// <summary>
    /// Assignment type
    /// </summary>
    public VariableAssignment Assignment { get; set; } = VariableAssignment.SetTo;

    /// <summary>
    /// Value or expression
    /// </summary>
    public string Value { get; set; } = string.Empty;
}

public enum VariableAssignment
{
    SetTo,
    Increase,
    Decrease,
    Multiply,
    Divide,
    Append
}

/// <summary>
/// Set task completion status
/// </summary>
public class SetTasksAction
{
    /// <summary>
    /// Task key
    /// </summary>
    public string TaskKey { get; set; } = string.Empty;

    /// <summary>
    /// Set to completed (true) or not completed (false)
    /// </summary>
    public bool Completed { get; set; } = true;
}

/// <summary>
/// Execute another task
/// </summary>
public class ExecuteTaskAction
{
    /// <summary>
    /// Task key to execute
    /// </summary>
    public string TaskKey { get; set; } = string.Empty;
}

/// <summary>
/// Change conversation state
/// </summary>
public class ConversationAction
{
    /// <summary>
    /// Character key
    /// </summary>
    public string CharacterKey { get; set; } = string.Empty;

    /// <summary>
    /// Topic key to set
    /// </summary>
    public string? TopicKey { get; set; }

    /// <summary>
    /// End conversation (true) or enter conversation (false)
    /// </summary>
    public bool EndConversation { get; set; }
}

/// <summary>
/// End the game
/// </summary>
public class EndGameAction
{
    /// <summary>
    /// How to end the game
    /// </summary>
    public EndGameType Type { get; set; } = EndGameType.Win;

    /// <summary>
    /// Message to display
    /// </summary>
    public string Message { get; set; } = string.Empty;
}

public enum EndGameType
{
    Running,
    Win,
    Lose,
    Neutral
}

/// <summary>
/// Manage game time
/// </summary>
public class TimeAction
{
    /// <summary>
    /// Time operation
    /// </summary>
    public TimeOperation Operation { get; set; } = TimeOperation.Advance;

    /// <summary>
    /// Number of turns/seconds
    /// </summary>
    public int Amount { get; set; } = 1;
}

public enum TimeOperation
{
    Advance,
    Pause,
    Resume
}

/// <summary>
/// Award or deduct points
/// </summary>
public class ScoreAction
{
    /// <summary>
    /// Points to award (positive) or deduct (negative)
    /// </summary>
    public int Points { get; set; }
}

/// <summary>
/// Display a message
/// </summary>
public class DisplayMessageAction
{
    /// <summary>
    /// Message to display
    /// </summary>
    public Description Message { get; set; } = new();
}
