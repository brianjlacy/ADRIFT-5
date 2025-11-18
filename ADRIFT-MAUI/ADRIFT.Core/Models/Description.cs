namespace ADRIFT.Core.Models;

/// <summary>
/// Represents a dynamic description that can change based on restrictions/conditions
/// ADRIFT uses multiple descriptions for the same object/location that display based on game state
/// </summary>
public class Description
{
    public List<SingleDescription> Descriptions { get; set; } = new();

    /// <summary>
    /// Gets the current description text based on restrictions and display rules
    /// </summary>
    public string GetText()
    {
        // For now, return the first description
        // In full implementation, this would evaluate restrictions
        if (Descriptions.Count > 0)
            return Descriptions[0].Text;

        return string.Empty;
    }

    /// <summary>
    /// Adds a simple text description
    /// </summary>
    public void AddDescription(string text)
    {
        Descriptions.Add(new SingleDescription { Text = text });
    }

    public static implicit operator Description(string text)
    {
        var desc = new Description();
        desc.AddDescription(text);
        return desc;
    }

    public static implicit operator string(Description desc)
    {
        return desc.GetText();
    }
}

/// <summary>
/// A single description entry with optional display conditions
/// </summary>
public class SingleDescription
{
    public string Text { get; set; } = string.Empty;
    public DisplayWhen DisplayWhen { get; set; } = DisplayWhen.Always;
    public bool DisplayOnce { get; set; } = false;
    public bool ReturnToDefault { get; set; } = false;
    public string TabLabel { get; set; } = "Default Description";
    public List<Restriction> Restrictions { get; set; } = new();
    public bool HasBeenDisplayed { get; set; } = false;

    /// <summary>
    /// Determines if this description should be shown based on current game state
    /// </summary>
    public bool ShouldDisplay()
    {
        // Check if already displayed and set to display only once
        if (DisplayOnce && HasBeenDisplayed)
            return false;

        // Check all restrictions
        foreach (var restriction in Restrictions)
        {
            if (!restriction.IsMet())
                return false;
        }

        return true;
    }
}

/// <summary>
/// When to display a description
/// </summary>
public enum DisplayWhen
{
    Always,
    StartOfTurn,
    AfterAction,
    OnEntry,
    OnExamine
}

/// <summary>
/// A restriction/condition that must be met for something to happen
/// </summary>
public class Restriction
{
    public RestrictionType Type { get; set; }
    public string Expression { get; set; } = string.Empty;
    public bool MustBeTrue { get; set; } = true;

    /// <summary>
    /// Evaluates if this restriction is met
    /// </summary>
    public bool IsMet()
    {
        // TODO: Implement expression evaluation
        // For now, always return true
        return true;
    }
}

/// <summary>
/// Types of restrictions
/// </summary>
public enum RestrictionType
{
    Expression,      // General expression that evaluates to true/false
    Location,        // Player must be at a specific location
    ObjectLocation,  // Object must be at a specific location
    Property,        // Property must have a specific value
    Task,           // Task must be complete/incomplete
    Variable        // Variable must have a specific value
}
