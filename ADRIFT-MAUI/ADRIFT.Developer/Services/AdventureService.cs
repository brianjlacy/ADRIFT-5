using ADRIFT.Core.Models;

namespace ADRIFT.Services;

public class AdventureService : IAdventureService
{
    private Adventure? _currentAdventure;

    public Adventure? CurrentAdventure => _currentAdventure;

    public event EventHandler<AdventureChangedEventArgs>? AdventureChanged;

    public Task<Adventure> CreateNewAdventureAsync()
    {
        var adventure = new Adventure();
        _currentAdventure = adventure;
        AdventureChanged?.Invoke(this, new AdventureChangedEventArgs { Adventure = adventure });
        return Task.FromResult(adventure);
    }

    public async Task<Adventure?> LoadAdventureAsync(string filePath)
    {
        try
        {
            // TODO: Implement file loading using FileIO class
            // var adventure = await Task.Run(() => FileIO.LoadAdventure(filePath));
            // _currentAdventure = adventure;
            // AdventureChanged?.Invoke(this, new AdventureChangedEventArgs
            // {
            //     Adventure = adventure,
            //     FilePath = filePath
            // });
            // return adventure;

            await Task.CompletedTask;
            return null;
        }
        catch (Exception ex)
        {
            // TODO: Proper error handling
            throw new InvalidOperationException($"Failed to load adventure: {ex.Message}", ex);
        }
    }

    public async Task<bool> SaveAdventureAsync(Adventure adventure, string filePath)
    {
        try
        {
            // TODO: Implement file saving using FileIO class
            // await Task.Run(() => FileIO.SaveAdventure(adventure, filePath));
            // return true;

            await Task.CompletedTask;
            return false;
        }
        catch (Exception ex)
        {
            // TODO: Proper error handling
            throw new InvalidOperationException($"Failed to save adventure: {ex.Message}", ex);
        }
    }

    public Task<Location> CreateLocationAsync(string name)
    {
        var location = new Location { Name = name };
        // TODO: Add to current adventure
        return Task.FromResult(location);
    }

    public Task<AdriftObject> CreateObjectAsync(string name)
    {
        var obj = new AdriftObject { Name = name };
        return Task.FromResult(obj);
    }

    public Task<Core.Models.Task> CreateTaskAsync(string name)
    {
        var task = new Core.Models.Task { Command = name };
        return Task.FromResult(task);
    }

    public Task<Character> CreateCharacterAsync(string name)
    {
        var character = new Character { Name = name };
        return Task.FromResult(character);
    }

    public Task<Event> CreateEventAsync(string name)
    {
        var evt = new Event { Description = name };
        return Task.FromResult(evt);
    }

    public Task<Variable> CreateVariableAsync(string name)
    {
        var variable = new Variable { Name = name };
        return Task.FromResult(variable);
    }

    public Task<IEnumerable<Location>> GetLocationsAsync()
    {
        if (_currentAdventure == null)
            return Task.FromResult(Enumerable.Empty<Location>());

        return Task.FromResult(_currentAdventure.Locations.Values.AsEnumerable());
    }

    public Task<IEnumerable<AdriftObject>> GetObjectsAsync()
    {
        if (_currentAdventure == null)
            return Task.FromResult(Enumerable.Empty<AdriftObject>());

        return Task.FromResult(_currentAdventure.Objects.Values.AsEnumerable());
    }

    public Task<IEnumerable<Core.Models.Task>> GetTasksAsync()
    {
        if (_currentAdventure == null)
            return Task.FromResult(Enumerable.Empty<Core.Models.Task>());

        return Task.FromResult(_currentAdventure.Tasks.Values.AsEnumerable());
    }

    public Task<IEnumerable<Character>> GetCharactersAsync()
    {
        if (_currentAdventure == null)
            return Task.FromResult(Enumerable.Empty<Character>());

        return Task.FromResult(_currentAdventure.Characters.Values.AsEnumerable());
    }

    public Task<IEnumerable<Event>> GetEventsAsync()
    {
        if (_currentAdventure == null)
            return Task.FromResult(Enumerable.Empty<Event>());

        return Task.FromResult(_currentAdventure.Events.Values.AsEnumerable());
    }

    public Task<IEnumerable<Variable>> GetVariablesAsync()
    {
        if (_currentAdventure == null)
            return Task.FromResult(Enumerable.Empty<Variable>());

        return Task.FromResult(_currentAdventure.Variables.Values.AsEnumerable());
    }

    public Task<AdventureStatistics> GetStatisticsAsync()
    {
        if (_currentAdventure == null)
        {
            return Task.FromResult(new AdventureStatistics());
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

        return Task.FromResult(stats);
    }
}
