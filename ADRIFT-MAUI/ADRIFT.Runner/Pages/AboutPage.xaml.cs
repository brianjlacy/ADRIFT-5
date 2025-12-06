using ADRIFT.Core.Models;
using System.Reflection;

namespace ADRIFT.Runner.Pages;

public partial class AboutPage : ContentPage
{
    private const string ADRIFT_WEBSITE = "https://www.adrift.co";
    private const string ADRIFT_FORUM = "https://www.adrift.co/cgi/yabb/YaBB.pl";
    private const string ADRIFT_MANUAL = "https://www.adrift.co/manual/";

    public AboutPage()
    {
        InitializeComponent();
        LoadSystemInformation();
    }

    public void SetAdventure(Adventure adventure)
    {
        if (adventure != null)
        {
            AdventureTitleLabel.Text = !string.IsNullOrWhiteSpace(adventure.Title)
                ? adventure.Title
                : "Untitled Adventure";

            AdventureAuthorLabel.Text = !string.IsNullOrWhiteSpace(adventure.Author)
                ? adventure.Author
                : "Unknown";

            AdventureVersionLabel.Text = !string.IsNullOrWhiteSpace(adventure.Version)
                ? adventure.Version
                : "1.0";

            AdventureGenreLabel.Text = !string.IsNullOrWhiteSpace(adventure.Genre)
                ? adventure.Genre
                : "Adventure";
        }
        else
        {
            AdventureTitleLabel.Text = "No adventure loaded";
            AdventureAuthorLabel.Text = "-";
            AdventureVersionLabel.Text = "-";
            AdventureGenreLabel.Text = "-";
        }
    }

    private void LoadSystemInformation()
    {
        // Load version from assembly
        var version = Assembly.GetExecutingAssembly().GetName().Version;
        VersionLabel.Text = $"Version {version?.Major}.{version?.Minor}.{version?.Build ?? 0}";

        // Platform information
        PlatformLabel.Text = DeviceInfo.Platform.ToString();

        // .NET version
        DotNetLabel.Text = Environment.Version.ToString();

        // Device information
        var deviceType = DeviceInfo.Idiom switch
        {
            DeviceIdiom.Phone => "Phone",
            DeviceIdiom.Tablet => "Tablet",
            DeviceIdiom.Desktop => "Desktop",
            DeviceIdiom.TV => "TV",
            DeviceIdiom.Watch => "Watch",
            _ => "Unknown"
        };

        DeviceLabel.Text = $"{DeviceInfo.Manufacturer} {DeviceInfo.Model} ({deviceType})";
    }

    private async void OnWebsiteTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            await Browser.OpenAsync(ADRIFT_WEBSITE, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not open website: {ex.Message}", "OK");
        }
    }

    private async void OnForumTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            await Browser.OpenAsync(ADRIFT_FORUM, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not open forum: {ex.Message}", "OK");
        }
    }

    private async void OnHelpTapped(object? sender, TappedEventArgs e)
    {
        try
        {
            await Browser.OpenAsync(ADRIFT_MANUAL, BrowserLaunchMode.SystemPreferred);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Could not open manual: {ex.Message}", "OK");
        }
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
