namespace ADRIFT.Core.Models;

/// <summary>
/// User Defined Function (UDF)
/// Custom functions that can be called from descriptions
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class UserFunction : AdriftItem
{
    /// <summary>
    /// Function name (called as {FunctionName:arg1:arg2:...})
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Function description/purpose
    /// </summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>
    /// Output text/logic (Description with alternates for conditional output)
    /// </summary>
    public Description Output { get; set; } = new();

    /// <summary>
    /// Arguments this function accepts
    /// </summary>
    public List<FunctionArgument> Arguments { get; set; } = new();

    public override string DisplayName => Name;
    public override string ItemType => "UserFunction";
}

/// <summary>
/// Argument for a user function
/// </summary>
public class FunctionArgument
{
    /// <summary>
    /// Argument number (1-based)
    /// </summary>
    public int ArgumentNumber { get; set; }

    /// <summary>
    /// Argument name (for display)
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    /// Type of argument
    /// </summary>
    public FunctionArgumentType Type { get; set; } = FunctionArgumentType.Text;
}

/// <summary>
/// Type of function argument
/// </summary>
public enum FunctionArgumentType
{
    Object,
    Character,
    Location,
    Number,
    Text
}
