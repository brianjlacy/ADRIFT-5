using System.Text.RegularExpressions;
using ADRIFT.Core.Models;

namespace ADRIFT.Engine.Expressions;

/// <summary>
/// Modern expression evaluator for ADRIFT expressions
/// Supports variables, functions, operators, and game state queries
/// </summary>
public class ExpressionEvaluator
{
    private readonly Adventure? _adventure;
    private readonly Dictionary<string, Func<string[], object>> _functions;

    public ExpressionEvaluator(Adventure? adventure = null)
    {
        _adventure = adventure;
        _functions = InitializeFunctions();
    }

    /// <summary>
    /// Evaluates an expression and returns the result
    /// </summary>
    public object Evaluate(string expression)
    {
        if (string.IsNullOrWhiteSpace(expression))
            return string.Empty;

        try
        {
            // Replace variables with their values
            expression = ReplaceVariables(expression);

            // Replace game state placeholders
            expression = ReplaceGameState(expression);

            // Evaluate functions
            expression = EvaluateFunctions(expression);

            // Evaluate boolean logic and comparisons
            if (IsLogicalExpression(expression))
                return EvaluateLogical(expression);

            // Evaluate arithmetic
            if (IsNumericExpression(expression))
                return EvaluateNumeric(expression);

            // Return as string
            return expression;
        }
        catch (Exception ex)
        {
            // Log error but don't crash
            Console.WriteLine($"Expression evaluation error: {ex.Message}");
            return string.Empty;
        }
    }

    /// <summary>
    /// Evaluates to boolean for use in restrictions
    /// </summary>
    public bool EvaluateBoolean(string expression)
    {
        var result = Evaluate(expression);

        return result switch
        {
            bool b => b,
            int i => i != 0,
            double d => d != 0.0,
            string s => !string.IsNullOrWhiteSpace(s) && s != "0" && !s.Equals("false", StringComparison.OrdinalIgnoreCase),
            _ => false
        };
    }

    /// <summary>
    /// Replaces variable references with their values
    /// Format: %variableName% or %variableName[index]%
    /// </summary>
    private string ReplaceVariables(string expression)
    {
        if (_adventure == null)
            return expression;

        var pattern = @"%([a-zA-Z_][a-zA-Z0-9_]*)(?:\[(\d+)\])?%";
        return Regex.Replace(expression, pattern, match =>
        {
            var varName = match.Groups[1].Value;
            var index = match.Groups[2].Success ? int.Parse(match.Groups[2].Value) : 1;

            if (_adventure.Variables.TryGetValue(varName, out var variable))
            {
                return variable.Type switch
                {
                    "Integer" => variable.IntValue.ToString(),
                    "Text" => variable.StringValue,
                    "Boolean" => variable.BoolValue.ToString(),
                    _ => string.Empty
                };
            }

            return match.Value; // Keep original if not found
        });
    }

    /// <summary>
    /// Replaces game state placeholders
    /// %player%, %score%, %maxscore%, %turns%, etc.
    /// </summary>
    private string ReplaceGameState(string expression)
    {
        if (_adventure == null)
            return expression;

        expression = expression.Replace("%player%", "Player", StringComparison.OrdinalIgnoreCase);
        expression = expression.Replace("%score%", "0", StringComparison.OrdinalIgnoreCase); // TODO: Implement scoring
        expression = expression.Replace("%maxscore%", _adventure.MaxScore.ToString(), StringComparison.OrdinalIgnoreCase);
        expression = expression.Replace("%turns%", "0", StringComparison.OrdinalIgnoreCase); // TODO: Implement turn tracking

        return expression;
    }

    /// <summary>
    /// Evaluates function calls
    /// Format: FUNCTION(arg1, arg2, ...)
    /// </summary>
    private string EvaluateFunctions(string expression)
    {
        // Pattern to match function calls
        var pattern = @"([A-Z]+)\(((?:[^()]|\((?:[^()]|\([^()]*\))*\))*)\)";

        while (Regex.IsMatch(expression, pattern, RegexOptions.IgnoreCase))
        {
            expression = Regex.Replace(expression, pattern, match =>
            {
                var funcName = match.Groups[1].Value.ToUpperInvariant();
                var argsString = match.Groups[2].Value;

                // Split arguments (respecting nested parentheses and quotes)
                var args = SplitArguments(argsString);

                // Recursively evaluate each argument
                var evaluatedArgs = args.Select(arg => Evaluate(arg).ToString() ?? string.Empty).ToArray();

                // Call the function
                if (_functions.TryGetValue(funcName, out var func))
                {
                    return func(evaluatedArgs).ToString() ?? string.Empty;
                }

                return match.Value; // Unknown function, keep as-is
            }, RegexOptions.IgnoreCase);
        }

        return expression;
    }

    /// <summary>
    /// Splits function arguments respecting nested parentheses and quotes
    /// </summary>
    private List<string> SplitArguments(string argsString)
    {
        var args = new List<string>();
        var currentArg = new System.Text.StringBuilder();
        var depth = 0;
        var inQuotes = false;

        foreach (var c in argsString)
        {
            switch (c)
            {
                case '"' when depth == 0:
                    inQuotes = !inQuotes;
                    currentArg.Append(c);
                    break;
                case '(' when !inQuotes:
                    depth++;
                    currentArg.Append(c);
                    break;
                case ')' when !inQuotes:
                    depth--;
                    currentArg.Append(c);
                    break;
                case ',' when depth == 0 && !inQuotes:
                    args.Add(currentArg.ToString().Trim());
                    currentArg.Clear();
                    break;
                default:
                    currentArg.Append(c);
                    break;
            }
        }

        if (currentArg.Length > 0)
            args.Add(currentArg.ToString().Trim());

        return args;
    }

    /// <summary>
    /// Checks if expression contains logical operators
    /// </summary>
    private bool IsLogicalExpression(string expression)
    {
        return expression.Contains("AND", StringComparison.OrdinalIgnoreCase) ||
               expression.Contains("OR", StringComparison.OrdinalIgnoreCase) ||
               expression.Contains("==") ||
               expression.Contains("!=") ||
               expression.Contains(">=") ||
               expression.Contains("<=") ||
               expression.Contains(">") ||
               expression.Contains("<");
    }

    /// <summary>
    /// Evaluates logical expressions
    /// </summary>
    private bool EvaluateLogical(string expression)
    {
        // Handle AND/OR operators
        if (expression.Contains(" AND ", StringComparison.OrdinalIgnoreCase))
        {
            var parts = expression.Split(new[] { " AND " }, StringSplitOptions.None);
            return parts.All(p => EvaluateBoolean(p));
        }

        if (expression.Contains(" OR ", StringComparison.OrdinalIgnoreCase))
        {
            var parts = expression.Split(new[] { " OR " }, StringSplitOptions.None);
            return parts.Any(p => EvaluateBoolean(p));
        }

        // Handle comparison operators
        if (expression.Contains("=="))
        {
            var parts = expression.Split("==");
            return Evaluate(parts[0]).ToString() == Evaluate(parts[1]).ToString();
        }

        if (expression.Contains("!="))
        {
            var parts = expression.Split("!=");
            return Evaluate(parts[0]).ToString() != Evaluate(parts[1]).ToString();
        }

        if (expression.Contains(">="))
        {
            var parts = expression.Split(">=");
            return CompareNumeric(parts[0], parts[1]) >= 0;
        }

        if (expression.Contains("<="))
        {
            var parts = expression.Split("<=");
            return CompareNumeric(parts[0], parts[1]) <= 0;
        }

        if (expression.Contains(">"))
        {
            var parts = expression.Split(">");
            return CompareNumeric(parts[0], parts[1]) > 0;
        }

        if (expression.Contains("<"))
        {
            var parts = expression.Split("<");
            return CompareNumeric(parts[0], parts[1]) < 0;
        }

        return !string.IsNullOrWhiteSpace(expression);
    }

    /// <summary>
    /// Compares two numeric expressions
    /// </summary>
    private int CompareNumeric(string left, string right)
    {
        var leftVal = Convert.ToDouble(Evaluate(left));
        var rightVal = Convert.ToDouble(Evaluate(right));
        return leftVal.CompareTo(rightVal);
    }

    /// <summary>
    /// Checks if expression is numeric
    /// </summary>
    private bool IsNumericExpression(string expression)
    {
        return expression.Contains("+") ||
               expression.Contains("-") ||
               expression.Contains("*") ||
               expression.Contains("/") ||
               double.TryParse(expression, out _);
    }

    /// <summary>
    /// Evaluates numeric expressions
    /// </summary>
    private double EvaluateNumeric(string expression)
    {
        // Simple evaluation using DataTable.Compute for basic arithmetic
        // In production, use a proper expression parser
        try
        {
            var dt = new System.Data.DataTable();
            return Convert.ToDouble(dt.Compute(expression, null));
        }
        catch
        {
            return 0.0;
        }
    }

    /// <summary>
    /// Initializes built-in functions
    /// </summary>
    private Dictionary<string, Func<string[], object>> InitializeFunctions()
    {
        return new Dictionary<string, Func<string[], object>>(StringComparer.OrdinalIgnoreCase)
        {
            // Conditional
            ["IF"] = args => args.Length >= 3 && EvaluateBoolean(args[0]) ? args[1] : args[2],
            ["EITHER"] = args => args.Length >= 2 ? args[new Random().Next(args.Length)] : string.Empty,

            // String functions
            ["UCASE"] = args => args.Length > 0 ? args[0].ToUpperInvariant() : string.Empty,
            ["LCASE"] = args => args.Length > 0 ? args[0].ToLowerInvariant() : string.Empty,
            ["PCASE"] = args => args.Length > 0 ? ProperCase(args[0]) : string.Empty,
            ["LEFT"] = args => args.Length >= 2 && int.TryParse(args[1], out int len) ? args[0].Substring(0, Math.Min(len, args[0].Length)) : string.Empty,
            ["RIGHT"] = args => args.Length >= 2 && int.TryParse(args[1], out int len) ? args[0].Substring(Math.Max(0, args[0].Length - len)) : string.Empty,
            ["MID"] = args => args.Length >= 3 && int.TryParse(args[1], out int start) && int.TryParse(args[2], out int len) ? args[0].Substring(Math.Max(0, start - 1), Math.Min(len, args[0].Length - start + 1)) : string.Empty,
            ["LEN"] = args => args.Length > 0 ? args[0].Length : 0,
            ["INSTR"] = args => args.Length >= 2 ? args[0].IndexOf(args[1], StringComparison.OrdinalIgnoreCase) + 1 : 0,
            ["REPLACE"] = args => args.Length >= 3 ? args[0].Replace(args[1], args[2]) : string.Empty,

            // Numeric functions
            ["ABS"] = args => args.Length > 0 && double.TryParse(args[0], out double val) ? Math.Abs(val) : 0,
            ["MIN"] = args => args.Length > 0 ? args.Min(a => double.TryParse(a, out double v) ? v : double.MaxValue) : 0,
            ["MAX"] = args => args.Length > 0 ? args.Max(a => double.TryParse(a, out double v) ? v : double.MinValue) : 0,
            ["RAND"] = args =>
            {
                if (args.Length >= 2 && int.TryParse(args[0], out int min) && int.TryParse(args[1], out int max))
                    return new Random().Next(min, max + 1);
                return 0;
            },
            ["MOD"] = args => args.Length >= 2 && int.TryParse(args[0], out int a) && int.TryParse(args[1], out int b) ? a % b : 0,
            ["VAL"] = args => args.Length > 0 && double.TryParse(args[0], out double val) ? val : 0,
            ["STR"] = args => args.Length > 0 ? args[0] : string.Empty,

            // Array/Collection
            ["ONEOF"] = args => args.Length > 0 ? args[new Random().Next(args.Length)] : string.Empty,
        };
    }

    /// <summary>
    /// Converts string to proper case (Title Case)
    /// </summary>
    private static string ProperCase(string text)
    {
        if (string.IsNullOrEmpty(text))
            return text;

        var words = text.Split(' ');
        for (int i = 0; i < words.Length; i++)
        {
            if (words[i].Length > 0)
            {
                words[i] = char.ToUpper(words[i][0]) + words[i].Substring(1).ToLower();
            }
        }
        return string.Join(" ", words);
    }
}
