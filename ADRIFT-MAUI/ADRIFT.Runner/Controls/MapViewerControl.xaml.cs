using ADRIFT.Core.Models;
using ADRIFT.Core.Engine;

namespace ADRIFT.Runner.Controls;

public partial class MapViewerControl : ContentView
{
    private Adventure? _adventure;
    private GameState? _gameState;
    private Dictionary<string, PointF> _tapTargets = new();

    public event EventHandler<string>? LocationSelected;

    public MapViewerControl()
    {
        InitializeComponent();

        // Add tap gesture recognizer for navigation
        var tapGesture = new TapGestureRecognizer();
        tapGesture.Tapped += OnMapTapped;
        MapDisplay.GestureRecognizers.Add(tapGesture);
    }

    public void SetAdventure(Adventure adventure, GameState gameState)
    {
        _adventure = adventure;
        _gameState = gameState;

        MapDisplay.SetAdventure(adventure, gameState);
        UpdateLocationLabel();
        CalculateTapTargets();
    }

    public void UpdatePlayerLocation()
    {
        MapDisplay.UpdatePlayerLocation();
        UpdateLocationLabel();
        CalculateTapTargets();
    }

    public void UpdatePlayerLocation(string locationKey)
    {
        if (_gameState != null)
        {
            _gameState.CurrentLocationKey = locationKey;
            UpdatePlayerLocation();
        }
    }

    private void UpdateLocationLabel()
    {
        if (_gameState == null || _adventure == null)
        {
            LocationLabel.Text = "No adventure loaded";
            return;
        }

        if (_adventure.Locations.TryGetValue(_gameState.CurrentLocationKey, out var location))
        {
            var exitCount = location.Directions.Count;
            LocationLabel.Text = $"You are at: {location.ShortDescription} ({exitCount} exit{(exitCount != 1 ? "s" : "")})";
        }
        else
        {
            LocationLabel.Text = "Unknown location";
        }
    }

    private void CalculateTapTargets()
    {
        _tapTargets.Clear();

        if (_gameState == null || _adventure == null)
            return;

        // Only allow navigation to connected locations
        if (_adventure.Locations.TryGetValue(_gameState.CurrentLocationKey, out var currentLocation))
        {
            foreach (var direction in currentLocation.Directions)
            {
                _tapTargets[direction.DestinationKey] = new PointF(0, 0);
            }
        }
    }

    private void OnMapTapped(object? sender, TappedEventArgs e)
    {
        if (_gameState == null || _adventure == null || e.GetPosition(MapDisplay) is not Point tapPoint)
            return;

        // Find which location was tapped
        var tappedLocation = FindLocationAtPoint(tapPoint);

        if (!string.IsNullOrEmpty(tappedLocation))
        {
            // Check if this location is connected to current location
            if (_tapTargets.ContainsKey(tappedLocation))
            {
                // Raise event for navigation
                LocationSelected?.Invoke(this, tappedLocation);
            }
            else if (tappedLocation == _gameState.CurrentLocationKey)
            {
                // Tapped current location - show info
                LocationLabel.Text = $"You are already at: {_adventure.Locations[tappedLocation].ShortDescription}";
            }
            else
            {
                // Tapped disconnected location
                LocationLabel.Text = "You cannot go there from here";

                // Restore label after 2 seconds
                Dispatcher.DispatchDelayed(TimeSpan.FromSeconds(2), UpdateLocationLabel);
            }
        }
    }

    private string? FindLocationAtPoint(Point tapPoint)
    {
        // Use MapView's hit testing to find the tapped location
        return MapDisplay.GetLocationAtPoint(tapPoint);
    }

    private void OnRefreshClicked(object? sender, EventArgs e)
    {
        if (_adventure != null && _gameState != null)
        {
            SetAdventure(_adventure, _gameState);
        }
    }

    public void Clear()
    {
        _adventure = null;
        _gameState = null;
        _tapTargets.Clear();
        LocationLabel.Text = "Tap a connected location to navigate";
        MapDisplay.Invalidate();
    }
}
