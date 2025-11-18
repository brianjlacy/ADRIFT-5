namespace ADRIFT.Core.Models;

/// <summary>
/// Main adventure container
/// </summary>
public class Adventure
{
    public string Title { get; set; } = "New Adventure";
    public string Author { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Version { get; set; } = "1.0";

    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime Modified { get; set; } = DateTime.Now;

    // Collections
    public Dictionary<string, Location> Locations { get; set; } = new();
    public Dictionary<string, AdriftObject> Objects { get; set; } = new();
    public Dictionary<string, Character> Characters { get; set; } = new();
    public Dictionary<string, Task> Tasks { get; set; } = new();
    public Dictionary<string, Event> Events { get; set; } = new();
    public Dictionary<string, Variable> Variables { get; set; } = new();
    public Dictionary<string, Synonym> Synonyms { get; set; } = new();
    public Dictionary<string, Group> Groups { get; set; } = new();
    public Dictionary<string, Hint> Hints { get; set; } = new();

    // Settings
    public string PlayerKey { get; set; } = string.Empty;
    public string StartLocationKey { get; set; } = string.Empty;
    public int MaxScore { get; set; }
    public string IntroductionText { get; set; } = string.Empty;
    public string WinText { get; set; } = string.Empty;
    public string LoseText { get; set; } = string.Empty;

    public int TotalItems => Locations.Count + Objects.Count + Characters.Count +
                             Tasks.Count + Events.Count + Variables.Count +
                             Synonyms.Count + Groups.Count + Hints.Count;
}
