using ADRIFT.Developer.Utilities;

namespace ADRIFT.Developer.Controls;

public partial class GenTextBox : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(GenTextBox), string.Empty,
            BindingMode.TwoWay, propertyChanged: OnTextChanged);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(GenTextBox), "Enter text...");

    public static readonly BindableProperty FontSizeProperty =
        BindableProperty.Create(nameof(FontSize), typeof(double), typeof(GenTextBox), 14.0);

    public static readonly BindableProperty ShowToolbarProperty =
        BindableProperty.Create(nameof(ShowToolbar), typeof(bool), typeof(GenTextBox), true);

    public static readonly BindableProperty ShowStatusBarProperty =
        BindableProperty.Create(nameof(ShowStatusBar), typeof(bool), typeof(GenTextBox), true);

    public string Text
    {
        get => (string)GetValue(TextProperty);
        set => SetValue(TextProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public double FontSize
    {
        get => (double)GetValue(FontSizeProperty);
        set => SetValue(FontSizeProperty, value);
    }

    public bool ShowToolbar
    {
        get => (bool)GetValue(ShowToolbarProperty);
        set => SetValue(ShowToolbarProperty, value);
    }

    public bool ShowStatusBar
    {
        get => (bool)GetValue(ShowStatusBarProperty);
        set => SetValue(ShowStatusBarProperty, value);
    }

    public GenTextBox()
    {
        InitializeComponent();
        TextEditor.TextChanged += OnEditorTextChanged;
    }

    private static void OnTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is GenTextBox control)
        {
            control.UpdateCharCount();
        }
    }

    private void OnEditorTextChanged(object? sender, TextChangedEventArgs e)
    {
        UpdateCharCount();
    }

    private void UpdateCharCount()
    {
        CharCountLabel.Text = $"{(Text?.Length ?? 0)} chars";
    }

    private void OnBoldClicked(object? sender, EventArgs e)
    {
        InsertMarkdown("**", "**", "bold text");
    }

    private void OnItalicClicked(object? sender, EventArgs e)
    {
        InsertMarkdown("*", "*", "italic text");
    }

    private void OnUnderlineClicked(object? sender, EventArgs e)
    {
        InsertMarkdown("__", "__", "underlined text");
    }

    private void OnHeading1Clicked(object? sender, EventArgs e)
    {
        InsertAtLineStart("# ", "Heading 1");
    }

    private void OnHeading2Clicked(object? sender, EventArgs e)
    {
        InsertAtLineStart("## ", "Heading 2");
    }

    private void OnBulletListClicked(object? sender, EventArgs e)
    {
        InsertAtLineStart("- ", "List item");
    }

    private void OnNumberListClicked(object? sender, EventArgs e)
    {
        InsertAtLineStart("1. ", "List item");
    }

    private void InsertMarkdown(string prefix, string suffix, string placeholder)
    {
        var cursorPosition = TextEditor.CursorPosition;
        var text = Text ?? string.Empty;

        // Check if there's a selection
        var selectionLength = TextEditor.SelectionLength;
        if (selectionLength > 0)
        {
            // Wrap selected text
            var selectedText = text.Substring(cursorPosition, selectionLength);
            var newText = text.Substring(0, cursorPosition) + prefix + selectedText + suffix + text.Substring(cursorPosition + selectionLength);
            Text = newText;
            TextEditor.CursorPosition = cursorPosition + prefix.Length + selectedText.Length + suffix.Length;
        }
        else
        {
            // Insert placeholder with markdown
            var newText = text.Substring(0, cursorPosition) + prefix + placeholder + suffix + text.Substring(cursorPosition);
            Text = newText;
            TextEditor.CursorPosition = cursorPosition + prefix.Length;
            TextEditor.SelectionLength = placeholder.Length;
        }
    }

    private void InsertAtLineStart(string prefix, string placeholder)
    {
        var cursorPosition = TextEditor.CursorPosition;
        var text = Text ?? string.Empty;

        // Find start of current line
        var lineStart = text.LastIndexOf('\n', Math.Max(0, cursorPosition - 1)) + 1;

        // Insert prefix at line start
        var newText = text.Substring(0, lineStart) + prefix + text.Substring(lineStart);
        Text = newText;

        // If line is empty, add placeholder
        if (lineStart == cursorPosition || text.Substring(lineStart, cursorPosition - lineStart).Trim().Length == 0)
        {
            var placeholderPosition = lineStart + prefix.Length;
            newText = text.Substring(0, placeholderPosition) + placeholder + text.Substring(placeholderPosition);
            Text = newText;
            TextEditor.CursorPosition = placeholderPosition;
            TextEditor.SelectionLength = placeholder.Length;
        }
        else
        {
            TextEditor.CursorPosition = cursorPosition + prefix.Length;
        }
    }

    private async void OnPreviewClicked(object? sender, EventArgs e)
    {
        var markdown = Text ?? string.Empty;
        var html = MarkdownAdapter.MarkdownToHtml(markdown);

        await DisplayAlert("Markdown Preview", $"HTML Output:\n\n{html}\n\nRendered:\n{MarkdownAdapter.MarkdownToPlainText(markdown)}", "OK");
    }

    private async Task DisplayAlert(string title, string message, string cancel)
    {
        if (Application.Current?.MainPage != null)
        {
            await Application.Current.MainPage.DisplayAlert(title, message, cancel);
        }
    }
}
