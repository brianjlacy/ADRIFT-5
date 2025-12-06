using System.Collections;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.Controls;

public partial class AutoCompleteCombo : ContentView
{
    public static readonly BindableProperty TextProperty =
        BindableProperty.Create(nameof(Text), typeof(string), typeof(AutoCompleteCombo), string.Empty,
            BindingMode.TwoWay, propertyChanged: OnTextPropertyChanged);

    public static readonly BindableProperty PlaceholderProperty =
        BindableProperty.Create(nameof(Placeholder), typeof(string), typeof(AutoCompleteCombo), "Search...");

    public static readonly BindableProperty ItemsSourceProperty =
        BindableProperty.Create(nameof(ItemsSource), typeof(IList), typeof(AutoCompleteCombo), null,
            propertyChanged: OnItemsSourceChanged);

    public static readonly BindableProperty MinimumPrefixLengthProperty =
        BindableProperty.Create(nameof(MinimumPrefixLength), typeof(int), typeof(AutoCompleteCombo), 1);

    public static readonly BindableProperty MaxSuggestionsProperty =
        BindableProperty.Create(nameof(MaxSuggestions), typeof(int), typeof(AutoCompleteCombo), 10);

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

    public IList ItemsSource
    {
        get => (IList)GetValue(ItemsSourceProperty);
        set => SetValue(ItemsSourceProperty, value);
    }

    public int MinimumPrefixLength
    {
        get => (int)GetValue(MinimumPrefixLengthProperty);
        set => SetValue(MinimumPrefixLengthProperty, value);
    }

    public int MaxSuggestions
    {
        get => (int)GetValue(MaxSuggestionsProperty);
        set => SetValue(MaxSuggestionsProperty, value);
    }

    public event EventHandler<string>? ItemSelected;

    private ObservableCollection<string> _filteredSuggestions = new();
    private bool _isUpdatingText;
    private bool _shouldShowDropdown;

    public AutoCompleteCombo()
    {
        InitializeComponent();
        SuggestionsCollectionView.ItemsSource = _filteredSuggestions;
    }

    private static void OnTextPropertyChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is AutoCompleteCombo control && !control._isUpdatingText)
        {
            control.FilterSuggestions(newValue as string ?? string.Empty);
        }
    }

    private static void OnItemsSourceChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is AutoCompleteCombo control)
        {
            control.FilterSuggestions(control.Text);
        }
    }

    private void OnTextChanged(object? sender, TextChangedEventArgs e)
    {
        if (!_isUpdatingText)
        {
            FilterSuggestions(e.NewTextValue);
            _shouldShowDropdown = true;
        }
    }

    private void FilterSuggestions(string searchText)
    {
        _filteredSuggestions.Clear();

        if (string.IsNullOrWhiteSpace(searchText) || searchText.Length < MinimumPrefixLength || ItemsSource == null)
        {
            HideDropdown();
            return;
        }

        var matches = ItemsSource
            .Cast<object>()
            .Select(item => item?.ToString() ?? string.Empty)
            .Where(text => text.Contains(searchText, StringComparison.OrdinalIgnoreCase))
            .OrderBy(text => !text.StartsWith(searchText, StringComparison.OrdinalIgnoreCase)) // Prioritize prefix matches
            .ThenBy(text => text)
            .Take(MaxSuggestions)
            .ToList();

        foreach (var match in matches)
        {
            _filteredSuggestions.Add(match);
        }

        if (_filteredSuggestions.Count > 0 && _shouldShowDropdown)
        {
            ShowDropdown();
        }
        else
        {
            HideDropdown();
        }
    }

    private void OnSelectionChanged(object? sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is string selectedItem)
        {
            SelectItem(selectedItem);
        }
    }

    private void OnItemTapped(object? sender, TappedEventArgs e)
    {
        if (sender is Grid grid && grid.BindingContext is string selectedItem)
        {
            SelectItem(selectedItem);
        }
    }

    private void SelectItem(string selectedItem)
    {
        _isUpdatingText = true;
        _shouldShowDropdown = false;

        Text = selectedItem;
        SearchEntry.Text = selectedItem;

        HideDropdown();

        _isUpdatingText = false;

        ItemSelected?.Invoke(this, selectedItem);
    }

    private void OnEntryFocused(object? sender, FocusEventArgs e)
    {
        _shouldShowDropdown = true;
        FilterSuggestions(Text);
    }

    private void OnEntryUnfocused(object? sender, FocusEventArgs e)
    {
        // Delay hiding to allow tap to register
        Dispatcher.DispatchDelayed(TimeSpan.FromMilliseconds(200), () =>
        {
            HideDropdown();
        });
    }

    private void OnEntryCompleted(object? sender, EventArgs e)
    {
        HideDropdown();
    }

    private void ShowDropdown()
    {
        DropdownBorder.IsVisible = true;
    }

    private void HideDropdown()
    {
        DropdownBorder.IsVisible = false;
        _shouldShowDropdown = false;
    }
}
