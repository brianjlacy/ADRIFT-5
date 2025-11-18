namespace ADRIFT.Core.Models;

public class Variable : AdriftItem
{
    public string Name { get; set; } = string.Empty;
    public VariableType Type { get; set; } = VariableType.Integer;
    public string InitialValue { get; set; } = "0";
    public string CurrentValue { get; set; } = "0";
    public string Description { get; set; } = string.Empty;

    public override string DisplayName => Name;
    public override string ItemType => "Variable";
}

public enum VariableType
{
    Integer,
    Text,
    Boolean
}

public class Synonym : AdriftItem
{
    public string OriginalWord { get; set; } = string.Empty;
    public List<string> SynonymWords { get; set; } = new();

    public override string DisplayName => OriginalWord;
    public override string ItemType => "Synonym";
}

public class Group : AdriftItem
{
    public string Name { get; set; } = string.Empty;
    public GroupType Type { get; set; } = GroupType.Characters;
    public List<string> MemberKeys { get; set; } = new();
    public string Description { get; set; } = string.Empty;

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

public class Hint : AdriftItem
{
    public string Question { get; set; } = string.Empty;
    public string? RelatedTaskKey { get; set; }
    public List<HintText> Hints { get; set; } = new();

    public override string DisplayName => Question;
    public override string ItemType => "Hint";
}

public class HintText
{
    public int Order { get; set; }
    public string Text { get; set; } = string.Empty;
}
