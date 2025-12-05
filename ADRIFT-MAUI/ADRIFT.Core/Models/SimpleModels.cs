namespace ADRIFT.Core.Models;

/// <summary>
/// Variable for storing game state
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Variable : AdriftItem
{
    /// <summary>
    /// Variable name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Variable type
    /// </summary>
    public VariableType Type { get; set; } = VariableType.Integer;

    /// <summary>
    /// Description/purpose
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Array length (1 = single variable, >1 = array)
    /// </summary>
    public int Length { get; set; } = 1;

    /// <summary>
    /// Initial integer value (for numeric variables)
    /// </summary>
    public int InitialIntValue { get; set; } = 0;

    /// <summary>
    /// Initial string value (for text variables)
    /// </summary>
    public string InitialStringValue { get; set; } = string.Empty;

    /// <summary>
    /// Current integer values (for numeric variables and arrays)
    /// </summary>
    public int[] IntValues { get; set; } = new int[1];

    /// <summary>
    /// Current string values (for text variables and arrays)
    /// </summary>
    public string[] StringValues { get; set; } = new string[1];

    /// <summary>
    /// Helper for single value access
    /// </summary>
    public int IntValue
    {
        get => IntValues.Length > 0 ? IntValues[0] : 0;
        set
        {
            if (IntValues.Length == 0)
                IntValues = new int[1];
            IntValues[0] = value;
        }
    }

    /// <summary>
    /// Helper for single value access
    /// </summary>
    public string StringValue
    {
        get => StringValues.Length > 0 ? StringValues[0] : string.Empty;
        set
        {
            if (StringValues.Length == 0)
                StringValues = new string[1];
            StringValues[0] = value;
        }
    }

    /// <summary>
    /// Initialize array with proper length
    /// </summary>
    public void InitializeArray()
    {
        IntValues = new int[Length];
        StringValues = new string[Length];
        for (int i = 0; i < Length; i++)
        {
            IntValues[i] = InitialIntValue;
            StringValues[i] = InitialStringValue;
        }
    }

    public override string DisplayName => Name;
    public override string ItemType => "Variable";
}

public enum VariableType
{
    Integer,
    Text
}

/// <summary>
/// Synonym for parser word replacement
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Synonym : AdriftItem
{
    /// <summary>
    /// Word to change to
    /// </summary>
    public string OriginalWord { get; set; } = string.Empty;

    /// <summary>
    /// Words to change from (all map to OriginalWord)
    /// </summary>
    public List<string> SynonymWords { get; set; } = new();

    public override string DisplayName => OriginalWord;
    public override string ItemType => "Synonym";
}

/// <summary>
/// Group of items (locations, objects, characters, tasks, events)
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Group : AdriftItem
{
    /// <summary>
    /// Group name
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Type of items in this group
    /// </summary>
    public GroupType Type { get; set; } = GroupType.Characters;

    /// <summary>
    /// Member keys (items in this group)
    /// </summary>
    public List<string> MemberKeys { get; set; } = new();

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Properties that apply to all members of this group
    /// </summary>
    public Dictionary<string, Property> Properties { get; set; } = new();

    public override string DisplayName => Name;
    public override string ItemType => "Group";
}

public enum GroupType
{
    Characters,
    Objects,
    Locations,
    Tasks,
    Events
}

/// <summary>
/// Hint system
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Hint : AdriftItem
{
    /// <summary>
    /// Hint question/topic
    /// </summary>
    public string Question { get; set; } = string.Empty;

    /// <summary>
    /// Related task key (optional)
    /// </summary>
    public string? RelatedTaskKey { get; set; }

    /// <summary>
    /// Subtle hint (first level)
    /// </summary>
    public string SubtleHint { get; set; } = string.Empty;

    /// <summary>
    /// Sledgehammer hint (obvious/direct)
    /// </summary>
    public string SledgeHammerHint { get; set; } = string.Empty;

    /// <summary>
    /// Restrictions for when this hint is available
    /// </summary>
    public List<Restriction> Restrictions { get; set; } = new();

    /// <summary>
    /// Has player seen the subtle hint?
    /// </summary>
    public bool SubtleHintSeen { get; set; }

    /// <summary>
    /// Has player seen the sledgehammer hint?
    /// </summary>
    public bool SledgeHammerHintSeen { get; set; }

    public override string DisplayName => Question;
    public override string ItemType => "Hint";
}
