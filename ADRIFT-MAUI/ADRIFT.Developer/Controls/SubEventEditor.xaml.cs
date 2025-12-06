using System.Collections.ObjectModel;

namespace ADRIFT.Developer.Controls;

public partial class SubEventEditor : ContentView
{
    public static readonly BindableProperty WhenProperty =
        BindableProperty.Create(nameof(When), typeof(int), typeof(SubEventEditor), 0, BindingMode.TwoWay);

    public static readonly BindableProperty WhenRandomProperty =
        BindableProperty.Create(nameof(WhenRandom), typeof(int), typeof(SubEventEditor), 0, BindingMode.TwoWay);

    public static readonly BindableProperty DescriptionTextProperty =
        BindableProperty.Create(nameof(DescriptionText), typeof(string), typeof(SubEventEditor), string.Empty, BindingMode.TwoWay);

    public int When
    {
        get => (int)GetValue(WhenProperty);
        set => SetValue(WhenProperty, value);
    }

    public int WhenRandom
    {
        get => (int)GetValue(WhenRandomProperty);
        set => SetValue(WhenRandomProperty, value);
    }

    public string DescriptionText
    {
        get => (string)GetValue(DescriptionTextProperty);
        set => SetValue(DescriptionTextProperty, value);
    }

    public event EventHandler? DeleteClicked;
    public event EventHandler<string>? ActionAdded;
    public event EventHandler<string>? ActionRemoved;

    public ObservableCollection<string> Actions { get; } = new();

    public SubEventEditor()
    {
        InitializeComponent();
        ActionsCollectionView.ItemsSource = Actions;
    }

    private void OnDeleteClicked(object? sender, EventArgs e)
    {
        DeleteClicked?.Invoke(this, EventArgs.Empty);
    }

    private async void OnAddActionClicked(object? sender, EventArgs e)
    {
        var actionTypes = new string[]
        {
            "Display Message",
            "Move Object",
            "Move Character",
            "Set Property",
            "Set Variable",
            "Execute Task",
            "End Game"
        };

        var result = await DisplayActionSheet("Select Action Type", "Cancel", null, actionTypes);

        if (result != null && result != "Cancel")
        {
            var actionDescription = $"{result}: (configure)";
            Actions.Add(actionDescription);
            ActionAdded?.Invoke(this, actionDescription);
        }
    }

    private void OnRemoveActionClicked(object? sender, EventArgs e)
    {
        if (sender is Button button && button.BindingContext is string action)
        {
            Actions.Remove(action);
            ActionRemoved?.Invoke(this, action);
        }
    }

    private Task<string> DisplayActionSheet(string title, string cancel, string? destruction, params string[] buttons)
    {
        if (Application.Current?.MainPage != null)
        {
            return Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }
        return Task.FromResult(cancel);
    }

    public void LoadSubEvent(int when, int whenRandom, string description, IEnumerable<string> actions)
    {
        When = when;
        WhenRandom = whenRandom;
        DescriptionText = description;

        Actions.Clear();
        foreach (var action in actions)
        {
            Actions.Add(action);
        }
    }
}
