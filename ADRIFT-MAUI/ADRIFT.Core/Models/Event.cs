namespace ADRIFT.Core.Models;

/// <summary>
/// Represents a timed or triggered event in the adventure
/// Full ADRIFT 5 compatibility implementation
/// </summary>
public class Event : AdriftItem
{
    // Core identity
    public string Name { get; set; } = string.Empty;
    public Description Description { get; set; } = new();

    // When to start
    public WhenStartEvent WhenStart { get; set; } = WhenStartEvent.Immediately;

    /// <summary>
    /// Delay before starting (in turns or seconds)
    /// </summary>
    public int StartDelay { get; set; }

    /// <summary>
    /// Random component to start delay
    /// </summary>
    public int StartDelayRandom { get; set; }

    /// <summary>
    /// For BetweenXandYTurns start type
    /// </summary>
    public int StartDelayMin { get; set; }
    public int StartDelayMax { get; set; }

    /// <summary>
    /// Task that must complete to start event (for AfterATask start type)
    /// </summary>
    public string? TriggerTaskKey { get; set; }

    // How long the event runs
    public int Length { get; set; } // Duration in turns or seconds (-1 = infinite)

    // What triggered this event (for display)
    public string WhatSetOff { get; set; } = string.Empty;

    // Measurement unit
    public MeasureType Measure { get; set; } = MeasureType.Turns;

    // Current status
    public EventStatus Status { get; set; } = EventStatus.NotYetStarted;

    // Display settings
    public WhenShowEvent WhenShow { get; set; } = WhenShowEvent.Always;

    // Sub-events (things that happen during the event)
    public List<SubEvent> SubEvents { get; set; } = new();

    // Look text (text shown when player looks)
    public Description? LookText { get; set; }

    // Tracking
    public int CurrentTurn { get; set; } // Current turn within event
    public int TurnStarted { get; set; } // Game turn when event started
    public bool HasStarted => Status != EventStatus.NotYetStarted;

    // Properties (integrated with property system)
    public Dictionary<string, Property> Properties { get; set; } = new();

    public override string DisplayName => Name;
    public override string ItemType => "Event";
}

/// <summary>
/// When the event starts
/// </summary>
public enum WhenStartEvent
{
    Immediately, // Start at game start
    AfterATask, // Start after a task completes
    BetweenXandYTurns // Start randomly between X and Y turns
}

/// <summary>
/// Event status
/// </summary>
public enum EventStatus
{
    NotYetStarted,
    CountingDownToStart,
    Running,
    Finished,
    Paused
}

/// <summary>
/// Time measurement
/// </summary>
public enum MeasureType
{
    Turns,
    Seconds
}

/// <summary>
/// When to show event description
/// </summary>
public enum WhenShowEvent
{
    Always, // Show every turn
    StartOnly, // Show only when event starts
    Never // Never show automatically
}

/// <summary>
/// Sub-event that occurs at a specific time during the event
/// </summary>
public class SubEvent
{
    /// <summary>
    /// When this sub-event occurs (turn/second number within event)
    /// </summary>
    public int When { get; set; }

    /// <summary>
    /// Random component to when
    /// </summary>
    public int WhenRandom { get; set; }

    /// <summary>
    /// Description/message to display
    /// </summary>
    public Description Description { get; set; } = new();

    /// <summary>
    /// Actions to execute
    /// </summary>
    public List<Action> Actions { get; set; } = new();

    /// <summary>
    /// Repeat this sub-event?
    /// </summary>
    public bool Repeating { get; set; }

    /// <summary>
    /// Repeat every N turns/seconds (if Repeating = true)
    /// </summary>
    public int RepeatInterval { get; set; } = 1;

    /// <summary>
    /// Has this sub-event occurred?
    /// </summary>
    public bool HasOccurred { get; set; }

    /// <summary>
    /// Last turn/second this sub-event occurred
    /// </summary>
    public int LastOccurred { get; set; }
}
