namespace ADRIFT.Core.Models;

public class Task : AdriftItem
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public TaskType Type { get; set; } = TaskType.General;
    public int Priority { get; set; } = 5;

    // Commands
    public List<TaskCommand> Commands { get; set; } = new();

    // Restrictions
    public List<string> Restrictions { get; set; } = new();

    // Actions
    public List<TaskAction> SuccessActions { get; set; } = new();
    public List<TaskAction> FailureActions { get; set; } = new();

    // Output
    public string SuccessMessage { get; set; } = string.Empty;
    public string FailureMessage { get; set; } = string.Empty;

    // Advanced
    public bool IsRepeatable { get; set; }
    public int ScoreValue { get; set; }

    public override string DisplayName => Name;
    public override string ItemType => "Task";
}

public enum TaskType
{
    General,
    System,
    Specific
}

public class TaskCommand
{
    public string Command { get; set; } = string.Empty;
    public List<string> Synonyms { get; set; } = new();
}

public class TaskAction
{
    public string ActionType { get; set; } = string.Empty;
    public Dictionary<string, string> Parameters { get; set; } = new();
}
