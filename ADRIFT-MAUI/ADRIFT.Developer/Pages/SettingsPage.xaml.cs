using ADRIFT.Core.Models;
using ADRIFT.Developer.Services;

namespace ADRIFT.Developer.Pages;

public partial class SettingsPage : ContentPage
{
    private readonly AdventureService _adventureService;

    public SettingsPage()
    {
        InitializeComponent();
        _adventureService = AdventureService.Instance;
        LoadSettings();
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        LoadSettings();
    }

    private void LoadSettings()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
            return;

        // General Information
        TitleEntry.Text = adventure.Title;
        AuthorEntry.Text = adventure.Author;
        VersionEntry.Text = adventure.Version;
        DescriptionEditor.Text = adventure.Description;
        GenreEntry.Text = adventure.Genre ?? string.Empty;
        LanguageEntry.Text = adventure.Language ?? "English";

        // Game Settings
        MaxScoreEntry.Text = adventure.MaxScore.ToString();
        UseTimeCheckBox.IsChecked = adventure.UseTime;
        ShowExitsCheckBox.IsChecked = adventure.ShowExits;
        Use8PointCompassCheckBox.IsChecked = adventure.Use8PointCompass;
        EnableBattleSystemCheckBox.IsChecked = adventure.EnableBattleSystem;
        TaskExecutionModePicker.SelectedItem = adventure.TaskExecutionMode.ToString();
        DefaultPerspectivePicker.SelectedItem = adventure.DefaultPerspective.ToString();

        // Display Settings
        CustomFontNameEntry.Text = adventure.CustomFontName ?? string.Empty;
        CustomFontSizeEntry.Text = adventure.CustomFontSize?.ToString() ?? string.Empty;
        BackgroundColorEntry.Text = adventure.BackgroundColor ?? string.Empty;
        ForegroundColorEntry.Text = adventure.ForegroundColor ?? string.Empty;
        LinkColorEntry.Text = adventure.LinkColor ?? string.Empty;

        // Game Text
        IntroductionTextEditor.Text = adventure.IntroductionText?.Text ?? string.Empty;
        WinTextEditor.Text = adventure.WinText?.Text ?? string.Empty;
        LoseTextEditor.Text = adventure.LoseText?.Text ?? string.Empty;
        NotUnderstoodMessageEntry.Text = adventure.NotUnderstoodMessage ?? string.Empty;

        // Library Settings
        IsLibraryCheckBox.IsChecked = adventure.IsLibrary;

        // Metadata
        IFIDEntry.Text = adventure.IFID ?? "(Will be auto-generated)";
        ForgivenessLevelPicker.SelectedItem = adventure.ForgivenessLevel ?? "Polite";
        if (adventure.FirstPublished.HasValue)
        {
            FirstPublishedPicker.Date = adventure.FirstPublished.Value;
        }
    }

    private async void OnSave(object? sender, EventArgs e)
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await DisplayAlert("Error", "No adventure loaded.", "OK");
                return;
            }

            // General Information
            adventure.Title = TitleEntry.Text?.Trim() ?? "Untitled Adventure";
            adventure.Author = AuthorEntry.Text?.Trim() ?? string.Empty;
            adventure.Version = VersionEntry.Text?.Trim() ?? "1.0";
            adventure.Description = DescriptionEditor.Text?.Trim() ?? string.Empty;
            adventure.Genre = GenreEntry.Text?.Trim();
            adventure.Language = LanguageEntry.Text?.Trim() ?? "English";

            // Game Settings
            if (int.TryParse(MaxScoreEntry.Text, out var maxScore))
            {
                adventure.MaxScore = maxScore;
            }
            adventure.UseTime = UseTimeCheckBox.IsChecked;
            adventure.ShowExits = ShowExitsCheckBox.IsChecked;
            adventure.Use8PointCompass = Use8PointCompassCheckBox.IsChecked;
            adventure.EnableBattleSystem = EnableBattleSystemCheckBox.IsChecked;

            if (Enum.TryParse<TaskExecutionMode>(TaskExecutionModePicker.SelectedItem?.ToString(), out var execMode))
            {
                adventure.TaskExecutionMode = execMode;
            }

            if (Enum.TryParse<Perspective>(DefaultPerspectivePicker.SelectedItem?.ToString(), out var perspective))
            {
                adventure.DefaultPerspective = perspective;
            }

            // Display Settings
            adventure.CustomFontName = string.IsNullOrWhiteSpace(CustomFontNameEntry.Text) ? null : CustomFontNameEntry.Text.Trim();
            if (int.TryParse(CustomFontSizeEntry.Text, out var fontSize))
            {
                adventure.CustomFontSize = fontSize;
            }
            else
            {
                adventure.CustomFontSize = null;
            }
            adventure.BackgroundColor = string.IsNullOrWhiteSpace(BackgroundColorEntry.Text) ? null : BackgroundColorEntry.Text.Trim();
            adventure.ForegroundColor = string.IsNullOrWhiteSpace(ForegroundColorEntry.Text) ? null : ForegroundColorEntry.Text.Trim();
            adventure.LinkColor = string.IsNullOrWhiteSpace(LinkColorEntry.Text) ? null : LinkColorEntry.Text.Trim();

            // Game Text
            adventure.IntroductionText = new Description { Text = IntroductionTextEditor.Text?.Trim() ?? string.Empty };
            adventure.WinText = new Description { Text = WinTextEditor.Text?.Trim() ?? string.Empty };
            adventure.LoseText = new Description { Text = LoseTextEditor.Text?.Trim() ?? string.Empty };
            adventure.NotUnderstoodMessage = string.IsNullOrWhiteSpace(NotUnderstoodMessageEntry.Text) ? null : NotUnderstoodMessageEntry.Text.Trim();

            // Library Settings
            adventure.IsLibrary = IsLibraryCheckBox.IsChecked;

            // Metadata
            // IFID should be auto-generated if empty - for now just keep existing
            adventure.ForgivenessLevel = ForgivenessLevelPicker.SelectedItem?.ToString();
            adventure.FirstPublished = FirstPublishedPicker.Date;

            // Generate IFID if not present
            if (string.IsNullOrEmpty(adventure.IFID))
            {
                adventure.IFID = Guid.NewGuid().ToString().ToUpper();
                IFIDEntry.Text = adventure.IFID;
            }

            // Update modified timestamp
            adventure.Modified = DateTime.Now;

            await DisplayAlert("Success", "Settings saved successfully.", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save settings: {ex.Message}", "OK");
        }
    }

    private async void OnPickBackgroundColor(object? sender, EventArgs e)
    {
        // For now, just show an info message
        // TODO: Implement color picker dialog
        await DisplayAlert("Color Picker", "Color picker not yet implemented. Please enter color name or hex code (e.g., #FFFFFF or White).", "OK");
    }

    private async void OnPickForegroundColor(object? sender, EventArgs e)
    {
        await DisplayAlert("Color Picker", "Color picker not yet implemented. Please enter color name or hex code (e.g., #000000 or Black).", "OK");
    }

    private async void OnPickLinkColor(object? sender, EventArgs e)
    {
        await DisplayAlert("Color Picker", "Color picker not yet implemented. Please enter color name or hex code (e.g., #0000FF or Blue).", "OK");
    }
}
