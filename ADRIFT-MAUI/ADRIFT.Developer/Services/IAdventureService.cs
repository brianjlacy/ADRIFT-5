using ADRIFT.Core.Models;

namespace ADRIFT.Services;

public interface IAdventureService
{
    // Adventure management
    Task<Adventure?> LoadAdventureAsync(string filePath);
    Task<bool> SaveAdventureAsync(Adventure adventure, string filePath);
    Task<Adventure> CreateNewAdventureAsync();

    // Current adventure
    Adventure? CurrentAdventure { get; }
    event EventHandler<AdventureChangedEventArgs>? AdventureChanged;

    // Item management
    Task<Location> CreateLocationAsync(string name);
    Task<AdriftObject> CreateObjectAsync(string name);
    Task<Core.Models.Task> CreateTaskAsync(string name);
    Task<Character> CreateCharacterAsync(string name);
    Task<Event> CreateEventAsync(string name);
    Task<Variable> CreateVariableAsync(string name);

    // Queries
    Task<IEnumerable<Location>> GetLocationsAsync();
    Task<IEnumerable<AdriftObject>> GetObjectsAsync();
    Task<IEnumerable<Core.Models.Task>> GetTasksAsync();
    Task<IEnumerable<Character>> GetCharactersAsync();
    Task<IEnumerable<Event>> GetEventsAsync();
    Task<IEnumerable<Variable>> GetVariablesAsync();

    // Statistics
    Task<AdventureStatistics> GetStatisticsAsync();
}

public class AdventureChangedEventArgs : EventArgs
{
    public Adventure? Adventure { get; init; }
    public string? FilePath { get; init; }
}

public record AdventureStatistics
{
    public int LocationCount { get; init; }
    public int ObjectCount { get; init; }
    public int TaskCount { get; init; }
    public int CharacterCount { get; init; }
    public int EventCount { get; init; }
    public int VariableCount { get; init; }
    public int GroupCount { get; init; }
}
