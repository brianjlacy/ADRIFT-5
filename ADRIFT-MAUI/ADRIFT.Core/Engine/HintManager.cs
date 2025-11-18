using ADRIFT.Core.Models;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Manages hint display and tracking
/// </summary>
public class HintManager
{
    private readonly Adventure _adventure;
    private readonly GameState _state;
    private readonly Dictionary<string, int> _hintLevelsShown; // HintKey -> level shown (0=none, 1=subtle, 2=medium, 3=spoiler)

    public HintManager(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _state = state;
        _hintLevelsShown = new Dictionary<string, int>();
    }

    /// <summary>
    /// Get all available hints based on current game state
    /// </summary>
    public List<Hint> GetAvailableHints()
    {
        var availableHints = new List<Hint>();

        foreach (var hint in _adventure.Hints.Values)
        {
            if (IsHintAvailable(hint))
            {
                availableHints.Add(hint);
            }
        }

        return availableHints.OrderBy(h => h.Question).ToList();
    }

    /// <summary>
    /// Check if a hint should be available
    /// </summary>
    private bool IsHintAvailable(Hint hint)
    {
        // If hint has no restrictions, it's always available
        if (hint.RestrictToTasks.Count == 0)
        {
            return true;
        }

        // Check if any of the restricted tasks are incomplete
        foreach (var taskKey in hint.RestrictToTasks)
        {
            if (!_state.IsTaskCompleted(taskKey))
            {
                return true; // Hint is available because task is not yet completed
            }
        }

        return false; // All related tasks are completed, hint no longer needed
    }

    /// <summary>
    /// Get the next hint level for a specific hint
    /// </summary>
    public string GetNextHintLevel(string hintKey)
    {
        if (!_adventure.Hints.TryGetValue(hintKey, out var hint))
        {
            return "Hint not found.";
        }

        var currentLevel = _hintLevelsShown.GetValueOrDefault(hintKey, 0);

        string hintText = currentLevel switch
        {
            0 => hint.SubtleHint,
            1 => hint.MediumHint,
            2 => hint.SpoilerHint,
            _ => "No more hints available."
        };

        // Update the level shown
        if (currentLevel < 3)
        {
            _hintLevelsShown[hintKey] = currentLevel + 1;
        }

        // Add level indicator
        var levelText = currentLevel switch
        {
            0 => "[Subtle Hint]",
            1 => "[Medium Hint]",
            2 => "[Spoiler]",
            _ => ""
        };

        return $"{levelText}\n{hintText}";
    }

    /// <summary>
    /// Get the current hint level shown for a hint
    /// </summary>
    public int GetCurrentLevel(string hintKey)
    {
        return _hintLevelsShown.GetValueOrDefault(hintKey, 0);
    }

    /// <summary>
    /// Reset hint tracking (for new game/restart)
    /// </summary>
    public void Reset()
    {
        _hintLevelsShown.Clear();
    }

    /// <summary>
    /// Get hint level name
    /// </summary>
    public string GetHintLevelName(int level)
    {
        return level switch
        {
            0 => "Not yet shown",
            1 => "Subtle hint shown",
            2 => "Medium hint shown",
            3 => "Spoiler shown",
            _ => "Unknown"
        };
    }

    /// <summary>
    /// Check if there are any available hints
    /// </summary>
    public bool HasAvailableHints()
    {
        return GetAvailableHints().Count > 0;
    }
}
