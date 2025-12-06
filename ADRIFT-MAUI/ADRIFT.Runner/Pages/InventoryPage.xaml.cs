using ADRIFT.Core.Models;
using ADRIFT.Core.Engine;

namespace ADRIFT.Runner.Pages;

public partial class InventoryPage : ContentPage
{
    private Adventure? _adventure;
    private GameState? _gameState;

    public event EventHandler<string>? ItemActionRequested;

    public InventoryPage()
    {
        InitializeComponent();
    }

    public void SetAdventure(Adventure adventure, GameState gameState)
    {
        _adventure = adventure;
        _gameState = gameState;
        RefreshInventory();
    }

    public void RefreshInventory()
    {
        ItemsContainer.Children.Clear();

        if (_adventure == null || _gameState == null)
        {
            EmptyFrame.IsVisible = true;
            ItemCountLabel.Text = "No game loaded";
            return;
        }

        // Get items carried by the player
        var carriedItems = GetCarriedItems();

        if (carriedItems.Count == 0)
        {
            EmptyFrame.IsVisible = true;
            ItemCountLabel.Text = "You are carrying 0 items";
            return;
        }

        EmptyFrame.IsVisible = false;
        ItemCountLabel.Text = $"You are carrying {carriedItems.Count} item{(carriedItems.Count != 1 ? "s" : "")}";

        // Display each item
        foreach (var item in carriedItems)
        {
            var itemFrame = CreateItemCard(item);
            ItemsContainer.Children.Add(itemFrame);
        }
    }

    private List<AdventureObject> GetCarriedItems()
    {
        if (_adventure == null || _gameState == null)
            return new List<AdventureObject>();

        var items = new List<AdventureObject>();

        foreach (var obj in _adventure.Objects.Values)
        {
            // Check if item is carried by the player
            if (obj.Location?.LocationKey == "Player" || obj.HeldBy == "Player")
            {
                items.Add(obj);
            }
        }

        return items.OrderBy(o => o.GetFullName()).ToList();
    }

    private Frame CreateItemCard(AdventureObject item)
    {
        var frame = new Frame
        {
            BorderColor = Color.FromArgb("#2196F3"),
            BackgroundColor = Color.FromArgb("#E3F2FD"),
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

        // Item name
        var nameLabel = new Label
        {
            Text = item.GetFullName(),
            FontSize = 16,
            FontAttributes = FontAttributes.Bold,
            TextColor = Colors.Black
        };
        Grid.SetRow(nameLabel, 0);
        Grid.SetColumn(nameLabel, 0);
        grid.Children.Add(nameLabel);

        // Item description (short)
        var descLabel = new Label
        {
            Text = GetShortDescription(item),
            FontSize = 12,
            TextColor = Color.FromArgb("#555555"),
            LineBreakMode = LineBreakMode.TailTruncation,
            MaxLines = 2
        };
        Grid.SetRow(descLabel, 1);
        Grid.SetColumn(descLabel, 0);
        Grid.SetColumnSpan(descLabel, 2);
        grid.Children.Add(descLabel);

        // Action buttons
        var actionStack = new HorizontalStackLayout
        {
            Spacing = 5,
            VerticalOptions = LayoutOptions.Start
        };

        var examineButton = new Button
        {
            Text = "👁️",
            FontSize = 14,
            BackgroundColor = Color.FromArgb("#2196F3"),
            TextColor = Colors.White,
            WidthRequest = 35,
            HeightRequest = 35,
            CornerRadius = 17,
            Padding = 0,
            ToolTipProperties = { Text = "Examine" }
        };
        examineButton.Clicked += (s, e) => OnExamineItem(item);
        actionStack.Children.Add(examineButton);

        var useButton = new Button
        {
            Text = "⚡",
            FontSize = 14,
            BackgroundColor = Color.FromArgb("#4CAF50"),
            TextColor = Colors.White,
            WidthRequest = 35,
            HeightRequest = 35,
            CornerRadius = 17,
            Padding = 0,
            ToolTipProperties = { Text = "Use" }
        };
        useButton.Clicked += (s, e) => OnUseItem(item);
        actionStack.Children.Add(useButton);

        var dropButton = new Button
        {
            Text = "🗑️",
            FontSize = 14,
            BackgroundColor = Color.FromArgb("#F44336"),
            TextColor = Colors.White,
            WidthRequest = 35,
            HeightRequest = 35,
            CornerRadius = 17,
            Padding = 0,
            ToolTipProperties = { Text = "Drop" }
        };
        dropButton.Clicked += (s, e) => OnDropItem(item);
        actionStack.Children.Add(dropButton);

        Grid.SetRow(actionStack, 0);
        Grid.SetColumn(actionStack, 1);
        grid.Children.Add(actionStack);

        frame.Content = grid;
        return frame;
    }

    private string GetShortDescription(AdventureObject item)
    {
        if (!string.IsNullOrWhiteSpace(item.Description))
        {
            var desc = item.Description.Trim();
            if (desc.Length > 100)
                return desc.Substring(0, 97) + "...";
            return desc;
        }

        return "No description available";
    }

    private async void OnExamineItem(AdventureObject item)
    {
        StatusLabel.Text = $"Examining: {item.GetFullName()}";

        var description = !string.IsNullOrWhiteSpace(item.Description)
            ? item.Description
            : "You see nothing special about it.";

        await DisplayAlert("Examine Item", $"{item.GetFullName()}\n\n{description}", "OK");

        StatusLabel.Text = "Select an item to view actions";
    }

    private async void OnUseItem(AdventureObject item)
    {
        StatusLabel.Text = $"Using: {item.GetFullName()}";

        // Raise event for game engine to process
        ItemActionRequested?.Invoke(this, $"use {item.GetFullName()}");

        await DisplayAlert("Use Item", $"You attempt to use {item.GetFullName()}.", "OK");

        StatusLabel.Text = "Select an item to view actions";
    }

    private async void OnDropItem(AdventureObject item)
    {
        var result = await DisplayAlert(
            "Drop Item",
            $"Are you sure you want to drop {item.GetFullName()}?",
            "Yes",
            "No");

        if (result)
        {
            StatusLabel.Text = $"Dropping: {item.GetFullName()}";

            // Raise event for game engine to process
            ItemActionRequested?.Invoke(this, $"drop {item.GetFullName()}");

            // Refresh inventory after dropping
            RefreshInventory();

            StatusLabel.Text = $"Dropped {item.GetFullName()}";
            await Task.Delay(2000);
            StatusLabel.Text = "Select an item to view actions";
        }
    }

    private void OnRefreshClicked(object? sender, EventArgs e)
    {
        RefreshInventory();
        StatusLabel.Text = "Inventory refreshed";
    }

    private async void OnCloseClicked(object? sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("..");
    }
}
