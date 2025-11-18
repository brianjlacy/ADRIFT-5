using System.Text.RegularExpressions;
using ADRIFT.Core.Models;

namespace ADRIFT.Engine.Parser;

/// <summary>
/// Parses player commands and matches them to tasks
/// Modern replacement for Babel parser
/// </summary>
public class CommandParser
{
    private readonly Adventure _adventure;
    private readonly Dictionary<string, string> _synonyms;

    public CommandParser(Adventure adventure)
    {
        _adventure = adventure;
        _synonyms = BuildSynonymDictionary();
    }

    /// <summary>
    /// Parses a player command and returns matching tasks
    /// </summary>
    public List<TaskMatch> ParseCommand(string command)
    {
        if (string.IsNullOrWhiteSpace(command))
            return new List<TaskMatch>();

        // Normalize command
        command = NormalizeCommand(command);

        // Apply synonyms
        command = ApplySynonyms(command);

        // Find matching tasks
        var matches = new List<TaskMatch>();

        foreach (var task in _adventure.Tasks.Values)
        {
            var score = MatchTask(command, task);
            if (score > 0)
            {
                matches.Add(new TaskMatch
                {
                    Task = task,
                    Score = score,
                    Command = command
                });
            }
        }

        // Sort by score (highest first)
        matches.Sort((a, b) => b.Score.CompareTo(a.Score));

        return matches;
    }

    /// <summary>
    /// Normalizes a command (lowercase, trim, etc.)
    /// </summary>
    private string NormalizeCommand(string command)
    {
        command = command.Trim().ToLowerInvariant();

        // Remove multiple spaces
        command = Regex.Replace(command, @"\s+", " ");

        // Remove punctuation at end
        command = command.TrimEnd('.', '!', '?');

        return command;
    }

    /// <summary>
    /// Applies synonym replacements to the command
    /// </summary>
    private string ApplySynonyms(string command)
    {
        var words = command.Split(' ');

        for (int i = 0; i < words.Length; i++)
        {
            if (_synonyms.TryGetValue(words[i], out var replacement))
            {
                words[i] = replacement;
            }
        }

        return string.Join(" ", words);
    }

    /// <summary>
    /// Matches a command against a task's command patterns
    /// </summary>
    private int MatchTask(string command, ADRIFT.Core.Models.Task task)
    {
        int bestScore = 0;

        foreach (var taskCommand in task.Commands)
        {
            var score = MatchPattern(command, taskCommand.Command);
            if (score > bestScore)
                bestScore = score;
        }

        return bestScore;
    }

    /// <summary>
    /// Matches a command against a pattern
    /// Returns a score (0 = no match, higher = better match)
    /// </summary>
    private int MatchPattern(string command, string pattern)
    {
        // Convert ADRIFT pattern to regex
        // Patterns can include:
        // - Literal text: "take", "look at"
        // - Object references: "#object#"
        // - Character references: "#character#"
        // - Optional words: "[the]"
        // - Alternatives: "get/take"

        pattern = pattern.ToLowerInvariant();

        // Handle object/character/location references
        pattern = Regex.Replace(pattern, @"#(\w+)#", match =>
        {
            var refType = match.Groups[1].Value;
            return refType switch
            {
                "object" => @"(\w+(?:\s+\w+)*)", // Match object names
                "objects" => @"(\w+(?:\s+\w+)*)", // Match multiple objects
                "character" => @"(\w+(?:\s+\w+)*)", // Match character names
                "characters" => @"(\w+(?:\s+\w+)*)",
                "location" => @"(\w+(?:\s+\w+)*)", // Match location names
                "text" => @"(.+)", // Match any text
                "number" => @"(\d+)", // Match numbers
                _ => match.Value
            };
        });

        // Handle optional words [word]
        pattern = Regex.Replace(pattern, @"\[(\w+)\]", "(?:$1)?");

        // Handle alternatives word1/word2
        pattern = Regex.Replace(pattern, @"(\w+)/(\w+)", "(?:$1|$2)");

        // Escape special regex characters except those we've already processed
        pattern = Regex.Escape(pattern);

        // Unescape our patterns
        pattern = pattern.Replace(@"\(", "(").Replace(@"\)", ")");
        pattern = pattern.Replace(@"\?", "?").Replace(@"\|", "|");
        pattern = pattern.Replace(@"\+", "+").Replace(@"\*", "*");

        // Add word boundaries
        pattern = $@"^{pattern}$";

        try
        {
            var regex = new Regex(pattern, RegexOptions.IgnoreCase);
            var match = regex.Match(command);

            if (match.Success)
            {
                // Calculate score based on how specific the match is
                int score = 100;

                // Bonus for exact match
                if (match.Value == command)
                    score += 50;

                // Bonus for capturing groups (more specific)
                score += match.Groups.Count * 10;

                return score;
            }
        }
        catch
        {
            // Regex compilation failed, try simple string match
            if (command.Contains(pattern.ToLowerInvariant()))
                return 50;
        }

        return 0;
    }

    /// <summary>
    /// Builds synonym dictionary from adventure
    /// </summary>
    private Dictionary<string, string> BuildSynonymDictionary()
    {
        var dict = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        foreach (var synonym in _adventure.Synonyms.Values)
        {
            foreach (var word in synonym.Replacements)
            {
                dict[word.ToLowerInvariant()] = synonym.CommonName.ToLowerInvariant();
            }
        }

        return dict;
    }

    /// <summary>
    /// Extracts referenced objects/characters from a matched command
    /// </summary>
    public CommandReferences ExtractReferences(string command, string pattern)
    {
        var refs = new CommandReferences();

        // Convert pattern to regex and extract captured groups
        pattern = pattern.ToLowerInvariant();

        // Track what each group captures
        var captureTypes = new List<string>();

        pattern = Regex.Replace(pattern, @"#(\w+)#", match =>
        {
            var refType = match.Groups[1].Value;
            captureTypes.Add(refType);
            return refType switch
            {
                "object" or "objects" => @"(\w+(?:\s+\w+)*)",
                "character" or "characters" => @"(\w+(?:\s+\w+)*)",
                "location" => @"(\w+(?:\s+\w+)*)",
                "text" => @"(.+)",
                "number" => @"(\d+)",
                _ => match.Value
            };
        });

        try
        {
            var regex = new Regex($"^{pattern}$", RegexOptions.IgnoreCase);
            var match = regex.Match(command);

            if (match.Success)
            {
                for (int i = 1; i < match.Groups.Count; i++)
                {
                    var captureType = i - 1 < captureTypes.Count ? captureTypes[i - 1] : "unknown";
                    var value = match.Groups[i].Value;

                    switch (captureType)
                    {
                        case "object":
                        case "objects":
                            refs.Objects.Add(FindObject(value));
                            break;
                        case "character":
                        case "characters":
                            refs.Characters.Add(FindCharacter(value));
                            break;
                        case "location":
                            refs.Location = FindLocation(value);
                            break;
                        case "text":
                            refs.Text = value;
                            break;
                        case "number":
                            if (int.TryParse(value, out int num))
                                refs.Number = num;
                            break;
                    }
                }
            }
        }
        catch
        {
            // Ignore parsing errors
        }

        return refs;
    }

    private string? FindObject(string name)
    {
        name = name.ToLowerInvariant();
        foreach (var obj in _adventure.Objects.Values)
        {
            if (obj.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                obj.Aliases.Any(a => a.Equals(name, StringComparison.OrdinalIgnoreCase)))
            {
                return obj.Key;
            }
        }
        return null;
    }

    private string? FindCharacter(string name)
    {
        name = name.ToLowerInvariant();
        foreach (var character in _adventure.Characters.Values)
        {
            if (character.Name.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return character.Key;
            }
        }
        return null;
    }

    private string? FindLocation(string name)
    {
        name = name.ToLowerInvariant();
        foreach (var location in _adventure.Locations.Values)
        {
            if (location.ShortDescription.Equals(name, StringComparison.OrdinalIgnoreCase))
            {
                return location.Key;
            }
        }
        return null;
    }
}

/// <summary>
/// Represents a matched task with score
/// </summary>
public class TaskMatch
{
    public ADRIFT.Core.Models.Task Task { get; set; } = null!;
    public int Score { get; set; }
    public string Command { get; set; } = string.Empty;
}

/// <summary>
/// References extracted from a command
/// </summary>
public class CommandReferences
{
    public List<string?> Objects { get; set; } = new();
    public List<string?> Characters { get; set; } = new();
    public string? Location { get; set; }
    public string? Text { get; set; }
    public int? Number { get; set; }
}
