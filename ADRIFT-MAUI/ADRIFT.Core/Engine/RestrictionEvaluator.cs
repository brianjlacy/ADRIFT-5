using ADRIFT.Core.Models;
using System.Text.RegularExpressions;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Evaluates restrictions and conditions for tasks and events
/// </summary>
public class RestrictionEvaluator
{
    private readonly Adventure _adventure;
    private readonly GameState _state;

    public RestrictionEvaluator(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _state = state;
    }

    /// <summary>
    /// Evaluate all restrictions for a task
    /// </summary>
    public bool EvaluateTaskRestrictions(Core.Models.Task task, Dictionary<string, string> parameters)
    {
        if (task.Restrictions.Count == 0)
            return true;

        foreach (var restriction in task.Restrictions)
        {
            if (!EvaluateRestriction(restriction, parameters))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Evaluate event trigger conditions
    /// </summary>
    public bool EvaluateEventConditions(Event evt)
    {
        if (evt.TriggerConditions.Count == 0)
            return true;

        foreach (var condition in evt.TriggerConditions)
        {
            if (!EvaluateRestriction(condition, new Dictionary<string, string>()))
                return false;
        }

        return true;
    }

    /// <summary>
    /// Evaluate a single restriction string
    /// </summary>
    public bool EvaluateRestriction(string restriction, Dictionary<string, string> parameters)
    {
        if (string.IsNullOrWhiteSpace(restriction))
            return true;

        // Normalize
        restriction = restriction.Trim();

        // Handle boolean logic operators
        if (restriction.Contains(" AND ", StringComparison.OrdinalIgnoreCase))
        {
            var parts = SplitByOperator(restriction, " AND ");
            return parts.All(p => EvaluateRestriction(p, parameters));
        }

        if (restriction.Contains(" OR ", StringComparison.OrdinalIgnoreCase))
        {
            var parts = SplitByOperator(restriction, " OR ");
            return parts.Any(p => EvaluateRestriction(p, parameters));
        }

        // Handle NOT
        if (restriction.StartsWith("NOT ", StringComparison.OrdinalIgnoreCase))
        {
            var inner = restriction.Substring(4).Trim();
            return !EvaluateRestriction(inner, parameters);
        }

        // Handle parentheses
        if (restriction.StartsWith("(") && restriction.EndsWith(")"))
        {
            var inner = restriction.Substring(1, restriction.Length - 2);
            return EvaluateRestriction(inner, parameters);
        }

        // Evaluate atomic restriction
        return EvaluateAtomicRestriction(restriction, parameters);
    }

    private List<string> SplitByOperator(string text, string op)
    {
        // Simple split - doesn't handle nested parentheses perfectly
        // TODO: Implement proper expression parser
        var parts = new List<string>();
        var current = "";
        var depth = 0;

        var opIndex = 0;
        for (int i = 0; i < text.Length; i++)
        {
            if (text[i] == '(')
                depth++;
            else if (text[i] == ')')
                depth--;

            if (depth == 0 && i <= text.Length - op.Length &&
                text.Substring(i, op.Length).Equals(op, StringComparison.OrdinalIgnoreCase))
            {
                parts.Add(current.Trim());
                current = "";
                i += op.Length - 1;
            }
            else
            {
                current += text[i];
            }
        }

        if (!string.IsNullOrWhiteSpace(current))
            parts.Add(current.Trim());

        return parts;
    }

    private bool EvaluateAtomicRestriction(string restriction, Dictionary<string, string> parameters)
    {
        // Resolve parameters
        restriction = ResolveParameters(restriction, parameters);

        // Player has object
        if (TryParsePlayerHas(restriction, out var objectKey))
        {
            return _state.HasObject(objectKey);
        }

        // Player at location
        if (TryParsePlayerAt(restriction, out var locationKey))
        {
            return _state.CurrentLocationKey == locationKey;
        }

        // Object at location
        if (TryParseObjectAt(restriction, out var objKey, out var locKey))
        {
            return _state.ObjectLocations.GetValueOrDefault(objKey) == locKey;
        }

        // Task completed
        if (TryParseTaskCompleted(restriction, out var taskKey))
        {
            return _state.IsTaskCompleted(taskKey);
        }

        // Variable comparison
        if (TryParseVariableComparison(restriction, out var varKey, out var op, out var value))
        {
            return EvaluateVariableComparison(varKey, op, value);
        }

        // Object property check
        if (TryParseObjectProperty(restriction, out var propObjKey, out var property, out var propValue))
        {
            return EvaluateObjectProperty(propObjKey, property, propValue);
        }

        // Character at location
        if (TryParseCharacterAt(restriction, out var charKey, out var charLocKey))
        {
            return _state.CharacterLocations.GetValueOrDefault(charKey) == charLocKey;
        }

        // Score comparison
        if (TryParseScoreComparison(restriction, out var scoreOp, out var scoreValue))
        {
            return EvaluateNumericComparison(_state.Score, scoreOp, scoreValue);
        }

        // Default: return true for unknown restrictions
        return true;
    }

    private string ResolveParameters(string text, Dictionary<string, string> parameters)
    {
        foreach (var param in parameters)
        {
            text = text.Replace($"%{param.Key}%", param.Value, StringComparison.OrdinalIgnoreCase);
        }
        return text;
    }

    private bool TryParsePlayerHas(string restriction, out string objectKey)
    {
        objectKey = string.Empty;

        // "Player has <object>"
        // "Player holding <object>"
        // "Player carrying <object>"
        var patterns = new[]
        {
            @"Player has (.+)",
            @"Player holding (.+)",
            @"Player carrying (.+)",
            @"Has (.+)"
        };

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(restriction, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                objectKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "object");
                return !string.IsNullOrEmpty(objectKey);
            }
        }

        return false;
    }

    private bool TryParsePlayerAt(string restriction, out string locationKey)
    {
        locationKey = string.Empty;

        // "Player at <location>"
        // "Player in <location>"
        var patterns = new[]
        {
            @"Player (?:at|in) (.+)",
            @"At (.+)"
        };

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(restriction, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                locationKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "location");
                return !string.IsNullOrEmpty(locationKey);
            }
        }

        return false;
    }

    private bool TryParseObjectAt(string restriction, out string objectKey, out string locationKey)
    {
        objectKey = string.Empty;
        locationKey = string.Empty;

        // "<object> at <location>"
        var match = Regex.Match(restriction, @"(.+?)\s+(?:at|in)\s+(.+)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            objectKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "object");
            locationKey = ResolveEntityKey(match.Groups[2].Value.Trim(), "location");
            return !string.IsNullOrEmpty(objectKey) && !string.IsNullOrEmpty(locationKey);
        }

        return false;
    }

    private bool TryParseTaskCompleted(string restriction, out string taskKey)
    {
        taskKey = string.Empty;

        // "Task <task> completed"
        // "<task> completed"
        var patterns = new[]
        {
            @"Task (.+?) completed",
            @"(.+?) completed",
            @"Task (.+?) done"
        };

        foreach (var pattern in patterns)
        {
            var match = Regex.Match(restriction, pattern, RegexOptions.IgnoreCase);
            if (match.Success)
            {
                taskKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "task");
                return !string.IsNullOrEmpty(taskKey);
            }
        }

        return false;
    }

    private bool TryParseVariableComparison(string restriction, out string variableKey, out string op, out string value)
    {
        variableKey = string.Empty;
        op = string.Empty;
        value = string.Empty;

        // "<variable> <operator> <value>"
        // Operators: =, ==, !=, <, >, <=, >=
        var match = Regex.Match(restriction, @"(.+?)\s*(==?|!=|<=?|>=?)\s*(.+)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            variableKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "variable");
            op = match.Groups[2].Value.Trim();
            value = match.Groups[3].Value.Trim().Trim('"');
            return !string.IsNullOrEmpty(variableKey);
        }

        return false;
    }

    private bool TryParseObjectProperty(string restriction, out string objectKey, out string property, out string value)
    {
        objectKey = string.Empty;
        property = string.Empty;
        value = string.Empty;

        // "<object> is <property>"
        // "Chest is locked"
        // "Door is open"
        var match = Regex.Match(restriction, @"(.+?)\s+is\s+(.+)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            objectKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "object");
            var propertyText = match.Groups[2].Value.Trim();

            // Map property text to property names
            var propertyMap = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase)
            {
                { "locked", "IsLocked" },
                { "unlocked", "IsLocked:false" },
                { "open", "IsOpen" },
                { "closed", "IsOpen:false" },
                { "visible", "IsVisible" },
                { "hidden", "IsVisible:false" }
            };

            if (propertyMap.TryGetValue(propertyText, out var mapped))
            {
                var parts = mapped.Split(':');
                property = parts[0];
                value = parts.Length > 1 ? parts[1] : "true";
                return !string.IsNullOrEmpty(objectKey);
            }
        }

        return false;
    }

    private bool TryParseCharacterAt(string restriction, out string characterKey, out string locationKey)
    {
        characterKey = string.Empty;
        locationKey = string.Empty;

        // "<character> at <location>"
        var match = Regex.Match(restriction, @"(.+?)\s+(?:at|in)\s+(.+)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            characterKey = ResolveEntityKey(match.Groups[1].Value.Trim(), "character");
            locationKey = ResolveEntityKey(match.Groups[2].Value.Trim(), "location");

            // If first entity wasn't a character, it might be an object
            if (string.IsNullOrEmpty(characterKey))
                return false;

            return !string.IsNullOrEmpty(locationKey);
        }

        return false;
    }

    private bool TryParseScoreComparison(string restriction, out string op, out int value)
    {
        op = string.Empty;
        value = 0;

        // "Score <operator> <value>"
        var match = Regex.Match(restriction, @"Score\s*(==?|!=|<=?|>=?)\s*(\d+)", RegexOptions.IgnoreCase);
        if (match.Success)
        {
            op = match.Groups[1].Value;
            value = int.Parse(match.Groups[2].Value);
            return true;
        }

        return false;
    }

    private string ResolveEntityKey(string name, string type)
    {
        name = name.Trim();

        // If already a key format, return as-is
        if (name.Length > 3 && char.IsUpper(name[0]))
            return name;

        // Search by name
        switch (type.ToLower())
        {
            case "object":
                var obj = _adventure.Objects.Values.FirstOrDefault(o =>
                    o.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                    o.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
                return obj?.Key ?? name;

            case "location":
                var loc = _adventure.Locations.Values.FirstOrDefault(l =>
                    l.ShortDescription.Equals(name, StringComparison.OrdinalIgnoreCase));
                return loc?.Key ?? name;

            case "character":
                var chr = _adventure.Characters.Values.FirstOrDefault(c =>
                    c.Name.Equals(name, StringComparison.OrdinalIgnoreCase) ||
                    c.FullName.Equals(name, StringComparison.OrdinalIgnoreCase));
                return chr?.Key ?? name;

            case "task":
                var task = _adventure.Tasks.Values.FirstOrDefault(t =>
                    t.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return task?.Key ?? name;

            case "variable":
                var variable = _adventure.Variables.Values.FirstOrDefault(v =>
                    v.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
                return variable?.Key ?? name;

            default:
                return name;
        }
    }

    private bool EvaluateVariableComparison(string variableKey, string op, string value)
    {
        var currentValue = _state.GetVariableValue(variableKey);

        // Get variable type
        if (!_adventure.Variables.TryGetValue(variableKey, out var variable))
            return false;

        switch (variable.Type)
        {
            case VariableType.Numeric:
                if (double.TryParse(currentValue, out var currentNum) &&
                    double.TryParse(value, out var compareNum))
                {
                    return EvaluateNumericComparison(currentNum, op, compareNum);
                }
                break;

            case VariableType.Text:
                return EvaluateTextComparison(currentValue, op, value);

            case VariableType.Boolean:
                if (bool.TryParse(currentValue, out var currentBool) &&
                    bool.TryParse(value, out var compareBool))
                {
                    return EvaluateBooleanComparison(currentBool, op, compareBool);
                }
                break;
        }

        return false;
    }

    private bool EvaluateNumericComparison(double current, string op, double compare)
    {
        return op switch
        {
            "=" or "==" => Math.Abs(current - compare) < 0.0001,
            "!=" => Math.Abs(current - compare) >= 0.0001,
            "<" => current < compare,
            ">" => current > compare,
            "<=" => current <= compare,
            ">=" => current >= compare,
            _ => false
        };
    }

    private bool EvaluateTextComparison(string current, string op, string compare)
    {
        return op switch
        {
            "=" or "==" => current.Equals(compare, StringComparison.OrdinalIgnoreCase),
            "!=" => !current.Equals(compare, StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    private bool EvaluateBooleanComparison(bool current, string op, bool compare)
    {
        return op switch
        {
            "=" or "==" => current == compare,
            "!=" => current != compare,
            _ => false
        };
    }

    private bool EvaluateObjectProperty(string objectKey, string property, string expectedValue)
    {
        var stateKey = $"{objectKey}.{property}";
        var actualValue = _state.ObjectStates.GetValueOrDefault(stateKey, false);
        var expected = expectedValue.Equals("true", StringComparison.OrdinalIgnoreCase);

        return actualValue == expected;
    }
}
