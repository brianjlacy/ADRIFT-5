namespace ADRIFT.Core.Models;

public class Character : AdriftItem
{
    public string Prefix { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    public List<string> Aliases { get; set; } = new();

    public string Description { get; set; } = string.Empty;
    public CharacterType Type { get; set; } = CharacterType.NPC;
    public string PersonalityTraits { get; set; } = string.Empty;

    // Location
    public string? InitialLocationKey { get; set; }
    public bool CanMove { get; set; } = true;
    public bool FollowsPlayer { get; set; }

    // Inventory
    public List<string> InventoryKeys { get; set; } = new();

    // Walk route
    public bool HasWalkRoute { get; set; }
    public List<WalkStep> WalkSteps { get; set; } = new();
    public bool WalkLoops { get; set; }

    // Conversation
    public List<ConversationTopic> Topics { get; set; } = new();
    public string GeneralGreeting { get; set; } = string.Empty;
    public string UnknownTopicResponse { get; set; } = string.Empty; // Response when topic not found

    public string FullName => string.IsNullOrWhiteSpace(Prefix) ? Name : $"{Prefix} {Name}";
    public override string DisplayName => FullName;
    public override string ItemType => "Character";
}

public enum CharacterType
{
    NPC,
    Companion,
    Enemy,
    Merchant,
    Guard
}

public class WalkStep
{
    public int StepNumber { get; set; }
    public string LocationKey { get; set; } = string.Empty;
    public string Direction { get; set; } = string.Empty;
    public int DelayTurns { get; set; }
}

public class ConversationTopic
{
    public string TopicName { get; set; } = string.Empty;
    public string Keywords { get; set; } = string.Empty;
    public string Response { get; set; } = string.Empty;
    public string TellResponse { get; set; } = string.Empty; // Response when player tells about this topic
}
