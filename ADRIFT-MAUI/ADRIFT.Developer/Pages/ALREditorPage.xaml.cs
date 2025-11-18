using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class ALREditorPage : ContentPage
{
    private readonly ALR _alr;
    private readonly AdventureService _adventureService;

    public ALREditorPage(ALR alr)
    {
        InitializeComponent();
        _alr = alr;
        _adventureService = AdventureService.Instance;

        LoadALR();
    }

    private void LoadALR()
    {
        OldTextEditor.Text = _alr.OldText;
        NewTextEditor.Text = _alr.NewText.Text;
        CaseSensitiveCheckBox.IsChecked = _alr.CaseSensitive;
        WholeWordsOnlyCheckBox.IsChecked = _alr.WholeWordsOnly;
        OrderStepper.Value = _alr.Order;
        OrderLabel.Text = _alr.Order.ToString();
    }

    private void OnOrderChanged(object? sender, ValueChangedEventArgs e)
    {
        OrderLabel.Text = ((int)e.NewValue).ToString();
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        try
        {
            _alr.OldText = OldTextEditor.Text?.Trim() ?? "";
            _alr.NewText.Text = NewTextEditor.Text?.Trim() ?? "";
            _alr.CaseSensitive = CaseSensitiveCheckBox.IsChecked;
            _alr.WholeWordsOnly = WholeWordsOnlyCheckBox.IsChecked;
            _alr.Order = (int)OrderStepper.Value;

            await DisplayAlert("Success", "ALR saved successfully.", "OK");
            await Navigation.PopAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save ALR: {ex.Message}", "OK");
        }
    }

    private async void OnDelete(object? sender, EventArgs e)
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        var result = await DisplayAlert(
            "Delete ALR",
            $"Are you sure you want to delete this text override?",
            "Delete", "Cancel");

        if (result)
        {
            adventure.ALRs.Remove(_alr.Key);
            await DisplayAlert("Success", "ALR deleted.", "OK");
            await Navigation.PopAsync();
        }
    }
}
