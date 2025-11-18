using ADRIFT.Core.Models;
using ADRIFT.Engine.Converters;
using ADRIFT.Engine.FileIO;

namespace ADRIFT.Engine;

/// <summary>
/// High-level API for loading and saving ADRIFT adventures
/// Handles file I/O, conversion, and error handling
/// </summary>
public class AdventureLoader
{
    /// <summary>
    /// Loads an ADRIFT adventure from a TAF file
    /// </summary>
    /// <param name="filePath">Path to the .taf file</param>
    /// <param name="password">Optional password for protected adventures</param>
    /// <returns>Loaded Adventure object</returns>
    /// <exception cref="FileNotFoundException">If the file doesn't exist</exception>
    /// <exception cref="UnauthorizedAccessException">If password is incorrect</exception>
    /// <exception cref="InvalidDataException">If the file format is invalid</exception>
    public static async Task<Adventure> LoadAdventureAsync(string filePath, string? password = null)
    {
        return await Task.Run(() =>
        {
            // Load TAF file
            var adventureData = TafFileIO.LoadTafFile(filePath, password);

            if (adventureData == null)
                throw new InvalidDataException($"Failed to load adventure from {filePath}");

            // Convert to MAUI model
            var adventure = AdventureConverter.FromAdventureData(adventureData);

            return adventure;
        });
    }

    /// <summary>
    /// Saves an ADRIFT adventure to a TAF file
    /// </summary>
    /// <param name="filePath">Path to save the .taf file</param>
    /// <param name="adventure">Adventure to save</param>
    /// <param name="compress">Whether to compress the file (default: true)</param>
    /// <param name="password">Optional password to protect the adventure</param>
    public static async Task SaveAdventureAsync(string filePath, Adventure adventure, bool compress = true, string? password = null)
    {
        await Task.Run(() =>
        {
            // Convert to AdventureData
            var adventureData = AdventureConverter.ToAdventureData(adventure);

            // Save TAF file
            TafFileIO.SaveTafFile(filePath, adventureData, compress, password);
        });
    }

    /// <summary>
    /// Checks if a file is a valid ADRIFT TAF file
    /// </summary>
    public static bool IsValidTafFile(string filePath)
    {
        if (!File.Exists(filePath))
            return false;

        try
        {
            using var fileStream = File.OpenRead(filePath);
            using var reader = new BinaryReader(fileStream);

            // Read first 12 bytes and check if it looks like a TAF file
            if (fileStream.Length < 12)
                return false;

            var header = reader.ReadBytes(12);

            // Check for "Version" signature (encoded)
            var decoded = new byte[header.Length];
            var key = TafFileIO.GetObfuscationKeyForValidation();

            for (int i = 0; i < header.Length && i < key.Length; i++)
            {
                decoded[i] = (byte)(header[i] ^ key[i]);
            }

            var headerStr = System.Text.Encoding.ASCII.GetString(decoded);
            return headerStr.StartsWith("Version ", StringComparison.OrdinalIgnoreCase);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Checks if a TAF file is password protected
    /// </summary>
    public static bool IsPasswordProtected(string filePath)
    {
        if (!File.Exists(filePath))
            return false;

        try
        {
            using var fileStream = File.OpenRead(filePath);

            if (fileStream.Length < 14)
                return false;

            // Seek to -14 bytes from end
            fileStream.Seek(-14, SeekOrigin.End);

            var reader = new BinaryReader(fileStream);
            var encodedPassword = reader.ReadBytes(8);

            // Decode to check if it's not empty
            var key = TafFileIO.GetObfuscationKeyForValidation();
            var decoded = new byte[8];

            for (int i = 0; i < 8; i++)
            {
                decoded[i] = (byte)(encodedPassword[i] ^ key[i]);
            }

            var passwordStr = System.Text.Encoding.ASCII.GetString(decoded).TrimEnd('\0');
            return !string.IsNullOrWhiteSpace(passwordStr);
        }
        catch
        {
            return false;
        }
    }

    /// <summary>
    /// Gets basic metadata from a TAF file without fully loading it
    /// </summary>
    public static async Task<AdventureMetadata?> GetMetadataAsync(string filePath)
    {
        return await Task.Run(() =>
        {
            try
            {
                // For now, we need to load the full file to get metadata
                // In the future, we could optimize this to read only the header
                var adventure = LoadAdventureAsync(filePath).Result;

                return new AdventureMetadata
                {
                    Title = adventure.Title,
                    Author = adventure.Author,
                    Version = adventure.Version,
                    Created = adventure.Created,
                    Modified = adventure.Modified,
                    TotalItems = adventure.TotalItems,
                    IsPasswordProtected = IsPasswordProtected(filePath)
                };
            }
            catch
            {
                return null;
            }
        });
    }
}

/// <summary>
/// Metadata about an adventure file
/// </summary>
public class AdventureMetadata
{
    public string Title { get; set; } = "";
    public string Author { get; set; } = "";
    public string Version { get; set; } = "";
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; }
    public int TotalItems { get; set; }
    public bool IsPasswordProtected { get; set; }
}
