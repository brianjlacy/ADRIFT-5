namespace ADRIFT.Runner.Pages;

public partial class SettingsPage : ContentPage
{
    private const string PREF_TEXT_SIZE = "TextSize";
    private const string PREF_BG_COLOR = "BackgroundColor";
    private const string PREF_TEXT_COLOR = "TextColor";
    private const string PREF_SOUND_ENABLED = "SoundEnabled";
    private const string PREF_SOUND_VOLUME = "SoundVolume";
    private const string PREF_MUSIC_ENABLED = "MusicEnabled";
    private const string PREF_MUSIC_VOLUME = "MusicVolume";
    private const string PREF_SCREEN_READER = "ScreenReader";
    private const string PREF_HIGH_CONTRAST = "HighContrast";
    private const string PREF_REDUCE_ANIMATIONS = "ReduceAnimations";
    private const string PREF_AUTO_SAVE = "AutoSave";
    private const string PREF_CONFIRM_EXIT = "ConfirmExit";
    private const string PREF_HISTORY_SIZE = "HistorySize";

    public event EventHandler? SettingsChanged;

    public SettingsPage()
    {
        InitializeComponent();
        LoadSettings();
    }

    private void LoadSettings()
    {
        // Display settings
        TextSizeSlider.Value = Preferences.Get(PREF_TEXT_SIZE, 14.0);
        BackgroundColorPicker.SelectedIndex = Preferences.Get(PREF_BG_COLOR, 0);
        TextColorPicker.SelectedIndex = Preferences.Get(PREF_TEXT_COLOR, 0);

        // Audio settings
        SoundEffectsSwitch.IsToggled = Preferences.Get(PREF_SOUND_ENABLED, true);
        SoundVolumeSlider.Value = Preferences.Get(PREF_SOUND_VOLUME, 80.0);
        MusicSwitch.IsToggled = Preferences.Get(PREF_MUSIC_ENABLED, false);
        MusicVolumeSlider.Value = Preferences.Get(PREF_MUSIC_VOLUME, 50.0);

        // Accessibility settings
        ScreenReaderSwitch.IsToggled = Preferences.Get(PREF_SCREEN_READER, false);
        HighContrastSwitch.IsToggled = Preferences.Get(PREF_HIGH_CONTRAST, false);
        ReduceAnimationsSwitch.IsToggled = Preferences.Get(PREF_REDUCE_ANIMATIONS, false);

        // Gameplay settings
        AutoSaveSwitch.IsToggled = Preferences.Get(PREF_AUTO_SAVE, true);
        ConfirmExitSwitch.IsToggled = Preferences.Get(PREF_CONFIRM_EXIT, true);
        HistorySizePicker.SelectedIndex = GetHistorySizeIndex(Preferences.Get(PREF_HISTORY_SIZE, 50));
    }

    private void SaveSettings()
    {
        // Display settings
        Preferences.Set(PREF_TEXT_SIZE, TextSizeSlider.Value);
        Preferences.Set(PREF_BG_COLOR, BackgroundColorPicker.SelectedIndex);
        Preferences.Set(PREF_TEXT_COLOR, TextColorPicker.SelectedIndex);

        // Audio settings
        Preferences.Set(PREF_SOUND_ENABLED, SoundEffectsSwitch.IsToggled);
        Preferences.Set(PREF_SOUND_VOLUME, SoundVolumeSlider.Value);
        Preferences.Set(PREF_MUSIC_ENABLED, MusicSwitch.IsToggled);
        Preferences.Set(PREF_MUSIC_VOLUME, MusicVolumeSlider.Value);

        // Accessibility settings
        Preferences.Set(PREF_SCREEN_READER, ScreenReaderSwitch.IsToggled);
        Preferences.Set(PREF_HIGH_CONTRAST, HighContrastSwitch.IsToggled);
        Preferences.Set(PREF_REDUCE_ANIMATIONS, ReduceAnimationsSwitch.IsToggled);

        // Gameplay settings
        Preferences.Set(PREF_AUTO_SAVE, AutoSaveSwitch.IsToggled);
        Preferences.Set(PREF_CONFIRM_EXIT, ConfirmExitSwitch.IsToggled);
        Preferences.Set(PREF_HISTORY_SIZE, GetHistorySize());

        // Notify listeners
        SettingsChanged?.Invoke(this, EventArgs.Empty);
    }

    private int GetHistorySize()
    {
        return HistorySizePicker.SelectedIndex switch
        {
            0 => 10,
            1 => 25,
            2 => 50,
            3 => 100,
            _ => 50
        };
    }

    private int GetHistorySizeIndex(int size)
    {
        return size switch
        {
            10 => 0,
            25 => 1,
            50 => 2,
            100 => 3,
            _ => 2
        };
    }

    // Display Settings Event Handlers
    private void OnTextSizeChanged(object? sender, ValueChangedEventArgs e)
    {
        var size = (int)e.NewValue;
        TextSizeLabel.Text = size.ToString();
        PreviewText.FontSize = size;
        SaveSettings();
    }

    private void OnBackgroundColorChanged(object? sender, EventArgs e)
    {
        SaveSettings();
        ApplyBackgroundColor();
    }

    private void OnTextColorChanged(object? sender, EventArgs e)
    {
        SaveSettings();
        ApplyTextColor();
    }

    private void ApplyBackgroundColor()
    {
        var bgColor = BackgroundColorPicker.SelectedIndex switch
        {
            0 => Color.FromArgb("#1a1a1a"), // Dark
            1 => Colors.White, // Light
            2 => Color.FromArgb("#f4ecd8"), // Sepia
            3 => Colors.Black, // High Contrast
            _ => Color.FromArgb("#1a1a1a")
        };
        PreviewText.BackgroundColor = bgColor;
    }

    private void ApplyTextColor()
    {
        var textColor = TextColorPicker.SelectedIndex switch
        {
            0 => Colors.White,
            1 => Colors.Black,
            2 => Color.FromArgb("#00ff00"), // Green
            3 => Color.FromArgb("#ffbf00"), // Amber
            _ => Colors.White
        };
        PreviewText.TextColor = textColor;
    }

    // Audio Settings Event Handlers
    private void OnSoundEffectsToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();
    }

    private void OnSoundVolumeChanged(object? sender, ValueChangedEventArgs e)
    {
        SaveSettings();
    }

    private void OnMusicToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();
    }

    private void OnMusicVolumeChanged(object? sender, ValueChangedEventArgs e)
    {
        SaveSettings();
    }

    // Accessibility Event Handlers
    private void OnScreenReaderToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();
    }

    private void OnHighContrastToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();

        if (e.Value)
        {
            BackgroundColorPicker.SelectedIndex = 3; // High Contrast
        }
    }

    private void OnReduceAnimationsToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();
    }

    // Gameplay Event Handlers
    private void OnAutoSaveToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();
    }

    private void OnConfirmExitToggled(object? sender, ToggledEventArgs e)
    {
        SaveSettings();
    }

    private void OnHistorySizeChanged(object? sender, EventArgs e)
    {
        SaveSettings();
    }

    // Action Button Handlers
    private async void OnResetDefaultsClicked(object? sender, EventArgs e)
    {
        var result = await DisplayAlert(
            "Reset Settings",
            "Are you sure you want to reset all settings to their default values?",
            "Reset",
            "Cancel");

        if (result)
        {
            // Clear all preferences
            Preferences.Clear();

            // Reload default values
            LoadSettings();

            await DisplayAlert("Success", "Settings have been reset to defaults", "OK");
        }
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        SaveSettings();
    }

    // Public getters for settings values
    public double GetTextSize() => Preferences.Get(PREF_TEXT_SIZE, 14.0);
    public int GetBackgroundColorIndex() => Preferences.Get(PREF_BG_COLOR, 0);
    public int GetTextColorIndex() => Preferences.Get(PREF_TEXT_COLOR, 0);
    public bool IsSoundEnabled() => Preferences.Get(PREF_SOUND_ENABLED, true);
    public double GetSoundVolume() => Preferences.Get(PREF_SOUND_VOLUME, 80.0);
    public bool IsMusicEnabled() => Preferences.Get(PREF_MUSIC_ENABLED, false);
    public double GetMusicVolume() => Preferences.Get(PREF_MUSIC_VOLUME, 50.0);
    public bool IsScreenReaderEnabled() => Preferences.Get(PREF_SCREEN_READER, false);
    public bool IsHighContrastEnabled() => Preferences.Get(PREF_HIGH_CONTRAST, false);
    public bool IsReduceAnimationsEnabled() => Preferences.Get(PREF_REDUCE_ANIMATIONS, false);
    public bool IsAutoSaveEnabled() => Preferences.Get(PREF_AUTO_SAVE, true);
    public bool IsConfirmExitEnabled() => Preferences.Get(PREF_CONFIRM_EXIT, true);
    public int GetCommandHistorySize() => Preferences.Get(PREF_HISTORY_SIZE, 50);
}
