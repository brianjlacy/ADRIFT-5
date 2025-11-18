using System.IO.Compression;
using System.Xml;
using System.Xml.Serialization;
using ADRIFT.Core.Models;

namespace ADRIFT.Core.IO;

/// <summary>
/// Handles loading and saving ADRIFT adventures to/from files.
/// Supports both XML format and compressed TAF format for compatibility.
/// </summary>
public static class AdventureFileIO
{
    private const string XML_HEADER = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>";
    private const string TAF_MAGIC = "TAF";
    private const int CURRENT_VERSION = 5000036; // ADRIFT 5.0.36

    /// <summary>
    /// Load an adventure from a file (.taf or .xml)
    /// </summary>
    public static async Task<Adventure> LoadAdventureAsync(string filePath)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Adventure file not found: {filePath}");

        // Read file bytes
        var fileBytes = await File.ReadAllBytesAsync(filePath);

        // Check if it's a compressed TAF file
        if (fileBytes.Length >= 3 &&
            fileBytes[0] == 'T' && fileBytes[1] == 'A' && fileBytes[2] == 'F')
        {
            return await LoadCompressedTafAsync(fileBytes);
        }
        else
        {
            // Try loading as XML
            return await LoadXmlAsync(filePath);
        }
    }

    /// <summary>
    /// Save an adventure to a file (.taf or .xml)
    /// </summary>
    public static async Task SaveAdventureAsync(Adventure adventure, string filePath, bool compress = true)
    {
        if (compress && filePath.EndsWith(".taf", StringComparison.OrdinalIgnoreCase))
        {
            await SaveCompressedTafAsync(adventure, filePath);
        }
        else
        {
            await SaveXmlAsync(adventure, filePath);
        }
    }

    #region XML Loading/Saving

    private static async Task<Adventure> LoadXmlAsync(string filePath)
    {
        using var stream = File.OpenRead(filePath);
        using var reader = XmlReader.Create(stream, new XmlReaderSettings
        {
            IgnoreWhitespace = true,
            IgnoreComments = true
        });

        var adventure = new Adventure();

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Adventure":
                        await ReadAdventureMetadataAsync(reader, adventure);
                        break;
                    case "Locations":
                        await ReadLocationsAsync(reader, adventure);
                        break;
                    case "Objects":
                        await ReadObjectsAsync(reader, adventure);
                        break;
                    case "Characters":
                        await ReadCharactersAsync(reader, adventure);
                        break;
                    case "Tasks":
                        await ReadTasksAsync(reader, adventure);
                        break;
                    case "Events":
                        await ReadEventsAsync(reader, adventure);
                        break;
                    case "Variables":
                        await ReadVariablesAsync(reader, adventure);
                        break;
                    case "Groups":
                        await ReadGroupsAsync(reader, adventure);
                        break;
                    case "Synonyms":
                        await ReadSynonymsAsync(reader, adventure);
                        break;
                    case "Hints":
                        await ReadHintsAsync(reader, adventure);
                        break;
                }
            }
        }

        return adventure;
    }

    private static async Task SaveXmlAsync(Adventure adventure, string filePath)
    {
        var settings = new XmlWriterSettings
        {
            Indent = true,
            IndentChars = "  ",
            Async = true,
            Encoding = System.Text.Encoding.UTF8
        };

        using var stream = File.Create(filePath);
        using var writer = XmlWriter.Create(stream, settings);

        await writer.WriteStartDocumentAsync();
        await writer.WriteStartElementAsync(null, "AdriftAdventure", null);
        await writer.WriteAttributeStringAsync(null, "version", null, adventure.Version);

        // Write metadata
        await WriteAdventureMetadataAsync(writer, adventure);

        // Write collections
        await WriteLocationsAsync(writer, adventure);
        await WriteObjectsAsync(writer, adventure);
        await WriteCharactersAsync(writer, adventure);
        await WriteTasksAsync(writer, adventure);
        await WriteEventsAsync(writer, adventure);
        await WriteVariablesAsync(writer, adventure);
        await WriteGroupsAsync(writer, adventure);
        await WriteSynonymsAsync(writer, adventure);
        await WriteHintsAsync(writer, adventure);

        await writer.WriteEndElementAsync(); // AdriftAdventure
        await writer.WriteEndDocumentAsync();
    }

    #endregion

    #region TAF Loading/Saving (Compressed)

    private static async Task<Adventure> LoadCompressedTafAsync(byte[] fileBytes)
    {
        // Skip TAF header (first 3 bytes)
        using var compressedStream = new MemoryStream(fileBytes, 3, fileBytes.Length - 3);
        using var decompressedStream = new MemoryStream();

        // Decompress using GZip
        using (var gzipStream = new GZipStream(compressedStream, CompressionMode.Decompress))
        {
            await gzipStream.CopyToAsync(decompressedStream);
        }

        decompressedStream.Position = 0;

        // Parse decompressed XML
        using var reader = XmlReader.Create(decompressedStream);
        var adventure = new Adventure();

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Adventure")
            {
                await ReadAdventureMetadataAsync(reader, adventure);
                break;
            }
        }

        return adventure;
    }

    private static async Task SaveCompressedTafAsync(Adventure adventure, string filePath)
    {
        using var fileStream = File.Create(filePath);

        // Write TAF header
        await fileStream.WriteAsync(new byte[] { (byte)'T', (byte)'A', (byte)'F' });

        // Compress XML content
        using var gzipStream = new GZipStream(fileStream, CompressionLevel.Optimal);
        using var writer = XmlWriter.Create(gzipStream, new XmlWriterSettings
        {
            Indent = false,
            Encoding = System.Text.Encoding.UTF8
        });

        await writer.WriteStartDocumentAsync();
        await writer.WriteStartElementAsync(null, "Adventure", null);

        await WriteAdventureMetadataAsync(writer, adventure);
        await WriteLocationsAsync(writer, adventure);
        await WriteObjectsAsync(writer, adventure);
        await WriteCharactersAsync(writer, adventure);
        await WriteTasksAsync(writer, adventure);
        await WriteEventsAsync(writer, adventure);
        await WriteVariablesAsync(writer, adventure);
        await WriteGroupsAsync(writer, adventure);
        await WriteSynonymsAsync(writer, adventure);
        await WriteHintsAsync(writer, adventure);

        await writer.WriteEndElementAsync();
        await writer.WriteEndDocumentAsync();
    }

    #endregion

    #region Read Methods

    private static async Task ReadAdventureMetadataAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Adventure")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Title":
                        adventure.Title = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Author":
                        adventure.Author = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        adventure.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Version":
                        adventure.Version = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "IntroductionText":
                        adventure.IntroductionText = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "WinText":
                        adventure.WinText = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "LoseText":
                        adventure.LoseText = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "StartLocationKey":
                        adventure.StartLocationKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "PlayerKey":
                        adventure.PlayerKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "MaxScore":
                        adventure.MaxScore = await reader.ReadElementContentAsIntAsync();
                        break;
                }
            }
        }
    }

    private static async Task ReadLocationsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Locations")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Location")
            {
                var location = await ReadLocationAsync(reader);
                adventure.Locations[location.Key] = location;
            }
        }
    }

    private static async Task<Location> ReadLocationAsync(XmlReader reader)
    {
        var location = new Location();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) location.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Location")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "ShortDescription":
                        location.ShortDescription = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "LongDescription":
                        location.LongDescription = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "HideOnMap":
                        location.HideOnMap = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsLibrary":
                        location.IsLibrary = await reader.ReadElementContentAsBooleanAsync();
                        break;
                }
            }
        }

        return location;
    }

    private static async Task ReadObjectsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Objects")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Object")
            {
                var obj = await ReadObjectAsync(reader);
                adventure.Objects[obj.Key] = obj;
            }
        }
    }

    private static async Task<AdriftObject> ReadObjectAsync(XmlReader reader)
    {
        var obj = new AdriftObject();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) obj.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Object")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Article":
                        obj.Article = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Prefix":
                        obj.Prefix = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Name":
                        obj.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "ShortDescription":
                        obj.ShortDescription = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "LongDescription":
                        obj.LongDescription = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "IsStatic":
                        obj.IsStatic = await reader.ReadElementContentAsBooleanAsync();
                        break;
                }
            }
        }

        return obj;
    }

    private static async Task ReadVariablesAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Variables")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Variable")
            {
                var variable = await ReadVariableAsync(reader);
                adventure.Variables[variable.Key] = variable;
            }
        }
    }

    private static async Task<Variable> ReadVariableAsync(XmlReader reader)
    {
        var variable = new Variable();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) variable.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Variable")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Name":
                        variable.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Type":
                        if (Enum.TryParse<VariableType>(await reader.ReadElementContentAsStringAsync(), out var varType))
                            variable.Type = varType;
                        break;
                    case "InitialValue":
                        variable.InitialValue = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "CurrentValue":
                        variable.CurrentValue = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        variable.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                }
            }
        }

        return variable;
    }

    private static async Task ReadGroupsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Groups")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Group")
            {
                var group = await ReadGroupAsync(reader);
                adventure.Groups[group.Key] = group;
            }
        }
    }

    private static async Task<Group> ReadGroupAsync(XmlReader reader)
    {
        var group = new Group();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) group.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Group")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Name":
                        group.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Type":
                        if (Enum.TryParse<GroupType>(await reader.ReadElementContentAsStringAsync(), out var groupType))
                            group.Type = groupType;
                        break;
                    case "Description":
                        group.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "MemberKeys":
                        await ReadMemberKeysAsync(reader, group);
                        break;
                }
            }
        }

        return group;
    }

    private static async Task ReadMemberKeysAsync(XmlReader reader, Group group)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "MemberKeys")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "MemberKey")
            {
                var memberKey = await reader.ReadElementContentAsStringAsync();
                if (!string.IsNullOrEmpty(memberKey))
                    group.MemberKeys.Add(memberKey);
            }
        }
    }

    private static async Task ReadSynonymsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Synonyms")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Synonym")
            {
                var synonym = await ReadSynonymAsync(reader);
                adventure.Synonyms[synonym.Key] = synonym;
            }
        }
    }

    private static async Task<Synonym> ReadSynonymAsync(XmlReader reader)
    {
        var synonym = new Synonym();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) synonym.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Synonym")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "OriginalWord":
                        synonym.OriginalWord = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "SynonymWords":
                        await ReadSynonymWordsAsync(reader, synonym);
                        break;
                }
            }
        }

        return synonym;
    }

    private static async Task ReadSynonymWordsAsync(XmlReader reader, Synonym synonym)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "SynonymWords")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Word")
            {
                var word = await reader.ReadElementContentAsStringAsync();
                if (!string.IsNullOrEmpty(word))
                    synonym.SynonymWords.Add(word);
            }
        }
    }

    private static async Task ReadHintsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Hints")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Hint")
            {
                var hint = await ReadHintAsync(reader);
                adventure.Hints[hint.Key] = hint;
            }
        }
    }

    private static async Task<Hint> ReadHintAsync(XmlReader reader)
    {
        var hint = new Hint();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) hint.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Hint")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Question":
                        hint.Question = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "RelatedTaskKey":
                        hint.RelatedTaskKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "HintTexts":
                        await ReadHintTextsAsync(reader, hint);
                        break;
                }
            }
        }

        return hint;
    }

    private static async Task ReadHintTextsAsync(XmlReader reader, Hint hint)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "HintTexts")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "HintText")
            {
                var hintText = new HintText();

                var orderAttr = reader.GetAttribute("Order");
                if (!string.IsNullOrEmpty(orderAttr) && int.TryParse(orderAttr, out var order))
                    hintText.Order = order;

                hintText.Text = await reader.ReadElementContentAsStringAsync();
                hint.Hints.Add(hintText);
            }
        }
    }

    private static async Task ReadCharactersAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Characters")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Character")
            {
                var character = await ReadCharacterAsync(reader);
                adventure.Characters[character.Key] = character;
            }
        }
    }

    private static async Task<Character> ReadCharacterAsync(XmlReader reader)
    {
        var character = new Character();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) character.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Character")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Prefix":
                        character.Prefix = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Name":
                        character.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        character.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Type":
                        if (Enum.TryParse<CharacterType>(await reader.ReadElementContentAsStringAsync(), out var charType))
                            character.Type = charType;
                        break;
                    case "PersonalityTraits":
                        character.PersonalityTraits = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "InitialLocationKey":
                        character.InitialLocationKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "CanMove":
                        character.CanMove = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "FollowsPlayer":
                        character.FollowsPlayer = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "GeneralGreeting":
                        character.GeneralGreeting = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Aliases":
                        await ReadStringListAsync(reader, character.Aliases, "Aliases", "Alias");
                        break;
                    case "InventoryKeys":
                        await ReadStringListAsync(reader, character.InventoryKeys, "InventoryKeys", "ObjectKey");
                        break;
                    case "WalkRoute":
                        await ReadWalkRouteAsync(reader, character);
                        break;
                    case "Topics":
                        await ReadConversationTopicsAsync(reader, character);
                        break;
                }
            }
        }

        return character;
    }

    private static async Task ReadStringListAsync(XmlReader reader, List<string> list, string containerName, string itemName)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == containerName)
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == itemName)
            {
                var value = await reader.ReadElementContentAsStringAsync();
                if (!string.IsNullOrEmpty(value))
                    list.Add(value);
            }
        }
    }

    private static async Task ReadWalkRouteAsync(XmlReader reader, Character character)
    {
        var hasWalkRoute = reader.GetAttribute("HasWalkRoute");
        if (!string.IsNullOrEmpty(hasWalkRoute))
            character.HasWalkRoute = bool.Parse(hasWalkRoute);

        var loops = reader.GetAttribute("Loops");
        if (!string.IsNullOrEmpty(loops))
            character.WalkLoops = bool.Parse(loops);

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "WalkRoute")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Step")
            {
                var step = new WalkStep();

                var stepNum = reader.GetAttribute("Number");
                if (!string.IsNullOrEmpty(stepNum) && int.TryParse(stepNum, out var num))
                    step.StepNumber = num;

                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Step")
                        break;

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "LocationKey":
                                step.LocationKey = await reader.ReadElementContentAsStringAsync();
                                break;
                            case "Direction":
                                step.Direction = await reader.ReadElementContentAsStringAsync();
                                break;
                            case "DelayTurns":
                                step.DelayTurns = await reader.ReadElementContentAsIntAsync();
                                break;
                        }
                    }
                }

                character.WalkSteps.Add(step);
            }
        }
    }

    private static async Task ReadConversationTopicsAsync(XmlReader reader, Character character)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Topics")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Topic")
            {
                var topic = new ConversationTopic();

                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Topic")
                        break;

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "TopicName":
                                topic.TopicName = await reader.ReadElementContentAsStringAsync();
                                break;
                            case "Keywords":
                                topic.Keywords = await reader.ReadElementContentAsStringAsync();
                                break;
                            case "Response":
                                topic.Response = await reader.ReadElementContentAsStringAsync();
                                break;
                        }
                    }
                }

                character.Topics.Add(topic);
            }
        }
    }

    // Stub methods for complex entities - to be implemented
    private static Task ReadTasksAsync(XmlReader reader, Adventure adventure) => Task.CompletedTask;
    private static Task ReadEventsAsync(XmlReader reader, Adventure adventure) => Task.CompletedTask;

    #endregion

    #region Write Methods

    private static async Task WriteAdventureMetadataAsync(XmlWriter writer, Adventure adventure)
    {
        await writer.WriteStartElementAsync(null, "Metadata", null);

        await writer.WriteElementStringAsync(null, "Title", null, adventure.Title);
        await writer.WriteElementStringAsync(null, "Author", null, adventure.Author);
        await writer.WriteElementStringAsync(null, "Description", null, adventure.Description);
        await writer.WriteElementStringAsync(null, "Version", null, adventure.Version);
        await writer.WriteElementStringAsync(null, "IntroductionText", null, adventure.IntroductionText);
        await writer.WriteElementStringAsync(null, "WinText", null, adventure.WinText);
        await writer.WriteElementStringAsync(null, "LoseText", null, adventure.LoseText);
        await writer.WriteElementStringAsync(null, "StartLocationKey", null, adventure.StartLocationKey);
        await writer.WriteElementStringAsync(null, "PlayerKey", null, adventure.PlayerKey);
        await writer.WriteElementStringAsync(null, "MaxScore", null, adventure.MaxScore.ToString());

        await writer.WriteEndElementAsync(); // Metadata
    }

    private static async Task WriteLocationsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Locations.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Locations", null);

        foreach (var location in adventure.Locations.Values)
        {
            await writer.WriteStartElementAsync(null, "Location", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, location.Key);

            await writer.WriteElementStringAsync(null, "ShortDescription", null, location.ShortDescription);
            await writer.WriteElementStringAsync(null, "LongDescription", null, location.LongDescription);
            if (location.HideOnMap)
                await writer.WriteElementStringAsync(null, "HideOnMap", null, "true");
            if (location.IsLibrary)
                await writer.WriteElementStringAsync(null, "IsLibrary", null, "true");

            await writer.WriteEndElementAsync(); // Location
        }

        await writer.WriteEndElementAsync(); // Locations
    }

    private static async Task WriteObjectsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Objects.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Objects", null);

        foreach (var obj in adventure.Objects.Values)
        {
            await writer.WriteStartElementAsync(null, "Object", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, obj.Key);

            await writer.WriteElementStringAsync(null, "Article", null, obj.Article);
            await writer.WriteElementStringAsync(null, "Prefix", null, obj.Prefix);
            await writer.WriteElementStringAsync(null, "Name", null, obj.Name);
            await writer.WriteElementStringAsync(null, "ShortDescription", null, obj.ShortDescription);
            await writer.WriteElementStringAsync(null, "LongDescription", null, obj.LongDescription);
            if (obj.IsStatic)
                await writer.WriteElementStringAsync(null, "IsStatic", null, "true");

            await writer.WriteEndElementAsync(); // Object
        }

        await writer.WriteEndElementAsync(); // Objects
    }

    private static async Task WriteVariablesAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Variables.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Variables", null);

        foreach (var variable in adventure.Variables.Values)
        {
            await writer.WriteStartElementAsync(null, "Variable", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, variable.Key);

            await writer.WriteElementStringAsync(null, "Name", null, variable.Name);
            await writer.WriteElementStringAsync(null, "Type", null, variable.Type.ToString());
            await writer.WriteElementStringAsync(null, "InitialValue", null, variable.InitialValue);
            await writer.WriteElementStringAsync(null, "CurrentValue", null, variable.CurrentValue);
            if (!string.IsNullOrEmpty(variable.Description))
                await writer.WriteElementStringAsync(null, "Description", null, variable.Description);

            await writer.WriteEndElementAsync(); // Variable
        }

        await writer.WriteEndElementAsync(); // Variables
    }

    private static async Task WriteGroupsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Groups.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Groups", null);

        foreach (var group in adventure.Groups.Values)
        {
            await writer.WriteStartElementAsync(null, "Group", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, group.Key);

            await writer.WriteElementStringAsync(null, "Name", null, group.Name);
            await writer.WriteElementStringAsync(null, "Type", null, group.Type.ToString());
            if (!string.IsNullOrEmpty(group.Description))
                await writer.WriteElementStringAsync(null, "Description", null, group.Description);

            if (group.MemberKeys.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "MemberKeys", null);
                foreach (var memberKey in group.MemberKeys)
                {
                    await writer.WriteElementStringAsync(null, "MemberKey", null, memberKey);
                }
                await writer.WriteEndElementAsync(); // MemberKeys
            }

            await writer.WriteEndElementAsync(); // Group
        }

        await writer.WriteEndElementAsync(); // Groups
    }

    private static async Task WriteSynonymsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Synonyms.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Synonyms", null);

        foreach (var synonym in adventure.Synonyms.Values)
        {
            await writer.WriteStartElementAsync(null, "Synonym", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, synonym.Key);

            await writer.WriteElementStringAsync(null, "OriginalWord", null, synonym.OriginalWord);

            if (synonym.SynonymWords.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "SynonymWords", null);
                foreach (var word in synonym.SynonymWords)
                {
                    await writer.WriteElementStringAsync(null, "Word", null, word);
                }
                await writer.WriteEndElementAsync(); // SynonymWords
            }

            await writer.WriteEndElementAsync(); // Synonym
        }

        await writer.WriteEndElementAsync(); // Synonyms
    }

    private static async Task WriteHintsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Hints.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Hints", null);

        foreach (var hint in adventure.Hints.Values)
        {
            await writer.WriteStartElementAsync(null, "Hint", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, hint.Key);

            await writer.WriteElementStringAsync(null, "Question", null, hint.Question);
            if (!string.IsNullOrEmpty(hint.RelatedTaskKey))
                await writer.WriteElementStringAsync(null, "RelatedTaskKey", null, hint.RelatedTaskKey);

            if (hint.Hints.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "HintTexts", null);
                foreach (var hintText in hint.Hints.OrderBy(h => h.Order))
                {
                    await writer.WriteStartElementAsync(null, "HintText", null);
                    await writer.WriteAttributeStringAsync(null, "Order", null, hintText.Order.ToString());
                    await writer.WriteStringAsync(hintText.Text);
                    await writer.WriteEndElementAsync(); // HintText
                }
                await writer.WriteEndElementAsync(); // HintTexts
            }

            await writer.WriteEndElementAsync(); // Hint
        }

        await writer.WriteEndElementAsync(); // Hints
    }

    private static async Task WriteCharactersAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Characters.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Characters", null);

        foreach (var character in adventure.Characters.Values)
        {
            await writer.WriteStartElementAsync(null, "Character", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, character.Key);

            if (!string.IsNullOrEmpty(character.Prefix))
                await writer.WriteElementStringAsync(null, "Prefix", null, character.Prefix);
            await writer.WriteElementStringAsync(null, "Name", null, character.Name);
            await writer.WriteElementStringAsync(null, "Description", null, character.Description);
            await writer.WriteElementStringAsync(null, "Type", null, character.Type.ToString());

            if (!string.IsNullOrEmpty(character.PersonalityTraits))
                await writer.WriteElementStringAsync(null, "PersonalityTraits", null, character.PersonalityTraits);

            if (!string.IsNullOrEmpty(character.InitialLocationKey))
                await writer.WriteElementStringAsync(null, "InitialLocationKey", null, character.InitialLocationKey);

            if (!character.CanMove)
                await writer.WriteElementStringAsync(null, "CanMove", null, "false");
            if (character.FollowsPlayer)
                await writer.WriteElementStringAsync(null, "FollowsPlayer", null, "true");

            if (!string.IsNullOrEmpty(character.GeneralGreeting))
                await writer.WriteElementStringAsync(null, "GeneralGreeting", null, character.GeneralGreeting);

            // Write Aliases
            if (character.Aliases.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Aliases", null);
                foreach (var alias in character.Aliases)
                {
                    await writer.WriteElementStringAsync(null, "Alias", null, alias);
                }
                await writer.WriteEndElementAsync(); // Aliases
            }

            // Write Inventory
            if (character.InventoryKeys.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "InventoryKeys", null);
                foreach (var objKey in character.InventoryKeys)
                {
                    await writer.WriteElementStringAsync(null, "ObjectKey", null, objKey);
                }
                await writer.WriteEndElementAsync(); // InventoryKeys
            }

            // Write Walk Route
            if (character.HasWalkRoute && character.WalkSteps.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "WalkRoute", null);
                await writer.WriteAttributeStringAsync(null, "HasWalkRoute", null, "true");
                if (character.WalkLoops)
                    await writer.WriteAttributeStringAsync(null, "Loops", null, "true");

                foreach (var step in character.WalkSteps.OrderBy(s => s.StepNumber))
                {
                    await writer.WriteStartElementAsync(null, "Step", null);
                    await writer.WriteAttributeStringAsync(null, "Number", null, step.StepNumber.ToString());

                    await writer.WriteElementStringAsync(null, "LocationKey", null, step.LocationKey);
                    await writer.WriteElementStringAsync(null, "Direction", null, step.Direction);
                    if (step.DelayTurns > 0)
                        await writer.WriteElementStringAsync(null, "DelayTurns", null, step.DelayTurns.ToString());

                    await writer.WriteEndElementAsync(); // Step
                }

                await writer.WriteEndElementAsync(); // WalkRoute
            }

            // Write Conversation Topics
            if (character.Topics.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Topics", null);

                foreach (var topic in character.Topics)
                {
                    await writer.WriteStartElementAsync(null, "Topic", null);

                    await writer.WriteElementStringAsync(null, "TopicName", null, topic.TopicName);
                    await writer.WriteElementStringAsync(null, "Keywords", null, topic.Keywords);
                    await writer.WriteElementStringAsync(null, "Response", null, topic.Response);

                    await writer.WriteEndElementAsync(); // Topic
                }

                await writer.WriteEndElementAsync(); // Topics
            }

            await writer.WriteEndElementAsync(); // Character
        }

        await writer.WriteEndElementAsync(); // Characters
    }

    // Stub write methods for complex entities - to be implemented
    private static Task WriteTasksAsync(XmlWriter writer, Adventure adventure) => Task.CompletedTask;
    private static Task WriteEventsAsync(XmlWriter writer, Adventure adventure) => Task.CompletedTask;

    #endregion
}
