using System.Text;
using System.Text.RegularExpressions;

namespace ADRIFT.Developer.Utilities;

/// <summary>
/// Two-way adapter between ADRIFT text format and Markdown
/// Provides conversion utilities for rich text editing
/// </summary>
public static class MarkdownAdapter
{
    /// <summary>
    /// Convert Markdown to plain text (strips formatting)
    /// </summary>
    public static string MarkdownToPlainText(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        var text = markdown;

        // Remove headers
        text = Regex.Replace(text, @"^#{1,6}\s+", "", RegexOptions.Multiline);

        // Remove bold/italic/underline
        text = Regex.Replace(text, @"\*\*(.+?)\*\*", "$1"); // Bold
        text = Regex.Replace(text, @"__(.+?)__", "$1");     // Underline
        text = Regex.Replace(text, @"\*(.+?)\*", "$1");     // Italic
        text = Regex.Replace(text, @"_(.+?)_", "$1");       // Italic alt

        // Remove links but keep text
        text = Regex.Replace(text, @"\[(.+?)\]\(.+?\)", "$1");

        // Remove images
        text = Regex.Replace(text, @"!\[.+?\]\(.+?\)", "");

        // Remove list markers
        text = Regex.Replace(text, @"^[\*\-\+]\s+", "", RegexOptions.Multiline);
        text = Regex.Replace(text, @"^\d+\.\s+", "", RegexOptions.Multiline);

        // Remove code blocks
        text = Regex.Replace(text, @"`(.+?)`", "$1");
        text = Regex.Replace(text, @"```[\s\S]+?```", "", RegexOptions.Multiline);

        return text.Trim();
    }

    /// <summary>
    /// Convert Markdown to HTML (basic conversion)
    /// </summary>
    public static string MarkdownToHtml(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        var html = new StringBuilder();
        var lines = markdown.Split('\n');
        var inCodeBlock = false;
        var inList = false;

        foreach (var line in lines)
        {
            var trimmed = line.TrimStart();

            // Code blocks
            if (trimmed.StartsWith("```"))
            {
                if (inCodeBlock)
                {
                    html.AppendLine("</code></pre>");
                    inCodeBlock = false;
                }
                else
                {
                    html.AppendLine("<pre><code>");
                    inCodeBlock = true;
                }
                continue;
            }

            if (inCodeBlock)
            {
                html.AppendLine(System.Net.WebUtility.HtmlEncode(line));
                continue;
            }

            // Headers
            if (trimmed.StartsWith("# "))
            {
                html.AppendLine($"<h1>{ProcessInlineMarkdown(trimmed.Substring(2))}</h1>");
                continue;
            }
            if (trimmed.StartsWith("## "))
            {
                html.AppendLine($"<h2>{ProcessInlineMarkdown(trimmed.Substring(3))}</h2>");
                continue;
            }
            if (trimmed.StartsWith("### "))
            {
                html.AppendLine($"<h3>{ProcessInlineMarkdown(trimmed.Substring(4))}</h3>");
                continue;
            }

            // Lists
            if (trimmed.StartsWith("- ") || trimmed.StartsWith("* ") || trimmed.StartsWith("+ "))
            {
                if (!inList)
                {
                    html.AppendLine("<ul>");
                    inList = true;
                }
                html.AppendLine($"<li>{ProcessInlineMarkdown(trimmed.Substring(2))}</li>");
                continue;
            }

            var numberMatch = Regex.Match(trimmed, @"^(\d+)\.\s+(.*)");
            if (numberMatch.Success)
            {
                if (!inList)
                {
                    html.AppendLine("<ol>");
                    inList = true;
                }
                html.AppendLine($"<li>{ProcessInlineMarkdown(numberMatch.Groups[2].Value)}</li>");
                continue;
            }

            // Close list if we were in one
            if (inList && !string.IsNullOrWhiteSpace(trimmed))
            {
                html.AppendLine(trimmed.StartsWith("-") || trimmed.StartsWith("*") || Regex.IsMatch(trimmed, @"^\d+\.") ? "" : "</ul>");
                inList = false;
            }

            // Regular paragraph
            if (!string.IsNullOrWhiteSpace(trimmed))
            {
                html.AppendLine($"<p>{ProcessInlineMarkdown(trimmed)}</p>");
            }
        }

        if (inList)
        {
            html.AppendLine("</ul>");
        }

        return html.ToString();
    }

    /// <summary>
    /// Process inline markdown (bold, italic, links, etc.)
    /// </summary>
    private static string ProcessInlineMarkdown(string text)
    {
        // Bold
        text = Regex.Replace(text, @"\*\*(.+?)\*\*", "<strong>$1</strong>");

        // Italic
        text = Regex.Replace(text, @"\*(.+?)\*", "<em>$1</em>");
        text = Regex.Replace(text, @"_(.+?)_", "<em>$1</em>");

        // Underline (non-standard markdown, but useful)
        text = Regex.Replace(text, @"__(.+?)__", "<u>$1</u>");

        // Inline code
        text = Regex.Replace(text, @"`(.+?)`", "<code>$1</code>");

        // Links
        text = Regex.Replace(text, @"\[(.+?)\]\((.+?)\)", "<a href=\"$2\">$1</a>");

        // Images
        text = Regex.Replace(text, @"!\[(.+?)\]\((.+?)\)", "<img src=\"$2\" alt=\"$1\" />");

        return text;
    }

    /// <summary>
    /// Convert plain text to Markdown (adds basic structure)
    /// </summary>
    public static string PlainTextToMarkdown(string plainText)
    {
        if (string.IsNullOrEmpty(plainText))
            return string.Empty;

        // For now, just return as-is since plain text is valid markdown
        // Future: could detect patterns and add markdown formatting
        return plainText;
    }

    /// <summary>
    /// Convert ADRIFT RTF/rich text to Markdown
    /// (Placeholder for future RTF conversion if needed)
    /// </summary>
    public static string AdriftToMarkdown(string adriftText)
    {
        if (string.IsNullOrEmpty(adriftText))
            return string.Empty;

        // Current ADRIFT MAUI format is plain text
        // If RTF format is encountered, this method would parse it
        // For now, treat as plain text
        return adriftText;
    }

    /// <summary>
    /// Convert Markdown to ADRIFT format
    /// (Placeholder for future RTF conversion if needed)
    /// </summary>
    public static string MarkdownToAdrift(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return string.Empty;

        // Current ADRIFT MAUI format is plain text
        // Could strip markdown or convert to RTF in future
        // For now, preserve markdown in text
        return markdown;
    }

    /// <summary>
    /// Strip all markdown formatting (useful for game runtime display)
    /// </summary>
    public static string StripMarkdown(string markdown)
    {
        return MarkdownToPlainText(markdown);
    }

    /// <summary>
    /// Validate markdown syntax
    /// </summary>
    public static bool IsValidMarkdown(string markdown)
    {
        if (string.IsNullOrEmpty(markdown))
            return true;

        try
        {
            // Check for balanced formatting markers
            var boldCount = Regex.Matches(markdown, @"\*\*").Count;
            var italicCount = Regex.Matches(markdown, @"(?<!\*)\*(?!\*)").Count;
            var underlineCount = Regex.Matches(markdown, @"__").Count;

            // Must be even (opening and closing)
            if (boldCount % 2 != 0) return false;
            if (italicCount % 2 != 0) return false;
            if (underlineCount % 2 != 0) return false;

            return true;
        }
        catch
        {
            return false;
        }
    }
}
