namespace ADRIFT.Core.Models;

/// <summary>
/// Base class for all ADRIFT items
/// </summary>
public abstract class AdriftItem
{
    public string Key { get; set; } = string.Empty;
    public bool IsLibrary { get; set; }
    public DateTime LastModified { get; set; } = DateTime.Now;

    /// <summary>
    /// Display name for the item
    /// </summary>
    public abstract string DisplayName { get; }

    /// <summary>
    /// Item type for categorization
    /// </summary>
    public abstract string ItemType { get; }
}
