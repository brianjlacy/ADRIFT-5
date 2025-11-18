using ADRIFT.Core.Models;
using System.Text.RegularExpressions;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Formats text output with ALR (ADRIFT Language Resources) support
/// </summary>
public class TextFormatter
{
    private readonly Adventure _adventure;
    private readonly GameState _state;

    public TextFormatter(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _state = state;
    }

    /// <summary>
    /// Format text with variable and entity substitutions
    /// </summary>
    public string Format(string text, Dictionary<string, string>? parameters = null)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        parameters ??= new Dictionary<string, string>();

        // Process property access first (e.g., %object%.Property%)
        text = ProcessPropertyAccess(text, parameters);

        // Process ALR functions
        text = ProcessFunctions(text);

        // Replace variables
        text = ReplaceVariables(text);

        // Replace parameters
        text = ReplaceParameters(text, parameters);

        // Replace entity references
        text = ReplaceEntityReferences(text);

        // Process formatting tags
        text = ProcessFormatting(text);

        // Apply ALR (Alternate Reality Layer) text overrides LAST
        // This allows ALRs to override any generated text
        text = ProcessALRs(text);

        return text;
    }

    private string ProcessPropertyAccess(string text, Dictionary<string, string> parameters)
    {
        // Handle patterns like %object%.LongDescription%
        return Regex.Replace(text, @"%(\w+)%\.(\w+)%", m =>
        {
            var entityRef = m.Groups[1].Value;
            var propertyName = m.Groups[2].Value;

            // Resolve entity key from parameters
            var entityKey = parameters.GetValueOrDefault(entityRef, entityRef);

            // Get the property value
            if (_adventure.Objects.TryGetValue(entityKey, out var obj))
            {
                return GetObjectProperty(obj, propertyName);
            }

            if (_adventure.Characters.TryGetValue(entityKey, out var character))
            {
                return GetCharacterProperty(character, propertyName);
            }

            if (_adventure.Locations.TryGetValue(entityKey, out var location))
            {
                return GetLocationProperty(location, propertyName);
            }

            return m.Value; // Keep original if not found
        });
    }

    private string GetObjectProperty(AdriftObject obj, string propertyName)
    {
        return propertyName.ToLower() switch
        {
            "name" => obj.Name,
            "fullname" => obj.FullName,
            "article" => obj.Article,
            "prefix" => obj.Prefix,
            "shortdescription" => obj.ShortDescription,
            "longdescription" => obj.LongDescription,
            "description" => obj.LongDescription,
            "readingtext" => obj.ReadingText ?? "",
            _ => ""
        };
    }

    private string GetCharacterProperty(Character character, string propertyName)
    {
        return propertyName.ToLower() switch
        {
            "name" => character.Name,
            "fullname" => character.FullName,
            "description" => character.Description,
            "greeting" => character.GeneralGreeting,
            _ => ""
        };
    }

    private string GetLocationProperty(Location location, string propertyName)
    {
        return propertyName.ToLower() switch
        {
            "name" => location.ShortDescription,
            "shortdescription" => location.ShortDescription,
            "longdescription" => location.LongDescription,
            "description" => location.LongDescription,
            _ => ""
        };
    }

    private string ProcessFunctions(string text)
    {
        // ALR functions like %GetArticle[object]%, %Upper[text]%, etc.
        // Process in specific order to handle nested functions correctly

        // ADRIFT 5 Text Functions - Complete Implementation

        // ========== NAME REFERENCE FUNCTIONS (with explicit keys) ==========

        // %ObjectName[key]% - Get object name by key
        text = Regex.Replace(text, @"%ObjectName\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Objects.TryGetValue(key, out var obj))
                return obj.Name;
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %CharacterName[key]% - Get character name by key
        text = Regex.Replace(text, @"%CharacterName\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Characters.TryGetValue(key, out var character))
                return character.Name;
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %LocationName[key]% - Get location name by key
        text = Regex.Replace(text, @"%LocationName\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Locations.TryGetValue(key, out var location))
                return location.ShortDescription.GetText();
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %CharacterDescriptor[key]% - Get character descriptor
        text = Regex.Replace(text, @"%CharacterDescriptor\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Characters.TryGetValue(key, out var character))
                return character.Descriptor;
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %ObjectArticle[key]% - Get object article
        text = Regex.Replace(text, @"%ObjectArticle\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Objects.TryGetValue(key, out var obj))
                return obj.Article;
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // ========== LOCATION QUERY FUNCTIONS ==========

        // %CharacterLocation[key]% - Where is this character?
        text = Regex.Replace(text, @"%CharacterLocation\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Characters.TryGetValue(key, out var character))
            {
                var locationKey = _state.GetCharacterLocation(key);
                if (!string.IsNullOrEmpty(locationKey) && _adventure.Locations.TryGetValue(locationKey, out var location))
                    return location.ShortDescription.GetText();
            }
            return "unknown";
        }, RegexOptions.IgnoreCase);

        // %ObjectLocation[key]% - Where is this object?
        text = Regex.Replace(text, @"%ObjectLocation\[([^\]]+)\]%", m =>
        {
            var key = m.Groups[1].Value.Trim();
            if (_adventure.Objects.TryGetValue(key, out var obj))
            {
                var locationKey = _state.GetObjectLocation(key);
                if (!string.IsNullOrEmpty(locationKey) && _adventure.Locations.TryGetValue(locationKey, out var location))
                    return location.ShortDescription.GetText();
                else if (_state.HasObject(key))
                    return "inventory";
            }
            return "unknown";
        }, RegexOptions.IgnoreCase);

        // %DisplayLocation% - Current location description
        text = Regex.Replace(text, @"%DisplayLocation%", m =>
        {
            var location = _state.GetCurrentLocation();
            return location?.LongDescription.GetText() ?? "";
        }, RegexOptions.IgnoreCase);

        // ========== LIST FUNCTIONS (with specific location keys) ==========

        // %ListObjectsAtLocation[key]% - List objects at specific location
        text = Regex.Replace(text, @"%ListObjectsAtLocation\[([^\]]+)\]%", m =>
        {
            var locationKey = m.Groups[1].Value.Trim();
            return ListObjectsAtLocation(locationKey);
        }, RegexOptions.IgnoreCase);

        // %ListCharactersAtLocation[key]% - List characters at specific location
        text = Regex.Replace(text, @"%ListCharactersAtLocation\[([^\]]+)\]%", m =>
        {
            var locationKey = m.Groups[1].Value.Trim();
            return ListCharactersAtLocation(locationKey);
        }, RegexOptions.IgnoreCase);

        // ========== GAME STATE FUNCTIONS ==========

        // %Player% - Player character name
        text = Regex.Replace(text, @"%Player%", m => _state.PlayerName, RegexOptions.IgnoreCase);

        // %MaxScore% - Maximum possible score
        text = Regex.Replace(text, @"%MaxScore%", m => _adventure.MaxScore.ToString(), RegexOptions.IgnoreCase);

        // %Time% - Current game time
        text = Regex.Replace(text, @"%Time%", m => FormatGameTime(), RegexOptions.IgnoreCase);

        // ========== DIRECTION FUNCTIONS ==========

        // %Direction[abbrev]% - Convert direction abbreviation to full name
        text = Regex.Replace(text, @"%Direction\[([^\]]+)\]%", m =>
        {
            var dir = m.Groups[1].Value.Trim().ToUpper();
            return dir switch
            {
                "N" => "north",
                "NE" => "northeast",
                "E" => "east",
                "SE" => "southeast",
                "S" => "south",
                "SW" => "southwest",
                "W" => "west",
                "NW" => "northwest",
                "U" or "UP" => "up",
                "D" or "DOWN" => "down",
                "IN" => "in",
                "OUT" => "out",
                _ => dir.ToLower()
            };
        }, RegexOptions.IgnoreCase);

        // ========== PROPERTY FUNCTIONS ==========

        // %Property[key,propertyName]% - Get property value
        text = Regex.Replace(text, @"%Property\[([^,]+),([^\]]+)\]%", m =>
        {
            var itemKey = m.Groups[1].Value.Trim();
            var propertyName = m.Groups[2].Value.Trim();
            return GetPropertyValue(itemKey, propertyName);
        }, RegexOptions.IgnoreCase);

        // ========== EXPRESSION EVALUATION ==========

        // %Expr[expression]% - Evaluate mathematical expression
        text = Regex.Replace(text, @"%Expr\[([^\]]+)\]%", m =>
        {
            var expression = m.Groups[1].Value.Trim();
            return EvaluateExpression(expression);
        }, RegexOptions.IgnoreCase);

        // ========== ADVANCED CONDITIONAL (ADRIFT 5 syntax) ==========

        // %if[condition]truetext%else%falsetext%ifend% - ADRIFT 5 conditional syntax
        text = Regex.Replace(text, @"%if\[([^\]]+)\](.+?)%else%(.+?)%ifend%", m =>
        {
            var condition = m.Groups[1].Value.Trim();
            var trueText = m.Groups[2].Value;
            var falseText = m.Groups[3].Value;
            bool result = EvaluateCondition(condition);
            return result ? trueText : falseText;
        }, RegexOptions.IgnoreCase);

        // %if[condition]text%ifend% - ADRIFT 5 conditional without else
        text = Regex.Replace(text, @"%if\[([^\]]+)\](.+?)%ifend%", m =>
        {
            var condition = m.Groups[1].Value.Trim();
            var trueText = m.Groups[2].Value;
            bool result = EvaluateCondition(condition);
            return result ? trueText : "";
        }, RegexOptions.IgnoreCase);

        // ========== TEXT CASE FUNCTIONS ==========

        // %Sentence[text]% - Sentence case (first letter uppercase, rest as-is)
        text = Regex.Replace(text, @"%Sentence\[([^\]]+)\]%", m =>
        {
            var content = m.Groups[1].Value;
            if (string.IsNullOrEmpty(content)) return content;
            return char.ToUpper(content[0]) + content.Substring(1);
        }, RegexOptions.IgnoreCase);

        // %Upper[text]% - Uppercase first letter
        text = Regex.Replace(text, @"%Upper\[([^\]]+)\]%", m =>
        {
            var content = m.Groups[1].Value;
            return string.IsNullOrEmpty(content) ? content : char.ToUpper(content[0]) + content.Substring(1);
        }, RegexOptions.IgnoreCase);

        // %Lower[text]% - Lowercase first letter
        text = Regex.Replace(text, @"%Lower\[([^\]]+)\]%", m =>
        {
            var content = m.Groups[1].Value;
            return string.IsNullOrEmpty(content) ? content : char.ToLower(content[0]) + content.Substring(1);
        }, RegexOptions.IgnoreCase);

        // %Caps[text]% - ALL CAPS
        text = Regex.Replace(text, @"%Caps\[([^\]]+)\]%", m =>
        {
            return m.Groups[1].Value.ToUpper();
        }, RegexOptions.IgnoreCase);

        // %Proper[text]% - Title Case
        text = Regex.Replace(text, @"%Proper\[([^\]]+)\]%", m =>
        {
            var content = m.Groups[1].Value;
            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(content.ToLower());
        }, RegexOptions.IgnoreCase);

        // ========== ARTICLE FUNCTIONS ==========

        // %a[object]% - "a" or "an" (indefinite article only)
        text = Regex.Replace(text, @"%a\[([^\]]+)\]%", m =>
        {
            var entityRef = m.Groups[1].Value;
            var entity = ResolveEntity(entityRef);
            if (entity != null)
            {
                return StartsWithVowelSound(entity) ? "an" : "a";
            }
            return "a";
        }, RegexOptions.IgnoreCase);

        // %the[object]% - "the" (definite article only)
        text = Regex.Replace(text, @"%the\[([^\]]+)\]%", m =>
        {
            return "the";
        }, RegexOptions.IgnoreCase);

        // %TheOf[object]% - "the object" or "object's"
        text = Regex.Replace(text, @"%TheOf\[([^\]]+)\]%", m =>
        {
            var entityRef = m.Groups[1].Value;
            var entity = ResolveEntity(entityRef);
            return entity != null ? $"the {entity}" : entityRef;
        }, RegexOptions.IgnoreCase);

        // %The[object]% - "the object"
        text = Regex.Replace(text, @"%The\[([^\]]+)\]%", m =>
        {
            var entityRef = m.Groups[1].Value;
            var entity = ResolveEntity(entityRef);
            return entity != null ? $"the {entity}" : entityRef;
        }, RegexOptions.IgnoreCase);

        // %Name[object]% - object name without article
        text = Regex.Replace(text, @"%Name\[([^\]]+)\]%", m =>
        {
            var entityRef = m.Groups[1].Value;
            var entity = ResolveEntity(entityRef);
            return entity ?? entityRef;
        }, RegexOptions.IgnoreCase);

        // %ALong[object]% - "a" or "an" + object
        text = Regex.Replace(text, @"%ALong\[([^\]]+)\]%", m =>
        {
            var entityRef = m.Groups[1].Value;
            var entity = ResolveEntity(entityRef);
            if (entity != null)
            {
                var article = StartsWithVowelSound(entity) ? "an" : "a";
                return $"{article} {entity}";
            }
            return entityRef;
        }, RegexOptions.IgnoreCase);

        // ========== PRONOUN FUNCTIONS ==========

        // Context-sensitive pronouns based on last referenced character/object
        // %he%, %she%, %it%, %they% - Subject pronouns
        text = Regex.Replace(text, @"%he%", m => GetSubjectPronoun("male"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%she%", m => GetSubjectPronoun("female"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%it%", m => GetSubjectPronoun("neuter"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%they%", m => GetSubjectPronoun("plural"), RegexOptions.IgnoreCase);

        // %him%, %her%, %them% - Object pronouns
        text = Regex.Replace(text, @"%him%", m => GetObjectPronoun("male"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%her%", m => GetObjectPronoun("female"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%them%", m => GetObjectPronoun("plural"), RegexOptions.IgnoreCase);

        // %his%, %her%, %its%, %their% - Possessive pronouns
        text = Regex.Replace(text, @"%his%", m => GetPossessivePronoun("male"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%her%", m => GetPossessivePronoun("female"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%its%", m => GetPossessivePronoun("neuter"), RegexOptions.IgnoreCase);
        text = Regex.Replace(text, @"%their%", m => GetPossessivePronoun("plural"), RegexOptions.IgnoreCase);

        // %UCase[text]% - ALL UPPERCASE
        text = Regex.Replace(text, @"%UCase\[([^\]]+)\]%", m =>
        {
            return m.Groups[1].Value.ToUpper();
        }, RegexOptions.IgnoreCase);

        // %LCase[text]% - all lowercase
        text = Regex.Replace(text, @"%LCase\[([^\]]+)\]%", m =>
        {
            return m.Groups[1].Value.ToLower();
        }, RegexOptions.IgnoreCase);

        // %Number[n]% - Convert number to words
        text = Regex.Replace(text, @"%Number\[(\d+)\]%", m =>
        {
            if (int.TryParse(m.Groups[1].Value, out var num))
            {
                return NumberToWords(num);
            }
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %ListObjects[]% - List objects at current location
        text = Regex.Replace(text, @"%ListObjects\[\]%", m =>
        {
            return ListObjectsAtCurrentLocation();
        }, RegexOptions.IgnoreCase);

        // %ListCharacters[]% - List characters at current location
        text = Regex.Replace(text, @"%ListCharacters\[\]%", m =>
        {
            return ListCharactersAtCurrentLocation();
        }, RegexOptions.IgnoreCase);

        // %ListExits[]% - List available exits
        text = Regex.Replace(text, @"%ListExits\[\]%", m =>
        {
            return ListExitsFromCurrentLocation();
        }, RegexOptions.IgnoreCase);

        // %Either[option1|option2|option3]% - Random choice
        text = Regex.Replace(text, @"%Either\[([^\]]+)\]%", m =>
        {
            var options = m.Groups[1].Value.Split('|');
            if (options.Length > 0)
            {
                var random = new Random();
                return options[random.Next(options.Length)].Trim();
            }
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %Random[min,max]% - Random number in range
        text = Regex.Replace(text, @"%Random\[(\d+),(\d+)\]%", m =>
        {
            if (int.TryParse(m.Groups[1].Value, out var min) &&
                int.TryParse(m.Groups[2].Value, out var max))
            {
                var random = new Random();
                return random.Next(min, max + 1).ToString();
            }
            return m.Value;
        }, RegexOptions.IgnoreCase);

        // %If[condition,true_text,false_text]% - Conditional text
        text = Regex.Replace(text, @"%If\[([^,]+),([^,]+),([^\]]+)\]%", m =>
        {
            var condition = m.Groups[1].Value.Trim();
            var trueText = m.Groups[2].Value.Trim();
            var falseText = m.Groups[3].Value.Trim();

            // Evaluate simple conditions
            bool result = EvaluateCondition(condition);
            return result ? trueText : falseText;
        }, RegexOptions.IgnoreCase);

        return text;
    }

    private string ReplaceVariables(string text)
    {
        // %VariableName%
        return Regex.Replace(text, @"%(\w+)%", m =>
        {
            var variableName = m.Groups[1].Value;

            // Check if it's a variable
            var variable = _adventure.Variables.Values.FirstOrDefault(v =>
                v.Name.Equals(variableName, StringComparison.OrdinalIgnoreCase));

            if (variable != null)
            {
                return _state.GetVariableValue(variable.Key);
            }

            // Check for special variables
            switch (variableName.ToLower())
            {
                case "score":
                    return _state.Score.ToString();
                case "turns":
                    return _state.TurnCount.ToString();
                case "playername":
                case "player":
                    return _state.PlayerName;
                default:
                    return m.Value; // Keep original if not found
            }
        });
    }

    private string ReplaceParameters(string text, Dictionary<string, string> parameters)
    {
        foreach (var param in parameters)
        {
            var placeholder = $"%{param.Key}%";
            if (text.Contains(placeholder, StringComparison.OrdinalIgnoreCase))
            {
                var value = ResolveEntity(param.Value) ?? param.Value;
                text = text.Replace(placeholder, value, StringComparison.OrdinalIgnoreCase);
            }
        }

        return text;
    }

    private string ReplaceEntityReferences(string text)
    {
        // Replace object/character references
        // Format: %ObjectKey% or %CharacterKey%

        return Regex.Replace(text, @"%(\w+)%", m =>
        {
            var key = m.Groups[1].Value;

            // Try to resolve as entity
            if (_adventure.Objects.ContainsKey(key))
            {
                return _adventure.Objects[key].FullName;
            }

            if (_adventure.Characters.ContainsKey(key))
            {
                return _adventure.Characters[key].FullName;
            }

            if (_adventure.Locations.ContainsKey(key))
            {
                return _adventure.Locations[key].ShortDescription;
            }

            // Keep original if not found
            return m.Value;
        });
    }

    private string ProcessFormatting(string text)
    {
        // Process basic formatting tags

        // Bold: **text** or <b>text</b>
        text = Regex.Replace(text, @"\*\*(.+?)\*\*", "$1"); // Remove bold markers for plain text
        text = Regex.Replace(text, @"<b>(.+?)</b>", "$1", RegexOptions.IgnoreCase);

        // Italic: *text* or <i>text</i>
        text = Regex.Replace(text, @"(?<!\*)\*(?!\*)(.+?)\*(?!\*)", "$1");
        text = Regex.Replace(text, @"<i>(.+?)</i>", "$1", RegexOptions.IgnoreCase);

        // Remove other HTML-like tags
        text = Regex.Replace(text, @"<[^>]+>", "");

        return text;
    }

    private string? ResolveEntity(string reference)
    {
        // Try to resolve entity reference to its display name

        // Object
        if (_adventure.Objects.TryGetValue(reference, out var obj))
        {
            return obj.FullName;
        }

        // Character
        if (_adventure.Characters.TryGetValue(reference, out var character))
        {
            return character.FullName;
        }

        // Location
        if (_adventure.Locations.TryGetValue(reference, out var location))
        {
            return location.ShortDescription;
        }

        // Try finding by name
        var objByName = _adventure.Objects.Values.FirstOrDefault(o =>
            o.Name.Equals(reference, StringComparison.OrdinalIgnoreCase) ||
            o.FullName.Equals(reference, StringComparison.OrdinalIgnoreCase));
        if (objByName != null)
            return objByName.FullName;

        var charByName = _adventure.Characters.Values.FirstOrDefault(c =>
            c.Name.Equals(reference, StringComparison.OrdinalIgnoreCase) ||
            c.FullName.Equals(reference, StringComparison.OrdinalIgnoreCase));
        if (charByName != null)
            return charByName.FullName;

        return null;
    }

    private bool StartsWithVowelSound(string word)
    {
        if (string.IsNullOrEmpty(word))
            return false;

        var firstChar = char.ToLower(word[0]);
        return firstChar == 'a' || firstChar == 'e' || firstChar == 'i' || firstChar == 'o' || firstChar == 'u';
    }

    /// <summary>
    /// Wrap text to a specific width (for console output)
    /// </summary>
    public static string WrapText(string text, int width = 80)
    {
        if (string.IsNullOrEmpty(text) || width <= 0)
            return text;

        var paragraphs = text.Split(new[] { "\n\n", "\r\n\r\n" }, StringSplitOptions.None);
        var wrappedParagraphs = new List<string>();

        foreach (var paragraph in paragraphs)
        {
            var lines = new List<string>();
            var currentLine = "";
            var words = paragraph.Split(new[] { ' ', '\t' }, StringSplitOptions.RemoveEmptyEntries);

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 > width)
                {
                    if (!string.IsNullOrEmpty(currentLine))
                    {
                        lines.Add(currentLine);
                        currentLine = word;
                    }
                    else
                    {
                        lines.Add(word); // Word is longer than width
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(currentLine))
                        currentLine += " ";
                    currentLine += word;
                }
            }

            if (!string.IsNullOrEmpty(currentLine))
            {
                lines.Add(currentLine);
            }

            wrappedParagraphs.Add(string.Join("\n", lines));
        }

        return string.Join("\n\n", wrappedParagraphs);
    }

    /// <summary>
    /// Strip all formatting and ALR tags from text
    /// </summary>
    public static string StripFormatting(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        // Remove ALR functions
        text = Regex.Replace(text, @"%\w+\[[^\]]+\]%", "");

        // Remove variable references
        text = Regex.Replace(text, @"%\w+%", "");

        // Remove formatting
        text = Regex.Replace(text, @"\*\*(.+?)\*\*", "$1");
        text = Regex.Replace(text, @"(?<!\*)\*(?!\*)(.+?)\*(?!\*)", "$1");
        text = Regex.Replace(text, @"<[^>]+>", "");

        return text;
    }

    /// <summary>
    /// Generate article ("a" or "an") for a word
    /// </summary>
    public static string GetArticle(string word)
    {
        if (string.IsNullOrEmpty(word))
            return "a";

        var firstChar = char.ToLower(word[0]);
        return (firstChar == 'a' || firstChar == 'e' || firstChar == 'i' || firstChar == 'o' || firstChar == 'u')
            ? "an"
            : "a";
    }

    /// <summary>
    /// Convert number to words (1-99)
    /// </summary>
    private string NumberToWords(int number)
    {
        if (number == 0) return "zero";
        if (number < 0) return "minus " + NumberToWords(-number);

        string[] ones = { "", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine",
                          "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
                          "seventeen", "eighteen", "nineteen" };
        string[] tens = { "", "", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

        if (number < 20)
            return ones[number];

        if (number < 100)
        {
            var ten = number / 10;
            var one = number % 10;
            return tens[ten] + (one > 0 ? "-" + ones[one] : "");
        }

        if (number < 1000)
        {
            var hundred = number / 100;
            var remainder = number % 100;
            return ones[hundred] + " hundred" + (remainder > 0 ? " and " + NumberToWords(remainder) : "");
        }

        return number.ToString(); // Fallback for large numbers
    }

    /// <summary>
    /// List all visible objects at the current location
    /// </summary>
    private string ListObjectsAtCurrentLocation()
    {
        var objects = _state.GetObjectsAtCurrentLocation()
            .Where(o => !o.IsStatic && !_state.HasObject(o.Key)) // Exclude static and carried objects
            .ToList();

        if (objects.Count == 0)
            return "nothing";

        if (objects.Count == 1)
            return objects[0].FullName;

        var names = objects.Select(o => o.FullName).ToList();
        return FormatList(names);
    }

    /// <summary>
    /// List all characters at the current location
    /// </summary>
    private string ListCharactersAtCurrentLocation()
    {
        var characters = _state.GetCharactersAtCurrentLocation();

        if (characters.Count == 0)
            return "no one";

        if (characters.Count == 1)
            return characters[0].FullName;

        var names = characters.Select(c => c.FullName).ToList();
        return FormatList(names);
    }

    /// <summary>
    /// List all exits from the current location
    /// </summary>
    private string ListExitsFromCurrentLocation()
    {
        var location = _state.GetCurrentLocation();
        if (location == null || location.Directions.Count == 0)
            return "nowhere";

        var exits = location.Directions
            .Where(d => !string.IsNullOrEmpty(d.DestinationKey))
            .Select(d => d.DirectionName)
            .ToList();

        if (exits.Count == 0)
            return "nowhere";

        if (exits.Count == 1)
            return exits[0];

        return FormatList(exits);
    }

    /// <summary>
    /// Format a list of items with commas and "and"
    /// </summary>
    private string FormatList(List<string> items)
    {
        if (items.Count == 0) return "";
        if (items.Count == 1) return items[0];
        if (items.Count == 2) return $"{items[0]} and {items[1]}";

        var allButLast = string.Join(", ", items.Take(items.Count - 1));
        return $"{allButLast}, and {items.Last()}";
    }

    /// <summary>
    /// Evaluate a simple condition (for %If% function)
    /// </summary>
    private bool EvaluateCondition(string condition)
    {
        condition = condition.Trim();

        // Handle task completion: TaskCompleted(TaskName)
        if (condition.StartsWith("TaskCompleted(", StringComparison.OrdinalIgnoreCase))
        {
            var taskRef = Regex.Match(condition, @"TaskCompleted\(([^)]+)\)", RegexOptions.IgnoreCase).Groups[1].Value;
            var task = _adventure.Tasks.Values.FirstOrDefault(t =>
                t.Name.Equals(taskRef, StringComparison.OrdinalIgnoreCase) ||
                t.Key.Equals(taskRef, StringComparison.OrdinalIgnoreCase));
            return task != null && _state.IsTaskCompleted(task.Key);
        }

        // Handle has object: HasObject(ObjectName)
        if (condition.StartsWith("HasObject(", StringComparison.OrdinalIgnoreCase))
        {
            var objRef = Regex.Match(condition, @"HasObject\(([^)]+)\)", RegexOptions.IgnoreCase).Groups[1].Value;
            var obj = _adventure.Objects.Values.FirstOrDefault(o =>
                o.Name.Equals(objRef, StringComparison.OrdinalIgnoreCase) ||
                o.Key.Equals(objRef, StringComparison.OrdinalIgnoreCase));
            return obj != null && _state.HasObject(obj.Key);
        }

        // Handle variable comparisons: Variable = Value
        var variableMatch = Regex.Match(condition, @"(\w+)\s*(=|>|<|>=|<=|!=)\s*(.+)", RegexOptions.IgnoreCase);
        if (variableMatch.Success)
        {
            var varName = variableMatch.Groups[1].Value;
            var op = variableMatch.Groups[2].Value;
            var value = variableMatch.Groups[3].Value.Trim();

            var variable = _adventure.Variables.Values.FirstOrDefault(v =>
                v.Name.Equals(varName, StringComparison.OrdinalIgnoreCase));

            if (variable != null)
            {
                var currentValue = _state.GetVariableValue(variable.Key);

                if (variable.Type == VariableType.Integer)
                {
                    if (int.TryParse(currentValue, out var current) && int.TryParse(value, out var target))
                    {
                        return op switch
                        {
                            "=" => current == target,
                            ">" => current > target,
                            "<" => current < target,
                            ">=" => current >= target,
                            "<=" => current <= target,
                            "!=" => current != target,
                            _ => false
                        };
                    }
                }
                else
                {
                    return op switch
                    {
                        "=" => currentValue.Equals(value, StringComparison.OrdinalIgnoreCase),
                        "!=" => !currentValue.Equals(value, StringComparison.OrdinalIgnoreCase),
                        _ => false
                    };
                }
            }
        }

        // Default: try to parse as boolean
        return bool.TryParse(condition, out var result) && result;
    }

    /// <summary>
    /// List objects at a specific location
    /// </summary>
    private string ListObjectsAtLocation(string locationKey)
    {
        var objects = _adventure.Objects.Values
            .Where(o => !o.IsStatic && _state.GetObjectLocation(o.Key) == locationKey)
            .ToList();

        if (objects.Count == 0)
            return "nothing";

        if (objects.Count == 1)
            return objects[0].FullName;

        var names = objects.Select(o => o.FullName).ToList();
        return FormatList(names);
    }

    /// <summary>
    /// List characters at a specific location
    /// </summary>
    private string ListCharactersAtLocation(string locationKey)
    {
        var characters = _adventure.Characters.Values
            .Where(c => _state.GetCharacterLocation(c.Key) == locationKey)
            .ToList();

        if (characters.Count == 0)
            return "no one";

        if (characters.Count == 1)
            return characters[0].FullName;

        var names = characters.Select(c => c.FullName).ToList();
        return FormatList(names);
    }

    /// <summary>
    /// Format current game time
    /// </summary>
    private string FormatGameTime()
    {
        // Return turn count or formatted time if adventure has time system
        if (_adventure.UseTime)
        {
            var totalMinutes = _state.TimeElapsed;
            var hours = totalMinutes / 60;
            var minutes = totalMinutes % 60;
            return $"{hours:D2}:{minutes:D2}";
        }
        return $"Turn {_state.TurnCount}";
    }

    /// <summary>
    /// Get property value for an item
    /// </summary>
    private string GetPropertyValue(string itemKey, string propertyName)
    {
        // Check if item exists and has the property
        var item = _adventure.GetItem(itemKey);
        if (item == null)
            return "";

        // Try to get property from object
        if (item is AdriftObject obj && obj.Properties.TryGetValue(propertyName, out var objProp))
        {
            return objProp.CurrentValue ?? "";
        }

        // Try to get property from character
        if (item is Character character && character.Properties.TryGetValue(propertyName, out var charProp))
        {
            return charProp.CurrentValue ?? "";
        }

        // Try to get property from location
        if (item is Location location && location.Properties.TryGetValue(propertyName, out var locProp))
        {
            return locProp.CurrentValue ?? "";
        }

        return "";
    }

    /// <summary>
    /// Evaluate mathematical expression
    /// </summary>
    private string EvaluateExpression(string expression)
    {
        try
        {
            // Replace variables in expression first
            expression = ReplaceVariablesInExpression(expression);

            // Simple expression evaluator (supports +, -, *, /, parentheses)
            var result = EvaluateSimpleExpression(expression);
            return result.ToString();
        }
        catch
        {
            return expression; // Return original if evaluation fails
        }
    }

    private string ReplaceVariablesInExpression(string expression)
    {
        return Regex.Replace(expression, @"\b([a-zA-Z_]\w*)\b", m =>
        {
            var varName = m.Groups[1].Value;
            var variable = _adventure.Variables.Values.FirstOrDefault(v =>
                v.Name.Equals(varName, StringComparison.OrdinalIgnoreCase));

            if (variable != null && variable.Type == VariableType.Integer)
            {
                return _state.GetVariableValue(variable.Key);
            }

            return m.Value; // Keep original if not a variable
        });
    }

    private double EvaluateSimpleExpression(string expression)
    {
        // Simple recursive descent parser for basic arithmetic
        expression = expression.Replace(" ", "");

        return ParseExpression(expression);
    }

    private double ParseExpression(string expr)
    {
        // Handle addition and subtraction
        var parts = Regex.Split(expr, @"(?<=[^+\-*/])[+\-](?=[^+\-*/])");
        if (parts.Length > 1)
        {
            double result = ParseTerm(parts[0]);
            for (int i = 1; i < parts.Length; i++)
            {
                var op = expr[expr.IndexOf(parts[i]) - 1];
                var value = ParseTerm(parts[i]);
                result = op == '+' ? result + value : result - value;
            }
            return result;
        }

        return ParseTerm(expr);
    }

    private double ParseTerm(string term)
    {
        // Handle multiplication and division
        var parts = term.Split(new[] { '*', '/' }, StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length > 1)
        {
            double result = ParseFactor(parts[0]);
            for (int i = 1; i < parts.Length; i++)
            {
                var op = term[term.IndexOf(parts[i]) - 1];
                var value = ParseFactor(parts[i]);
                result = op == '*' ? result * value : result / value;
            }
            return result;
        }

        return ParseFactor(term);
    }

    private double ParseFactor(string factor)
    {
        // Handle parentheses
        if (factor.StartsWith("(") && factor.EndsWith(")"))
        {
            return ParseExpression(factor.Substring(1, factor.Length - 2));
        }

        // Parse number
        if (double.TryParse(factor, out var num))
            return num;

        return 0;
    }

    /// <summary>
    /// Get subject pronoun based on gender
    /// </summary>
    private string GetSubjectPronoun(string gender)
    {
        return gender.ToLower() switch
        {
            "male" => "he",
            "female" => "she",
            "neuter" => "it",
            "plural" => "they",
            _ => "it"
        };
    }

    /// <summary>
    /// Get object pronoun based on gender
    /// </summary>
    private string GetObjectPronoun(string gender)
    {
        return gender.ToLower() switch
        {
            "male" => "him",
            "female" => "her",
            "neuter" => "it",
            "plural" => "them",
            _ => "it"
        };
    }

    /// <summary>
    /// Get possessive pronoun based on gender
    /// </summary>
    private string GetPossessivePronoun(string gender)
    {
        return gender.ToLower() switch
        {
            "male" => "his",
            "female" => "her",
            "neuter" => "its",
            "plural" => "their",
            _ => "its"
        };
    }

    /// <summary>
    /// Process ALR (Alternate Reality Layer) text overrides
    /// Applies find/replace transformations in priority order
    /// </summary>
    private string ProcessALRs(string text)
    {
        if (_adventure.ALRs == null || _adventure.ALRs.Count == 0)
            return text;

        // Get all ALRs sorted by Order (ascending - lower order = higher priority)
        var sortedALRs = _adventure.ALRs.Values
            .OrderBy(alr => alr.Order)
            .ToList();

        // Apply each ALR in order
        foreach (var alr in sortedALRs)
        {
            if (string.IsNullOrEmpty(alr.OldText))
                continue;

            // Get the replacement text (with alternate descriptions evaluated)
            var newText = alr.NewText.GetText();

            // Apply the find/replace based on options
            text = ApplyALR(text, alr.OldText, newText, alr.CaseSensitive, alr.WholeWordsOnly);
        }

        return text;
    }

    /// <summary>
    /// Apply a single ALR transformation
    /// </summary>
    private string ApplyALR(string text, string oldText, string newText, bool caseSensitive, bool wholeWordsOnly)
    {
        if (string.IsNullOrEmpty(oldText))
            return text;

        // Build regex pattern based on options
        string pattern;

        if (wholeWordsOnly)
        {
            // Match whole words only using word boundaries
            pattern = @"\b" + Regex.Escape(oldText) + @"\b";
        }
        else
        {
            // Match any occurrence
            pattern = Regex.Escape(oldText);
        }

        // Apply replacement with appropriate case sensitivity
        var options = caseSensitive ? RegexOptions.None : RegexOptions.IgnoreCase;

        try
        {
            text = Regex.Replace(text, pattern, newText, options);
        }
        catch (Exception)
        {
            // If regex fails, fall back to simple string replace
            if (caseSensitive)
            {
                text = text.Replace(oldText, newText);
            }
            else
            {
                // Case-insensitive replace for fallback
                int index = text.IndexOf(oldText, StringComparison.OrdinalIgnoreCase);
                while (index >= 0)
                {
                    text = text.Remove(index, oldText.Length).Insert(index, newText);
                    index = text.IndexOf(oldText, index + newText.Length, StringComparison.OrdinalIgnoreCase);
                }
            }
        }

        return text;
    }
}
