namespace ADRIFT.Core.Models;

/// <summary>
/// Sound/multimedia resource
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Sound : AdriftItem
{
    /// <summary>
    /// Sound name/title
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Description
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// File path or URL
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Is this embedded in the TAF file or external?
    /// </summary>
    public bool IsEmbedded { get; set; }

    /// <summary>
    /// Embedded file data (if IsEmbedded = true)
    /// </summary>
    public byte[]? EmbeddedData { get; set; }

    /// <summary>
    /// Base64 encoded sound data (alternative to EmbeddedData)
    /// </summary>
    public string Data { get; set; } = string.Empty;

    /// <summary>
    /// Sound format
    /// </summary>
    public SoundFormat Format { get; set; } = SoundFormat.WAV;

    /// <summary>
    /// Default channel to play on (1-8)
    /// </summary>
    public int DefaultChannel { get; set; } = 1;

    /// <summary>
    /// Loop this sound?
    /// </summary>
    public bool Loop { get; set; }

    /// <summary>
    /// Volume (0-100)
    /// </summary>
    public int Volume { get; set; } = 100;

    /// <summary>
    /// Auto-play when game starts?
    /// </summary>
    public bool AutoPlay { get; set; }

    public override string DisplayName => Name;
    public override string ItemType => "Sound";
}

/// <summary>
/// Sound file format
/// </summary>
public enum SoundFormat
{
    WAV,
    MP3,
    MIDI,
    OGG,
    Unknown
}

/// <summary>
/// Media resource (image, sound, video)
/// </summary>
public class MediaResource
{
    /// <summary>
    /// Resource name/identifier
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Resource type
    /// </summary>
    public MediaType Type { get; set; } = MediaType.Image;

    /// <summary>
    /// File path or URL
    /// </summary>
    public string FilePath { get; set; } = string.Empty;

    /// <summary>
    /// Is this embedded in the TAF file?
    /// </summary>
    public bool IsEmbedded { get; set; }

    /// <summary>
    /// Embedded data
    /// </summary>
    public byte[]? Data { get; set; }

    /// <summary>
    /// MIME type
    /// </summary>
    public string? MimeType { get; set; }

    /// <summary>
    /// Original file name
    /// </summary>
    public string? OriginalFileName { get; set; }
}

/// <summary>
/// Media type
/// </summary>
public enum MediaType
{
    Image,
    Sound,
    Video
}
