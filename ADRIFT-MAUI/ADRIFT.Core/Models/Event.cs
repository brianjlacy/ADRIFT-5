namespace ADRIFT.Core.Models;

public class Event : AdriftItem
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;

    public EventType Type { get; set; } = EventType.Triggered;
    public TriggerType Trigger { get; set; } = TriggerType.OnCondition;

    // Time-based settings
    public int DelayTurns { get; set; }
    public int RepeatTurns { get; set; }

    // Conditions
    public List<string> TriggerConditions { get; set; } = new();

    // Hierarchy
    public string? ParentEventKey { get; set; }
    public List<string> SubEventKeys { get; set; } = new();

    // Actions
    public List<EventAction> Actions { get; set; } = new();
    public string OutputText { get; set; } = string.Empty;

    public override string DisplayName => Name;
    public override string ItemType => "Event";
}

public enum EventType
{
    TimeBased,
    Triggered,
    Repeating
}

public enum TriggerType
{
    AfterTime,
    OnCondition,
    Immediate
}

public class EventAction
{
    public int Order { get; set; }
    public string ActionType { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public Dictionary<string, string> Parameters { get; set; } = new();
}
