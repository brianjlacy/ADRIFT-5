namespace ADRIFT.Developer.Controls;

public partial class RestrictionSummary : ContentView
{
    public static readonly BindableProperty RestrictionTextProperty =
        BindableProperty.Create(nameof(RestrictionText), typeof(string), typeof(RestrictionSummary), "No restriction",
            propertyChanged: OnRestrictionTextChanged);

    public static readonly BindableProperty IsPassingProperty =
        BindableProperty.Create(nameof(IsPassing), typeof(bool), typeof(RestrictionSummary), true,
            propertyChanged: OnIsPassingChanged);

    public static readonly BindableProperty IsMustProperty =
        BindableProperty.Create(nameof(IsMust), typeof(bool), typeof(RestrictionSummary), true,
            propertyChanged: OnIsMustChanged);

    public string RestrictionText
    {
        get => (string)GetValue(RestrictionTextProperty);
        set => SetValue(RestrictionTextProperty, value);
    }

    public bool IsPassing
    {
        get => (bool)GetValue(IsPassingProperty);
        set => SetValue(IsPassingProperty, value);
    }

    public bool IsMust
    {
        get => (bool)GetValue(IsMustProperty);
        set => SetValue(IsMustProperty, value);
    }

    public event EventHandler? EditClicked;

    public RestrictionSummary()
    {
        InitializeComponent();
        UpdateIndicator();
    }

    private static void OnRestrictionTextChanged(BindableObject bindable, object oldValue, object newValue)
    {
        // Text binding handles this automatically
    }

    private static void OnIsPassingChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RestrictionSummary control)
        {
            control.UpdateIndicator();
        }
    }

    private static void OnIsMustChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RestrictionSummary control)
        {
            control.UpdateIndicator();
        }
    }

    private void UpdateIndicator()
    {
        // Determine indicator based on Must/MustNot and passing state
        // Must + Passing = ✓ (green)
        // Must + Not Passing = ✗ (red)
        // MustNot + Passing = ✓ (green)
        // MustNot + Not Passing = ✗ (red)

        bool shouldPass = IsMust ? IsPassing : !IsPassing;

        if (shouldPass)
        {
            IndicatorLabel.Text = "✓";
            IndicatorLabel.TextColor = Colors.Green;
        }
        else
        {
            IndicatorLabel.Text = "✗";
            IndicatorLabel.TextColor = Colors.Red;
        }
    }

    private void OnEditClicked(object? sender, EventArgs e)
    {
        EditClicked?.Invoke(this, EventArgs.Empty);
    }

    public void SetRestrictionState(bool isMust, bool isPassing)
    {
        IsMust = isMust;
        IsPassing = isPassing;
    }
}
