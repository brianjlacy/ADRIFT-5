using System.Collections.ObjectModel;

namespace ADRIFT.Developer.Controls;

public partial class CommandInputControl : ContentView
{
    public static readonly BindableProperty ShowQuickCommandsProperty =
        BindableProperty.Create(nameof(ShowQuickCommands), typeof(bool), typeof(CommandInputControl), true);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(CommandInputControl), "Enter command...");

    public bool ShowQuickCommands
    {
        get => (bool)GetValue(ShowQuickCommandsProperty);
        set => SetValue(ShowQuickCommandsProperty, value);
    }

    public string Placeholder
    {
        get => (string)GetValue(PlaceholderProperty);
        set => SetValue(PlaceholderProperty, value);
    }

    public event EventHandler<string>? CommandSubmitted;
    public event EventHandler<string>? CommandSelected;

    private readonly ObservableCollection<string> _commandHistory = new();
    private int _historyIndex = -1;
    private const int MaxHistorySize = 50;

    public CommandInputControl()
    {
        InitializeComponent();
    }

    public void AddQuickCommand(string commandText, string displayText)
    {
        var button = new Button
        {
            Text = displayText,
            FontSize = 12,
            Padding = new Thickness(12, 6),
            BackgroundColor = Application.Current?.Resources["ADRIFTPrimary"] as Color,
            TextColor = Colors.White,
            CornerRadius = 4
        };

        button.Clicked += (s, e) =>
        {
            CommandEntry.Text = commandText;
            OnSubmitCommand(s, e);
        };

        QuickCommandsContainer.Children.Add(button);
    }

    public void ClearQuickCommands()
    {
        QuickCommandsContainer.Children.Clear();
    }

    public void SetCommandText(string text)
    {
        CommandEntry.Text = text;
    }

    public void ClearCommandText()
    {
        CommandEntry.Text = string.Empty;
    }

    public void FocusCommandEntry()
    {
        CommandEntry.Focus();
    }

    public void SetSuggestions(IEnumerable<string> suggestions)
    {
        CommandEntry.ItemsSource = suggestions?.ToList();
    }

    private void OnSubmitCommand(object? sender, EventArgs e)
    {
        var command = CommandEntry.Text?.Trim();

        if (string.IsNullOrWhiteSpace(command))
            return;

        // Add to history
        AddToHistory(command);

        // Raise event
        CommandSubmitted?.Invoke(this, command);

        // Clear input
        CommandEntry.Text = string.Empty;

        // Reset history index
        _historyIndex = -1;
    }

    private void OnCommandSelected(object? sender, string e)
    {
        if (!string.IsNullOrWhiteSpace(e))
        {
            CommandSelected?.Invoke(this, e);

            // Auto-submit on selection if desired
            // Uncomment the line below to auto-submit
            // OnSubmitCommand(sender, EventArgs.Empty);
        }
    }

    private void OnPreviousCommand(object? sender, EventArgs e)
    {
        if (_commandHistory.Count == 0)
            return;

        // Move backward in history
        if (_historyIndex == -1)
        {
            _historyIndex = _commandHistory.Count - 1;
        }
        else if (_historyIndex > 0)
        {
            _historyIndex--;
        }

        if (_historyIndex >= 0 && _historyIndex < _commandHistory.Count)
        {
            CommandEntry.Text = _commandHistory[_historyIndex];
        }
    }

    private void OnNextCommand(object? sender, EventArgs e)
    {
        if (_commandHistory.Count == 0 || _historyIndex == -1)
            return;

        // Move forward in history
        _historyIndex++;

        if (_historyIndex >= _commandHistory.Count)
        {
            // Reached the end - clear input
            _historyIndex = -1;
            CommandEntry.Text = string.Empty;
        }
        else
        {
            CommandEntry.Text = _commandHistory[_historyIndex];
        }
    }

    private void AddToHistory(string command)
    {
        // Don't add duplicates if the same as the last command
        if (_commandHistory.Count > 0 && _commandHistory[^1] == command)
            return;

        _commandHistory.Add(command);

        // Maintain max history size
        while (_commandHistory.Count > MaxHistorySize)
        {
            _commandHistory.RemoveAt(0);
        }
    }

    public void ClearHistory()
    {
        _commandHistory.Clear();
        _historyIndex = -1;
    }

    public IReadOnlyList<string> GetHistory()
    {
        return _commandHistory.ToList();
    }

    public void LoadHistory(IEnumerable<string> history)
    {
        _commandHistory.Clear();

        foreach (var command in history)
        {
            _commandHistory.Add(command);
        }

        // Maintain max history size
        while (_commandHistory.Count > MaxHistorySize)
        {
            _commandHistory.RemoveAt(0);
        }

        _historyIndex = -1;
    }
}
