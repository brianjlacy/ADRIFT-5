namespace ADRIFT.Core.Models;

/// <summary>
/// Simplified action for task execution
/// Used for SuccessActions and FailureActions in tasks
/// </summary>
public class TaskAction
{
    /// <summary>
    /// Execution order (lower = earlier)
    /// </summary>
    public int Order { get; set; }

    /// <summary>
    /// Action type (MoveObject, SetVariable, etc.)
    /// </summary>
    public string ActionType { get; set; } = string.Empty;

    /// <summary>
    /// Action parameters
    /// </summary>
    public Dictionary<string, string> Parameters { get; set; } = new();

    /// <summary>
    /// Output text to display
    /// </summary>
    public string OutputText { get; set; } = string.Empty;

    /// <summary>
    /// Delay in turns before executing
    /// </summary>
    public int Delay { get; set; }

    /// <summary>
    /// Parent task key (for reference tracking)
    /// </summary>
    public string? ParentTaskKey { get; set; }
}
