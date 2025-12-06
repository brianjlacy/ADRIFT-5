using ADRIFT.Core.Models;
using ADRIFT.Core.Engine;
using System.Text.Json;

namespace ADRIFT.Runner.Pages;

public partial class SaveLoadPage : ContentPage
{
    private Adventure? _adventure;
    private GameState? _gameState;
    private string _saveDirectory = "";

    public event EventHandler<string>? GameSaved;
    public event EventHandler<string>? GameLoaded;

    public SaveLoadPage()
    {
        InitializeComponent();
        InitializeSaveDirectory();
    }

    private void InitializeSaveDirectory()
    {
        _saveDirectory = Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            "ADRIFT",
            "Saves");

        if (!Directory.Exists(_saveDirectory))
        {
            Directory.CreateDirectory(_saveDirectory);
        }
    }

    public void SetAdventure(Adventure adventure, GameState gameState)
    {
        _adventure = adventure;
        _gameState = gameState;
        RefreshSaveFiles();
    }

    public void RefreshSaveFiles()
    {
        SaveFilesContainer.Children.Clear();

        if (!Directory.Exists(_saveDirectory))
        {
            EmptyFrame.IsVisible = true;
            return;
        }

        var saveFiles = Directory.GetFiles(_saveDirectory, "*.sav")
            .OrderByDescending(f => File.GetLastWriteTime(f))
            .ToList();

        if (saveFiles.Count == 0)
        {
            EmptyFrame.IsVisible = true;
            return;
        }

        EmptyFrame.IsVisible = false;

        foreach (var saveFile in saveFiles)
        {
            var saveCard = CreateSaveFileCard(saveFile);
            SaveFilesContainer.Children.Add(saveCard);
        }
    }

    private Frame CreateSaveFileCard(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);
        var lastModified = File.GetLastWriteTime(filePath);
        var fileSize = new FileInfo(filePath).Length;

        var frame = new Frame
        {
            BorderColor = Color.FromArgb("#4CAF50"),
            BackgroundColor = Color.FromArgb("#E8F5E9"),
            Padding = 12,
            CornerRadius = 8,
            Margin = new Thickness(0, 0, 0, 5)
        };

        var grid = new Grid
        {
            ColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) },
                new ColumnDefinition { Width = GridLength.Auto }
            },
            RowDefinitions = new RowDefinitionCollection
            {
                new RowDefinition { Height = GridLength.Auto },
                new RowDefinition { Height = GridLength.Auto }
            }
        };

        // File name
        var nameLabel = new Label
        {
            Text = fileName,
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black
        };
        Grid.SetRow(nameLabel, 0);
        Grid.SetColumn(nameLabel, 0);
        grid.Children.Add(nameLabel);

        // File info
        var infoLabel = new Label
        {
            Text = $"Modified: {lastModified:g} • Size: {FormatFileSize(fileSize)}",
            FontSize = 11,
            TextColor = Color.FromArgb("#555555")
        };
        Grid.SetRow(infoLabel, 1);
        Grid.SetColumn(infoLabel, 0);
        Grid.SetColumnSpan(infoLabel, 2);
        grid.Children.Add(infoLabel);

        // Action buttons
        var actionStack = new HorizontalStackLayout
        {
            Spacing = 5,
            VerticalOptions = LayoutOptions.Start
        };

        var loadButton = new Button
        {
            Text = "📂",
            FontSize = 16,
            BackgroundColor = Color.FromArgb("#4CAF50"),
            TextColor = Colors.White,
            WidthRequest = 40,
            HeightRequest = 40,
            CornerRadius = 20,
            Padding = 0,
            ToolTipProperties = { Text = "Load" }
        };
        loadButton.Clicked += (s, e) => OnLoadSaveFile(filePath);
        actionStack.Children.Add(loadButton);

        var deleteButton = new Button
        {
            Text = "🗑️",
            FontSize = 16,
            BackgroundColor = Color.FromArgb("#F44336"),
            TextColor = Colors.White,
            WidthRequest = 40,
            HeightRequest = 40,
            CornerRadius = 20,
            Padding = 0,
            ToolTipProperties = { Text = "Delete" }
        };
        deleteButton.Clicked += (s, e) => OnDeleteSaveFile(filePath);
        actionStack.Children.Add(deleteButton);

        Grid.SetRow(actionStack, 0);
        Grid.SetColumn(actionStack, 1);
        grid.Children.Add(actionStack);

        frame.Content = grid;
        return frame;
    }

    private string FormatFileSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }

    private async void OnQuickSaveClicked(object? sender, EventArgs e)
    {
        if (_gameState == null || _adventure == null)
        {
            await DisplayAlert("Error", "No game is currently loaded", "OK");
            return;
        }

        var fileName = "QuickSave.sav";
        await SaveGame(fileName);
    }

    private async void OnQuickLoadClicked(object? sender, EventArgs e)
    {
        var filePath = Path.Combine(_saveDirectory, "QuickSave.sav");

        if (!File.Exists(filePath))
        {
            await DisplayAlert("Error", "No quick save found", "OK");
            return;
        }

        await LoadGame(filePath);
    }

    private async void OnSaveAsClicked(object? sender, EventArgs e)
    {
        if (_gameState == null || _adventure == null)
        {
            await DisplayAlert("Error", "No game is currently loaded", "OK");
            return;
        }

        var fileName = await DisplayPromptAsync(
            "Save Game",
            "Enter a name for your save file:",
            "Save",
            "Cancel",
            placeholder: "My Save",
            maxLength: 50);

        if (!string.IsNullOrWhiteSpace(fileName))
        {
            // Sanitize filename
            foreach (var c in Path.GetInvalidFileNameChars())
            {
                fileName = fileName.Replace(c, '_');
            }

            fileName = fileName + ".sav";
            await SaveGame(fileName);
        }
    }

    private async Task SaveGame(string fileName)
    {
        try
        {
            if (_gameState == null || _adventure == null)
            {
                await DisplayAlert("Error", "No game state to save", "OK");
                return;
            }

            var filePath = Path.Combine(_saveDirectory, fileName);

            // Create save data
            var saveData = new
            {
                GameState = _gameState,
                AdventureKey = _adventure.Key,
                SaveDate = DateTime.Now,
                Version = "1.0"
            };

            var json = JsonSerializer.Serialize(saveData, new JsonSerializerOptions
            {
                WriteIndented = true
            });

            await File.WriteAllTextAsync(filePath, json);

            StatusLabel.Text = $"Game saved: {Path.GetFileNameWithoutExtension(fileName)}";
            GameSaved?.Invoke(this, filePath);

            RefreshSaveFiles();

            await DisplayAlert("Success", $"Game saved successfully to {fileName}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to save game: {ex.Message}", "OK");
            StatusLabel.Text = "Save failed";
        }
    }

    private async void OnLoadSaveFile(string filePath)
    {
        await LoadGame(filePath);
    }

    private async Task LoadGame(string filePath)
    {
        try
        {
            if (!File.Exists(filePath))
            {
                await DisplayAlert("Error", "Save file not found", "OK");
                return;
            }

            var json = await File.ReadAllTextAsync(filePath);
            var saveData = JsonSerializer.Deserialize<JsonElement>(json);

            // In a real implementation, we would deserialize the GameState
            // and notify the game engine to restore the state
            var fileName = Path.GetFileNameWithoutExtension(filePath);
            StatusLabel.Text = $"Loaded: {fileName}";
            GameLoaded?.Invoke(this, filePath);

            await DisplayAlert("Success", $"Game loaded from {fileName}", "OK");
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"Failed to load game: {ex.Message}", "OK");
            StatusLabel.Text = "Load failed";
        }
    }

    private async void OnDeleteSaveFile(string filePath)
    {
        var fileName = Path.GetFileNameWithoutExtension(filePath);

        var result = await DisplayAlert(
            "Delete Save",
            $"Are you sure you want to delete '{fileName}'? This cannot be undone.",
            "Delete",
            "Cancel");

        if (result)
        {
            try
            {
                File.Delete(filePath);
                StatusLabel.Text = $"Deleted: {fileName}";
                RefreshSaveFiles();
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to delete save file: {ex.Message}", "OK");
            }
        }
    }

    private void OnRefreshClicked(object? sender, EventArgs e)
    {
        RefreshSaveFiles();
        StatusLabel.Text = "Save list refreshed";
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
