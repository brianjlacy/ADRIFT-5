namespace ADRIFT.Runner.Controls;

/// <summary>
/// Custom Entry control with command history navigation
/// </summary>
public class CommandEntry : Entry
{
    public event EventHandler<string>? HistoryUpRequested;
    public event EventHandler<string>? HistoryDownRequested;

    public CommandEntry()
    {
        // Subscribe to key events if available
    }

    protected override void OnHandlerChanged()
    {
        base.OnHandlerChanged();

#if WINDOWS
        // Windows-specific keyboard handling
        if (Handler?.PlatformView is Microsoft.UI.Xaml.Controls.TextBox textBox)
        {
            textBox.KeyDown += (s, e) =>
            {
                if (e.Key == Windows.System.VirtualKey.Up)
                {
                    HistoryUpRequested?.Invoke(this, Text ?? "");
                    e.Handled = true;
                }
                else if (e.Key == Windows.System.VirtualKey.Down)
                {
                    HistoryDownRequested?.Invoke(this, Text ?? "");
                    e.Handled = true;
                }
            };
        }
#endif
    }

    public void SetText(string text)
    {
        Text = text;

        // Move cursor to end
        CursorPosition = text.Length;
    }
}
