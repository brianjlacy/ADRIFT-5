using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.ViewModels;

public partial class MainViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public MainViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        LoadAdventure();
    }

    [ObservableProperty]
    private string adventureTitle = "New Adventure";

    [ObservableProperty]
    private string author = "";

    [ObservableProperty]
    private string introduction = "";

    [ObservableProperty]
    private string winningText = "";

    [ObservableProperty]
    private string version = "5.0.36.6";

    [ObservableProperty]
    private int locationCount;

    [ObservableProperty]
    private int objectCount;

    [ObservableProperty]
    private int taskCount;

    [ObservableProperty]
    private int characterCount;

    [ObservableProperty]
    private int eventCount;

    [ObservableProperty]
    private int variableCount;

    [ObservableProperty]
    private int groupCount;

    [ObservableProperty]
    private string lastModified = "Never";

    [ObservableProperty]
    private bool hasUnsavedChanges;

    [ObservableProperty]
    private string? currentFilePath;

    [ObservableProperty]
    private string statusMessage = "Ready";

    private void LoadAdventure()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure != null)
        {
            AdventureTitle = string.IsNullOrWhiteSpace(adventure.Title) ? "Untitled Adventure" : adventure.Title;
            Author = adventure.Author;
            Introduction = adventure.Introduction;
            Version = adventure.Version;

            if (adventure.LastModified != default)
            {
                LastModified = adventure.LastModified.ToString("g");
            }
        }

        UpdateStatistics();
    }

    private void UpdateStatistics()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure != null)
        {
            LocationCount = adventure.Locations.Count;
            ObjectCount = adventure.Objects.Count;
            TaskCount = adventure.Tasks.Count;
            CharacterCount = adventure.Characters.Count;
            EventCount = adventure.Events.Count;
            VariableCount = adventure.Variables.Count;
            GroupCount = adventure.Groups.Count;
        }
        else
        {
            LocationCount = 0;
            ObjectCount = 0;
            TaskCount = 0;
            CharacterCount = 0;
            EventCount = 0;
            VariableCount = 0;
            GroupCount = 0;
        }
    }

    [RelayCommand]
    private async Task Refresh()
    {
        LoadAdventure();
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task NewLocation()
    {
        await Shell.Current.GoToAsync("locationeditor");
    }

    [RelayCommand]
    private async Task ViewLocations()
    {
        await Shell.Current.GoToAsync("//locations");
    }

    [RelayCommand]
    private async Task NewObject()
    {
        await Shell.Current.GoToAsync("objecteditor");
    }

    [RelayCommand]
    private async Task ViewObjects()
    {
        await Shell.Current.GoToAsync("//objects");
    }

    [RelayCommand]
    private async Task NewTask()
    {
        await Shell.Current.GoToAsync("taskeditor");
    }

    [RelayCommand]
    private async Task ViewTasks()
    {
        await Shell.Current.GoToAsync("//tasks");
    }

    [RelayCommand]
    private async Task NewCharacter()
    {
        await Shell.Current.GoToAsync("charactereditor");
    }

    [RelayCommand]
    private async Task ViewCharacters()
    {
        await Shell.Current.GoToAsync("//characters");
    }

    [RelayCommand]
    private async Task NewEvent()
    {
        await Shell.Current.GoToAsync("eventeditor");
    }

    [RelayCommand]
    private async Task ViewEvents()
    {
        await Shell.Current.GoToAsync("//events");
    }

    [RelayCommand]
    private async Task NewVariable()
    {
        await Shell.Current.GoToAsync("variableeditor");
    }

    [RelayCommand]
    private async Task ViewVariables()
    {
        await Shell.Current.GoToAsync("//variables");
    }

    [RelayCommand]
    private async Task ViewGroups()
    {
        await Shell.Current.GoToAsync("//groups");
    }

    [RelayCommand]
    private async Task TestAdventure()
    {
        if (_adventureService.CurrentAdventure == null)
        {
            await Shell.Current.DisplayAlert("Error", "No adventure to test", "OK");
            return;
        }

        await Shell.Current.DisplayAlert("Test Adventure",
            "Testing feature will launch the ADRIFT Runner with your adventure.\n\n" +
            "Note: Runner implementation is in progress.",
            "OK");
    }

    [RelayCommand]
    private async Task ViewMap()
    {
        await Shell.Current.GoToAsync("//map");
    }

    [RelayCommand]
    private async Task EditAdventureSettings()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
        {
            await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
            return;
        }

        // TODO: Show adventure settings dialog
        await Shell.Current.DisplayAlert("Adventure Settings",
            $"Title: {adventure.Title}\n" +
            $"Author: {adventure.Author}\n" +
            $"Version: {adventure.Version}\n\n" +
            "Settings editor coming soon",
            "OK");
    }

    [RelayCommand]
    private async Task ShowStatistics()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null)
        {
            await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
            return;
        }

        var totalConnections = adventure.Locations.Values.Sum(l => l.Directions.Count);
        var isolatedLocations = adventure.Locations.Values.Count(l => l.Directions.Count == 0);
        var staticObjects = adventure.Objects.Values.Count(o => o.IsStatic);
        var dynamicObjects = adventure.Objects.Values.Count(o => !o.IsStatic);

        var stats = $"Adventure Statistics:\n\n" +
                   $"Locations: {LocationCount} ({isolatedLocations} isolated)\n" +
                   $"Objects: {ObjectCount} ({staticObjects} static, {dynamicObjects} dynamic)\n" +
                   $"Tasks: {TaskCount}\n" +
                   $"Characters: {CharacterCount}\n" +
                   $"Events: {EventCount}\n" +
                   $"Variables: {VariableCount}\n" +
                   $"Groups: {GroupCount}\n" +
                   $"Connections: {totalConnections}\n\n" +
                   $"Last Modified: {LastModified}";

        await Shell.Current.DisplayAlert("Statistics", stats, "OK");
    }

    #region File Operations

    [RelayCommand]
    private async Task NewAdventure()
    {
        // Check for unsaved changes
        if (HasUnsavedChanges)
        {
            var result = await Shell.Current.DisplayAlert(
                "Unsaved Changes",
                "You have unsaved changes. Do you want to continue and lose these changes?",
                "Continue", "Cancel");

            if (!result)
                return;
        }

        try
        {
            var adventure = await _adventureService.CreateNewAdventureAsync();
            CurrentFilePath = null;
            HasUnsavedChanges = false;
            LoadAdventure();
            StatusMessage = "New adventure created";
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to create new adventure: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task OpenAdventure()
    {
        // Check for unsaved changes
        if (HasUnsavedChanges)
        {
            var result = await Shell.Current.DisplayAlert(
                "Unsaved Changes",
                "You have unsaved changes. Do you want to save before opening another adventure?",
                "Save", "Don't Save");

            if (result)
            {
                await SaveAdventure();
            }
        }

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
                StatusMessage = "Loading adventure...";
                var adventure = await _adventureService.LoadAdventureAsync(result.FullPath);

                if (adventure != null)
                {
                    CurrentFilePath = result.FullPath;
                    HasUnsavedChanges = false;
                    LoadAdventure();
                    StatusMessage = $"Loaded {Path.GetFileName(result.FullPath)}";
                }
            }
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to load adventure";
            await Shell.Current.DisplayAlert("Error", $"Failed to open adventure: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task SaveAdventure()
    {
        if (string.IsNullOrEmpty(CurrentFilePath))
        {
            await SaveAdventureAs();
            return;
        }

        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure to save", "OK");
                return;
            }

            StatusMessage = "Saving adventure...";
            await _adventureService.SaveAdventureAsync(adventure, CurrentFilePath);
            HasUnsavedChanges = false;
            LoadAdventure(); // Refresh to update LastModified
            StatusMessage = $"Saved {Path.GetFileName(CurrentFilePath)}";
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to save adventure";
            await Shell.Current.DisplayAlert("Error", $"Failed to save adventure: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task SaveAdventureAs()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure to save", "OK");
                return;
            }

            // MAUI doesn't have a built-in SaveFilePicker yet, so we use a prompt
            var fileName = await Shell.Current.DisplayPromptAsync(
                "Save Adventure",
                "Enter file name (without extension):",
                initialValue: string.IsNullOrWhiteSpace(adventure.Title) ? "MyAdventure" : adventure.Title,
                placeholder: "MyAdventure");

            if (string.IsNullOrWhiteSpace(fileName))
                return;

            var formatChoice = await Shell.Current.DisplayActionSheet(
                "Select Format",
                "Cancel",
                null,
                "Compressed TAF (.taf)",
                "XML (.xml)");

            if (formatChoice == null || formatChoice == "Cancel")
                return;

            var extension = formatChoice.Contains("TAF") ? ".taf" : ".xml";

            // Use app's documents directory
            var documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            var adriftPath = Path.Combine(documentsPath, "ADRIFT Adventures");

            if (!Directory.Exists(adriftPath))
                Directory.CreateDirectory(adriftPath);

            var filePath = Path.Combine(adriftPath, fileName + extension);

            // Check if file exists
            if (File.Exists(filePath))
            {
                var overwrite = await Shell.Current.DisplayAlert(
                    "File Exists",
                    $"The file '{fileName}{extension}' already exists. Do you want to overwrite it?",
                    "Yes", "No");

                if (!overwrite)
                {
                    await SaveAdventureAs(); // Try again
                    return;
                }
            }

            StatusMessage = "Saving adventure...";
            await _adventureService.SaveAdventureAsync(adventure, filePath);
            CurrentFilePath = filePath;
            HasUnsavedChanges = false;
            LoadAdventure(); // Refresh to update LastModified
            StatusMessage = $"Saved as {Path.GetFileName(filePath)}";

            await Shell.Current.DisplayAlert("Success", $"Adventure saved to:\n{filePath}", "OK");
        }
        catch (Exception ex)
        {
            StatusMessage = "Failed to save adventure";
            await Shell.Current.DisplayAlert("Error", $"Failed to save adventure: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task CloseAdventure()
    {
        // Check for unsaved changes
        if (HasUnsavedChanges)
        {
            var result = await Shell.Current.DisplayAlert(
                "Unsaved Changes",
                "You have unsaved changes. Do you want to save before closing?",
                "Save", "Don't Save");

            if (result)
            {
                await SaveAdventure();
            }
        }

        // Reset to empty state
        await _adventureService.CreateNewAdventureAsync();
        CurrentFilePath = null;
        HasUnsavedChanges = false;
        LoadAdventure();
        StatusMessage = "Adventure closed";
    }

    #endregion
}
