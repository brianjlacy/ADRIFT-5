using ADRIFT.Core.Models;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.Controls;

public partial class DirectionEditor : ContentView
{
    public static readonly BindableProperty SelectedDirectionIndexProperty =
        BindableProperty.Create(nameof(SelectedDirectionIndex), typeof(int), typeof(DirectionEditor), 0,
            BindingMode.TwoWay, propertyChanged: OnDirectionChanged);

    public static readonly BindableProperty SelectedLocationKeyProperty =
        BindableProperty.Create(nameof(SelectedLocationKey), typeof(string), typeof(DirectionEditor), string.Empty,
            BindingMode.TwoWay);

    public static readonly BindableProperty AvailableLocationsProperty =
        BindableProperty.Create(nameof(AvailableLocations), typeof(ObservableCollection<string>),
            typeof(DirectionEditor), null, propertyChanged: OnAvailableLocationsChanged);

    public static readonly BindableProperty HasRestrictionsProperty =
        BindableProperty.Create(nameof(HasRestrictions), typeof(bool), typeof(DirectionEditor), false);

    public static readonly BindableProperty RestrictionCountTextProperty =
        BindableProperty.Create(nameof(RestrictionCountText), typeof(string), typeof(DirectionEditor), string.Empty);

    public int SelectedDirectionIndex
    {
        get => (int)GetValue(SelectedDirectionIndexProperty);
        set => SetValue(SelectedDirectionIndexProperty, value);
    }

    public string SelectedLocationKey
    {
        get => (string)GetValue(SelectedLocationKeyProperty);
        set => SetValue(SelectedLocationKeyProperty, value);
    }

    public ObservableCollection<string> AvailableLocations
    {
        get => (ObservableCollection<string>)GetValue(AvailableLocationsProperty);
        set => SetValue(AvailableLocationsProperty, value);
    }

    public bool HasRestrictions
    {
        get => (bool)GetValue(HasRestrictionsProperty);
        set => SetValue(HasRestrictionsProperty, value);
    }

    public string RestrictionCountText
    {
        get => (string)GetValue(RestrictionCountTextProperty);
        set => SetValue(RestrictionCountTextProperty, value);
    }

    public event EventHandler<DirectionEnum>? DirectionChanged;
    public event EventHandler? RestrictionButtonClicked;

    private readonly List<string> _directions = new()
    {
        "North", "North East", "East", "South East",
        "South", "South West", "West", "North West",
        "Up", "Down", "In", "Out"
    };

    public DirectionEditor()
    {
        InitializeComponent();

        // Populate direction picker
        foreach (var direction in _directions)
        {
            DirectionPicker.Items.Add(direction);
        }

        DirectionPicker.SelectedIndex = 0;

        // Initialize location picker
        AvailableLocations = new ObservableCollection<string>();
    }

    private static void OnDirectionChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DirectionEditor control && newValue is int index && index >= 0 && index < 12)
        {
            var direction = (DirectionEnum)index;
            control.DirectionChanged?.Invoke(control, direction);
        }
    }

    private static void OnAvailableLocationsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is DirectionEditor control && newValue is ObservableCollection<string> locations)
        {
            control.LocationPicker.ItemsSource = locations;
        }
    }

    private void OnRestrictionClicked(object? sender, EventArgs e)
    {
        RestrictionButtonClicked?.Invoke(this, EventArgs.Empty);
    }

    public DirectionEnum GetSelectedDirection()
    {
        return (DirectionEnum)SelectedDirectionIndex;
    }

    public void SetDirection(DirectionEnum direction)
    {
        SelectedDirectionIndex = (int)direction;
    }

    public void SetRestrictionCount(int count)
    {
        HasRestrictions = count > 0;
        RestrictionCountText = count > 0 ? $"{count} restriction(s) applied" : string.Empty;

        // Update button color based on restrictions
        RestrictionButton.BackgroundColor = HasRestrictions
            ? Application.Current?.Resources["ADRIFTPrimary"] as Color ?? Colors.Blue
            : Application.Current?.Resources["Gray400"] as Color ?? Colors.Gray;
    }
}
