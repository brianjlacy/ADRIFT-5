namespace ADRIFT.Core.Models;

/// <summary>
/// Represents a location/room in the adventure
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Location : AdriftItem
{
    // Core descriptions
    public Description ShortDescription { get; set; } = new();
    public Description LongDescription { get; set; } = new();

    // Map settings
    public bool HideOnMap { get; set; }

    // Directional exits (12 standard directions)
    public DirectionExit North { get; set; } = new();
    public DirectionExit NorthEast { get; set; } = new();
    public DirectionExit East { get; set; } = new();
    public DirectionExit SouthEast { get; set; } = new();
    public DirectionExit South { get; set; } = new();
    public DirectionExit SouthWest { get; set; } = new();
    public DirectionExit West { get; set; } = new();
    public DirectionExit NorthWest { get; set; } = new();
    public DirectionExit Up { get; set; } = new();
    public DirectionExit Down { get; set; } = new();
    public DirectionExit In { get; set; } = new();
    public DirectionExit Out { get; set; } = new();

    // Display options
    public WhichObjectsToListEnum HideObjects { get; set; } = WhichObjectsToListEnum.AllListedObjects;

    // Custom enter/exit text
    public bool ShowEnterText { get; set; }
    public string EnterText { get; set; } = string.Empty;
    public bool ShowExitText { get; set; }
    public string ExitText { get; set; } = string.Empty;

    // Properties (integrated with property system)
    public Dictionary<string, Property> Properties { get; set; } = new();

    // Tracking which characters have seen this location
    public HashSet<string> SeenByCharacterKeys { get; set; } = new();

    public override string DisplayName => ShortDescription.ToString();
    public override string ItemType => "Location";

    /// <summary>
    /// Get all directional exits as a dictionary
    /// </summary>
    public Dictionary<DirectionEnum, DirectionExit> GetAllDirections()
    {
        return new Dictionary<DirectionEnum, DirectionExit>
        {
            { DirectionEnum.North, North },
            { DirectionEnum.NorthEast, NorthEast },
            { DirectionEnum.East, East },
            { DirectionEnum.SouthEast, SouthEast },
            { DirectionEnum.South, South },
            { DirectionEnum.SouthWest, SouthWest },
            { DirectionEnum.West, West },
            { DirectionEnum.NorthWest, NorthWest },
            { DirectionEnum.Up, Up },
            { DirectionEnum.Down, Down },
            { DirectionEnum.In, In },
            { DirectionEnum.Out, Out }
        };
    }

    /// <summary>
    /// Set direction exit
    /// </summary>
    public void SetDirection(DirectionEnum direction, DirectionExit exit)
    {
        switch (direction)
        {
            case DirectionEnum.North: North = exit; break;
            case DirectionEnum.NorthEast: NorthEast = exit; break;
            case DirectionEnum.East: East = exit; break;
            case DirectionEnum.SouthEast: SouthEast = exit; break;
            case DirectionEnum.South: South = exit; break;
            case DirectionEnum.SouthWest: SouthWest = exit; break;
            case DirectionEnum.West: West = exit; break;
            case DirectionEnum.NorthWest: NorthWest = exit; break;
            case DirectionEnum.Up: Up = exit; break;
            case DirectionEnum.Down: Down = exit; break;
            case DirectionEnum.In: In = exit; break;
            case DirectionEnum.Out: Out = exit; break;
        }
    }
}

/// <summary>
/// Directional exit from a location
/// </summary>
public class DirectionExit
{
    /// <summary>
    /// Key of the destination location (empty if no exit)
    /// </summary>
    public string LocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Restrictions that must pass to use this exit
    /// </summary>
    public List<Restriction> Restrictions { get; set; } = new();

    /// <summary>
    /// Whether this exit has ever been blocked (for tracking)
    /// </summary>
    public bool EverBeenBlocked { get; set; }

    /// <summary>
    /// Check if this exit exists (has a destination)
    /// </summary>
    public bool Exists => !string.IsNullOrEmpty(LocationKey);

    /// <summary>
    /// Check if this exit has restrictions
    /// </summary>
    public bool HasRestrictions => Restrictions.Count > 0;
}

/// <summary>
/// Standard 12 directions in ADRIFT
/// </summary>
public enum DirectionEnum
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
    Up,
    Down,
    In,
    Out
}

/// <summary>
/// Control which objects are auto-listed in location description
/// </summary>
public enum WhichObjectsToListEnum
{
    AllListedObjects,
    AllGeneralListedObjects,
    AllSpecialListedObjects,
    NoObjects
}
