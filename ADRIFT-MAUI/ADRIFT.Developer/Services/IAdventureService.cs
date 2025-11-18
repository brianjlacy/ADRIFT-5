using ADRIFT.Core.Models;

namespace ADRIFT.Developer.Services;

public interface IAdventureService
{
    // Adventure management
    System.Threading.Tasks.Task<Adventure?> LoadAdventureAsync(string filePath);
    System.Threading.Tasks.Task<bool> SaveAdventureAsync(Adventure adventure, string filePath);
    System.Threading.Tasks.Task<Adventure> CreateNewAdventureAsync();

    // Current adventure
    Adventure? CurrentAdventure { get; }
    event EventHandler<AdventureChangedEventArgs>? AdventureChanged;

    // Item management
    System.Threading.Tasks.Task<Core.Models.Location> CreateLocationAsync(string name);
    System.Threading.Tasks.Task<AdriftObject> CreateObjectAsync(string name);
    System.Threading.Tasks.Task<Core.Models.Task> CreateTaskAsync(string name);
    System.Threading.Tasks.Task<Character> CreateCharacterAsync(string name);
    System.Threading.Tasks.Task<Event> CreateEventAsync(string name);
    System.Threading.Tasks.Task<Variable> CreateVariableAsync(string name);

    // Queries
    System.Threading.Tasks.Task<IEnumerable<Core.Models.Location>> GetLocationsAsync();
    System.Threading.Tasks.Task<IEnumerable<AdriftObject>> GetObjectsAsync();
    System.Threading.Tasks.Task<IEnumerable<Core.Models.Task>> GetTasksAsync();
    System.Threading.Tasks.Task<IEnumerable<Character>> GetCharactersAsync();
    System.Threading.Tasks.Task<IEnumerable<Event>> GetEventsAsync();
    System.Threading.Tasks.Task<IEnumerable<Variable>> GetVariablesAsync();

    // Statistics
    System.Threading.Tasks.Task<AdventureStatistics> GetStatisticsAsync();
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
