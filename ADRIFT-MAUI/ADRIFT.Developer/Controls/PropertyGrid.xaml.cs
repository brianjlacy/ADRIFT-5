using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.Controls;

public partial class PropertyGrid : ContentView
{
    public static readonly BindableProperty PropertiesProperty =
        BindableProperty.Create(nameof(Properties), typeof(ObservableCollection<PropertyGridItem>),
            typeof(PropertyGrid), null, propertyChanged: OnPropertiesChanged);

    public ObservableCollection<PropertyGridItem> Properties
    {
        get => (ObservableCollection<PropertyGridItem>)GetValue(PropertiesProperty);
        set => SetValue(PropertiesProperty, value);
    }

    public PropertyGrid()
    {
        InitializeComponent();
        Properties = new ObservableCollection<PropertyGridItem>();
    }

    private static void OnPropertiesChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is PropertyGrid control && newValue is ObservableCollection<PropertyGridItem> properties)
        {
            control.BuildPropertyGrid(properties);
        }
    }

    private void BuildPropertyGrid(ObservableCollection<PropertyGridItem> properties)
    {
        PropertiesContainer.Clear();

        string? currentCategory = null;
        VerticalStackLayout? categoryContainer = null;

        foreach (var property in properties)
        {
            // Create category header if new category
            if (property.Category != currentCategory)
            {
                currentCategory = property.Category;

                if (!string.IsNullOrWhiteSpace(currentCategory))
                {
                    var categoryHeader = new Frame
                    {
                        BackgroundColor = Application.Current?.Resources["Gray100"] as Color ?? Colors.LightGray,
                        Padding = new Thickness(10, 8),
                        CornerRadius = 4,
                        Margin = new Thickness(0, 10, 0, 2),
                        Content = new Label
                        {
                            Text = currentCategory,
                            FontAttributes = FontAttributes.Bold,
                            FontSize = 14
                        }
                    };

                    PropertiesContainer.Add(categoryHeader);
                }

                categoryContainer = new VerticalStackLayout { Spacing = 1 };
                PropertiesContainer.Add(categoryContainer);
            }

            // Create property row
            var propertyRow = CreatePropertyRow(property);
            categoryContainer?.Add(propertyRow);
        }
    }

    private View CreatePropertyRow(PropertyGridItem property)
    {
        var grid = new Grid
        {
            ColumnDefinitions =
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(1.5, GridUnitType.Star) }
            },
            Padding = new Thickness(10, 8),
            BackgroundColor = Colors.White
        };

        // Property name label
        var nameLabel = new Label
        {
            Text = property.DisplayName,
            VerticalOptions = LayoutOptions.Center,
            FontSize = 13
        };
        grid.Add(nameLabel, 0, 0);

        // Property value editor
        var editor = CreateEditor(property);
        grid.Add(editor, 1, 0);

        return grid;
    }

    private View CreateEditor(PropertyGridItem property)
    {
        return property.EditorType switch
        {
            PropertyEditorType.Text => CreateTextEditor(property),
            PropertyEditorType.Number => CreateNumberEditor(property),
            PropertyEditorType.Boolean => CreateBooleanEditor(property),
            PropertyEditorType.Picker => CreatePickerEditor(property),
            PropertyEditorType.MultilineText => CreateMultilineEditor(property),
            PropertyEditorType.ReadOnly => CreateReadOnlyEditor(property),
            _ => CreateTextEditor(property)
        };
    }

    private Entry CreateTextEditor(PropertyGridItem property)
    {
        var entry = new Entry
        {
            Text = property.Value?.ToString() ?? string.Empty,
            Placeholder = property.Placeholder,
            FontSize = 13
        };

        entry.TextChanged += (s, e) =>
        {
            property.Value = e.NewTextValue;
            property.OnValueChanged();
        };

        return entry;
    }

    private Entry CreateNumberEditor(PropertyGridItem property)
    {
        var entry = new Entry
        {
            Text = property.Value?.ToString() ?? "0",
            Keyboard = Keyboard.Numeric,
            FontSize = 13
        };

        entry.TextChanged += (s, e) =>
        {
            if (int.TryParse(e.NewTextValue, out var value))
            {
                property.Value = value;
                property.OnValueChanged();
            }
        };

        return entry;
    }

    private CheckBox CreateBooleanEditor(PropertyGridItem property)
    {
        var checkBox = new CheckBox
        {
            IsChecked = property.Value is bool boolVal && boolVal,
            VerticalOptions = LayoutOptions.Center
        };

        checkBox.CheckedChanged += (s, e) =>
        {
            property.Value = e.Value;
            property.OnValueChanged();
        };

        return checkBox;
    }

    private Picker CreatePickerEditor(PropertyGridItem property)
    {
        var picker = new Picker
        {
            FontSize = 13,
            VerticalOptions = LayoutOptions.Center
        };

        if (property.Options != null)
        {
            foreach (var option in property.Options)
            {
                picker.Items.Add(option);
            }
        }

        if (property.Value != null)
        {
            var index = picker.Items.IndexOf(property.Value.ToString()!);
            if (index >= 0)
            {
                picker.SelectedIndex = index;
            }
        }

        picker.SelectedIndexChanged += (s, e) =>
        {
            if (picker.SelectedIndex >= 0)
            {
                property.Value = picker.Items[picker.SelectedIndex];
                property.OnValueChanged();
            }
        };

        return picker;
    }

    private Editor CreateMultilineEditor(PropertyGridItem property)
    {
        var editor = new Editor
        {
            Text = property.Value?.ToString() ?? string.Empty,
            FontSize = 13,
            HeightRequest = 80,
            AutoSize = EditorAutoSizeOption.TextChanges
        };

        editor.TextChanged += (s, e) =>
        {
            property.Value = e.NewTextValue;
            property.OnValueChanged();
        };

        return editor;
    }

    private Label CreateReadOnlyEditor(PropertyGridItem property)
    {
        return new Label
        {
            Text = property.Value?.ToString() ?? string.Empty,
            FontSize = 13,
            VerticalOptions = LayoutOptions.Center,
            TextColor = Application.Current?.Resources["Gray600"] as Color ?? Colors.Gray
        };
    }
}

/// <summary>
/// Represents a property in the property grid
/// </summary>
public partial class PropertyGridItem : ObservableObject
{
    [ObservableProperty]
    private string displayName = "";

    [ObservableProperty]
    private string? category;

    [ObservableProperty]
    private object? value;

    [ObservableProperty]
    private PropertyEditorType editorType = PropertyEditorType.Text;

    [ObservableProperty]
    private string? placeholder;

    [ObservableProperty]
    private List<string>? options;

    public event EventHandler? ValueChanged;

    public void OnValueChanged()
    {
        ValueChanged?.Invoke(this, EventArgs.Empty);
    }
}

/// <summary>
/// Type of editor for a property
/// </summary>
public enum PropertyEditorType
{
    Text,
    Number,
    Boolean,
    Picker,
    MultilineText,
    ReadOnly
}
