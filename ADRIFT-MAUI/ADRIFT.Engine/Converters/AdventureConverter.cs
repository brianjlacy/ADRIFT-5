using ADRIFT.Core.Models;
using ADRIFT.Engine.FileIO;

namespace ADRIFT.Engine.Converters;

/// <summary>
/// Converts between AdventureData (TAF format) and Adventure (MAUI models)
/// Provides bidirectional conversion to maintain 100% backward compatibility
/// </summary>
public static class AdventureConverter
{
    /// <summary>
    /// Converts AdventureData (from TAF file) to Adventure (MAUI model)
    /// </summary>
    public static Adventure FromAdventureData(AdventureData data)
    {
        var adventure = new Adventure
        {
            Title = data.Title,
            Author = data.Author,
            Description = data.IntroductionText,
            Version = data.Version.ToString("F6"),
            Created = data.Created,
            Modified = data.LastUpdated,
            IntroductionText = data.IntroductionText,
            WinText = data.WinningText
        };

        // Convert locations
        foreach (var locData in data.Locations)
        {
            var location = ConvertLocation(locData);
            adventure.Locations[location.Key] = location;
        }

        // Convert objects
        foreach (var objData in data.Objects)
        {
            var obj = ConvertObject(objData);
            adventure.Objects[obj.Key] = obj;
        }

        // Convert characters
        foreach (var charData in data.Characters)
        {
            var character = ConvertCharacter(charData);
            adventure.Characters[character.Key] = character;
        }

        // Convert tasks
        foreach (var taskData in data.Tasks)
        {
            var task = ConvertTask(taskData);
            adventure.Tasks[task.Key] = task;
        }

        // Convert events
        foreach (var eventData in data.Events)
        {
            var evt = ConvertEvent(eventData);
            adventure.Events[evt.Key] = evt;
        }

        // Convert variables
        foreach (var varData in data.Variables)
        {
            var variable = ConvertVariable(varData);
            adventure.Variables[variable.Key] = variable;
        }

        // Convert groups
        foreach (var groupData in data.Groups)
        {
            var group = ConvertGroup(groupData);
            adventure.Groups[group.Key] = group;
        }

        // Convert hints
        foreach (var hintData in data.Hints)
        {
            var hint = ConvertHint(hintData);
            adventure.Hints[hint.Key] = hint;
        }

        // Convert synonyms
        foreach (var synData in data.Synonyms)
        {
            var synonym = ConvertSynonym(synData);
            adventure.Synonyms[synonym.Key] = synonym;
        }

        return adventure;
    }

    /// <summary>
    /// Converts Adventure (MAUI model) back to AdventureData (TAF format)
    /// </summary>
    public static AdventureData ToAdventureData(Adventure adventure)
    {
        var data = new AdventureData
        {
            Title = adventure.Title,
            Author = adventure.Author,
            Version = double.TryParse(adventure.Version, out double ver) ? ver : 5.0,
            Created = adventure.Created,
            LastUpdated = adventure.Modified,
            IntroductionText = adventure.IntroductionText,
            WinningText = adventure.WinText
        };

        // Convert locations back
        foreach (var location in adventure.Locations.Values)
        {
            data.Locations.Add(ConvertLocationBack(location));
        }

        // Convert objects back
        foreach (var obj in adventure.Objects.Values)
        {
            data.Objects.Add(ConvertObjectBack(obj));
        }

        // Convert characters back
        foreach (var character in adventure.Characters.Values)
        {
            data.Characters.Add(ConvertCharacterBack(character));
        }

        // Convert tasks back
        foreach (var task in adventure.Tasks.Values)
        {
            data.Tasks.Add(ConvertTaskBack(task));
        }

        // Convert events back
        foreach (var evt in adventure.Events.Values)
        {
            data.Events.Add(ConvertEventBack(evt));
        }

        // Convert variables back
        foreach (var variable in adventure.Variables.Values)
        {
            data.Variables.Add(ConvertVariableBack(variable));
        }

        // Convert groups back
        foreach (var group in adventure.Groups.Values)
        {
            data.Groups.Add(ConvertGroupBack(group));
        }

        // Convert hints back
        foreach (var hint in adventure.Hints.Values)
        {
            data.Hints.Add(ConvertHintBack(hint));
        }

        // Convert synonyms back
        foreach (var synonym in adventure.Synonyms.Values)
        {
            data.Synonyms.Add(ConvertSynonymBack(synonym));
        }

        return data;
    }

    // Forward conversion methods (TAF -> MAUI)
    private static Core.Models.Location ConvertLocation(LocationData data)
    {
        return new Core.Models.Location
        {
            Key = data.Key,
            ShortDescription = data.ShortDescription,
            LongDescription = data.LongDescription,
            HideOnMap = data.HideOnMap,
            Directions = data.Directions.Select(d => new Direction
            {
                DirectionName = d.Direction,
                DestinationKey = d.DestinationKey
            }).ToList(),
            Properties = new Dictionary<string, string>(data.Properties)
        };
    }

    private static AdriftObject ConvertObject(ObjectData data)
    {
        return new AdriftObject
        {
            Key = data.Key,
            Name = data.Name,
            Article = data.Article,
            Prefix = data.Prefix,
            Aliases = new List<string>(data.Aliases),
            Description = data.Description,
            Size = (ObjectSize)data.Size,
            Weight = (ObjectWeight)data.Weight,
            IsStatic = data.IsStatic,
            IsContainer = data.IsContainer,
            IsSurface = data.IsSurface,
            IsWearable = data.IsWearable,
            IsEdible = data.IsEdible,
            IsLightSource = data.IsLightSource,
            IsReadable = data.IsReadable,
            Capacity = data.Capacity,
            IsOpenable = data.IsOpenable,
            IsLockable = data.IsLockable,
            IsOpen = data.IsOpen,
            IsLocked = data.IsLocked,
            ReadableText = data.ReadableText,
            Properties = new Dictionary<string, string>(data.Properties)
        };
    }

    private static Character ConvertCharacter(CharacterData data)
    {
        return new Character
        {
            Key = data.Key,
            Name = data.Name,
            Gender = data.Gender,
            Description = data.Description,
            Inventory = new List<string>(data.Inventory),
            WalkRoute = data.WalkRoute.Select(w => new WalkStep
            {
                LocationKey = w.LocationKey,
                Turns = w.Turns
            }).ToList(),
            GeneralGreeting = data.GeneralGreeting,
            Topics = data.Topics.Select(t => new ConversationTopic
            {
                Keyword = t.Keyword,
                Response = t.Response
            }).ToList(),
            Properties = new Dictionary<string, string>(data.Properties)
        };
    }

    private static Core.Models.Task ConvertTask(TaskData data)
    {
        return new Core.Models.Task
        {
            Key = data.Key,
            Description = data.Description,
            Commands = data.Commands.Select(cmd => new TaskCommand { Command = cmd }).ToList(),
            SuccessMessage = data.CompletionMessage,
            ScoreValue = data.Score,
            IsRepeatable = data.RepeatableRestriction
        };
    }

    private static Event ConvertEvent(EventData data)
    {
        return new Event
        {
            Key = data.Key,
            Description = data.Description,
            EventType = data.EventType,
            DelayTurns = data.DelayTurns,
            RepeatInterval = data.RepeatInterval,
            IsRepeating = data.IsRepeating,
            ParentEventKey = data.ParentEventKey,
            SubEventKeys = new List<string>(data.SubEventKeys)
        };
    }

    private static Variable ConvertVariable(VariableData data)
    {
        return new Variable
        {
            Key = data.Key,
            Name = data.Name,
            Type = data.Type,
            IntValue = data.IntValue,
            StringValue = data.StringValue,
            BoolValue = data.BoolValue
        };
    }

    private static Group ConvertGroup(GroupData data)
    {
        return new Group
        {
            Key = data.Key,
            Name = data.Name,
            GroupType = data.GroupType,
            Members = new List<string>(data.Members)
        };
    }

    private static Hint ConvertHint(HintData data)
    {
        return new Hint
        {
            Key = data.Key,
            Question = data.Question,
            HintTexts = new List<string>(data.HintTexts)
        };
    }

    private static Synonym ConvertSynonym(SynonymData data)
    {
        return new Synonym
        {
            Key = data.Key,
            CommonName = data.CommonName,
            Replacements = new List<string>(data.Synonyms)
        };
    }

    // Backward conversion methods (MAUI -> TAF)
    private static LocationData ConvertLocationBack(Core.Models.Location location)
    {
        return new LocationData
        {
            Key = location.Key,
            ShortDescription = location.ShortDescription,
            LongDescription = location.LongDescription,
            HideOnMap = location.HideOnMap,
            Directions = location.Directions.Select(d => new DirectionData
            {
                Direction = d.DirectionName,
                DestinationKey = d.DestinationKey
            }).ToList(),
            Properties = new Dictionary<string, string>(location.Properties)
        };
    }

    private static ObjectData ConvertObjectBack(AdriftObject obj)
    {
        return new ObjectData
        {
            Key = obj.Key,
            Name = obj.Name,
            Article = obj.Article,
            Prefix = obj.Prefix,
            Aliases = new List<string>(obj.Aliases),
            Description = obj.Description,
            Size = (int)obj.Size,
            Weight = (int)obj.Weight,
            IsStatic = obj.IsStatic,
            IsContainer = obj.IsContainer,
            IsSurface = obj.IsSurface,
            IsWearable = obj.IsWearable,
            IsEdible = obj.IsEdible,
            IsLightSource = obj.IsLightSource,
            IsReadable = obj.IsReadable,
            Capacity = obj.Capacity,
            IsOpenable = obj.IsOpenable,
            IsLockable = obj.IsLockable,
            IsOpen = obj.IsOpen,
            IsLocked = obj.IsLocked,
            ReadableText = obj.ReadableText,
            Properties = new Dictionary<string, string>(obj.Properties)
        };
    }

    private static CharacterData ConvertCharacterBack(Character character)
    {
        return new CharacterData
        {
            Key = character.Key,
            Name = character.Name,
            Gender = character.Gender,
            Description = character.Description,
            Inventory = new List<string>(character.Inventory),
            WalkRoute = character.WalkRoute.Select(w => new WalkStepData
            {
                LocationKey = w.LocationKey,
                Turns = w.Turns
            }).ToList(),
            GeneralGreeting = character.GeneralGreeting,
            Topics = character.Topics.Select(t => new TopicData
            {
                Keyword = t.Keyword,
                Response = t.Response
            }).ToList(),
            Properties = new Dictionary<string, string>(character.Properties)
        };
    }

    private static TaskData ConvertTaskBack(Core.Models.Task task)
    {
        return new TaskData
        {
            Key = task.Key,
            Description = task.Description,
            Commands = task.Commands.Select(cmd => cmd.Command).ToList(),
            CompletionMessage = task.SuccessMessage,
            Score = task.ScoreValue,
            RepeatableRestriction = task.IsRepeatable
        };
    }

    private static EventData ConvertEventBack(Event evt)
    {
        return new EventData
        {
            Key = evt.Key,
            Description = evt.Description,
            EventType = evt.EventType,
            DelayTurns = evt.DelayTurns,
            RepeatInterval = evt.RepeatInterval,
            IsRepeating = evt.IsRepeating,
            ParentEventKey = evt.ParentEventKey,
            SubEventKeys = new List<string>(evt.SubEventKeys)
        };
    }

    private static VariableData ConvertVariableBack(Variable variable)
    {
        return new VariableData
        {
            Key = variable.Key,
            Name = variable.Name,
            Type = variable.Type,
            IntValue = variable.IntValue,
            StringValue = variable.StringValue,
            BoolValue = variable.BoolValue,
            InitialValue = variable.Type switch
            {
                "Integer" => variable.IntValue.ToString(),
                "Text" => variable.StringValue,
                "Boolean" => variable.BoolValue ? "1" : "0",
                _ => "0"
            }
        };
    }

    private static GroupData ConvertGroupBack(Group group)
    {
        return new GroupData
        {
            Key = group.Key,
            Name = group.Name,
            GroupType = group.GroupType,
            Members = new List<string>(group.Members)
        };
    }

    private static HintData ConvertHintBack(Hint hint)
    {
        return new HintData
        {
            Key = hint.Key,
            Question = hint.Question,
            HintTexts = new List<string>(hint.HintTexts)
        };
    }

    private static SynonymData ConvertSynonymBack(Synonym synonym)
    {
        return new SynonymData
        {
            Key = synonym.Key,
            CommonName = synonym.CommonName,
            Synonyms = new List<string>(synonym.Replacements)
        };
    }
}
