using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Manages multimedia resources (images, sounds) for ADRIFT games
/// </summary>
public class MultimediaManager
{
    private readonly Adventure _adventure;
    private readonly Dictionary<string, byte[]> _imageCache = new();
    private readonly Dictionary<string, byte[]> _soundCache = new();

    public MultimediaManager(Adventure adventure)
    {
        _adventure = adventure;
        LoadResources();
    }

    private void LoadResources()
    {
        // Load embedded resources from adventure
        foreach (var graphic in _adventure.Graphics.Values)
        {
            if (!string.IsNullOrEmpty(graphic.Data))
            {
                try
                {
                    var data = Convert.FromBase64String(graphic.Data);
                    _imageCache[graphic.Key] = data;
                }
                catch
                {
                    // Invalid base64 data
                }
            }
        }

        foreach (var sound in _adventure.Sounds.Values)
        {
            if (!string.IsNullOrEmpty(sound.Data))
            {
                try
                {
                    var data = Convert.FromBase64String(sound.Data);
                    _soundCache[sound.Key] = data;
                }
                catch
                {
                    // Invalid base64 data
                }
            }
        }
    }

    /// <summary>
    /// Get image data for a graphic key
    /// </summary>
    public byte[]? GetImageData(string key)
    {
        return _imageCache.GetValueOrDefault(key);
    }

    /// <summary>
    /// Get sound data for a sound key
    /// </summary>
    public byte[]? GetSoundData(string key)
    {
        return _soundCache.GetValueOrDefault(key);
    }

    /// <summary>
    /// Get image source for display
    /// </summary>
    public ImageSource? GetImageSource(string key)
    {
        var data = GetImageData(key);
        if (data == null)
            return null;

        try
        {
            return ImageSource.FromStream(() => new MemoryStream(data));
        }
        catch
        {
            return null;
        }
    }

    /// <summary>
    /// Check if an image exists
    /// </summary>
    public bool HasImage(string key)
    {
        return _imageCache.ContainsKey(key);
    }

    /// <summary>
    /// Check if a sound exists
    /// </summary>
    public bool HasSound(string key)
    {
        return _soundCache.ContainsKey(key);
    }

    /// <summary>
    /// Get all graphics for current location
    /// </summary>
    public List<Graphic> GetGraphicsForLocation(string locationKey)
    {
        return _adventure.Graphics.Values
            .Where(g => g.LocationKey == locationKey)
            .OrderBy(g => g.DisplayOrder)
            .ToList();
    }

    /// <summary>
    /// Play sound (platform-specific implementation needed)
    /// </summary>
    public async Task PlaySoundAsync(string key, bool loop = false)
    {
        var sound = _adventure.Sounds.Values.FirstOrDefault(s => s.Key == key);
        if (sound == null)
            return;

        // TODO: Platform-specific audio playback
        // For now, just log that sound should be played
        await Task.CompletedTask;
    }

    /// <summary>
    /// Stop all sounds
    /// </summary>
    public void StopAllSounds()
    {
        // TODO: Platform-specific audio control
    }
}
