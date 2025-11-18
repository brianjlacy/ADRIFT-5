namespace ADRIFT.Services;

public interface IAdventureService
{
    // Adventure management
    Task<clsAdventure?> LoadAdventureAsync(string filePath);
    Task<bool> SaveAdventureAsync(clsAdventure adventure, string filePath);
    Task<clsAdventure> CreateNewAdventureAsync();

    // Current adventure
    clsAdventure? CurrentAdventure { get; }
    event EventHandler<AdventureChangedEventArgs>? AdventureChanged;

    // Item management
    Task<clsLocation> CreateLocationAsync(string name);
    Task<clsObject> CreateObjectAsync(string name);
    Task<clsTask> CreateTaskAsync(string name);
    Task<clsCharacter> CreateCharacterAsync(string name);
    Task<clsEvent> CreateEventAsync(string name);
    Task<clsVariable> CreateVariableAsync(string name);

    // Queries
    Task<IEnumerable<clsLocation>> GetLocationsAsync();
    Task<IEnumerable<clsObject>> GetObjectsAsync();
    Task<IEnumerable<clsTask>> GetTasksAsync();
    Task<IEnumerable<clsCharacter>> GetCharactersAsync();
    Task<IEnumerable<clsEvent>> GetEventsAsync();
    Task<IEnumerable<clsVariable>> GetVariablesAsync();

    // Statistics
    Task<AdventureStatistics> GetStatisticsAsync();
}

public class AdventureChangedEventArgs : EventArgs
{
    public clsAdventure? Adventure { get; init; }
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
