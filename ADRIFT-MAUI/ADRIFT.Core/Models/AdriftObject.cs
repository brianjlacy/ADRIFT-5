namespace ADRIFT.Core.Models;

/// <summary>
/// Represents an object in the adventure
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class AdriftObject : AdriftItem
{
    // Core identity
    public List<string> Names { get; set; } = new() { "object" }; // Multiple names/aliases for the object
    public string Article { get; set; } = "a"; // a, an, the, some, etc.
    public string Prefix { get; set; } = string.Empty; // Additional prefix (e.g., "rusty" in "the rusty key")

    // Descriptions
    public Description Description { get; set; } = new(); // Main description (with alternates)
    public Description? ListDescription { get; set; } // Custom listing description

    // Static vs Dynamic
    public bool IsStatic { get; set; } // True = scenery, False = takeable

    // Dynamic object location
    public DynamicObjectLocation DynamicLocation { get; set; } = DynamicObjectLocation.Hidden;
    public string? LocationKey { get; set; } // For SingleLocation
    public string? LocationGroupKey { get; set; } // For LocationGroup
    public string? CharacterKey { get; set; } // For HeldBy/WornBy/PartOfCharacter
    public string? ObjectKey { get; set; } // For InContainer/OnObject/PartOfObject

    // Static object location
    public StaticObjectLocation StaticLocation { get; set; } = StaticObjectLocation.NoRooms;
    public string? StaticLocationKey { get; set; } // For SingleLocation
    public string? StaticLocationGroupKey { get; set; } // For LocationGroup
    public string? StaticCharacterKey { get; set; } // For PartOfCharacter
    public string? StaticObjectKey { get; set; } // For PartOfObject

    // Physical properties
    public ObjectSize Size { get; set; } = ObjectSize.Normal;
    public double Weight { get; set; } = 1.0;

    // Container properties (via property system)
    public bool IsContainer { get; set; }
    public bool IsOpenable { get; set; }
    public bool IsOpen { get; set; } = true;
    public bool IsLockable { get; set; }
    public bool IsLocked { get; set; }
    public string? KeyObjectKey { get; set; } // Key required to unlock
    public int Capacity { get; set; } = 100; // Maximum size of contents

    // Surface properties
    public bool IsSurface { get; set; }
    public int SurfaceCapacity { get; set; } = 100;

    // Wearable properties
    public bool IsWearable { get; set; }
    public string? WornWhere { get; set; } // Body part where worn
    public List<string> CoversBodyParts { get; set; } = new(); // Body parts covered

    // Readable properties
    public bool IsReadable { get; set; }
    public Description? ReadText { get; set; } // Text when read

    // Edible/Drinkable
    public bool IsEdible { get; set; }
    public bool IsDrinkable { get; set; }

    // Light source
    public bool IsLightSource { get; set; }
    public bool IsLit { get; set; }
    public int LightDuration { get; set; } // Turns until extinguished (-1 = infinite)

    // Sittable/Lieable/Climbable
    public bool IsSittable { get; set; }
    public bool IsLieable { get; set; }
    public bool IsClimbable { get; set; }

    // List control
    public ListOptions ListOptions { get; set; } = ListOptions.List;
    public string? CustomListPrefix { get; set; }

    // Properties (integrated with property system)
    public Dictionary<string, Property> Properties { get; set; } = new();

    // Tracking which characters have seen this object
    public HashSet<string> SeenByCharacterKeys { get; set; } = new();

    // Helper property for the primary name
    public string Name
    {
        get => Names.Count > 0 ? Names[0] : "object";
        set
        {
            if (Names.Count == 0)
                Names.Add(value);
            else
                Names[0] = value;
        }
    }

    // Helper property for full name with article and prefix
    public string FullName => string.Join(" ", new[] { Article, Prefix, Name }.Where(s => !string.IsNullOrWhiteSpace(s)));

    public override string DisplayName => FullName;
    public override string ItemType => "Object";
}

/// <summary>
/// Location for dynamic (takeable) objects
/// </summary>
public enum DynamicObjectLocation
{
    HeldByCharacter,
    WornByCharacter,
    InContainer,
    OnObject,
    PartOfCharacter,
    PartOfObject,
    Hidden,
    SingleLocation,
    LocationGroup
}

/// <summary>
/// Location for static (scenery) objects
/// </summary>
public enum StaticObjectLocation
{
    AllRooms,
    NoRooms,
    PartOfCharacter,
    PartOfObject,
    SingleLocation,
    LocationGroup
}

/// <summary>
/// How object appears in room listings
/// </summary>
public enum ListOptions
{
    List, // Normal listing
    ListWithCustomPrefix, // List with custom prefix
    DoNotList // Don't auto-list
}

/// <summary>
/// Object size (affects capacity)
/// </summary>
public enum ObjectSize
{
    Tiny = 0,
    Small = 1,
    Normal = 2,
    Large = 3,
    Huge = 4,
    VeryHuge = 5
}
