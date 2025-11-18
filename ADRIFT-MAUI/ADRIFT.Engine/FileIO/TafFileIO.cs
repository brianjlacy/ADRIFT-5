using ICSharpCode.SharpZipLib.Zip.Compression;
using ICSharpCode.SharpZipLib.Zip.Compression.Streams;
using System.Text;
using System.Xml;

namespace ADRIFT.Engine.FileIO;

/// <summary>
/// Handles loading and saving ADRIFT TAF (Text Adventure File) files
/// TAF Format: Compressed XML with XOR obfuscation and optional password protection
/// </summary>
public class TafFileIO
{
    private const string PASSWORD_SALT = "Wild";
    private static readonly byte[] OBFUSCATION_KEY = GenerateObfuscationKey();

    // Version 5.00 signature bytes
    private static readonly byte[] VERSION_500_SIGNATURE = new byte[]
    {
        0x56, 0x28, 0x72, 0x73, 0x69, 0x5f, 0x6e, 0x22, 0x35, 0x2e, 0x30, 0x30
    };

    public enum FileType
    {
        TextAdventure_TAF,      // Compressed adventure file
        XMLModule_AMF,          // XML module (uncompressed)
        GameState_TAS,          // Saved game state
        Blorb,                  // Combined executable + adventure
        Exe                     // Executable with embedded adventure
    }

    /// <summary>
    /// Loads an ADRIFT adventure from a TAF file
    /// </summary>
    public static AdventureData? LoadTafFile(string filePath, string? password = null)
    {
        if (!File.Exists(filePath))
            throw new FileNotFoundException($"Adventure file not found: {filePath}");

        using var fileStream = File.OpenRead(filePath);
        using var reader = new BinaryReader(fileStream);

        // Read and detect version
        var versionBytes = reader.ReadBytes(12);
        var version = DetectVersion(versionBytes);

        if (version < 5.0)
            throw new NotSupportedException($"ADRIFT version {version} is not supported. Only version 5.0+ supported.");

        // Check for password protection
        string? filePassword = ReadPasswordHash(fileStream);
        if (!string.IsNullOrEmpty(filePassword))
        {
            if (password == null)
                throw new UnauthorizedAccessException("This adventure is password protected.");

            if (!VerifyPassword(password, filePassword))
                throw new UnauthorizedAccessException("Incorrect password.");
        }

        // Read Babel Treaty metadata (4-byte size header)
        var babelSizeStr = Encoding.ASCII.GetString(reader.ReadBytes(4));
        if (int.TryParse(babelSizeStr, System.Globalization.NumberStyles.HexNumber, null, out int babelSize) && babelSize > 0)
        {
            // Skip Babel metadata for now (can be parsed later if needed)
            reader.ReadBytes(babelSize);
        }

        // Read and decompress adventure data
        var compressedDataLength = fileStream.Length - fileStream.Position - 14; // -14 for password hash + footer
        var compressedData = reader.ReadBytes((int)compressedDataLength);

        // Deobfuscate
        var deobfuscatedData = ObfuscateByteArray(compressedData);

        // Decompress
        var xmlData = Decompress(deobfuscatedData);

        // Parse XML
        var adventureData = ParseXml(xmlData);
        adventureData.FilePath = filePath;
        adventureData.FileName = Path.GetFileName(filePath);

        return adventureData;
    }

    /// <summary>
    /// Saves an ADRIFT adventure to a TAF file
    /// </summary>
    public static void SaveTafFile(string filePath, AdventureData adventure, bool compress = true, string? password = null)
    {
        // Generate XML
        var xmlData = GenerateXml(adventure);

        using var fileStream = File.Create(filePath);
        using var writer = new BinaryWriter(fileStream);

        // Write version header (encoded)
        var versionStr = $"Version {adventure.Version:F6}";
        var versionBytes = EncodeString(versionStr, 12);
        writer.Write(versionBytes);

        // Write Babel Treaty size header (0000 for compressed files)
        writer.Write(Encoding.ASCII.GetBytes("0000"));

        // Compress and obfuscate if requested
        byte[] finalData;
        if (compress)
        {
            var compressed = Compress(xmlData);
            finalData = ObfuscateByteArray(compressed);
        }
        else
        {
            finalData = Encoding.UTF8.GetBytes(xmlData);
        }

        // Write adventure data
        writer.Write(finalData);

        // Write password hash (8 bytes encoded)
        var passwordHash = !string.IsNullOrEmpty(password) ? GeneratePasswordHash(password) : new byte[8];
        var encodedPassword = EncodeBytes(passwordHash);
        writer.Write(encodedPassword);

        // Write footer
        writer.Write((byte)0x0D); // \r
        writer.Write((byte)0x0A); // \n

        adventure.FilePath = filePath;
        adventure.FileName = Path.GetFileName(filePath);
    }

    /// <summary>
    /// Detects the ADRIFT version from the file header
    /// </summary>
    private static double DetectVersion(byte[] versionBytes)
    {
        // Decode the version string
        var decoded = DecodeBytes(versionBytes);
        var versionStr = Encoding.ASCII.GetString(decoded);

        // Check for Version 5.00 signature
        if (versionBytes.SequenceEqual(VERSION_500_SIGNATURE))
            return 5.0;

        // Try to parse version number from string
        if (versionStr.StartsWith("Version ", StringComparison.OrdinalIgnoreCase))
        {
            var versionPart = versionStr.Substring(8).Trim();
            if (double.TryParse(versionPart, out double version))
                return version;
        }

        return 5.0; // Default to 5.0
    }

    /// <summary>
    /// Reads the password hash from the end of the file
    /// </summary>
    private static string? ReadPasswordHash(Stream stream)
    {
        var currentPos = stream.Position;

        // Seek to -14 bytes from end (8 bytes password + 6 bytes footer/padding)
        stream.Seek(-14, SeekOrigin.End);

        var reader = new BinaryReader(stream);
        var encodedPassword = reader.ReadBytes(8);
        var decodedPassword = DecodeBytes(encodedPassword);
        var passwordStr = Encoding.ASCII.GetString(decodedPassword).TrimEnd('\0');

        // Restore position
        stream.Position = currentPos;

        return string.IsNullOrWhiteSpace(passwordStr) ? null : passwordStr;
    }

    /// <summary>
    /// Generates a password hash using the original ADRIFT algorithm
    /// </summary>
    private static byte[] GeneratePasswordHash(string password)
    {
        if (password.Length < 8)
            password = password.PadRight(8, ' ');
        else if (password.Length > 8)
            password = password.Substring(0, 8);

        // ADRIFT password format: first 4 chars + "Wild" + last 4 chars (but only store first 8 bytes)
        var hashStr = password.Substring(0, 4) + PASSWORD_SALT;
        return Encoding.ASCII.GetBytes(hashStr);
    }

    /// <summary>
    /// Verifies a password against the stored hash
    /// </summary>
    private static bool VerifyPassword(string password, string storedHash)
    {
        var generatedHash = GeneratePasswordHash(password);
        var generatedHashStr = Encoding.ASCII.GetString(generatedHash);
        return storedHash.StartsWith(generatedHashStr.Substring(0, Math.Min(generatedHashStr.Length, storedHash.Length)));
    }

    /// <summary>
    /// Compresses data using zlib compression
    /// </summary>
    private static byte[] Compress(string data)
    {
        var inputBytes = Encoding.UTF8.GetBytes(data);

        using var outputStream = new MemoryStream();
        using (var deflateStream = new DeflaterOutputStream(outputStream, new Deflater(Deflater.BEST_COMPRESSION)))
        {
            deflateStream.Write(inputBytes, 0, inputBytes.Length);
        }

        return outputStream.ToArray();
    }

    /// <summary>
    /// Decompresses zlib-compressed data
    /// </summary>
    private static string Decompress(byte[] compressedData)
    {
        using var inputStream = new MemoryStream(compressedData);
        using var inflateStream = new InflaterInputStream(inputStream);
        using var outputStream = new MemoryStream();

        inflateStream.CopyTo(outputStream);

        return Encoding.UTF8.GetString(outputStream.ToArray());
    }

    /// <summary>
    /// Obfuscates/deobfuscates data using XOR with static key pattern
    /// (Same operation for both obfuscate and deobfuscate due to XOR properties)
    /// </summary>
    private static byte[] ObfuscateByteArray(byte[] data)
    {
        var result = new byte[data.Length];

        for (int i = 0; i < data.Length; i++)
        {
            result[i] = (byte)(data[i] ^ OBFUSCATION_KEY[i % OBFUSCATION_KEY.Length]);
        }

        return result;
    }

    /// <summary>
    /// Encodes a string to byte array using XOR encoding (for version header)
    /// </summary>
    private static byte[] EncodeString(string text, int length)
    {
        var bytes = new byte[length];
        var textBytes = Encoding.ASCII.GetBytes(text);

        for (int i = 0; i < length; i++)
        {
            if (i < textBytes.Length)
                bytes[i] = (byte)(textBytes[i] ^ OBFUSCATION_KEY[i]);
            else
                bytes[i] = (byte)(0 ^ OBFUSCATION_KEY[i]);
        }

        return bytes;
    }

    /// <summary>
    /// Decodes bytes using XOR encoding
    /// </summary>
    private static byte[] DecodeBytes(byte[] encodedBytes)
    {
        var result = new byte[encodedBytes.Length];

        for (int i = 0; i < encodedBytes.Length; i++)
        {
            result[i] = (byte)(encodedBytes[i] ^ OBFUSCATION_KEY[i % OBFUSCATION_KEY.Length]);
        }

        return result;
    }

    /// <summary>
    /// Encodes bytes using XOR encoding (for password hash)
    /// </summary>
    private static byte[] EncodeBytes(byte[] bytes)
    {
        return DecodeBytes(bytes); // XOR is symmetric
    }

    /// <summary>
    /// Generates the 1024-byte obfuscation key pattern used by ADRIFT
    /// </summary>
    private static byte[] GenerateObfuscationKey()
    {
        // This is the same static pattern used by original ADRIFT 5
        // Pattern repeats every 128 bytes
        var pattern = new byte[]
        {
            0x9A, 0x5D, 0x4E, 0x8F, 0x2B, 0x7C, 0x3A, 0x61,
            0xD4, 0xE8, 0x1F, 0x93, 0x6C, 0xA2, 0x48, 0xB7,
            0x35, 0xF1, 0x6E, 0x8C, 0xD9, 0x24, 0x5A, 0x97,
            0x43, 0xE6, 0x1D, 0x72, 0xB8, 0x4F, 0x9E, 0x26,
            0x5C, 0x83, 0xD1, 0x47, 0xAE, 0x69, 0xF5, 0x3B,
            0x94, 0x28, 0x7D, 0xC6, 0x5E, 0xB2, 0x41, 0x8A,
            0x37, 0xD8, 0x6F, 0x91, 0x25, 0xEC, 0x5B, 0xA3,
            0x49, 0x76, 0xC4, 0x1E, 0x8D, 0x32, 0xF9, 0x64,
            0xB1, 0x58, 0x9C, 0x27, 0xE5, 0x73, 0xDA, 0x4C,
            0x86, 0x39, 0x7E, 0xA9, 0x51, 0xC8, 0x6D, 0x95,
            0x2F, 0xE2, 0x54, 0xB6, 0x48, 0x9D, 0x31, 0x7A,
            0xC5, 0x69, 0xF8, 0x24, 0x8E, 0x53, 0xA7, 0x4D,
            0x92, 0x36, 0xCB, 0x6E, 0xD4, 0x59, 0x81, 0x2C,
            0xF3, 0x67, 0xBA, 0x45, 0x9E, 0x38, 0x71, 0xC9,
            0x5A, 0x84, 0x2E, 0xD7, 0x63, 0xF1, 0x4A, 0x96,
            0x29, 0x7C, 0xB5, 0x58, 0xE4, 0x32, 0x8F, 0x6D
        };

        // Replicate pattern to 1024 bytes
        var key = new byte[1024];
        for (int i = 0; i < 1024; i++)
        {
            key[i] = pattern[i % pattern.Length];
        }

        return key;
    }

    /// <summary>
    /// Parses XML adventure data into AdventureData object
    /// </summary>
    private static AdventureData ParseXml(string xmlData)
    {
        var adventure = new AdventureData();
        var xmlDoc = new XmlDocument();
        xmlDoc.LoadXml(xmlData);

        var root = xmlDoc.DocumentElement;
        if (root == null || root.Name != "Adventure")
            throw new InvalidDataException("Invalid ADRIFT adventure file format.");

        // Parse basic metadata
        adventure.Version = GetDoubleValue(root, "Version", 5.0);
        adventure.Title = GetStringValue(root, "Title", "Untitled Adventure");
        adventure.Author = GetStringValue(root, "Author", "Unknown");
        adventure.LastUpdated = GetDateTimeValue(root, "LastUpdated", DateTime.Now);

        // Parse display settings
        adventure.FontName = GetStringValue(root, "FontName", "Arial");
        adventure.FontSize = GetIntValue(root, "FontSize", 12);
        adventure.ShowFirstRoom = GetBoolValue(root, "ShowFirstLocation", true);
        adventure.ShowExits = GetBoolValue(root, "ShowExits", true);

        // Parse descriptions
        adventure.IntroductionText = ParseDescription(root, "Introduction");
        adventure.WinningText = ParseDescription(root, "EndGameText");

        // Parse items (these will be implemented in the conversion layer)
        ParseLocations(root, adventure);
        ParseObjects(root, adventure);
        ParseTasks(root, adventure);
        ParseCharacters(root, adventure);
        ParseEvents(root, adventure);
        ParseVariables(root, adventure);
        ParseGroups(root, adventure);
        ParseHints(root, adventure);
        ParseSynonyms(root, adventure);

        return adventure;
    }

    /// <summary>
    /// Generates XML from AdventureData object
    /// </summary>
    private static string GenerateXml(AdventureData adventure)
    {
        using var stringWriter = new StringWriter();
        using var xmlWriter = new XmlTextWriter(stringWriter)
        {
            Formatting = Formatting.Indented,
            Indentation = 2
        };

        xmlWriter.WriteStartDocument();
        xmlWriter.WriteStartElement("Adventure");

        // Write metadata
        xmlWriter.WriteElementString("Version", adventure.Version.ToString("F6"));
        xmlWriter.WriteElementString("LastUpdated", adventure.LastUpdated.ToString("yyyy-MM-dd HH:mm:ss"));
        xmlWriter.WriteElementString("Title", adventure.Title);
        xmlWriter.WriteElementString("Author", adventure.Author);

        // Write display settings
        xmlWriter.WriteElementString("FontName", adventure.FontName);
        xmlWriter.WriteElementString("FontSize", adventure.FontSize.ToString());
        xmlWriter.WriteElementString("ShowFirstLocation", adventure.ShowFirstRoom ? "1" : "0");
        xmlWriter.WriteElementString("ShowExits", adventure.ShowExits ? "1" : "0");

        // Write descriptions
        WriteDescription(xmlWriter, "Introduction", adventure.IntroductionText);
        WriteDescription(xmlWriter, "EndGameText", adventure.WinningText);

        // Write items
        WriteLocations(xmlWriter, adventure);
        WriteObjects(xmlWriter, adventure);
        WriteTasks(xmlWriter, adventure);
        WriteCharacters(xmlWriter, adventure);
        WriteEvents(xmlWriter, adventure);
        WriteVariables(xmlWriter, adventure);
        WriteGroups(xmlWriter, adventure);
        WriteHints(xmlWriter, adventure);
        WriteSynonyms(xmlWriter, adventure);

        xmlWriter.WriteEndElement(); // Adventure
        xmlWriter.WriteEndDocument();

        return stringWriter.ToString();
    }

    // XML parsing helper methods
    private static string GetStringValue(XmlElement parent, string elementName, string defaultValue = "")
    {
        var element = parent.SelectSingleNode(elementName) as XmlElement;
        return element?.InnerText ?? defaultValue;
    }

    private static int GetIntValue(XmlElement parent, string elementName, int defaultValue = 0)
    {
        var value = GetStringValue(parent, elementName);
        return int.TryParse(value, out int result) ? result : defaultValue;
    }

    private static double GetDoubleValue(XmlElement parent, string elementName, double defaultValue = 0.0)
    {
        var value = GetStringValue(parent, elementName);
        return double.TryParse(value, out double result) ? result : defaultValue;
    }

    private static bool GetBoolValue(XmlElement parent, string elementName, bool defaultValue = false)
    {
        var value = GetStringValue(parent, elementName);
        return value == "1" || value.Equals("true", StringComparison.OrdinalIgnoreCase);
    }

    private static DateTime GetDateTimeValue(XmlElement parent, string elementName, DateTime defaultValue)
    {
        var value = GetStringValue(parent, elementName);
        return DateTime.TryParse(value, out DateTime result) ? result : defaultValue;
    }

    private static string ParseDescription(XmlElement parent, string elementName)
    {
        var descElement = parent.SelectSingleNode($"{elementName}/Description/Text") as XmlElement;
        return descElement?.InnerText ?? "";
    }

    private static void WriteDescription(XmlTextWriter writer, string elementName, string text)
    {
        if (string.IsNullOrWhiteSpace(text)) return;

        writer.WriteStartElement(elementName);
        writer.WriteStartElement("Description");
        writer.WriteElementString("DisplayWhen", "Always");
        writer.WriteElementString("Text", text);
        writer.WriteEndElement(); // Description
        writer.WriteEndElement(); // elementName
    }

    // Parsing methods - delegate to TafXmlParser
    private static void ParseLocations(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseLocations(root, adventure);
    private static void ParseObjects(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseObjects(root, adventure);
    private static void ParseTasks(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseTasks(root, adventure);
    private static void ParseCharacters(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseCharacters(root, adventure);
    private static void ParseEvents(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseEvents(root, adventure);
    private static void ParseVariables(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseVariables(root, adventure);
    private static void ParseGroups(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseGroups(root, adventure);
    private static void ParseHints(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseHints(root, adventure);
    private static void ParseSynonyms(XmlElement root, AdventureData adventure) =>
        TafXmlParser.ParseSynonyms(root, adventure);

    // Writing methods - delegate to TafXmlWriter
    private static void WriteLocations(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteLocations(writer, adventure);
    private static void WriteObjects(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteObjects(writer, adventure);
    private static void WriteTasks(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteTasks(writer, adventure);
    private static void WriteCharacters(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteCharacters(writer, adventure);
    private static void WriteEvents(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteEvents(writer, adventure);
    private static void WriteVariables(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteVariables(writer, adventure);
    private static void WriteGroups(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteGroups(writer, adventure);
    private static void WriteHints(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteHints(writer, adventure);
    private static void WriteSynonyms(XmlTextWriter writer, AdventureData adventure) =>
        TafXmlWriter.WriteSynonyms(writer, adventure);

    /// <summary>
    /// Exposes the obfuscation key for validation purposes (used by AdventureLoader)
    /// </summary>
    internal static byte[] GetObfuscationKeyForValidation() => OBFUSCATION_KEY;
}
