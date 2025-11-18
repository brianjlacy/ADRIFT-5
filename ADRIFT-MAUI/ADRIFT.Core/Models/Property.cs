namespace ADRIFT.Core.Models;

/// <summary>
/// Custom property that can be applied to objects, characters, or locations
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Property : AdriftItem
{
    /// <summary>
    /// Property description/name
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Type of property
    /// </summary>
    public PropertyType Type { get; set; } = PropertyType.SelectionOnly;

    /// <summary>
    /// What this property applies to
    /// </summary>
    public PropertyOf PropertyOf { get; set; } = PropertyOf.Objects;

    /// <summary>
    /// Is this property mandatory for items of this type?
    /// </summary>
    public bool Mandatory { get; set; }

    /// <summary>
    /// Is this property currently selected for the item?
    /// </summary>
    public bool Selected { get; set; }

    /// <summary>
    /// String value (for Text type)
    /// </summary>
    public string StringValue { get; set; } = string.Empty;

    /// <summary>
    /// Integer value (for Integer type)
    /// </summary>
    public int IntValue { get; set; }

    /// <summary>
    /// List of possible states (for StateList type)
    /// </summary>
    public List<string> States { get; set; } = new();

    /// <summary>
    /// Current selected state (for StateList type)
    /// </summary>
    public string? SelectedState { get; set; }

    /// <summary>
    /// Named integer values (for ValueList type)
    /// </summary>
    public Dictionary<string, int> ValueList { get; set; } = new();

    /// <summary>
    /// Object key (for ObjectKey type)
    /// </summary>
    public string? ObjectKey { get; set; }

    /// <summary>
    /// Character key (for CharacterKey type)
    /// </summary>
    public string? CharacterKey { get; set; }

    /// <summary>
    /// Location key (for LocationKey type)
    /// </summary>
    public string? LocationKey { get; set; }

    /// <summary>
    /// Location group key (for LocationGroupKey type)
    /// </summary>
    public string? LocationGroupKey { get; set; }

    /// <summary>
    /// Dependent property key (only show this property if another property matches a value)
    /// </summary>
    public string? DependentKey { get; set; }

    /// <summary>
    /// Dependent property value (value that DependentKey must have to show this property)
    /// </summary>
    public string? DependentValue { get; set; }

    /// <summary>
    /// Restrict which items can have this property (based on another property)
    /// </summary>
    public string? RestrictProperty { get; set; }

    /// <summary>
    /// Value that RestrictProperty must have for this property to be available
    /// </summary>
    public string? RestrictValue { get; set; }

    /// <summary>
    /// Append states to another property's state list
    /// </summary>
    public string? AppendToProperty { get; set; }

    /// <summary>
    /// Only show for groups (not individual items)
    /// </summary>
    public bool GroupOnly { get; set; }

    /// <summary>
    /// Description text (for complex properties)
    /// </summary>
    public Description? DescriptionText { get; set; }

    public override string DisplayName => Description;
    public override string ItemType => "Property";
}

/// <summary>
/// Type of property
/// </summary>
public enum PropertyType
{
    /// <summary>
    /// Simple boolean - selected or not selected
    /// </summary>
    SelectionOnly,

    /// <summary>
    /// Integer value
    /// </summary>
    Integer,

    /// <summary>
    /// Text value
    /// </summary>
    Text,

    /// <summary>
    /// Reference to an object
    /// </summary>
    ObjectKey,

    /// <summary>
    /// Reference to a character
    /// </summary>
    CharacterKey,

    /// <summary>
    /// Reference to a location
    /// </summary>
    LocationKey,

    /// <summary>
    /// Reference to a location group
    /// </summary>
    LocationGroupKey,

    /// <summary>
    /// List of named states (dropdown selection)
    /// </summary>
    StateList,

    /// <summary>
    /// Named integer values
    /// </summary>
    ValueList
}

/// <summary>
/// What type of items this property applies to
/// </summary>
public enum PropertyOf
{
    Locations,
    Objects,
    Characters
}

/// <summary>
/// Standard ADRIFT 5 built-in properties
/// These property keys are used throughout ADRIFT
/// </summary>
public static class StandardProperties
{
    // Object properties
    public const string OBJECT_OPENABLE = "OpenStatus";
    public const string OBJECT_LOCKABLE = "Lockable";
    public const string OBJECT_KEY = "Key";
    public const string OBJECT_SURFACE = "Surface";
    public const string OBJECT_CONTAINER = "Container";
    public const string OBJECT_CAPACITY = "Capacity";
    public const string OBJECT_WEARABLE = "Wearable";
    public const string OBJECT_READABLE = "Readable";
    public const string OBJECT_EDIBLE = "Edible";
    public const string OBJECT_LIGHTGIVER = "LightSource";
    public const string OBJECT_SIZE = "SizeWeight";
    public const string OBJECT_DYNAMIC_OR_SCENIC = "Dynamic";
    public const string OBJECT_ARTICLE = "Article";
    public const string OBJECT_PREFIX = "Prefix";
    public const string OBJECT_LIST_DESC = "ListDesc";

    // Character properties
    public const string CHAR_POSITION = "CharPosition";
    public const string CHAR_HERE_DESC = "CharHereDesc";
    public const string CHAR_KNOWN_TO = "KnownTo";
    public const string CHAR_GENDER = "Gender";
    public const string CHAR_DESCRIPTOR = "Descriptor";
    public const string CHAR_BATTLE_STRENGTH = "Strength";
    public const string CHAR_BATTLE_STAMINA = "Stamina";
    public const string CHAR_BATTLE_DEFENSE = "Defense";
    public const string CHAR_BATTLE_ACCURACY = "Accuracy";

    // Location properties
    public const string LOC_DARK = "Dark";
    public const string LOC_SHORT_DESC = "ShortLocationDescription";
    public const string LOC_LONG_DESC = "LongLocationDescription";
}
