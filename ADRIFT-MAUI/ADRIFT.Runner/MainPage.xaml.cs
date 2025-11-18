using ADRIFT.Core.IO;
using ADRIFT.Core.Testing;
using ADRIFT.Runner.Pages;

namespace ADRIFT;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private async void OnLoadAdventureClicked(object sender, EventArgs e)
    {
        try
        {
            // Offer to load sample adventure or custom file
            var choice = await DisplayActionSheet(
                "Load Adventure",
                "Cancel",
                null,
                "Play Sample Adventure",
                "Load from File");

            if (choice == "Play Sample Adventure")
            {
                await LoadSampleAdventure();
            }
            else if (choice == "Load from File")
            {
                await LoadAdventureFromFile();
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load adventure: {ex.Message}", "OK");
        }
    }

    private async Task LoadSampleAdventure()
    {
        try
        {
            // Generate and load sample adventure
            var adventure = SampleDataGenerator.CreateSampleAdventure();

            // Navigate to game page
            var gamePage = new GamePage(adventure);
            await Navigation.PushAsync(gamePage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load sample adventure: {ex.Message}", "OK");
        }
    }

    private async Task LoadAdventureFromFile()
    {
        try
        {
            var customFileType = new FilePickerFileType(
                new Dictionary<DevicePlatform, IEnumerable<string>>
                {
                    { DevicePlatform.WinUI, new[] { ".taf", ".xml" } },
                    { DevicePlatform.macOS, new[] { "taf", "xml" } },
                    { DevicePlatform.MacCatalyst, new[] { "taf", "xml" } },
                });

            var options = new PickOptions
            {
                FileTypes = customFileType,
                PickerTitle = "Open ADRIFT Adventure"
            };

            var result = await FilePicker.Default.PickAsync(options);

            if (result != null)
            {
                // Load adventure from file
                var adventure = await AdventureFileIO.LoadAdventureAsync(result.FullPath);

                // Navigate to game page
                var gamePage = new GamePage(adventure);
                await Navigation.PushAsync(gamePage);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load adventure from file: {ex.Message}", "OK");
        }
    }
}
