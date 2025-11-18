using ADRIFT.Core.Models;
using ADRIFT.Core.IO;

namespace ADRIFT.Developer.Services;

public class AdventureService : IAdventureService
{
    private Adventure? _currentAdventure;

    public Adventure? CurrentAdventure => _currentAdventure;

    public event EventHandler<AdventureChangedEventArgs>? AdventureChanged;

    public System.Threading.Tasks.Task<Adventure> CreateNewAdventureAsync()
    {
        var adventure = new Adventure
        {
            Title = "New Adventure",
            Author = Environment.UserName,
            Version = "1.0",
            Created = DateTime.Now,
            Modified = DateTime.Now
        };
        _currentAdventure = adventure;
        AdventureChanged?.Invoke(this, new AdventureChangedEventArgs { Adventure = adventure });
        return System.Threading.Tasks.Task.FromResult(adventure);
    }

    public async System.Threading.Tasks.Task<Adventure?> LoadAdventureAsync(string filePath)
    {
        try
        {
            var adventure = await AdventureFileIO.LoadAdventureAsync(filePath);
            _currentAdventure = adventure;
            AdventureChanged?.Invoke(this, new AdventureChangedEventArgs
            {
                Adventure = adventure,
                FilePath = filePath
            });
            return adventure;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to load adventure: {ex.Message}", ex);
        }
    }

    public async System.Threading.Tasks.Task<bool> SaveAdventureAsync(Adventure adventure, string filePath)
    {
        try
        {
            // Update modification timestamp
            adventure.Modified = DateTime.Now;

            // Determine if we should compress based on file extension
            bool compress = filePath.EndsWith(".taf", StringComparison.OrdinalIgnoreCase);

            await AdventureFileIO.SaveAdventureAsync(adventure, filePath, compress);
            return true;
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException($"Failed to save adventure: {ex.Message}", ex);
        }
    }

    public System.Threading.Tasks.Task<Core.Models.Location> CreateLocationAsync(string name)
    {
        var location = new Core.Models.Location { ShortDescription = name };
        // TODO: Add to current adventure
        return System.Threading.Tasks.Task.FromResult(location);
    }

    public System.Threading.Tasks.Task<AdriftObject> CreateObjectAsync(string name)
    {
        var obj = new AdriftObject { Name = name };
        return System.Threading.Tasks.Task.FromResult(obj);
    }

    public System.Threading.Tasks.Task<Core.Models.Task> CreateTaskAsync(string name)
    {
        var task = new Core.Models.Task { Name = name };
        return System.Threading.Tasks.Task.FromResult(task);
    }

    public System.Threading.Tasks.Task<Character> CreateCharacterAsync(string name)
    {
        var character = new Character { Name = name };
        return System.Threading.Tasks.Task.FromResult(character);
    }

    public System.Threading.Tasks.Task<Event> CreateEventAsync(string name)
    {
        var evt = new Event { Description = name };
        return System.Threading.Tasks.Task.FromResult(evt);
    }

    public System.Threading.Tasks.Task<Variable> CreateVariableAsync(string name)
    {
        var variable = new Variable { Name = name };
        return System.Threading.Tasks.Task.FromResult(variable);
    }

    public System.Threading.Tasks.Task<IEnumerable<Core.Models.Location>> GetLocationsAsync()
    {
        if (_currentAdventure == null)
            return System.Threading.Tasks.Task.FromResult(Enumerable.Empty<Core.Models.Location>());

        return System.Threading.Tasks.Task.FromResult(_currentAdventure.Locations.Values.AsEnumerable());
    }

    public System.Threading.Tasks.Task<IEnumerable<AdriftObject>> GetObjectsAsync()
    {
        if (_currentAdventure == null)
            return System.Threading.Tasks.Task.FromResult(Enumerable.Empty<AdriftObject>());

        return System.Threading.Tasks.Task.FromResult(_currentAdventure.Objects.Values.AsEnumerable());
    }

    public System.Threading.Tasks.Task<IEnumerable<Core.Models.Task>> GetTasksAsync()
    {
        if (_currentAdventure == null)
            return System.Threading.Tasks.Task.FromResult(Enumerable.Empty<Core.Models.Task>());

        return System.Threading.Tasks.Task.FromResult(_currentAdventure.Tasks.Values.AsEnumerable());
    }

    public System.Threading.Tasks.Task<IEnumerable<Character>> GetCharactersAsync()
    {
        if (_currentAdventure == null)
            return System.Threading.Tasks.Task.FromResult(Enumerable.Empty<Character>());

        return System.Threading.Tasks.Task.FromResult(_currentAdventure.Characters.Values.AsEnumerable());
    }

    public System.Threading.Tasks.Task<IEnumerable<Event>> GetEventsAsync()
    {
        if (_currentAdventure == null)
            return System.Threading.Tasks.Task.FromResult(Enumerable.Empty<Event>());

        return System.Threading.Tasks.Task.FromResult(_currentAdventure.Events.Values.AsEnumerable());
    }

    public System.Threading.Tasks.Task<IEnumerable<Variable>> GetVariablesAsync()
    {
        if (_currentAdventure == null)
            return System.Threading.Tasks.Task.FromResult(Enumerable.Empty<Variable>());

        return System.Threading.Tasks.Task.FromResult(_currentAdventure.Variables.Values.AsEnumerable());
    }

    public System.Threading.Tasks.Task<AdventureStatistics> GetStatisticsAsync()
    {
        if (_currentAdventure == null)
        {
            return System.Threading.Tasks.Task.FromResult(new AdventureStatistics());
        }

        var stats = new AdventureStatistics
        {
            LocationCount = _currentAdventure.Locations.Count,
            ObjectCount = _currentAdventure.Objects.Count,
            TaskCount = _currentAdventure.Tasks.Count,
            CharacterCount = _currentAdventure.Characters.Count,
            EventCount = _currentAdventure.Events.Count,
            VariableCount = _currentAdventure.Variables.Count,
            GroupCount = _currentAdventure.Groups.Count
        };

        return System.Threading.Tasks.Task.FromResult(stats);
    }
}
