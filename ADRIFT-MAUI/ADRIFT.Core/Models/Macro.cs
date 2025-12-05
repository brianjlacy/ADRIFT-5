namespace ADRIFT.Core.Models;

/// <summary>
/// Macro - User-defined command shortcuts
/// Allows executing multiple commands with a single shortcut
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Macro : AdriftItem
{
    /// <summary>
    /// Macro title/name
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    /// Description of what this macro does
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Commands to execute (one per line)
    /// </summary>
    public List<string> Commands { get; set; } = new();

    /// <summary>
    /// Keyboard shortcut (e.g., "Ctrl+Q")
    /// </summary>
    public string? Shortcut { get; set; }

    /// <summary>
    /// Is this macro shared across all games?
    /// </summary>
    public bool IsShared { get; set; }

    /// <summary>
    /// IFID of game this macro belongs to (if not shared)
    /// </summary>
    public string? IFID { get; set; }

    public override string DisplayName => Title;
    public override string ItemType => "Macro";
}
