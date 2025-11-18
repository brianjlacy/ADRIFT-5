namespace ADRIFT.Core.Models;

public class AdriftObject : AdriftItem
{
    public string Article { get; set; } = "a";
    public string Prefix { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Aliases { get; set; } = new();

    public string ShortDescription { get; set; } = string.Empty;
    public string LongDescription { get; set; } = string.Empty;

    // Location
    public ObjectLocation LocationType { get; set; } = ObjectLocation.AtLocation;
    public string? LocationKey { get; set; }
    public string? ContainerKey { get; set; }
    public string? CharacterKey { get; set; }

    // Physical properties
    public ObjectSize Size { get; set; } = ObjectSize.Normal;
    public double Weight { get; set; } = 1.0;

    // Capabilities
    public bool IsStatic { get; set; }
    public bool IsContainer { get; set; }
    public bool IsSurface { get; set; }
    public bool IsWearable { get; set; }
    public bool IsEdible { get; set; }
    public bool IsLightSource { get; set; }
    public bool IsReadable { get; set; }

    // Container properties
    public int Capacity { get; set; } = 10;
    public bool IsOpenable { get; set; }
    public bool IsLockable { get; set; }
    public bool IsOpen { get; set; } = true;
    public bool IsLocked { get; set; }

    // Advanced
    public string? ReadingText { get; set; }
    public Dictionary<string, object> CustomProperties { get; set; } = new();

    public string FullName => string.Join(" ", new[] { Article, Prefix, Name }.Where(s => !string.IsNullOrWhiteSpace(s)));
    public override string DisplayName => FullName;
    public override string ItemType => "Object";
}

public enum ObjectLocation
{
    AtLocation,
    InsideObject,
    HeldByCharacter,
    WornByCharacter,
    HiddenAtLocation
}

public enum ObjectSize
{
    Tiny,
    Small,
    Normal,
    Large,
    Huge
}
