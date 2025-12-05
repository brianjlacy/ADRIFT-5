using ADRIFT.Core.Models;
using ADRIFT.Core.Engine;
using Microsoft.Maui.Graphics;

namespace ADRIFT.Runner.Controls;

/// <summary>
/// Custom control for rendering the adventure map
/// </summary>
public class MapView : GraphicsView
{
    private Adventure? _adventure;
    private GameState? _state;
    private MapDrawable _drawable;

    public MapView()
    {
        _drawable = new MapDrawable();
        Drawable = _drawable;
        HeightRequest = 300;
    }

    public void SetAdventure(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _state = state;
        _drawable.SetAdventure(adventure, state);
        Invalidate();
    }

    public void UpdatePlayerLocation()
    {
        if (_state != null)
        {
            _drawable.UpdatePlayerLocation(_state.CurrentLocationKey);
            Invalidate();
        }
    }
}

/// <summary>
/// Drawable implementation for rendering the map
/// </summary>
public class MapDrawable : IDrawable
{
    private Adventure? _adventure;
    private string _currentLocationKey = "";
    private Dictionary<string, PointF> _locationPositions = new();

    public void SetAdventure(Adventure adventure, GameState state)
    {
        _adventure = adventure;
        _currentLocationKey = state.CurrentLocationKey;
        CalculatePositions();
    }

    public void UpdatePlayerLocation(string locationKey)
    {
        _currentLocationKey = locationKey;
    }

    private void CalculatePositions()
    {
        if (_adventure == null || _adventure.Locations.Count == 0)
            return;

        _locationPositions.Clear();

        // Check if adventure has a predefined map
        if (_adventure.Map?.Pages?.Count > 0)
        {
            // Use map coordinates
            var firstPage = _adventure.Map.Pages[0];
            foreach (var node in firstPage.Nodes)
            {
                _locationPositions[node.LocationKey] = new PointF(
                    (float)node.X,
                    (float)node.Y
                );
            }
        }
        else
        {
            // Auto-layout: simple grid based on connections
            AutoLayoutLocations();
        }
    }

    private void AutoLayoutLocations()
    {
        if (_adventure == null)
            return;

        var locations = _adventure.Locations.Values.Where(l => !l.IsLibrary).ToList();
        if (locations.Count == 0)
            return;

        // Start with first location at center
        var startLocation = locations[0];
        _locationPositions[startLocation.Key] = new PointF(400, 200);

        var visited = new HashSet<string> { startLocation.Key };
        var queue = new Queue<string>();
        queue.Enqueue(startLocation.Key);

        // BFS to position connected locations
        while (queue.Count > 0)
        {
            var currentKey = queue.Dequeue();
            if (!_adventure.Locations.TryGetValue(currentKey, out var currentLoc))
                continue;

            var currentPos = _locationPositions[currentKey];

            // Position connected locations based on direction
            foreach (var direction in currentLoc.Directions)
            {
                if (visited.Contains(direction.DestinationKey))
                    continue;

                var offset = GetDirectionOffset(direction.DirectionName);
                var newPos = new PointF(
                    currentPos.X + offset.X * 100,
                    currentPos.Y + offset.Y * 80
                );

                _locationPositions[direction.DestinationKey] = newPos;
                visited.Add(direction.DestinationKey);
                queue.Enqueue(direction.DestinationKey);
            }
        }
    }

    private PointF GetDirectionOffset(string direction)
    {
        return direction.ToLower() switch
        {
            "north" or "n" => new PointF(0, -1),
            "south" or "s" => new PointF(0, 1),
            "east" or "e" => new PointF(1, 0),
            "west" or "w" => new PointF(-1, 0),
            "northeast" or "ne" => new PointF(0.7f, -0.7f),
            "northwest" or "nw" => new PointF(-0.7f, -0.7f),
            "southeast" or "se" => new PointF(0.7f, 0.7f),
            "southwest" or "sw" => new PointF(-0.7f, 0.7f),
            "up" or "u" => new PointF(0, -0.5f),
            "down" or "d" => new PointF(0, 0.5f),
            _ => new PointF(0, 0)
        };
    }

    public void Draw(ICanvas canvas, RectF dirtyRect)
    {
        if (_adventure == null || _locationPositions.Count == 0)
        {
            DrawEmptyMessage(canvas, dirtyRect);
            return;
        }

        canvas.FillColor = Color.FromArgb("#1a1a1a");
        canvas.FillRectangle(dirtyRect);

        // Calculate bounds for centering
        var minX = _locationPositions.Values.Min(p => p.X);
        var maxX = _locationPositions.Values.Max(p => p.X);
        var minY = _locationPositions.Values.Min(p => p.Y);
        var maxY = _locationPositions.Values.Max(p => p.Y);

        var mapWidth = maxX - minX;
        var mapHeight = maxY - minY;

        // Calculate scale to fit
        var scaleX = dirtyRect.Width / (mapWidth + 200);
        var scaleY = dirtyRect.Height / (mapHeight + 200);
        var scale = Math.Min(scaleX, scaleY);
        scale = Math.Min(scale, 1.5f); // Cap maximum scale

        // Draw connections first (so they appear behind rooms)
        DrawConnections(canvas, scale, minX, minY, dirtyRect);

        // Draw locations
        DrawLocations(canvas, scale, minX, minY, dirtyRect);
    }

    private void DrawConnections(ICanvas canvas, float scale, float minX, float minY, RectF dirtyRect)
    {
        if (_adventure == null)
            return;

        canvas.StrokeColor = Color.FromArgb("#404040");
        canvas.StrokeSize = 2;

        foreach (var kvp in _locationPositions)
        {
            if (!_adventure.Locations.TryGetValue(kvp.Key, out var location))
                continue;

            var fromPos = TransformPosition(kvp.Value, scale, minX, minY, dirtyRect);

            foreach (var direction in location.Directions)
            {
                if (_locationPositions.TryGetValue(direction.DestinationKey, out var destPos))
                {
                    var toPos = TransformPosition(destPos, scale, minX, minY, dirtyRect);
                    canvas.DrawLine(fromPos, toPos);
                }
            }
        }
    }

    private void DrawLocations(ICanvas canvas, float scale, float minX, float minY, RectF dirtyRect)
    {
        if (_adventure == null)
            return;

        const float roomSize = 60;

        foreach (var kvp in _locationPositions)
        {
            if (!_adventure.Locations.TryGetValue(kvp.Key, out var location))
                continue;

            var pos = TransformPosition(kvp.Value, scale, minX, minY, dirtyRect);
            var isCurrent = kvp.Key == _currentLocationKey;

            // Draw room box
            canvas.FillColor = isCurrent ? Color.FromArgb("#4CAF50") : Color.FromArgb("#2196F3");
            canvas.StrokeColor = isCurrent ? Color.FromArgb("#81C784") : Color.FromArgb("#64B5F6");
            canvas.StrokeSize = isCurrent ? 3 : 2;

            var rect = new RectF(pos.X - roomSize / 2, pos.Y - roomSize / 2, roomSize, roomSize);
            canvas.FillRoundedRectangle(rect, 8);
            canvas.DrawRoundedRectangle(rect, 8);

            // Draw room name
            canvas.FontColor = Colors.White;
            canvas.FontSize = 12;
            var name = location.ShortDescription;
            if (name.Length > 15)
                name = name.Substring(0, 12) + "...";

            canvas.DrawString(name, pos.X, pos.Y, HorizontalAlignment.Center);

            // Draw player marker if current location
            if (isCurrent)
            {
                canvas.FillColor = Colors.Yellow;
                canvas.FillCircle(pos.X, pos.Y - roomSize / 2 - 15, 8);
            }
        }
    }

    private PointF TransformPosition(PointF pos, float scale, float minX, float minY, RectF dirtyRect)
    {
        var x = (pos.X - minX) * scale + dirtyRect.Width / 2 - ((_locationPositions.Values.Max(p => p.X) - minX) * scale) / 2;
        var y = (pos.Y - minY) * scale + dirtyRect.Height / 2 - ((_locationPositions.Values.Max(p => p.Y) - minY) * scale) / 2;
        return new PointF(x, y);
    }

    private void DrawEmptyMessage(ICanvas canvas, RectF dirtyRect)
    {
        canvas.FillColor = Color.FromArgb("#1a1a1a");
        canvas.FillRectangle(dirtyRect);

        canvas.FontColor = Colors.Gray;
        canvas.FontSize = 14;
        canvas.DrawString("No map available", dirtyRect.Center.X, dirtyRect.Center.Y, HorizontalAlignment.Center);
    }
}
