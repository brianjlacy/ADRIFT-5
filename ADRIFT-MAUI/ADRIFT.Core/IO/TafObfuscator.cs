using System;

namespace ADRIFT.Core.IO;

/// <summary>
/// Handles obfuscation/deobfuscation of ADRIFT TAF files.
/// Uses the same algorithm as the original ADRIFT with seed 1976.
/// </summary>
public static class TafObfuscator
{
    private const int OBFUSCATION_SEED = 1976;

    /// <summary>
    /// Deobfuscate (decode) a byte array with optional offset
    /// </summary>
    public static byte[] Deobfuscate(byte[] data, long offset = 1)
    {
        if (data == null || data.Length == 0)
            return data;

        var random = new Random(OBFUSCATION_SEED);

        // Skip random numbers based on offset
        for (long i = 1; i < offset; i++)
        {
            random.Next(256);
        }

        var result = new byte[data.Length];
        for (int i = 0; i < data.Length; i++)
        {
            // Generate random value 0-254 (matches VB.NET Rnd() * 255 - 0.5)
            var randomValue = random.Next(255);
            result[i] = (byte)((data[i] ^ randomValue) % 256);
        }

        return result;
    }

    /// <summary>
    /// Obfuscate (encode) a byte array with optional offset
    /// Note: XOR is symmetric, so obfuscate and deobfuscate use the same operation
    /// </summary>
    public static byte[] Obfuscate(byte[] data, long offset = 1)
    {
        return Deobfuscate(data, offset);
    }

    /// <summary>
    /// Deobfuscate a byte array and convert to UTF-8 string
    /// </summary>
    public static string DeobfuscateToString(byte[] data, long offset = 1)
    {
        var deobfuscated = Deobfuscate(data, offset);
        return System.Text.Encoding.UTF8.GetString(deobfuscated);
    }

    /// <summary>
    /// Obfuscate a UTF-8 string to byte array
    /// </summary>
    public static byte[] ObfuscateFromString(string text, long offset = 1)
    {
        var bytes = System.Text.Encoding.UTF8.GetBytes(text);
        return Obfuscate(bytes, offset);
    }

    /// <summary>
    /// Check if bytes match expected obfuscated version patterns
    /// </summary>
    public static bool IsKnownVersionPattern(byte[] versionBytes)
    {
        if (versionBytes == null || versionBytes.Length != 12)
            return false;

        // Known obfuscated version patterns
        var version500 = new byte[] { 60, 66, 63, 201, 106, 135, 194, 207, 146, 69, 62, 97 };
        var version400 = new byte[] { 60, 66, 63, 201, 106, 135, 194, 207, 147, 69, 62, 97 };
        var version390 = new byte[] { 60, 66, 63, 201, 106, 135, 194, 207, 148, 69, 55, 97 };

        return ByteArraysEqual(versionBytes, version500) ||
               ByteArraysEqual(versionBytes, version400) ||
               ByteArraysEqual(versionBytes, version390);
    }

    /// <summary>
    /// Get version string from obfuscated or known pattern
    /// </summary>
    public static string GetVersionString(byte[] versionBytes)
    {
        if (versionBytes == null || versionBytes.Length != 12)
            return null;

        // Check known patterns first (for cross-platform compatibility)
        if (ByteArraysEqual(versionBytes, new byte[] { 60, 66, 63, 201, 106, 135, 194, 207, 146, 69, 62, 97 }))
            return "Version 5.00";
        if (ByteArraysEqual(versionBytes, new byte[] { 60, 66, 63, 201, 106, 135, 194, 207, 147, 69, 62, 97 }))
            return "Version 4.00";
        if (ByteArraysEqual(versionBytes, new byte[] { 60, 66, 63, 201, 106, 135, 194, 207, 148, 69, 55, 97 }))
            return "Version 3.90";

        // Try deobfuscating
        return DeobfuscateToString(versionBytes, 1);
    }

    private static bool ByteArraysEqual(byte[] a, byte[] b)
    {
        if (a.Length != b.Length) return false;
        for (int i = 0; i < a.Length; i++)
        {
            if (a[i] != b[i]) return false;
        }
        return true;
    }
}
