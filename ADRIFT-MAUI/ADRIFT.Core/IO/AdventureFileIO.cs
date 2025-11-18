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

    // Stub methods for complex entities - to be implemented
    private static Task ReadCharactersAsync(XmlReader reader, Adventure adventure) => Task.CompletedTask;
    private static Task ReadTasksAsync(XmlReader reader, Adventure adventure) => Task.CompletedTask;
    private static Task ReadEventsAsync(XmlReader reader, Adventure adventure) => Task.CompletedTask;
    private static Task ReadHintsAsync(XmlReader reader, Adventure adventure) => Task.CompletedTask;

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

    // Stub write methods for complex entities - to be implemented
    private static Task WriteCharactersAsync(XmlWriter writer, Adventure adventure) => Task.CompletedTask;
    private static Task WriteTasksAsync(XmlWriter writer, Adventure adventure) => Task.CompletedTask;
    private static Task WriteEventsAsync(XmlWriter writer, Adventure adventure) => Task.CompletedTask;
    private static Task WriteHintsAsync(XmlWriter writer, Adventure adventure) => Task.CompletedTask;

    #endregion
}
