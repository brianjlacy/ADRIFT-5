namespace ADRIFT.Core.Models;

/// <summary>
/// ALR (Alternate Reality Layer) - Text Override system
/// Allows find/replace of any text in descriptions with conditional alternates
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class ALR : AdriftItem
{
    /// <summary>
    /// Text to find and replace
    /// </summary>
    public string OldText { get; set; } = string.Empty;

    /// <summary>
    /// Replacement text (Description with alternates for conditional replacement)
    /// </summary>
    public Description NewText { get; set; } = new();

    /// <summary>
    /// Case sensitive matching
    /// </summary>
    public bool CaseSensitive { get; set; } = false;

    /// <summary>
    /// Only replace whole words (not partial matches)
    /// </summary>
    public bool WholeWordsOnly { get; set; } = true;

    /// <summary>
    /// Order/priority for applying ALRs (lower = earlier)
    /// </summary>
    public int Order { get; set; } = 100;

    public override string DisplayName => $"{OldText} → {NewText}";
    public override string ItemType => "ALR";
}
