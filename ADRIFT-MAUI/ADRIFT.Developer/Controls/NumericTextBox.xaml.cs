namespace ADRIFT.Developer.Controls;

public partial class NumericTextBox : ContentView
{
    public static readonly BindableProperty ValueProperty =
        BindableProperty.Create(nameof(Value), typeof(int), typeof(NumericTextBox), 0,
            BindingMode.TwoWay, propertyChanged: OnValueChanged);

    public static readonly BindableProperty MinimumProperty =
        BindableProperty.Create(nameof(Minimum), typeof(int), typeof(NumericTextBox), int.MinValue);

    public static readonly BindableProperty MaximumProperty =
        BindableProperty.Create(nameof(Maximum), typeof(int), typeof(NumericTextBox), int.MaxValue);

    public static readonly BindableProperty IncrementProperty =
        BindableProperty.Create(nameof(Increment), typeof(int), typeof(NumericTextBox), 1);

    public int Value
    {
        get => (int)GetValue(ValueProperty);
        set => SetValue(ValueProperty, value);
    }

    public int Minimum
    {
        get => (int)GetValue(MinimumProperty);
        set => SetValue(MinimumProperty, value);
    }

    public int Maximum
    {
        get => (int)GetValue(MaximumProperty);
        set => SetValue(MaximumProperty, value);
    }

    public int Increment
    {
        get => (int)GetValue(IncrementProperty);
        set => SetValue(IncrementProperty, value);
    }

    public event EventHandler<int>? ValueChanged;

    private bool _isUpdating;

    public NumericTextBox()
    {
        InitializeComponent();
    }

    private static void OnValueChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is NumericTextBox control && !control._isUpdating)
        {
            control._isUpdating = true;
            control.NumericEntry.Text = newValue.ToString();
            control._isUpdating = false;
            control.ValueChanged?.Invoke(control, (int)newValue);
        }
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (_isUpdating) return;

        if (string.IsNullOrWhiteSpace(e.NewTextValue))
        {
            _isUpdating = true;
            Value = Minimum;
            _isUpdating = false;
            return;
        }

        if (int.TryParse(e.NewTextValue, out var value))
        {
            _isUpdating = true;
            Value = ClampValue(value);
            _isUpdating = false;
        }
        else
        {
            // Invalid input - revert to current value
            _isUpdating = true;
            NumericEntry.Text = Value.ToString();
            _isUpdating = false;
        }
    }

    private void OnIncrementClicked(object? sender, EventArgs e)
    {
        Value = ClampValue(Value + Increment);
    }

    private void OnDecrementClicked(object? sender, EventArgs e)
    {
        Value = ClampValue(Value - Increment);
    }

    private int ClampValue(int value)
    {
        if (value < Minimum) return Minimum;
        if (value > Maximum) return Maximum;
        return value;
    }
}
