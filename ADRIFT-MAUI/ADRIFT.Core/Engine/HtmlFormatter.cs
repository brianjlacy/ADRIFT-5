using System.Text;
using System.Text.RegularExpressions;

namespace ADRIFT.Core.Engine;

/// <summary>
/// Converts ADRIFT formatted text to HTML for display in WebView
/// Handles markdown-style formatting and ADRIFT-specific tags
/// </summary>
public static class HtmlFormatter
{
    /// <summary>
    /// Convert ADRIFT formatted text to complete HTML document
    /// </summary>
    public static string ToHtml(string text, bool darkMode = false)
    {
        if (string.IsNullOrEmpty(text))
            return GenerateHtmlDocument("", darkMode);

        var formatted = FormatText(text);
        return GenerateHtmlDocument(formatted, darkMode);
    }

    /// <summary>
    /// Convert ADRIFT formatted text to HTML fragment (without document structure)
    /// </summary>
    public static string ToHtmlFragment(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        return FormatText(text);
    }

    /// <summary>
    /// Append formatted text to existing HTML content
    /// </summary>
    public static string AppendHtml(string existingHtml, string newText, bool darkMode = false)
    {
        if (string.IsNullOrEmpty(existingHtml))
            return ToHtml(newText, darkMode);

        // Extract body content from existing HTML
        var bodyMatch = Regex.Match(existingHtml, @"<body[^>]*>(.*)</body>", RegexOptions.Singleline);
        var existingContent = bodyMatch.Success ? bodyMatch.Groups[1].Value : existingHtml;

        // Format new text
        var formattedNew = FormatText(newText);

        // Combine
        var combined = existingContent + formattedNew;

        return GenerateHtmlDocument(combined, darkMode);
    }

    private static string FormatText(string text)
    {
        if (string.IsNullOrEmpty(text))
            return "";

        var result = text;

        // Escape existing HTML entities first (except our own tags)
        result = EscapeHtml(result);

        // Convert markdown-style formatting
        // **bold** → <strong>text</strong>
        result = Regex.Replace(result, @"\*\*([^\*]+)\*\*", "<strong>$1</strong>");

        // *italic* → <em>text</em> (but not if part of **)
        result = Regex.Replace(result, @"(?<!\*)\*([^\*]+)\*(?!\*)", "<em>$1</em>");

        // _underline_ → <u>text</u>
        result = Regex.Replace(result, @"_([^_]+)_", "<u>$1</u>");

        // Convert newlines
        // Double newline = paragraph break
        result = Regex.Replace(result, @"\n\n+", "</p><p>");

        // Single newline = line break
        result = result.Replace("\n", "<br>");

        // Wrap in paragraph if not already
        if (!result.StartsWith("<p>"))
            result = "<p>" + result;
        if (!result.EndsWith("</p>"))
            result = result + "</p>";

        // Convert > prompt to styled span
        result = result.Replace("<p>&gt; </p>", "<p class=\"prompt\">&gt; </p>");

        // Convert command lines (lines that immediately follow >)
        result = Regex.Replace(result, @"(?<=&gt; </p><p>)([^<]+)(?=</p>)",
            "<span class=\"command\">$1</span>");

        return result;
    }

    private static string EscapeHtml(string text)
    {
        return text
            .Replace("&", "&amp;")
            .Replace("<", "&lt;")
            .Replace(">", "&gt;")
            .Replace("\"", "&quot;")
            .Replace("'", "&#39;");
    }

    private static string GenerateHtmlDocument(string bodyContent, bool darkMode)
    {
        var bgColor = darkMode ? "#1a1a1a" : "#ffffff";
        var textColor = darkMode ? "#e0e0e0" : "#000000";
        var promptColor = darkMode ? "#4CAF50" : "#2E7D32";
        var commandColor = darkMode ? "#90CAF9" : "#1976D2";
        var linkColor = darkMode ? "#64B5F6" : "#1565C0";

        var html = new StringBuilder();
        html.AppendLine("<!DOCTYPE html>");
        html.AppendLine("<html>");
        html.AppendLine("<head>");
        html.AppendLine("    <meta charset=\"UTF-8\">");
        html.AppendLine("    <meta name=\"viewport\" content=\"width=device-width, initial-scale=1.0\">");
        html.AppendLine("    <style>");
        html.AppendLine("        body {");
        html.AppendLine($"            background-color: {bgColor};");
        html.AppendLine($"            color: {textColor};");
        html.AppendLine("            font-family: 'Courier New', Courier, monospace;");
        html.AppendLine("            font-size: 14px;");
        html.AppendLine("            line-height: 1.6;");
        html.AppendLine("            padding: 10px;");
        html.AppendLine("            margin: 0;");
        html.AppendLine("        }");
        html.AppendLine("        p {");
        html.AppendLine("            margin: 0.5em 0;");
        html.AppendLine("        }");
        html.AppendLine("        strong, b {");
        html.AppendLine("            font-weight: bold;");
        html.AppendLine("        }");
        html.AppendLine("        em, i {");
        html.AppendLine("            font-style: italic;");
        html.AppendLine("        }");
        html.AppendLine("        u {");
        html.AppendLine("            text-decoration: underline;");
        html.AppendLine("        }");
        html.AppendLine("        .prompt {");
        html.AppendLine($"            color: {promptColor};");
        html.AppendLine("            font-weight: bold;");
        html.AppendLine("            margin: 0.8em 0 0.2em 0;");
        html.AppendLine("        }");
        html.AppendLine("        .command {");
        html.AppendLine($"            color: {commandColor};");
        html.AppendLine("            font-weight: normal;");
        html.AppendLine("        }");
        html.AppendLine("        a {");
        html.AppendLine($"            color: {linkColor};");
        html.AppendLine("            text-decoration: underline;");
        html.AppendLine("        }");
        html.AppendLine("        a:hover {");
        html.AppendLine("            text-decoration: none;");
        html.AppendLine("        }");
        html.AppendLine("    </style>");
        html.AppendLine("</head>");
        html.AppendLine("<body>");
        html.AppendLine(bodyContent);
        html.AppendLine("</body>");
        html.AppendLine("</html>");

        return html.ToString();
    }

    /// <summary>
    /// Strip all HTML tags from text (for plain text export)
    /// </summary>
    public static string StripHtml(string html)
    {
        if (string.IsNullOrEmpty(html))
            return "";

        // Remove HTML tags
        var text = Regex.Replace(html, @"<[^>]+>", "");

        // Decode HTML entities
        text = text
            .Replace("&amp;", "&")
            .Replace("&lt;", "<")
            .Replace("&gt;", ">")
            .Replace("&quot;", "\"")
            .Replace("&#39;", "'")
            .Replace("&nbsp;", " ");

        return text.Trim();
    }

    /// <summary>
    /// Extract text content from HTML document
    /// </summary>
    public static string ExtractTextContent(string html)
    {
        if (string.IsNullOrEmpty(html))
            return "";

        // Extract body content
        var bodyMatch = Regex.Match(html, @"<body[^>]*>(.*)</body>", RegexOptions.Singleline);
        var bodyContent = bodyMatch.Success ? bodyMatch.Groups[1].Value : html;

        return StripHtml(bodyContent);
    }
}
