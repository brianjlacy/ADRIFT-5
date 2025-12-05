namespace ADRIFT.Core.Models;

/// <summary>
/// Represents a conditional restriction that must pass or fail
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Restriction
{
    /// <summary>
    /// Type of restriction
    /// </summary>
    public RestrictionType Type { get; set; } = RestrictionType.Location;

    /// <summary>
    /// Must pass or must not pass
    /// </summary>
    public MustEnum Must { get; set; } = MustEnum.Must;

    /// <summary>
    /// Key of the item being checked (location, object, character, task, etc.)
    /// </summary>
    public string ItemKey { get; set; } = string.Empty;

    /// <summary>
    /// For location restrictions
    /// </summary>
    public LocationRestriction? LocationRestriction { get; set; }

    /// <summary>
    /// For object restrictions
    /// </summary>
    public ObjectRestriction? ObjectRestriction { get; set; }

    /// <summary>
    /// For character restrictions
    /// </summary>
    public CharacterRestriction? CharacterRestriction { get; set; }

    /// <summary>
    /// For task restrictions
    /// </summary>
    public TaskRestriction? TaskRestriction { get; set; }

    /// <summary>
    /// For variable restrictions
    /// </summary>
    public VariableRestriction? VariableRestriction { get; set; }

    /// <summary>
    /// For property restrictions
    /// </summary>
    public PropertyRestriction? PropertyRestriction { get; set; }

    /// <summary>
    /// For direction restrictions
    /// </summary>
    public DirectionRestriction? DirectionRestriction { get; set; }

    /// <summary>
    /// For expression restrictions
    /// </summary>
    public ExpressionRestriction? ExpressionRestriction { get; set; }

    /// <summary>
    /// Message to display if restriction fails (optional)
    /// </summary>
    public string? FailMessage { get; set; }
}

/// <summary>
/// Type of restriction
/// </summary>
public enum RestrictionType
{
    Location,
    Object,
    Task,
    Character,
    Variable,
    Item,
    Property,
    Direction,
    Expression
}

/// <summary>
/// Must pass or must fail
/// </summary>
public enum MustEnum
{
    Must,
    MustNot
}

/// <summary>
/// Location-based restriction
/// </summary>
public class LocationRestriction
{
    /// <summary>
    /// What to check about the location
    /// </summary>
    public LocationRestrictionType Type { get; set; } = LocationRestrictionType.CharacterAtLocation;

    /// <summary>
    /// Character key (for character-at-location checks)
    /// </summary>
    public string CharacterKey { get; set; } = string.Empty;

    /// <summary>
    /// Location key to check
    /// </summary>
    public string LocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Location group key (if checking group membership)
    /// </summary>
    public string? LocationGroupKey { get; set; }
}

public enum LocationRestrictionType
{
    CharacterAtLocation,
    CharacterNotAtLocation,
    CharacterInGroup,
    CharacterNotInGroup
}

/// <summary>
/// Object-based restriction
/// </summary>
public class ObjectRestriction
{
    /// <summary>
    /// What to check about the object
    /// </summary>
    public ObjectRestrictionType Type { get; set; } = ObjectRestrictionType.AtLocation;

    /// <summary>
    /// Object key
    /// </summary>
    public string ObjectKey { get; set; } = string.Empty;

    /// <summary>
    /// Location key (for location checks)
    /// </summary>
    public string? LocationKey { get; set; }

    /// <summary>
    /// Character key (for held-by/worn-by checks)
    /// </summary>
    public string? CharacterKey { get; set; }

    /// <summary>
    /// Container object key (for inside checks)
    /// </summary>
    public string? ContainerKey { get; set; }

    /// <summary>
    /// Object group key (if checking group membership)
    /// </summary>
    public string? ObjectGroupKey { get; set; }
}

public enum ObjectRestrictionType
{
    AtLocation,
    InLocation,
    HeldByCharacter,
    WornByCharacter,
    InsideObject,
    OnObject,
    InGroup,
    Visible,
    InState,
    Exists,
    BeenSeenBy
}

/// <summary>
/// Character-based restriction
/// </summary>
public class CharacterRestriction
{
    /// <summary>
    /// What to check about the character
    /// </summary>
    public CharacterRestrictionType Type { get; set; } = CharacterRestrictionType.AtLocation;

    /// <summary>
    /// Character key
    /// </summary>
    public string CharacterKey { get; set; } = string.Empty;

    /// <summary>
    /// Location key (for location checks)
    /// </summary>
    public string? LocationKey { get; set; }

    /// <summary>
    /// Character group key (if checking group membership)
    /// </summary>
    public string? CharacterGroupKey { get; set; }

    /// <summary>
    /// Position (for position checks)
    /// </summary>
    public CharacterPosition? Position { get; set; }

    /// <summary>
    /// Object key (for inside/on object checks)
    /// </summary>
    public string? ObjectKey { get; set; }
}

public enum CharacterRestrictionType
{
    AtLocation,
    InGroup,
    Alone,
    WithCharacter,
    InPosition,
    InsideObject,
    OnObject,
    Exists,
    HasSeenLocation,
    HasSeenObject,
    HasSeenCharacter
}

/// <summary>
/// Task-based restriction
/// </summary>
public class TaskRestriction
{
    /// <summary>
    /// Task key
    /// </summary>
    public string TaskKey { get; set; } = string.Empty;

    /// <summary>
    /// Check if completed or not completed
    /// </summary>
    public bool Completed { get; set; } = true;
}

/// <summary>
/// Variable-based restriction
/// </summary>
public class VariableRestriction
{
    /// <summary>
    /// Variable key
    /// </summary>
    public string VariableKey { get; set; } = string.Empty;

    /// <summary>
    /// Comparison operator
    /// </summary>
    public ComparisonOperator Operator { get; set; } = ComparisonOperator.EqualTo;

    /// <summary>
    /// Value to compare against (string for both text and numeric)
    /// </summary>
    public string Value { get; set; } = string.Empty;

    /// <summary>
    /// Expression to compare against (if not a simple value)
    /// </summary>
    public string? Expression { get; set; }
}

public enum ComparisonOperator
{
    EqualTo,
    NotEqualTo,
    LessThan,
    LessThanOrEqualTo,
    GreaterThan,
    GreaterThanOrEqualTo
}

/// <summary>
/// Property-based restriction
/// </summary>
public class PropertyRestriction
{
    /// <summary>
    /// Property key
    /// </summary>
    public string PropertyKey { get; set; } = string.Empty;

    /// <summary>
    /// Item key (object/character/location that has the property)
    /// </summary>
    public string ItemKey { get; set; } = string.Empty;

    /// <summary>
    /// Expected value (for value-based properties)
    /// </summary>
    public string? Value { get; set; }

    /// <summary>
    /// For state list properties
    /// </summary>
    public string? StateName { get; set; }
}

/// <summary>
/// Direction-based restriction
/// </summary>
public class DirectionRestriction
{
    /// <summary>
    /// Direction to check
    /// </summary>
    public DirectionEnum Direction { get; set; } = DirectionEnum.North;

    /// <summary>
    /// Location key (check if this direction exists from this location)
    /// </summary>
    public string LocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Check if direction exists (true) or doesn't exist (false)
    /// </summary>
    public bool Exists { get; set; } = true;
}

/// <summary>
/// Expression-based restriction
/// </summary>
public class ExpressionRestriction
{
    /// <summary>
    /// Expression to evaluate (must return true/false)
    /// </summary>
    public string Expression { get; set; } = string.Empty;
}
