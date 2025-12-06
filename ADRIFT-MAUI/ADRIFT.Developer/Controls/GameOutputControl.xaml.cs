using Microsoft.Maui.Controls.Shapes;

namespace ADRIFT.Developer.Controls;

public partial class GameOutputControl : ContentView
{
    public static readonly BindableProperty AutoScrollProperty =
        BindableProperty.Create(nameof(AutoScroll), typeof(bool), typeof(GameOutputControl), true);

    public static readonly BindableProperty BackgroundColorProperty =
        BindableProperty.Create(nameof(BackgroundColor), typeof(Color), typeof(GameOutputControl), Colors.White);

    public bool AutoScroll
    {
        get => (bool)GetValue(AutoScrollProperty);
        set => SetValue(AutoScrollProperty, value);
    }

    public Color BackgroundColor
    {
        get => (Color)GetValue(BackgroundColorProperty);
        set => SetValue(BackgroundColorProperty, value);
    }

    public GameOutputControl()
    {
        InitializeComponent();
    }

    public void AppendText(string text, Color? textColor = null, FontAttributes fontAttributes = FontAttributes.None, double fontSize = 14)
    {
        var label = new Label
        {
            Text = text,
            TextColor = textColor ?? Colors.Black,
            FontAttributes = fontAttributes,
            FontSize = fontSize,
            LineBreakMode = LineBreakMode.WordWrap,
            Margin = new Thickness(0, 2)
        };

        OutputContainer.Children.Add(label);

        if (AutoScroll)
        {
            ScrollToBottom();
        }
    }

    public void AppendFormattedText(FormattedString formattedString)
    {
        var label = new Label
        {
            FormattedText = formattedString,
            LineBreakMode = LineBreakMode.WordWrap,
            Margin = new Thickness(0, 2)
        };

        OutputContainer.Children.Add(label);

        if (AutoScroll)
        {
            ScrollToBottom();
        }
    }

    public void AppendImage(ImageSource imageSource, double width = 200, double height = 200)
    {
        var image = new Image
        {
            Source = imageSource,
            WidthRequest = width,
            HeightRequest = height,
            Aspect = Aspect.AspectFit,
            Margin = new Thickness(0, 5)
        };

        OutputContainer.Children.Add(image);

        if (AutoScroll)
        {
            ScrollToBottom();
        }
    }

    public void AppendHtml(string html)
    {
        // Convert basic HTML to formatted text
        var formattedString = new FormattedString();

        // Simple HTML parsing - supports <b>, <i>, <br>
        var segments = ParseHtml(html);
        foreach (var segment in segments)
        {
            formattedString.Spans.Add(new Span
            {
                Text = segment.Text,
                FontAttributes = segment.FontAttributes,
                TextColor = segment.TextColor
            });
        }

        AppendFormattedText(formattedString);
    }

    private List<TextSegment> ParseHtml(string html)
    {
        var segments = new List<TextSegment>();
        var currentText = "";
        var currentAttributes = FontAttributes.None;
        var currentColor = Colors.Black;

        var i = 0;
        while (i < html.Length)
        {
            if (html[i] == '<')
            {
                // Save current text
                if (currentText.Length > 0)
                {
                    segments.Add(new TextSegment
                    {
                        Text = currentText,
                        FontAttributes = currentAttributes,
                        TextColor = currentColor
                    });
                    currentText = "";
                }

                // Parse tag
                var tagEnd = html.IndexOf('>', i);
                if (tagEnd == -1) break;

                var tag = html.Substring(i + 1, tagEnd - i - 1).ToLower();

                if (tag == "b" || tag == "strong")
                {
                    currentAttributes |= FontAttributes.Bold;
                }
                else if (tag == "/b" || tag == "/strong")
                {
                    currentAttributes &= ~FontAttributes.Bold;
                }
                else if (tag == "i" || tag == "em")
                {
                    currentAttributes |= FontAttributes.Italic;
                }
                else if (tag == "/i" || tag == "/em")
                {
                    currentAttributes &= ~FontAttributes.Italic;
                }
                else if (tag == "br" || tag == "br/")
                {
                    currentText += "\n";
                }

                i = tagEnd + 1;
            }
            else
            {
                currentText += html[i];
                i++;
            }
        }

        // Add remaining text
        if (currentText.Length > 0)
        {
            segments.Add(new TextSegment
            {
                Text = currentText,
                FontAttributes = currentAttributes,
                TextColor = currentColor
            });
        }

        return segments;
    }

    public void Clear()
    {
        OutputContainer.Children.Clear();
    }

    public void ScrollToBottom()
    {
        Dispatcher.Dispatch(async () =>
        {
            await Task.Delay(100); // Allow layout to complete
            await OutputScrollView.ScrollToAsync(0, double.MaxValue, false);
        });
    }

    public void ScrollToTop()
    {
        Dispatcher.Dispatch(async () =>
        {
            await OutputScrollView.ScrollToAsync(0, 0, true);
        });
    }
}

internal class TextSegment
{
    public string Text { get; set; } = "";
    public FontAttributes FontAttributes { get; set; } = FontAttributes.None;
    public Color TextColor { get; set; } = Colors.Black;
}
