using ADRIFT.Core.Models;

namespace ADRIFT.Core.Testing;

/// <summary>
/// Generates comprehensive sample adventure data for testing serialization and features
/// </summary>
public static class SampleDataGenerator
{
    public static Adventure CreateSampleAdventure()
    {
        var adventure = new Adventure
        {
            Title = "The Mystery Mansion",
            Author = "ADRIFT Developer",
            Version = "1.0",
            Created = DateTime.Now,
            Modified = DateTime.Now,
            Introduction = "You stand before a mysterious old mansion on a dark and stormy night...",
            MaxScore = 100
        };

        // Create locations with directions
        CreateLocations(adventure);

        // Create objects with various properties
        CreateObjects(adventure);

        // Create characters with conversations and walk routes
        CreateCharacters(adventure);

        // Create tasks with commands and actions
        CreateTasks(adventure);

        // Create events with triggers
        CreateEvents(adventure);

        // Create variables of different types
        CreateVariables(adventure);

        // Create groups
        CreateGroups(adventure);

        // Create synonyms
        CreateSynonyms(adventure);

        // Create hints
        CreateHints(adventure);

        return adventure;
    }

    private static void CreateLocations(Adventure adventure)
    {
        // Entrance Hall
        var entrance = new Location
        {
            Key = "Loc00001",
            ShortDescription = "Entrance Hall",
            LongDescription = "A grand entrance hall with a sweeping staircase. Dusty portraits line the walls, their subjects seeming to watch your every move. Doors lead north and east, and a staircase leads up.",
            HideOnMap = false
        };
        entrance.Directions.Add(new Direction { DirectionName = "North", DestinationKey = "Loc00002" });
        entrance.Directions.Add(new Direction { DirectionName = "East", DestinationKey = "Loc00003" });
        entrance.Directions.Add(new Direction { DirectionName = "Up", DestinationKey = "Loc00004" });
        adventure.Locations.Add(entrance.Key, entrance);

        // Library
        var library = new Location
        {
            Key = "Loc00002",
            ShortDescription = "Library",
            LongDescription = "Floor-to-ceiling bookshelves dominate this musty room. A leather armchair sits by an unlit fireplace.",
            HideOnMap = false
        };
        library.Directions.Add(new Direction { DirectionName = "South", DestinationKey = "Loc00001" });
        library.Directions.Add(new Direction
        {
            DirectionName = "West",
            DestinationKey = "Loc00005",
            RestrictionDescription = "A heavy iron door blocks the way. It appears to be locked."
        });
        adventure.Locations.Add(library.Key, library);

        // Dining Room
        var dining = new Location
        {
            Key = "Loc00003",
            ShortDescription = "Dining Room",
            LongDescription = "A long dining table stretches the length of this room. Cobwebs hang from the chandelier overhead.",
            HideOnMap = false
        };
        dining.Directions.Add(new Direction { DirectionName = "West", DestinationKey = "Loc00001" });
        adventure.Locations.Add(dining.Key, dining);

        // Upper Hallway
        var upper = new Location
        {
            Key = "Loc00004",
            ShortDescription = "Upper Hallway",
            LongDescription = "A dimly lit hallway on the second floor. Doors lead to various bedrooms, and the staircase leads back down.",
            HideOnMap = false
        };
        upper.Directions.Add(new Direction { DirectionName = "Down", DestinationKey = "Loc00001" });
        adventure.Locations.Add(upper.Key, upper);

        // Secret Room
        var secret = new Location
        {
            Key = "Loc00005",
            ShortDescription = "Secret Study",
            LongDescription = "A hidden study filled with ancient books and mysterious artifacts. This is where the mansion's secrets were kept.",
            HideOnMap = true
        };
        secret.Directions.Add(new Direction { DirectionName = "East", DestinationKey = "Loc00002" });
        adventure.Locations.Add(secret.Key, secret);
    }

    private static void CreateObjects(Adventure adventure)
    {
        // Brass Key - simple object
        var key = new AdriftObject
        {
            Key = "Obj00001",
            Article = "a",
            Prefix = "brass",
            Name = "key",
            Aliases = new List<string> { "brass key", "small key" },
            ShortDescription = "a small brass key",
            LongDescription = "A small brass key with intricate engravings.",
            LocationType = ObjectLocation.AtLocation,
            LocationKey = "Loc00003",
            Size = ObjectSize.Tiny,
            Weight = 0.1,
            IsStatic = false
        };
        adventure.Objects.Add(key.Key, key);

        // Wooden Chest - container object
        var chest = new AdriftObject
        {
            Key = "Obj00002",
            Article = "a",
            Prefix = "wooden",
            Name = "chest",
            Aliases = new List<string> { "box", "trunk" },
            ShortDescription = "an old wooden chest",
            LongDescription = "An old wooden chest bound with iron straps.",
            LocationType = ObjectLocation.AtLocation,
            LocationKey = "Loc00002",
            Size = ObjectSize.Large,
            Weight = 20.0,
            IsStatic = true,
            IsContainer = true,
            IsOpenable = true,
            IsLockable = true,
            IsOpen = false,
            IsLocked = true,
            Capacity = 15
        };
        adventure.Objects.Add(chest.Key, chest);

        // Ancient Book - readable object
        var book = new AdriftObject
        {
            Key = "Obj00003",
            Article = "an",
            Prefix = "ancient",
            Name = "book",
            Aliases = new List<string> { "tome", "volume" },
            ShortDescription = "an ancient leather-bound book",
            LongDescription = "A heavy tome bound in cracked leather. The title reads 'Secrets of the Mansion'.",
            LocationType = ObjectLocation.InsideObject,
            ContainerKey = "Obj00002",
            Size = ObjectSize.Small,
            Weight = 2.0,
            IsReadable = true,
            ReadingText = "The book contains cryptic passages about hidden rooms and ancient rituals. One passage mentions a brass key hidden in the dining room."
        };
        adventure.Objects.Add(book.Key, book);

        // Lantern - light source
        var lantern = new AdriftObject
        {
            Key = "Obj00004",
            Article = "a",
            Name = "lantern",
            ShortDescription = "an old lantern",
            LongDescription = "An old oil lantern with a glass globe.",
            LocationType = ObjectLocation.AtLocation,
            LocationKey = "Loc00001",
            Size = ObjectSize.Small,
            Weight = 1.5,
            IsLightSource = true
        };
        adventure.Objects.Add(lantern.Key, lantern);

        // Apple - edible object
        var apple = new AdriftObject
        {
            Key = "Obj00005",
            Article = "an",
            Name = "apple",
            ShortDescription = "a red apple",
            LongDescription = "A perfectly preserved red apple that looks somehow fresh despite the mansion's age.",
            LocationType = ObjectLocation.AtLocation,
            LocationKey = "Loc00003",
            Size = ObjectSize.Tiny,
            Weight = 0.2,
            IsEdible = true
        };
        adventure.Objects.Add(apple.Key, apple);

        // Cloak - wearable object
        var cloak = new AdriftObject
        {
            Key = "Obj00006",
            Article = "a",
            Prefix = "velvet",
            Name = "cloak",
            ShortDescription = "a velvet cloak",
            LongDescription = "A rich velvet cloak in deep purple.",
            LocationType = ObjectLocation.AtLocation,
            LocationKey = "Loc00004",
            Size = ObjectSize.Normal,
            Weight = 1.0,
            IsWearable = true
        };
        adventure.Objects.Add(cloak.Key, cloak);
    }

    private static void CreateCharacters(Adventure adventure)
    {
        // Butler - NPC with conversation topics
        var butler = new Character
        {
            Key = "Char0001",
            Prefix = "the",
            Name = "butler",
            Aliases = new List<string> { "servant", "old man" },
            Description = "An elderly butler in formal attire. His eyes seem to hold many secrets.",
            Type = CharacterType.NPC,
            PersonalityTraits = "Mysterious, formal, knowledgeable",
            InitialLocationKey = "Loc00001",
            CanMove = true,
            FollowsPlayer = false,
            GeneralGreeting = "Good evening. Welcome to the mansion."
        };

        butler.Topics.Add("topic1", new Topic
        {
            TopicName = "Mansion",
            Keywords = new List<string> { "mansion", "house", "building" },
            Response = new Description { Text = "Ah yes, the mansion. It has stood here for over two hundred years. Many strange things have happened within these walls." }
        });

        butler.Topics.Add("topic2", new Topic
        {
            TopicName = "Secret",
            Keywords = new List<string> { "secret", "mystery", "hidden" },
            Response = new Description { Text = "I'm afraid I cannot speak of such matters, sir. Some secrets are best left buried." }
        });

        butler.WalkSteps.Add(new WalkStep
        {
            StepNumber = 1,
            LocationKey = "Loc00002",
            Direction = "North",
            DelayTurns = 3
        });

        butler.WalkSteps.Add(new WalkStep
        {
            StepNumber = 2,
            LocationKey = "Loc00001",
            Direction = "South",
            DelayTurns = 3
        });

        butler.WalkLoops = true;
        butler.HasWalkRoute = true;

        adventure.Characters.Add(butler.Key, butler);

        // Cat - wandering companion
        var cat = new Character
        {
            Key = "Char0002",
            Prefix = "a",
            Name = "black cat",
            Aliases = new List<string> { "cat", "feline", "kitty" },
            Description = "A sleek black cat with gleaming green eyes.",
            Type = CharacterType.Companion,
            InitialLocationKey = "Loc00002",
            CanMove = true,
            FollowsPlayer = false,
            GeneralGreeting = "Meow."
        };

        adventure.Characters.Add(cat.Key, cat);
    }

    private static void CreateTasks(Adventure adventure)
    {
        // Take object task
        var takeTask = new Core.Models.Task
        {
            Key = "Task0001",
            Name = "Take object",
            Description = "Standard task for picking up objects",
            Type = TaskType.System,
            Priority = 5,
            IsRepeatable = true
        };

        takeTask.Commands.Add(new TaskCommand
        {
            Command = "take %object%",
            Synonyms = new List<string> { "get", "pick up", "grab" }
        });

        takeTask.SuccessActions.Add(new TaskAction
        {
            ActionType = "MoveObject",
            Parameters = new Dictionary<string, string>
            {
                { "Object", "%object%" },
                { "Destination", "Inventory" }
            }
        });

        takeTask.SuccessMessage = "You take the %object%.";
        adventure.Tasks.Add(takeTask.Key, takeTask);

        // Unlock chest task
        var unlockTask = new Core.Models.Task
        {
            Key = "Task0002",
            Name = "Unlock chest with key",
            Description = "Use the brass key to unlock the wooden chest",
            Type = TaskType.Specific,
            Priority = 8,
            IsRepeatable = false,
            ScoreValue = 10
        };

        unlockTask.Commands.Add(new TaskCommand
        {
            Command = "unlock chest with key"
        });

        unlockTask.Commands.Add(new TaskCommand
        {
            Command = "use key on chest"
        });

        unlockTask.Restrictions.Add("Player has brass key");
        unlockTask.Restrictions.Add("Chest is locked");

        unlockTask.SuccessActions.Add(new TaskAction
        {
            ActionType = "SetObjectProperty",
            Parameters = new Dictionary<string, string>
            {
                { "Object", "Obj00002" },
                { "Property", "IsLocked" },
                { "Value", "false" }
            }
        });

        unlockTask.SuccessMessage = "You insert the brass key into the lock and turn it. Click! The chest unlocks.";
        unlockTask.FailureMessage = "You don't have the right key.";

        adventure.Tasks.Add(unlockTask.Key, unlockTask);
    }

    private static void CreateEvents(Adventure adventure)
    {
        // Time-based event
        var stormEvent = new Event
        {
            Key = "Event001",
            Name = "Storm intensifies",
            Description = "The storm outside grows stronger",
            Type = EventType.TimeBased,
            Trigger = TriggerType.AfterTime,
            DelayTurns = 10,
            RepeatTurns = 0,
            OutputText = "A flash of lightning illuminates the room, followed by a deafening crack of thunder. The storm is intensifying."
        };

        adventure.Events.Add(stormEvent.Key, stormEvent);

        // Triggered event
        var unlockEvent = new Event
        {
            Key = "Event002",
            Name = "Secret passage revealed",
            Description = "Opening the chest reveals a secret",
            Type = EventType.Triggered,
            Trigger = TriggerType.OnCondition,
            OutputText = "As you open the chest, you hear a grinding sound. A section of the library wall slides aside, revealing a hidden passage!"
        };

        unlockEvent.TriggerConditions.Add("Chest is open");
        unlockEvent.TriggerConditions.Add("Player is in library");

        unlockEvent.Actions.Add(new EventAction
        {
            Order = 1,
            ActionType = "RemoveRestriction",
            Description = "Remove restriction from secret door",
            Parameters = new Dictionary<string, string>
            {
                { "Location", "Loc00002" },
                { "Direction", "West" }
            }
        });

        adventure.Events.Add(unlockEvent.Key, unlockEvent);
    }

    private static void CreateVariables(Adventure adventure)
    {
        // Numeric variable
        var score = new Variable
        {
            Key = "Var00001",
            Name = "Score",
            Type = VariableType.Numeric,
            InitialValue = "0",
            CurrentValue = "0",
            Description = "Player's current score"
        };
        adventure.Variables.Add(score.Key, score);

        // Text variable
        var playerName = new Variable
        {
            Key = "Var00002",
            Name = "PlayerName",
            Type = VariableType.Text,
            InitialValue = "Adventurer",
            CurrentValue = "Adventurer",
            Description = "The player's name"
        };
        adventure.Variables.Add(playerName.Key, playerName);

        // Boolean variable
        var stormActive = new Variable
        {
            Key = "Var00003",
            Name = "StormActive",
            Type = VariableType.Boolean,
            InitialValue = "true",
            CurrentValue = "true",
            Description = "Whether the storm is currently active"
        };
        adventure.Variables.Add(stormActive.Key, stormActive);
    }

    private static void CreateGroups(Adventure adventure)
    {
        // Object group - valuable items
        var valuables = new Group
        {
            Key = "Group001",
            Name = "Valuables",
            Type = GroupType.Objects,
            Description = "Valuable items in the mansion"
        };
        valuables.MemberKeys.Add("Obj00001"); // Brass key
        valuables.MemberKeys.Add("Obj00003"); // Ancient book
        adventure.Groups.Add(valuables.Key, valuables);

        // Location group - ground floor
        var groundFloor = new Group
        {
            Key = "Group002",
            Name = "Ground Floor",
            Type = GroupType.Locations,
            Description = "All rooms on the ground floor"
        };
        groundFloor.MemberKeys.Add("Loc00001"); // Entrance
        groundFloor.MemberKeys.Add("Loc00002"); // Library
        groundFloor.MemberKeys.Add("Loc00003"); // Dining room
        adventure.Groups.Add(groundFloor.Key, groundFloor);
    }

    private static void CreateSynonyms(Adventure adventure)
    {
        var moveSynonyms = new Synonym
        {
            Key = "Syn00001",
            Name = "Movement",
            Description = "Synonyms for movement commands"
        };
        moveSynonyms.Words.Add("go");
        moveSynonyms.Words.Add("walk");
        moveSynonyms.Words.Add("move");
        moveSynonyms.Words.Add("travel");
        adventure.Synonyms.Add(moveSynonyms.Key, moveSynonyms);

        var examineSynonyms = new Synonym
        {
            Key = "Syn00002",
            Name = "Examine",
            Description = "Synonyms for examining things"
        };
        examineSynonyms.Words.Add("examine");
        examineSynonyms.Words.Add("look at");
        examineSynonyms.Words.Add("inspect");
        examineSynonyms.Words.Add("check");
        adventure.Synonyms.Add(examineSynonyms.Key, examineSynonyms);
    }

    private static void CreateHints(Adventure adventure)
    {
        var chestHint = new Hint
        {
            Key = "Hint0001",
            Question = "How do I open the chest?",
            SubtleHint = "You'll need to find something that unlocks it.",
            MediumHint = "Look for a key somewhere in the mansion. Try the dining room.",
            SpoilerHint = "The brass key in the dining room unlocks the chest. Use the command 'unlock chest with key'."
        };
        chestHint.RestrictToTasks.Add("Task0002");
        adventure.Hints.Add(chestHint.Key, chestHint);

        var secretHint = new Hint
        {
            Key = "Hint0002",
            Question = "How do I find the secret room?",
            SubtleHint = "Opening the chest might trigger something interesting.",
            MediumHint = "Make sure you're in the library when you open the chest.",
            SpoilerHint = "Unlock and open the chest while standing in the library. A secret passage will be revealed on the west wall."
        };
        adventure.Hints.Add(secretHint.Key, secretHint);
    }
}
