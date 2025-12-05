using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class PropertyEditorPage : ContentPage
{
    private readonly Property _property;
    private readonly AdventureService _adventureService;

    public PropertyEditorPage(Property property)
    {
        InitializeComponent();
        _property = property;
        _adventureService = AdventureService.Instance;

        LoadProperty();
    }

    private void LoadProperty()
    {
        NameEntry.Text = _property.Name;
        DescriptionEditor.Text = _property.Description;

        // Setup type picker
        TypePicker.ItemsSource = Enum.GetNames(typeof(PropertyType)).ToList();
        TypePicker.SelectedItem = _property.Type.ToString();
        TypePicker.SelectedIndexChanged += OnTypeChanged;

        // Load type-specific fields
        LoadTypeSpecificFields();
    }

    private void OnTypeChanged(object? sender, EventArgs e)
    {
        LoadTypeSpecificFields();
    }

    private void LoadTypeSpecificFields()
    {
        var selectedType = Enum.Parse<PropertyType>(TypePicker.SelectedItem?.ToString() ?? "SelectionOnly");

        // Hide all frames first
        StatesFrame.IsVisible = false;
        IntegerFrame.IsVisible = false;
        TextFrame.IsVisible = false;

        // Show relevant frame based on type
        switch (selectedType)
        {
            case PropertyType.StateList:
                StatesFrame.IsVisible = true;
                StatesEditor.Text = string.Join("\n", _property.States);
                SelectedStateEntry.Text = _property.SelectedState ?? "";
                break;

            case PropertyType.Integer:
                IntegerFrame.IsVisible = true;
                IntegerValueEntry.Text = _property.IntValue.ToString();
                break;

            case PropertyType.Text:
                TextFrame.IsVisible = true;
                TextValueEditor.Text = _property.StringValue ?? "";
                break;
        }
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        try
        {
            // Save basic fields
            _property.Name = NameEntry.Text?.Trim() ?? "Unnamed Property";
            _property.Description = DescriptionEditor.Text?.Trim() ?? "";
            _property.Type = Enum.Parse<PropertyType>(TypePicker.SelectedItem?.ToString() ?? "SelectionOnly");

            // Save type-specific fields
            switch (_property.Type)
            {
                case PropertyType.StateList:
                    _property.States = StatesEditor.Text?
                        .Split('\n', StringSplitOptions.RemoveEmptyEntries)
                        .Select(s => s.Trim())
                        .ToList() ?? new List<string>();
                    _property.SelectedState = SelectedStateEntry.Text?.Trim();
                    break;

                case PropertyType.Integer:
                    if (int.TryParse(IntegerValueEntry.Text, out var intValue))
                    {
                        _property.IntValue = intValue;
                    }
                    break;

                case PropertyType.Text:
                    _property.StringValue = TextValueEditor.Text?.Trim() ?? "";
                    break;
            }

            await DisplayAlert("Success", "Property saved successfully.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save property: {ex.Message}", "OK");
        }
    }

    private async void OnDelete(object? sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        var result = await DisplayAlert(
            "Delete Property",
            $"Are you sure you want to delete '{_property.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            adventure.Properties.Remove(_property.Key);
            await DisplayAlert("Success", "Property deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}
