using ADRIFT.Core.Models;
using System.Text.RegularExpressions;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Parses player commands and matches them to tasks
/// </summary>
public class CommandParser
{
    private readonly Adventure _adventure;
    private readonly Dictionary<string, List<string>> _synonyms;

    public CommandParser(Adventure adventure)
    {
        _adventure = adventure;
        _synonyms = BuildSynonymDictionary();
    }

    private Dictionary<string, List<string>> BuildSynonymDictionary()
    {
        var dict = new Dictionary<string, List<string>>(StringComparer.OrdinalIgnoreCase);

        foreach (var synonym in _adventure.Synonyms.Values)
        {
            foreach (var word in synonym.Words)
            {
                if (!dict.ContainsKey(word))
                {
                    dict[word] = new List<string>();
                }
                dict[word].AddRange(synonym.Words.Where(w => !string.Equals(w, word, StringComparison.OrdinalIgnoreCase)));
            }
        }

        return dict;
    }

    /// <summary>
    /// Parse a player command and find matching tasks
    /// </summary>
    public List<TaskMatch> ParseCommand(string input, GameState gameState)
    {
        if (string.IsNullOrWhiteSpace(input))
            return new List<TaskMatch>();

        // Normalize input
        input = input.Trim().ToLower();

        // Expand synonyms
        var expandedInputs = ExpandSynonyms(input);

        var matches = new List<TaskMatch>();

        // Check all tasks
        foreach (var task in _adventure.Tasks.Values.OrderByDescending(t => t.Priority))
        {
            foreach (var command in task.Commands)
            {
                foreach (var expandedInput in expandedInputs)
                {
                    var match = TryMatchCommand(expandedInput, command, task, gameState);
                    if (match != null)
                    {
                        matches.Add(match);
                    }
                }
            }
        }

        // Sort by priority and specificity
        return matches.OrderByDescending(m => m.Task.Priority)
                     .ThenByDescending(m => m.Specificity)
                     .ToList();
    }

    private List<string> ExpandSynonyms(string input)
    {
        var results = new List<string> { input };
        var words = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var word in words)
        {
            if (_synonyms.TryGetValue(word, out var synonymList))
            {
                var newResults = new List<string>();
                foreach (var result in results)
                {
                    newResults.Add(result);
                    foreach (var synonym in synonymList)
                    {
                        newResults.Add(result.Replace(word, synonym, StringComparison.OrdinalIgnoreCase));
                    }
                }
                results = newResults;
            }
        }

        return results.Distinct().ToList();
    }

    private TaskMatch? TryMatchCommand(string input, TaskCommand command, Core.Models.Task task, GameState gameState)
    {
        var pattern = command.Command.ToLower();
        var parameters = new Dictionary<string, string>();

        // Handle wildcards and parameters
        if (command.IsRegex)
        {
            // Use regex matching
            try
            {
                var regex = new Regex(pattern, RegexOptions.IgnoreCase);
                var match = regex.Match(input);
                if (match.Success)
                {
                    return new TaskMatch
                    {
                        Task = task,
                        Command = command,
                        Input = input,
                        Parameters = parameters,
                        Specificity = CalculateSpecificity(pattern, input)
                    };
                }
            }
            catch
            {
                // Invalid regex, fall back to pattern matching
            }
        }

        // Pattern matching with %object%, %character%, etc.
        var matchResult = MatchPattern(input, pattern, gameState, parameters);
        if (matchResult)
        {
            return new TaskMatch
            {
                Task = task,
                Command = command,
                Input = input,
                Parameters = parameters,
                Specificity = CalculateSpecificity(pattern, input)
            };
        }

        return null;
    }

    private bool MatchPattern(string input, string pattern, GameState gameState, Dictionary<string, string> parameters)
    {
        // Split pattern and input into tokens
        var patternTokens = TokenizePattern(pattern);
        var inputWords = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        return MatchTokens(inputWords, 0, patternTokens, 0, gameState, parameters);
    }

    private List<PatternToken> TokenizePattern(string pattern)
    {
        var tokens = new List<PatternToken>();
        var parts = pattern.Split(' ', StringSplitOptions.RemoveEmptyEntries);

        foreach (var part in parts)
        {
            if (part.StartsWith("%") && part.EndsWith("%"))
            {
                var paramName = part.Trim('%');
                tokens.Add(new PatternToken
                {
                    Type = TokenType.Parameter,
                    Value = paramName,
                    IsWildcard = paramName == "*" || paramName == "text"
                });
            }
            else if (part == "*")
            {
                tokens.Add(new PatternToken
                {
                    Type = TokenType.Parameter,
                    Value = "*",
                    IsWildcard = true
                });
            }
            else
            {
                tokens.Add(new PatternToken
                {
                    Type = TokenType.Literal,
                    Value = part
                });
            }
        }

        return tokens;
    }

    private bool MatchTokens(string[] inputWords, int inputPos, List<PatternToken> patternTokens, int patternPos,
                            GameState gameState, Dictionary<string, string> parameters)
    {
        // Base cases
        if (patternPos >= patternTokens.Count && inputPos >= inputWords.Length)
            return true; // Both exhausted = success

        if (patternPos >= patternTokens.Count)
            return false; // Pattern exhausted but input remains

        if (inputPos >= inputWords.Length)
        {
            // Input exhausted - check if remaining pattern tokens are optional
            return false;
        }

        var currentToken = patternTokens[patternPos];

        if (currentToken.Type == TokenType.Literal)
        {
            // Must match exactly
            if (string.Equals(currentToken.Value, inputWords[inputPos], StringComparison.OrdinalIgnoreCase))
            {
                return MatchTokens(inputWords, inputPos + 1, patternTokens, patternPos + 1, gameState, parameters);
            }
            return false;
        }
        else if (currentToken.Type == TokenType.Parameter)
        {
            if (currentToken.IsWildcard)
            {
                // Wildcard - try matching 1+ words
                for (int wordCount = 1; wordCount <= inputWords.Length - inputPos; wordCount++)
                {
                    var captured = string.Join(" ", inputWords.Skip(inputPos).Take(wordCount));
                    parameters[currentToken.Value] = captured;

                    if (MatchTokens(inputWords, inputPos + wordCount, patternTokens, patternPos + 1, gameState, parameters))
                    {
                        return true;
                    }
                }
                parameters.Remove(currentToken.Value);
                return false;
            }
            else
            {
                // Entity parameter - try to match an object, character, location, etc.
                var matchedEntity = TryMatchEntity(inputWords, inputPos, currentToken.Value, gameState, out int wordsConsumed);
                if (matchedEntity != null)
                {
                    parameters[currentToken.Value] = matchedEntity;
                    return MatchTokens(inputWords, inputPos + wordsConsumed, patternTokens, patternPos + 1, gameState, parameters);
                }
                return false;
            }
        }

        return false;
    }

    private string? TryMatchEntity(string[] inputWords, int startPos, string entityType, GameState gameState, out int wordsConsumed)
    {
        wordsConsumed = 0;

        // Try matching progressively longer phrases (greedy)
        for (int length = Math.Min(5, inputWords.Length - startPos); length > 0; length--)
        {
            var phrase = string.Join(" ", inputWords.Skip(startPos).Take(length)).ToLower();

            switch (entityType.ToLower())
            {
                case "object":
                case "objects":
                    var obj = FindObject(phrase, gameState);
                    if (obj != null)
                    {
                        wordsConsumed = length;
                        return obj.Key;
                    }
                    break;

                case "character":
                case "characters":
                    var character = FindCharacter(phrase, gameState);
                    if (character != null)
                    {
                        wordsConsumed = length;
                        return character.Key;
                    }
                    break;

                case "direction":
                    var direction = FindDirection(phrase);
                    if (direction != null)
                    {
                        wordsConsumed = length;
                        return direction;
                    }
                    break;
            }
        }

        return null;
    }

    private AdriftObject? FindObject(string phrase, GameState gameState)
    {
        phrase = phrase.ToLower();

        // First, check objects visible to the player
        var visibleObjects = GetVisibleObjects(gameState);

        foreach (var obj in visibleObjects)
        {
            // Check full name
            if (obj.FullName.Equals(phrase, StringComparison.OrdinalIgnoreCase))
                return obj;

            // Check name alone
            if (obj.Name.Equals(phrase, StringComparison.OrdinalIgnoreCase))
                return obj;

            // Check aliases
            if (obj.Aliases.Any(a => a.Equals(phrase, StringComparison.OrdinalIgnoreCase)))
                return obj;

            // Check partial matches (e.g., "key" matches "brass key")
            if (obj.FullName.Contains(phrase, StringComparison.OrdinalIgnoreCase))
                return obj;
        }

        return null;
    }

    private Character? FindCharacter(string phrase, GameState gameState)
    {
        phrase = phrase.ToLower();

        // Check characters visible to the player
        var visibleCharacters = GetVisibleCharacters(gameState);

        foreach (var character in visibleCharacters)
        {
            if (character.FullName.Equals(phrase, StringComparison.OrdinalIgnoreCase))
                return character;

            if (character.Name.Equals(phrase, StringComparison.OrdinalIgnoreCase))
                return character;

            if (character.Aliases.Any(a => a.Equals(phrase, StringComparison.OrdinalIgnoreCase)))
                return character;
        }

        return null;
    }

    private string? FindDirection(string phrase)
    {
        var directions = new[] { "north", "south", "east", "west", "northeast", "northwest", "southeast", "southwest",
                                "up", "down", "in", "out", "n", "s", "e", "w", "ne", "nw", "se", "sw", "u", "d" };

        var directionMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
        {
            { "n", "North" }, { "s", "South" }, { "e", "East" }, { "w", "West" },
            { "ne", "Northeast" }, { "nw", "Northwest" }, { "se", "Southeast" }, { "sw", "Southwest" },
            { "u", "Up" }, { "d", "Down" },
            { "north", "North" }, { "south", "South" }, { "east", "East" }, { "west", "West" },
            { "northeast", "Northeast" }, { "northwest", "Northwest" },
            { "southeast", "Southeast" }, { "southwest", "Southwest" },
            { "up", "Up" }, { "down", "Down" }, { "in", "In" }, { "out", "Out" }
        };

        if (directionMap.TryGetValue(phrase, out var direction))
            return direction;

        return null;
    }

    private List<AdriftObject> GetVisibleObjects(GameState gameState)
    {
        var visible = new List<AdriftObject>();

        // Objects at current location
        foreach (var obj in _adventure.Objects.Values)
        {
            if (obj.LocationType == ObjectLocation.AtLocation && obj.LocationKey == gameState.CurrentLocationKey)
            {
                visible.Add(obj);
            }
        }

        // Objects in inventory
        if (gameState.Inventory != null)
        {
            visible.AddRange(gameState.Inventory);
        }

        return visible;
    }

    private List<Character> GetVisibleCharacters(GameState gameState)
    {
        var visible = new List<Character>();

        foreach (var character in _adventure.Characters.Values)
        {
            // Check if character is at current location
            var charLocation = gameState.CharacterLocations.GetValueOrDefault(character.Key);
            if (charLocation == gameState.CurrentLocationKey)
            {
                visible.Add(character);
            }
        }

        return visible;
    }

    private int CalculateSpecificity(string pattern, string input)
    {
        // More specific patterns get higher scores
        int score = 0;

        // Fewer wildcards = more specific
        int wildcards = pattern.Count(c => c == '%' || c == '*');
        score += (10 - wildcards) * 10;

        // Longer patterns = more specific
        score += pattern.Length;

        // Exact length match = bonus
        if (pattern.Split(' ').Length == input.Split(' ').Length)
            score += 20;

        return score;
    }
}

public class TaskMatch
{
    public Core.Models.Task Task { get; set; } = null!;
    public TaskCommand Command { get; set; } = null!;
    public string Input { get; set; } = string.Empty;
    public Dictionary<string, string> Parameters { get; set; } = new();
    public int Specificity { get; set; }
}

internal class PatternToken
{
    public TokenType Type { get; set; }
    public string Value { get; set; } = string.Empty;
    public bool IsWildcard { get; set; }
}

internal enum TokenType
{
    Literal,
    Parameter
}
