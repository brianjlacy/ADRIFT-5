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

        // Check if it's a TAF file by looking at the first 12 bytes
        // TAF files start with an obfuscated version string, not "TAF" header
        if (fileBytes.Length >= 12)
        {
            // Check if it looks like an obfuscated TAF file
            if (TafObfuscator.IsKnownVersionPattern(fileBytes.Take(12).ToArray()) ||
                filePath.EndsWith(".taf", StringComparison.OrdinalIgnoreCase))
            {
                return await LoadTafAsync(fileBytes, filePath);
            }
        }

        // Try loading as XML
        return await LoadXmlAsync(filePath);
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
                    case "Properties":
                        await ReadPropertiesAsync(reader, adventure);
                        break;
                    case "ALRs":
                        await ReadALRsAsync(reader, adventure);
                        break;
                    case "UserFunctions":
                        await ReadUserFunctionsAsync(reader, adventure);
                        break;
                    case "Macros":
                        await ReadMacrosAsync(reader, adventure);
                        break;
                    case "Map":
                        await ReadMapAsync(reader, adventure);
                        break;
                    case "Sounds":
                        await ReadSoundsAsync(reader, adventure);
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
        await WritePropertiesAsync(writer, adventure);
        await WriteALRsAsync(writer, adventure);
        await WriteUserFunctionsAsync(writer, adventure);
        await WriteMacrosAsync(writer, adventure);
        await WriteMapAsync(writer, adventure);
        await WriteSoundsAsync(writer, adventure);

        await writer.WriteEndElementAsync(); // AdriftAdventure
        await writer.WriteEndDocumentAsync();
    }

    #endregion

    #region TAF Loading/Saving (Binary Format)

    /// <summary>
    /// Load a TAF file (ADRIFT 5.0 binary format with obfuscation and ZLib compression)
    /// </summary>
    private static async Task<Adventure> LoadTafAsync(byte[] fileBytes, string filePath)
    {
        if (fileBytes.Length < 26)
            throw new InvalidDataException("File too small to be a valid TAF file");

        using var memStream = new MemoryStream(fileBytes);
        using var reader = new BinaryReader(memStream);

        // Read and deobfuscate version (first 12 bytes)
        var versionBytes = reader.ReadBytes(12);
        var versionString = TafObfuscator.GetVersionString(versionBytes);

        if (!versionString.StartsWith("Version "))
            throw new InvalidDataException("Not a valid ADRIFT TAF file");

        // Parse version number
        var versionNum = double.Parse(versionString.Replace("Version ", ""),
            System.Globalization.CultureInfo.InvariantCulture);

        // Read password from end of file (last 14 bytes: 12 bytes password + 2 bytes CRLF)
        memStream.Seek(fileBytes.Length - 14, SeekOrigin.Begin);
        var passwordBytes = reader.ReadBytes(12);
        var decodedPassword = TafObfuscator.Deobfuscate(passwordBytes, fileBytes.Length - 13);
        var passwordString = System.Text.Encoding.UTF8.GetString(decodedPassword);

        // Reset to after version header
        memStream.Seek(12, SeekOrigin.Begin);

        // Read Babel metadata size (4 hex characters) for version 5.0.20+
        int babelLength = 0;
        string babelMetadata = null;

        if (versionNum >= 5.0)
        {
            var sizeBytes = reader.ReadBytes(4);
            var sizeHex = System.Text.Encoding.UTF8.GetString(sizeBytes);

            if (sizeHex == "0000" || System.Text.Encoding.UTF8.GetString(reader.ReadBytes(4)) == "<ifi")
            {
                // Has Babel metadata or is newer format
                memStream.Seek(12, SeekOrigin.Begin);
                sizeBytes = reader.ReadBytes(4);
                sizeHex = System.Text.Encoding.UTF8.GetString(sizeBytes);

                if (IsHexString(sizeHex))
                {
                    babelLength = Convert.ToInt32(sizeHex, 16);
                    if (babelLength > 0 && babelLength < fileBytes.Length)
                    {
                        var babelBytes = reader.ReadBytes(babelLength);
                        babelMetadata = System.Text.Encoding.UTF8.GetString(babelBytes);
                    }
                    babelLength += 4; // Include size header
                }
            }
            else
            {
                // Pre-5.0.20 format, no metadata
                memStream.Seek(12, SeekOrigin.Begin);
            }
        }

        // Calculate compressed data position and length
        int dataStart = 12 + babelLength;
        int dataLength = (int)(fileBytes.Length - dataStart - 26); // Subtract footer (14 bytes + some padding)

        if (dataLength <= 0)
            throw new InvalidDataException("Invalid TAF file structure");

        // Read compressed and obfuscated data
        memStream.Seek(dataStart, SeekOrigin.Begin);
        var compressedObfuscatedData = reader.ReadBytes(dataLength);

        // Deobfuscate the compressed data
        var deobfuscated = TafObfuscator.Deobfuscate(compressedObfuscatedData, dataStart + 1);

        // Decompress using ZLib
        using var deobfuscatedStream = new MemoryStream(deobfuscated);
        using var zlibStream = new ZLibStream(deobfuscatedStream, CompressionMode.Decompress);
        using var decompressedStream = new MemoryStream();

        await zlibStream.CopyToAsync(decompressedStream);
        decompressedStream.Position = 0;

        // Parse the decompressed XML
        var adventure = await LoadXmlFromStreamAsync(decompressedStream);

        // Set adventure metadata
        adventure.Version = versionString.Replace("Version ", "");
        adventure.FullPath = filePath;
        adventure.Filename = Path.GetFileName(filePath);

        return adventure;
    }

    /// <summary>
    /// Save a TAF file (ADRIFT 5.0 binary format with obfuscation and ZLib compression)
    /// </summary>
    private static async Task SaveCompressedTafAsync(Adventure adventure, string filePath)
    {
        using var fileStream = File.Create(filePath);
        using var writer = new BinaryWriter(fileStream);

        // Write obfuscated version header
        var versionString = $"Version {adventure.Version}";
        var versionBytes = TafObfuscator.ObfuscateFromString(versionString, 1);
        writer.Write(versionBytes);

        // Write Babel metadata size (simplified - write "0000" for now)
        writer.Write(System.Text.Encoding.UTF8.GetBytes("0000"));

        // Create XML in memory
        using var xmlStream = new MemoryStream();
        using (var xmlWriter = XmlWriter.Create(xmlStream, new XmlWriterSettings
        {
            Indent = false,
            Encoding = System.Text.Encoding.UTF8
        }))
        {
            await xmlWriter.WriteStartDocumentAsync();
            await xmlWriter.WriteStartElementAsync(null, "Adventure", null);

            await WriteAdventureMetadataAsync(xmlWriter, adventure);
            await WriteLocationsAsync(xmlWriter, adventure);
            await WriteObjectsAsync(xmlWriter, adventure);
            await WriteCharactersAsync(xmlWriter, adventure);
            await WriteTasksAsync(xmlWriter, adventure);
            await WriteEventsAsync(xmlWriter, adventure);
            await WriteVariablesAsync(xmlWriter, adventure);
            await WriteGroupsAsync(xmlWriter, adventure);
            await WriteSynonymsAsync(xmlWriter, adventure);
            await WriteHintsAsync(xmlWriter, adventure);

            await xmlWriter.WriteEndElementAsync();
            await xmlWriter.WriteEndDocumentAsync();
        }

        var xmlBytes = xmlStream.ToArray();

        // Compress using ZLib
        using var compressedStream = new MemoryStream();
        using (var zlibStream = new ZLibStream(compressedStream, CompressionLevel.Optimal))
        {
            await zlibStream.WriteAsync(xmlBytes, 0, xmlBytes.Length);
        }

        var compressedBytes = compressedStream.ToArray();

        // Obfuscate compressed data
        var obfuscatedData = TafObfuscator.Obfuscate(compressedBytes, 16 + 1);

        // Write obfuscated compressed data
        writer.Write(obfuscatedData);

        // Write password footer
        var password = string.IsNullOrWhiteSpace(adventure.Password) ? "        " : adventure.Password;
        while (password.Length < 8) password += " ";
        var passwordString = password.Substring(0, 4) + "Wild" + password.Substring(4, 4);

        var passwordBytes = TafObfuscator.ObfuscateFromString(passwordString,
            obfuscatedData.Length + 16 + 1);
        writer.Write(passwordBytes);

        // Write CRLF
        writer.Write(new byte[] { 0x0D, 0x0A });
    }

    /// <summary>
    /// Helper to check if a string is valid hexadecimal
    /// </summary>
    private static bool IsHexString(string str)
    {
        if (string.IsNullOrEmpty(str)) return false;
        foreach (char c in str)
        {
            if (!Uri.IsHexDigit(c)) return false;
        }
        return true;
    }

    /// <summary>
    /// Load adventure XML from a stream
    /// </summary>
    private static async Task<Adventure> LoadXmlFromStreamAsync(Stream stream)
    {
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
                    case "Directions":
                        await ReadDirectionsAsync(reader, location);
                        break;
                }
            }
        }

        return location;
    }

    private static async Task ReadDirectionsAsync(XmlReader reader, Location location)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Directions")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Direction")
            {
                var direction = new Direction();
                var name = reader.GetAttribute("Name");
                if (!string.IsNullOrEmpty(name)) direction.DirectionName = name;

                var destination = reader.GetAttribute("Destination");
                if (!string.IsNullOrEmpty(destination)) direction.DestinationKey = destination;

                // Read restriction if present
                if (!reader.IsEmptyElement)
                {
                    while (await reader.ReadAsync())
                    {
                        if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Direction")
                            break;

                        if (reader.NodeType == XmlNodeType.Element && reader.Name == "Restriction")
                        {
                            direction.RestrictionDescription = await reader.ReadElementContentAsStringAsync();
                        }
                    }
                }

                location.Directions.Add(direction);
            }
        }
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
                    // Basic identification
                    case "Article":
                        obj.Article = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Prefix":
                        obj.Prefix = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Name":
                        obj.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Aliases":
                        await ReadStringListAsync(reader, obj.Aliases, "Aliases", "Alias");
                        break;

                    // Descriptions
                    case "ShortDescription":
                        obj.ShortDescription = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "LongDescription":
                        obj.LongDescription = await reader.ReadElementContentAsStringAsync();
                        break;

                    // Location
                    case "LocationType":
                        if (Enum.TryParse<ObjectLocation>(await reader.ReadElementContentAsStringAsync(), out var locType))
                            obj.LocationType = locType;
                        break;
                    case "LocationKey":
                        obj.LocationKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "ContainerKey":
                        obj.ContainerKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "CharacterKey":
                        obj.CharacterKey = await reader.ReadElementContentAsStringAsync();
                        break;

                    // Physical properties
                    case "Size":
                        if (Enum.TryParse<ObjectSize>(await reader.ReadElementContentAsStringAsync(), out var size))
                            obj.Size = size;
                        break;
                    case "Weight":
                        obj.Weight = await reader.ReadElementContentAsDoubleAsync();
                        break;

                    // Capabilities
                    case "IsStatic":
                        obj.IsStatic = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsContainer":
                        obj.IsContainer = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsSurface":
                        obj.IsSurface = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsWearable":
                        obj.IsWearable = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsEdible":
                        obj.IsEdible = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsLightSource":
                        obj.IsLightSource = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsReadable":
                        obj.IsReadable = await reader.ReadElementContentAsBooleanAsync();
                        break;

                    // Container properties
                    case "Capacity":
                        obj.Capacity = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "IsOpenable":
                        obj.IsOpenable = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsLockable":
                        obj.IsLockable = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsOpen":
                        obj.IsOpen = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IsLocked":
                        obj.IsLocked = await reader.ReadElementContentAsBooleanAsync();
                        break;

                    // Advanced
                    case "ReadingText":
                        obj.ReadingText = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "IsLibrary":
                        obj.IsLibrary = await reader.ReadElementContentAsBooleanAsync();
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

    private static async Task ReadTasksAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Tasks")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Task")
            {
                var task = await ReadTaskAsync(reader);
                adventure.Tasks[task.Key] = task;
            }
        }
    }

    private static async Task<Core.Models.Task> ReadTaskAsync(XmlReader reader)
    {
        var task = new Core.Models.Task();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) task.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Task")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Name":
                        task.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        task.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Type":
                        if (Enum.TryParse<TaskType>(await reader.ReadElementContentAsStringAsync(), out var taskType))
                            task.Type = taskType;
                        break;
                    case "Priority":
                        task.Priority = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "IsRepeatable":
                        task.IsRepeatable = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "ScoreValue":
                        task.ScoreValue = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "SuccessMessage":
                        task.SuccessMessage = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "FailureMessage":
                        task.FailureMessage = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Commands":
                        await ReadTaskCommandsAsync(reader, task);
                        break;
                    case "Restrictions":
                        await ReadStringListAsync(reader, task.Restrictions, "Restrictions", "Restriction");
                        break;
                    case "SuccessActions":
                        await ReadTaskActionsAsync(reader, task.SuccessActions, "SuccessActions");
                        break;
                    case "FailureActions":
                        await ReadTaskActionsAsync(reader, task.FailureActions, "FailureActions");
                        break;
                }
            }
        }

        return task;
    }

    private static async Task ReadTaskCommandsAsync(XmlReader reader, Core.Models.Task task)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Commands")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Command")
            {
                var taskCommand = new TaskCommand();

                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Command")
                        break;

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "CommandText":
                                taskCommand.Command = await reader.ReadElementContentAsStringAsync();
                                break;
                            case "Synonyms":
                                await ReadStringListAsync(reader, taskCommand.Synonyms, "Synonyms", "Synonym");
                                break;
                        }
                    }
                }

                task.Commands.Add(taskCommand);
            }
        }
    }

    private static async Task ReadTaskActionsAsync(XmlReader reader, List<TaskAction> actions, string containerName)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == containerName)
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Action")
            {
                var action = new TaskAction();
                action.ActionType = reader.GetAttribute("Type") ?? string.Empty;

                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Action")
                        break;

                    if (reader.NodeType == XmlNodeType.Element && reader.Name == "Parameter")
                    {
                        var paramName = reader.GetAttribute("Name");
                        var paramValue = await reader.ReadElementContentAsStringAsync();
                        if (!string.IsNullOrEmpty(paramName))
                            action.Parameters[paramName] = paramValue;
                    }
                }

                actions.Add(action);
            }
        }
    }

    private static async Task ReadEventsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Events")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Event")
            {
                var evt = await ReadEventAsync(reader);
                adventure.Events[evt.Key] = evt;
            }
        }
    }

    private static async Task<Event> ReadEventAsync(XmlReader reader)
    {
        var evt = new Event();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) evt.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Event")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Name":
                        evt.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        evt.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Type":
                        if (Enum.TryParse<EventType>(await reader.ReadElementContentAsStringAsync(), out var eventType))
                            evt.Type = eventType;
                        break;
                    case "Trigger":
                        if (Enum.TryParse<TriggerType>(await reader.ReadElementContentAsStringAsync(), out var triggerType))
                            evt.Trigger = triggerType;
                        break;
                    case "DelayTurns":
                        evt.DelayTurns = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "RepeatTurns":
                        evt.RepeatTurns = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "OutputText":
                        evt.OutputText = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "ParentEventKey":
                        evt.ParentEventKey = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "TriggerConditions":
                        await ReadStringListAsync(reader, evt.TriggerConditions, "TriggerConditions", "Condition");
                        break;
                    case "SubEventKeys":
                        await ReadStringListAsync(reader, evt.SubEventKeys, "SubEventKeys", "EventKey");
                        break;
                    case "Actions":
                        await ReadEventActionsAsync(reader, evt);
                        break;
                }
            }
        }

        return evt;
    }

    private static async Task ReadEventActionsAsync(XmlReader reader, Event evt)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Actions")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Action")
            {
                var action = new EventAction();

                var orderAttr = reader.GetAttribute("Order");
                if (!string.IsNullOrEmpty(orderAttr) && int.TryParse(orderAttr, out var order))
                    action.Order = order;

                action.ActionType = reader.GetAttribute("Type") ?? string.Empty;

                while (await reader.ReadAsync())
                {
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Action")
                        break;

                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {
                            case "Description":
                                action.Description = await reader.ReadElementContentAsStringAsync();
                                break;
                            case "Parameter":
                                var paramName = reader.GetAttribute("Name");
                                var paramValue = await reader.ReadElementContentAsStringAsync();
                                if (!string.IsNullOrEmpty(paramName))
                                    action.Parameters[paramName] = paramValue;
                                break;
                        }
                    }
                }

                evt.Actions.Add(action);
            }
        }
    }

    private static async Task ReadPropertiesAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Properties")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Property")
            {
                var property = await ReadPropertyAsync(reader);
                adventure.Properties[property.Key] = property;
            }
        }
    }

    private static async Task<Property> ReadPropertyAsync(XmlReader reader)
    {
        var property = new Property();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) property.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Property")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Description":
                        property.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Type":
                        if (Enum.TryParse<PropertyType>(await reader.ReadElementContentAsStringAsync(), out var type))
                            property.Type = type;
                        break;
                    case "PropertyOf":
                        if (Enum.TryParse<PropertyOf>(await reader.ReadElementContentAsStringAsync(), out var propertyOf))
                            property.PropertyOf = propertyOf;
                        break;
                    case "Mandatory":
                        property.Mandatory = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "StringValue":
                        property.StringValue = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "IntValue":
                        property.IntValue = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "States":
                        await ReadPropertyStatesAsync(reader, property);
                        break;
                }
            }
        }

        return property;
    }

    private static async Task ReadPropertyStatesAsync(XmlReader reader, Property property)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "States")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "State")
            {
                property.States.Add(await reader.ReadElementContentAsStringAsync());
            }
        }
    }

    private static async Task ReadALRsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ALRs")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "ALR")
            {
                var alr = await ReadALRAsync(reader);
                adventure.ALRs[alr.Key] = alr;
            }
        }
    }

    private static async Task<ALR> ReadALRAsync(XmlReader reader)
    {
        var alr = new ALR();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) alr.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "ALR")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "OldText":
                        alr.OldText = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "NewText":
                        alr.NewText = new Description { Text = await reader.ReadElementContentAsStringAsync() };
                        break;
                    case "CaseSensitive":
                        alr.CaseSensitive = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "WholeWordsOnly":
                        alr.WholeWordsOnly = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "Order":
                        alr.Order = await reader.ReadElementContentAsIntAsync();
                        break;
                }
            }
        }

        return alr;
    }

    private static async Task ReadUserFunctionsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "UserFunctions")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "UserFunction")
            {
                var func = await ReadUserFunctionAsync(reader);
                adventure.UserFunctions[func.Key] = func;
            }
        }
    }

    private static async Task<UserFunction> ReadUserFunctionAsync(XmlReader reader)
    {
        var func = new UserFunction();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) func.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "UserFunction")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Name":
                        func.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        func.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Output":
                        func.Output = new Description { Text = await reader.ReadElementContentAsStringAsync() };
                        break;
                    case "Arguments":
                        await ReadFunctionArgumentsAsync(reader, func);
                        break;
                }
            }
        }

        return func;
    }

    private static async Task ReadFunctionArgumentsAsync(XmlReader reader, UserFunction func)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Arguments")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Argument")
            {
                var arg = new FunctionArgument();

                var numAttr = reader.GetAttribute("Number");
                if (!string.IsNullOrEmpty(numAttr) && int.TryParse(numAttr, out var num))
                    arg.ArgumentNumber = num;

                var typeAttr = reader.GetAttribute("Type");
                if (!string.IsNullOrEmpty(typeAttr) && Enum.TryParse<FunctionArgumentType>(typeAttr, out var type))
                    arg.Type = type;

                arg.Name = await reader.ReadElementContentAsStringAsync();
                func.Arguments.Add(arg);
            }
        }
    }

    private static async Task ReadMacrosAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Macros")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Macro")
            {
                var macro = await ReadMacroAsync(reader);
                adventure.Macros.Add(macro);
            }
        }
    }

    private static async Task<Macro> ReadMacroAsync(XmlReader reader)
    {
        var macro = new Macro();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) macro.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Macro")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Title":
                        macro.Title = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        macro.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Shortcut":
                        macro.Shortcut = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "IsShared":
                        macro.IsShared = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "IFID":
                        macro.IFID = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Commands":
                        await ReadMacroCommandsAsync(reader, macro);
                        break;
                }
            }
        }

        return macro;
    }

    private static async Task ReadMacroCommandsAsync(XmlReader reader, Macro macro)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Commands")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Command")
            {
                macro.Commands.Add(await reader.ReadElementContentAsStringAsync());
            }
        }
    }

    private static async Task ReadMapAsync(XmlReader reader, Adventure adventure)
    {
        var autoLayoutAttr = reader.GetAttribute("AutoLayout");
        if (!string.IsNullOrEmpty(autoLayoutAttr) && bool.TryParse(autoLayoutAttr, out var autoLayout))
            adventure.Map.AutoLayout = autoLayout;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Map")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "MapPage")
            {
                var page = await ReadMapPageAsync(reader);
                adventure.Map.Pages.Add(page);
            }
        }
    }

    private static async Task<MapPage> ReadMapPageAsync(XmlReader reader)
    {
        var page = new MapPage();

        var numAttr = reader.GetAttribute("Number");
        if (!string.IsNullOrEmpty(numAttr) && int.TryParse(numAttr, out var num))
            page.PageNumber = num;

        var labelAttr = reader.GetAttribute("Label");
        if (!string.IsNullOrEmpty(labelAttr))
            page.Label = labelAttr;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "MapPage")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "MapNode")
            {
                var node = await ReadMapNodeAsync(reader);
                page.Nodes.Add(node);
            }
        }

        return page;
    }

    private static async Task<MapNode> ReadMapNodeAsync(XmlReader reader)
    {
        var node = new MapNode();

        var locKeyAttr = reader.GetAttribute("LocationKey");
        if (!string.IsNullOrEmpty(locKeyAttr))
            node.LocationKey = locKeyAttr;

        var xAttr = reader.GetAttribute("X");
        if (!string.IsNullOrEmpty(xAttr) && double.TryParse(xAttr, out var x))
            node.X = x;

        var yAttr = reader.GetAttribute("Y");
        if (!string.IsNullOrEmpty(yAttr) && double.TryParse(yAttr, out var y))
            node.Y = y;

        var zAttr = reader.GetAttribute("Z");
        if (!string.IsNullOrEmpty(zAttr) && double.TryParse(zAttr, out var z))
            node.Z = z;

        var pinnedAttr = reader.GetAttribute("Pinned");
        if (!string.IsNullOrEmpty(pinnedAttr) && bool.TryParse(pinnedAttr, out var pinned))
            node.Pinned = pinned;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "MapNode")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "MapLink")
            {
                var link = await ReadMapLinkAsync(reader);
                node.Links.Add(link);
            }
        }

        return node;
    }

    private static async Task<MapLink> ReadMapLinkAsync(XmlReader reader)
    {
        var link = new MapLink();

        var sourceAttr = reader.GetAttribute("Source");
        if (!string.IsNullOrEmpty(sourceAttr))
            link.SourceLocationKey = sourceAttr;

        var destAttr = reader.GetAttribute("Destination");
        if (!string.IsNullOrEmpty(destAttr))
            link.DestinationLocationKey = destAttr;

        var dirAttr = reader.GetAttribute("Direction");
        if (!string.IsNullOrEmpty(dirAttr) && Enum.TryParse<DirectionEnum>(dirAttr, out var dir))
            link.Direction = dir;

        var styleAttr = reader.GetAttribute("Style");
        if (!string.IsNullOrEmpty(styleAttr) && Enum.TryParse<LinkStyle>(styleAttr, out var style))
            link.Style = style;

        // Skip to end of MapLink element
        if (!reader.IsEmptyElement)
        {
            while (await reader.ReadAsync())
            {
                if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "MapLink")
                    break;
            }
        }

        return link;
    }

    private static async Task ReadSoundsAsync(XmlReader reader, Adventure adventure)
    {
        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Sounds")
                break;

            if (reader.NodeType == XmlNodeType.Element && reader.Name == "Sound")
            {
                var sound = await ReadSoundAsync(reader);
                adventure.Sounds[sound.Key] = sound;
            }
        }
    }

    private static async Task<Sound> ReadSoundAsync(XmlReader reader)
    {
        var sound = new Sound();
        var key = reader.GetAttribute("Key");
        if (!string.IsNullOrEmpty(key)) sound.Key = key;

        while (await reader.ReadAsync())
        {
            if (reader.NodeType == XmlNodeType.EndElement && reader.Name == "Sound")
                break;

            if (reader.NodeType == XmlNodeType.Element)
            {
                switch (reader.Name)
                {
                    case "Name":
                        sound.Name = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "Description":
                        sound.Description = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "FilePath":
                        sound.FilePath = await reader.ReadElementContentAsStringAsync();
                        break;
                    case "IsEmbedded":
                        sound.IsEmbedded = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "Format":
                        if (Enum.TryParse<SoundFormat>(await reader.ReadElementContentAsStringAsync(), out var format))
                            sound.Format = format;
                        break;
                    case "DefaultChannel":
                        sound.DefaultChannel = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "Loop":
                        sound.Loop = await reader.ReadElementContentAsBooleanAsync();
                        break;
                    case "Volume":
                        sound.Volume = await reader.ReadElementContentAsIntAsync();
                        break;
                    case "EmbeddedData":
                        var base64 = await reader.ReadElementContentAsStringAsync();
                        sound.EmbeddedData = Convert.FromBase64String(base64);
                        break;
                }
            }
        }

        return sound;
    }

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

            // Write directions
            if (location.Directions.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Directions", null);
                foreach (var direction in location.Directions)
                {
                    await writer.WriteStartElementAsync(null, "Direction", null);
                    await writer.WriteAttributeStringAsync(null, "Name", null, direction.DirectionName);
                    await writer.WriteAttributeStringAsync(null, "Destination", null, direction.DestinationKey);

                    if (!string.IsNullOrEmpty(direction.RestrictionDescription))
                    {
                        await writer.WriteElementStringAsync(null, "Restriction", null, direction.RestrictionDescription);
                    }

                    await writer.WriteEndElementAsync(); // Direction
                }
                await writer.WriteEndElementAsync(); // Directions
            }

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

            // Basic identification
            await writer.WriteElementStringAsync(null, "Article", null, obj.Article);
            await writer.WriteElementStringAsync(null, "Prefix", null, obj.Prefix);
            await writer.WriteElementStringAsync(null, "Name", null, obj.Name);

            // Aliases
            if (obj.Aliases.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Aliases", null);
                foreach (var alias in obj.Aliases)
                {
                    await writer.WriteElementStringAsync(null, "Alias", null, alias);
                }
                await writer.WriteEndElementAsync(); // Aliases
            }

            // Descriptions
            await writer.WriteElementStringAsync(null, "ShortDescription", null, obj.ShortDescription);
            await writer.WriteElementStringAsync(null, "LongDescription", null, obj.LongDescription);

            // Location
            await writer.WriteElementStringAsync(null, "LocationType", null, obj.LocationType.ToString());
            if (!string.IsNullOrEmpty(obj.LocationKey))
                await writer.WriteElementStringAsync(null, "LocationKey", null, obj.LocationKey);
            if (!string.IsNullOrEmpty(obj.ContainerKey))
                await writer.WriteElementStringAsync(null, "ContainerKey", null, obj.ContainerKey);
            if (!string.IsNullOrEmpty(obj.CharacterKey))
                await writer.WriteElementStringAsync(null, "CharacterKey", null, obj.CharacterKey);

            // Physical properties
            await writer.WriteElementStringAsync(null, "Size", null, obj.Size.ToString());
            if (obj.Weight != 1.0)
                await writer.WriteElementStringAsync(null, "Weight", null, obj.Weight.ToString());

            // Capabilities
            if (obj.IsStatic)
                await writer.WriteElementStringAsync(null, "IsStatic", null, "true");
            if (obj.IsContainer)
                await writer.WriteElementStringAsync(null, "IsContainer", null, "true");
            if (obj.IsSurface)
                await writer.WriteElementStringAsync(null, "IsSurface", null, "true");
            if (obj.IsWearable)
                await writer.WriteElementStringAsync(null, "IsWearable", null, "true");
            if (obj.IsEdible)
                await writer.WriteElementStringAsync(null, "IsEdible", null, "true");
            if (obj.IsLightSource)
                await writer.WriteElementStringAsync(null, "IsLightSource", null, "true");
            if (obj.IsReadable)
                await writer.WriteElementStringAsync(null, "IsReadable", null, "true");

            // Container properties
            if (obj.IsContainer || obj.IsOpenable || obj.IsLockable)
            {
                if (obj.Capacity != 10)
                    await writer.WriteElementStringAsync(null, "Capacity", null, obj.Capacity.ToString());
                if (obj.IsOpenable)
                    await writer.WriteElementStringAsync(null, "IsOpenable", null, "true");
                if (obj.IsLockable)
                    await writer.WriteElementStringAsync(null, "IsLockable", null, "true");
                if (obj.IsOpenable && !obj.IsOpen)
                    await writer.WriteElementStringAsync(null, "IsOpen", null, "false");
                if (obj.IsLocked)
                    await writer.WriteElementStringAsync(null, "IsLocked", null, "true");
            }

            // Advanced
            if (!string.IsNullOrEmpty(obj.ReadingText))
                await writer.WriteElementStringAsync(null, "ReadingText", null, obj.ReadingText);

            if (obj.IsLibrary)
                await writer.WriteElementStringAsync(null, "IsLibrary", null, "true");

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

    private static async Task WriteTasksAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Tasks.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Tasks", null);

        foreach (var task in adventure.Tasks.Values)
        {
            await writer.WriteStartElementAsync(null, "Task", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, task.Key);

            await writer.WriteElementStringAsync(null, "Name", null, task.Name);
            await writer.WriteElementStringAsync(null, "Description", null, task.Description);
            await writer.WriteElementStringAsync(null, "Type", null, task.Type.ToString());
            await writer.WriteElementStringAsync(null, "Priority", null, task.Priority.ToString());

            if (task.IsRepeatable)
                await writer.WriteElementStringAsync(null, "IsRepeatable", null, "true");
            if (task.ScoreValue > 0)
                await writer.WriteElementStringAsync(null, "ScoreValue", null, task.ScoreValue.ToString());

            if (!string.IsNullOrEmpty(task.SuccessMessage))
                await writer.WriteElementStringAsync(null, "SuccessMessage", null, task.SuccessMessage);
            if (!string.IsNullOrEmpty(task.FailureMessage))
                await writer.WriteElementStringAsync(null, "FailureMessage", null, task.FailureMessage);

            // Write Commands
            if (task.Commands.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Commands", null);
                foreach (var command in task.Commands)
                {
                    await writer.WriteStartElementAsync(null, "Command", null);

                    await writer.WriteElementStringAsync(null, "CommandText", null, command.Command);

                    if (command.Synonyms.Count > 0)
                    {
                        await writer.WriteStartElementAsync(null, "Synonyms", null);
                        foreach (var synonym in command.Synonyms)
                        {
                            await writer.WriteElementStringAsync(null, "Synonym", null, synonym);
                        }
                        await writer.WriteEndElementAsync(); // Synonyms
                    }

                    await writer.WriteEndElementAsync(); // Command
                }
                await writer.WriteEndElementAsync(); // Commands
            }

            // Write Restrictions
            if (task.Restrictions.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Restrictions", null);
                foreach (var restriction in task.Restrictions)
                {
                    await writer.WriteElementStringAsync(null, "Restriction", null, restriction);
                }
                await writer.WriteEndElementAsync(); // Restrictions
            }

            // Write Success Actions
            if (task.SuccessActions.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "SuccessActions", null);
                foreach (var action in task.SuccessActions)
                {
                    await writer.WriteStartElementAsync(null, "Action", null);
                    await writer.WriteAttributeStringAsync(null, "Type", null, action.ActionType);

                    foreach (var param in action.Parameters)
                    {
                        await writer.WriteStartElementAsync(null, "Parameter", null);
                        await writer.WriteAttributeStringAsync(null, "Name", null, param.Key);
                        await writer.WriteStringAsync(param.Value);
                        await writer.WriteEndElementAsync(); // Parameter
                    }

                    await writer.WriteEndElementAsync(); // Action
                }
                await writer.WriteEndElementAsync(); // SuccessActions
            }

            // Write Failure Actions
            if (task.FailureActions.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "FailureActions", null);
                foreach (var action in task.FailureActions)
                {
                    await writer.WriteStartElementAsync(null, "Action", null);
                    await writer.WriteAttributeStringAsync(null, "Type", null, action.ActionType);

                    foreach (var param in action.Parameters)
                    {
                        await writer.WriteStartElementAsync(null, "Parameter", null);
                        await writer.WriteAttributeStringAsync(null, "Name", null, param.Key);
                        await writer.WriteStringAsync(param.Value);
                        await writer.WriteEndElementAsync(); // Parameter
                    }

                    await writer.WriteEndElementAsync(); // Action
                }
                await writer.WriteEndElementAsync(); // FailureActions
            }

            await writer.WriteEndElementAsync(); // Task
        }

        await writer.WriteEndElementAsync(); // Tasks
    }

    private static async Task WriteEventsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Events.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Events", null);

        foreach (var evt in adventure.Events.Values)
        {
            await writer.WriteStartElementAsync(null, "Event", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, evt.Key);

            await writer.WriteElementStringAsync(null, "Name", null, evt.Name);
            await writer.WriteElementStringAsync(null, "Description", null, evt.Description);
            await writer.WriteElementStringAsync(null, "Type", null, evt.Type.ToString());
            await writer.WriteElementStringAsync(null, "Trigger", null, evt.Trigger.ToString());

            if (evt.DelayTurns > 0)
                await writer.WriteElementStringAsync(null, "DelayTurns", null, evt.DelayTurns.ToString());
            if (evt.RepeatTurns > 0)
                await writer.WriteElementStringAsync(null, "RepeatTurns", null, evt.RepeatTurns.ToString());

            if (!string.IsNullOrEmpty(evt.OutputText))
                await writer.WriteElementStringAsync(null, "OutputText", null, evt.OutputText);
            if (!string.IsNullOrEmpty(evt.ParentEventKey))
                await writer.WriteElementStringAsync(null, "ParentEventKey", null, evt.ParentEventKey);

            // Write Trigger Conditions
            if (evt.TriggerConditions.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "TriggerConditions", null);
                foreach (var condition in evt.TriggerConditions)
                {
                    await writer.WriteElementStringAsync(null, "Condition", null, condition);
                }
                await writer.WriteEndElementAsync(); // TriggerConditions
            }

            // Write Sub Event Keys
            if (evt.SubEventKeys.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "SubEventKeys", null);
                foreach (var eventKey in evt.SubEventKeys)
                {
                    await writer.WriteElementStringAsync(null, "EventKey", null, eventKey);
                }
                await writer.WriteEndElementAsync(); // SubEventKeys
            }

            // Write Actions
            if (evt.Actions.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Actions", null);
                foreach (var action in evt.Actions.OrderBy(a => a.Order))
                {
                    await writer.WriteStartElementAsync(null, "Action", null);
                    await writer.WriteAttributeStringAsync(null, "Order", null, action.Order.ToString());
                    await writer.WriteAttributeStringAsync(null, "Type", null, action.ActionType);

                    if (!string.IsNullOrEmpty(action.Description))
                        await writer.WriteElementStringAsync(null, "Description", null, action.Description);

                    foreach (var param in action.Parameters)
                    {
                        await writer.WriteStartElementAsync(null, "Parameter", null);
                        await writer.WriteAttributeStringAsync(null, "Name", null, param.Key);
                        await writer.WriteStringAsync(param.Value);
                        await writer.WriteEndElementAsync(); // Parameter
                    }

                    await writer.WriteEndElementAsync(); // Action
                }
                await writer.WriteEndElementAsync(); // Actions
            }

            await writer.WriteEndElementAsync(); // Event
        }

        await writer.WriteEndElementAsync(); // Events
    }

    private static async Task WritePropertiesAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Properties.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Properties", null);

        foreach (var property in adventure.Properties.Values)
        {
            await writer.WriteStartElementAsync(null, "Property", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, property.Key);

            await writer.WriteElementStringAsync(null, "Description", null, property.Description);
            await writer.WriteElementStringAsync(null, "Type", null, property.Type.ToString());
            await writer.WriteElementStringAsync(null, "PropertyOf", null, property.PropertyOf.ToString());
            await writer.WriteElementStringAsync(null, "Mandatory", null, property.Mandatory.ToString());

            if (!string.IsNullOrEmpty(property.StringValue))
                await writer.WriteElementStringAsync(null, "StringValue", null, property.StringValue);
            if (property.IntValue != 0)
                await writer.WriteElementStringAsync(null, "IntValue", null, property.IntValue.ToString());

            // States for StateList properties
            if (property.States.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "States", null);
                foreach (var state in property.States)
                {
                    await writer.WriteElementStringAsync(null, "State", null, state);
                }
                await writer.WriteEndElementAsync(); // States
            }

            await writer.WriteEndElementAsync(); // Property
        }

        await writer.WriteEndElementAsync(); // Properties
    }

    private static async Task WriteALRsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.ALRs.Count == 0) return;

        await writer.WriteStartElementAsync(null, "ALRs", null);

        foreach (var alr in adventure.ALRs.Values)
        {
            await writer.WriteStartElementAsync(null, "ALR", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, alr.Key);

            await writer.WriteElementStringAsync(null, "OldText", null, alr.OldText);
            await writer.WriteElementStringAsync(null, "NewText", null, alr.NewText.ToString());
            await writer.WriteElementStringAsync(null, "CaseSensitive", null, alr.CaseSensitive.ToString());
            await writer.WriteElementStringAsync(null, "WholeWordsOnly", null, alr.WholeWordsOnly.ToString());
            await writer.WriteElementStringAsync(null, "Order", null, alr.Order.ToString());

            await writer.WriteEndElementAsync(); // ALR
        }

        await writer.WriteEndElementAsync(); // ALRs
    }

    private static async Task WriteUserFunctionsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.UserFunctions.Count == 0) return;

        await writer.WriteStartElementAsync(null, "UserFunctions", null);

        foreach (var func in adventure.UserFunctions.Values)
        {
            await writer.WriteStartElementAsync(null, "UserFunction", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, func.Key);

            await writer.WriteElementStringAsync(null, "Name", null, func.Name);
            if (!string.IsNullOrEmpty(func.Description))
                await writer.WriteElementStringAsync(null, "Description", null, func.Description);
            await writer.WriteElementStringAsync(null, "Output", null, func.Output.ToString());

            if (func.Arguments.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Arguments", null);
                foreach (var arg in func.Arguments.OrderBy(a => a.ArgumentNumber))
                {
                    await writer.WriteStartElementAsync(null, "Argument", null);
                    await writer.WriteAttributeStringAsync(null, "Number", null, arg.ArgumentNumber.ToString());
                    await writer.WriteAttributeStringAsync(null, "Type", null, arg.Type.ToString());
                    await writer.WriteStringAsync(arg.Name);
                    await writer.WriteEndElementAsync(); // Argument
                }
                await writer.WriteEndElementAsync(); // Arguments
            }

            await writer.WriteEndElementAsync(); // UserFunction
        }

        await writer.WriteEndElementAsync(); // UserFunctions
    }

    private static async Task WriteMacrosAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Macros.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Macros", null);

        foreach (var macro in adventure.Macros)
        {
            await writer.WriteStartElementAsync(null, "Macro", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, macro.Key);

            await writer.WriteElementStringAsync(null, "Title", null, macro.Title);
            if (!string.IsNullOrEmpty(macro.Description))
                await writer.WriteElementStringAsync(null, "Description", null, macro.Description);
            if (!string.IsNullOrEmpty(macro.Shortcut))
                await writer.WriteElementStringAsync(null, "Shortcut", null, macro.Shortcut);
            await writer.WriteElementStringAsync(null, "IsShared", null, macro.IsShared.ToString());
            if (!string.IsNullOrEmpty(macro.IFID))
                await writer.WriteElementStringAsync(null, "IFID", null, macro.IFID);

            if (macro.Commands.Count > 0)
            {
                await writer.WriteStartElementAsync(null, "Commands", null);
                foreach (var command in macro.Commands)
                {
                    await writer.WriteElementStringAsync(null, "Command", null, command);
                }
                await writer.WriteEndElementAsync(); // Commands
            }

            await writer.WriteEndElementAsync(); // Macro
        }

        await writer.WriteEndElementAsync(); // Macros
    }

    private static async Task WriteMapAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Map.Pages.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Map", null);
        await writer.WriteAttributeStringAsync(null, "AutoLayout", null, adventure.Map.AutoLayout.ToString());

        foreach (var page in adventure.Map.Pages)
        {
            await writer.WriteStartElementAsync(null, "MapPage", null);
            await writer.WriteAttributeStringAsync(null, "Number", null, page.PageNumber.ToString());
            await writer.WriteAttributeStringAsync(null, "Label", null, page.Label);

            foreach (var node in page.Nodes)
            {
                await writer.WriteStartElementAsync(null, "MapNode", null);
                await writer.WriteAttributeStringAsync(null, "LocationKey", null, node.LocationKey);
                await writer.WriteAttributeStringAsync(null, "X", null, node.X.ToString());
                await writer.WriteAttributeStringAsync(null, "Y", null, node.Y.ToString());
                await writer.WriteAttributeStringAsync(null, "Z", null, node.Z.ToString());
                await writer.WriteAttributeStringAsync(null, "Pinned", null, node.Pinned.ToString());

                // Write links
                foreach (var link in node.Links)
                {
                    await writer.WriteStartElementAsync(null, "MapLink", null);
                    await writer.WriteAttributeStringAsync(null, "Source", null, link.SourceLocationKey);
                    await writer.WriteAttributeStringAsync(null, "Destination", null, link.DestinationLocationKey);
                    await writer.WriteAttributeStringAsync(null, "Direction", null, link.Direction.ToString());
                    await writer.WriteAttributeStringAsync(null, "Style", null, link.Style.ToString());
                    await writer.WriteEndElementAsync(); // MapLink
                }

                await writer.WriteEndElementAsync(); // MapNode
            }

            await writer.WriteEndElementAsync(); // MapPage
        }

        await writer.WriteEndElementAsync(); // Map
    }

    private static async Task WriteSoundsAsync(XmlWriter writer, Adventure adventure)
    {
        if (adventure.Sounds.Count == 0) return;

        await writer.WriteStartElementAsync(null, "Sounds", null);

        foreach (var sound in adventure.Sounds.Values)
        {
            await writer.WriteStartElementAsync(null, "Sound", null);
            await writer.WriteAttributeStringAsync(null, "Key", null, sound.Key);

            await writer.WriteElementStringAsync(null, "Name", null, sound.Name);
            if (!string.IsNullOrEmpty(sound.Description))
                await writer.WriteElementStringAsync(null, "Description", null, sound.Description);
            await writer.WriteElementStringAsync(null, "FilePath", null, sound.FilePath);
            await writer.WriteElementStringAsync(null, "IsEmbedded", null, sound.IsEmbedded.ToString());
            await writer.WriteElementStringAsync(null, "Format", null, sound.Format.ToString());
            await writer.WriteElementStringAsync(null, "DefaultChannel", null, sound.DefaultChannel.ToString());
            await writer.WriteElementStringAsync(null, "Loop", null, sound.Loop.ToString());
            await writer.WriteElementStringAsync(null, "Volume", null, sound.Volume.ToString());

            if (sound.IsEmbedded && sound.EmbeddedData != null)
            {
                await writer.WriteElementStringAsync(null, "EmbeddedData", null,
                    Convert.ToBase64String(sound.EmbeddedData));
            }

            await writer.WriteEndElementAsync(); // Sound
        }

        await writer.WriteEndElementAsync(); // Sounds
    }

    #endregion
}
