namespace ADRIFT.Core.Models;

/// <summary>
/// Graphic/image resource for display in adventure
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Graphic : AdriftItem
{
    /// <summary>
    /// Graphic name/title
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Base64 encoded image data
    /// </summary>
    public string Data { get; set; } = string.Empty;

    /// <summary>
    /// File path (if external)
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Is this embedded in the TAF file?
    /// </summary>
    public bool IsEmbedded { get; set; } = true;

    /// <summary>
    /// Location this graphic is displayed in
    /// </summary>
    public string LocationKey { get; set; } = string.Empty;

    /// <summary>
    /// Display order (for multiple graphics in same location)
    /// </summary>
    public int DisplayOrder { get; set; }

    /// <summary>
    /// Display position
    /// </summary>
    public GraphicPosition Position { get; set; } = GraphicPosition.Center;

    /// <summary>
    /// Display size
    /// </summary>
    public GraphicSize Size { get; set; } = GraphicSize.Original;

    /// <summary>
    /// Image format
    /// </summary>
    public ImageFormat Format { get; set; } = ImageFormat.PNG;

    public override string DisplayName => Name;
    public override string ItemType => "Graphic";
}

/// <summary>
/// Graphic display position
/// </summary>
public enum GraphicPosition
{
    Left,
    Center,
    Right,
    Top,
    Bottom
}

/// <summary>
/// Graphic display size
/// </summary>
public enum GraphicSize
{
    Original,
    Small,
    Medium,
    Large,
    Fullscreen
}

/// <summary>
/// Image file format
/// </summary>
public enum ImageFormat
{
    PNG,
    JPG,
    JPEG,
    GIF,
    BMP,
    Unknown
}
