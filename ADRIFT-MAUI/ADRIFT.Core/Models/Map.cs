namespace ADRIFT.Core.Models;

/// <summary>
/// Map system for ADRIFT adventures
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Map
{
    /// <summary>
    /// Map pages (different levels/areas)
    /// </summary>
    public List<MapPage> Pages { get; set; } = new();

    /// <summary>
    /// Auto-layout algorithm settings
    /// </summary>
    public bool AutoLayout { get; set; } = true;

    /// <summary>
    /// Node width for auto-layout
    /// </summary>
    public int DefaultNodeWidth { get; set; } = 100;

    /// <summary>
    /// Node height for auto-layout
    /// </summary>
    public int DefaultNodeHeight { get; set; } = 80;

    /// <summary>
    /// Spacing between nodes
    /// </summary>
    public int NodeSpacing { get; set; } = 20;

    /// <summary>
    /// Get or create a map page
    /// </summary>
    public MapPage GetOrCreatePage(int pageNumber)
    {
        var page = Pages.FirstOrDefault(p => p.PageNumber == pageNumber);
        if (page == null)
        {
            page = new MapPage { PageNumber = pageNumber, Label = $"Page {pageNumber}" };
            Pages.Add(page);
        }
        return page;
    }
}

/// <summary>
/// Map page (level/area in the map)
/// </summary>
public class MapPage
{
    /// <summary>
    /// Page number
    /// </summary>
    public int PageNumber { get; set; }

    /// <summary>
    /// Page label/name
    /// </summary>
    public string Label { get; set; } = string.Empty;

    /// <summary>
    /// Nodes on this page
    /// </summary>
    public List<MapNode> Nodes { get; set; } = new();

    /// <summary>
    /// Has player seen this page?
    /// </summary>
    public bool Seen { get; set; }
}

/// <summary>
/// Map node representing a location
/// </summary>
public class MapNode
{
    /// <summary>
    /// Location key this node represents
    /// </summary>
    public string LocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Display text on the node
    /// </summary>
    public string Text { get; set; } = string.Empty;

    /// <summary>
    /// X coordinate (in pixels)
    /// </summary>
    public double X { get; set; }

    /// <summary>
    /// Y coordinate (in pixels)
    /// </summary>
    public double Y { get; set; }

    /// <summary>
    /// Z coordinate (for 3D maps, typically 0 for 2D)
    /// </summary>
    public double Z { get; set; }

    /// <summary>
    /// Node width
    /// </summary>
    public double Width { get; set; } = 100;

    /// <summary>
    /// Node height
    /// </summary>
    public double Height { get; set; } = 80;

    /// <summary>
    /// Map page this node is on
    /// </summary>
    public int Page { get; set; }

    /// <summary>
    /// Is position locked (don't auto-layout this node)
    /// </summary>
    public bool Pinned { get; set; }

    /// <summary>
    /// Is this node overlapping with another?
    /// </summary>
    public bool IsOverlapping { get; set; }

    /// <summary>
    /// Has player seen this location?
    /// </summary>
    public bool Seen { get; set; }

    /// <summary>
    /// Links from this node
    /// </summary>
    public List<MapLink> Links { get; set; } = new();

    /// <summary>
    /// Special direction indicators
    /// </summary>
    public bool ShowUpIndicator { get; set; }
    public bool ShowDownIndicator { get; set; }
    public bool ShowInIndicator { get; set; }
    public bool ShowOutIndicator { get; set; }

    /// <summary>
    /// Anchor points for connections (8 directions)
    /// </summary>
    public Dictionary<AnchorPoint, (double X, double Y)> Anchors { get; set; } = new();

    /// <summary>
    /// Calculate anchor points based on node position and size
    /// </summary>
    public void CalculateAnchors()
    {
        double centerX = X + Width / 2;
        double centerY = Y + Height / 2;
        double halfWidth = Width / 2;
        double halfHeight = Height / 2;

        Anchors[AnchorPoint.North] = (centerX, Y);
        Anchors[AnchorPoint.NorthEast] = (X + Width, Y);
        Anchors[AnchorPoint.East] = (X + Width, centerY);
        Anchors[AnchorPoint.SouthEast] = (X + Width, Y + Height);
        Anchors[AnchorPoint.South] = (centerX, Y + Height);
        Anchors[AnchorPoint.SouthWest] = (X, Y + Height);
        Anchors[AnchorPoint.West] = (X, centerY);
        Anchors[AnchorPoint.NorthWest] = (X, Y);
    }
}

/// <summary>
/// Link between map nodes (directional connection)
/// </summary>
public class MapLink
{
    /// <summary>
    /// Source location key
    /// </summary>
    public string SourceLocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Destination location key
    /// </summary>
    public string DestinationLocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Source anchor point
    /// </summary>
    public AnchorPoint SourceAnchor { get; set; } = AnchorPoint.North;

    /// <summary>
    /// Destination anchor point
    /// </summary>
    public AnchorPoint DestinationAnchor { get; set; } = AnchorPoint.South;

    /// <summary>
    /// Is this a two-way connection?
    /// </summary>
    public bool IsDuplex { get; set; }

    /// <summary>
    /// Link style
    /// </summary>
    public LinkStyle Style { get; set; } = LinkStyle.Solid;

    /// <summary>
    /// Custom path points (for non-straight lines)
    /// </summary>
    public List<(double X, double Y)> PathPoints { get; set; } = new();

    /// <summary>
    /// Direction this link represents
    /// </summary>
    public DirectionEnum Direction { get; set; } = DirectionEnum.North;
}

/// <summary>
/// Anchor points on a map node
/// </summary>
public enum AnchorPoint
{
    North,
    NorthEast,
    East,
    SouthEast,
    South,
    SouthWest,
    West,
    NorthWest,
    Center
}

/// <summary>
/// Link line style
/// </summary>
public enum LinkStyle
{
    Solid, // Normal exit
    Dotted, // Restricted exit
    Dashed, // Special exit
    Hidden // Hidden connection
}
