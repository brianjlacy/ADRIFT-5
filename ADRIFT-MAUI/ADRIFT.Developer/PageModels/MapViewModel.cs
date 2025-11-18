using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class MapViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public MapViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        LocationNodes = new ObservableCollection<LocationNode>();
        ConnectionLines = new ObservableCollection<ConnectionLine>();
    }

    [ObservableProperty]
    private double zoomLevel = 1.0;

    [ObservableProperty]
    private double panX = 0;

    [ObservableProperty]
    private double panY = 0;

    [ObservableProperty]
    private ObservableCollection<LocationNode> locationNodes;

    [ObservableProperty]
    private ObservableCollection<ConnectionLine> connectionLines;

    [ObservableProperty]
    private LocationNode? selectedLocation;

    [ObservableProperty]
    private string statusMessage = "Map ready";

    [ObservableProperty]
    private int locationCount;

    [ObservableProperty]
    private int connectionCount;

    [ObservableProperty]
    private bool showHiddenLocations = true;

    [ObservableProperty]
    private bool autoLayout = true;

    public async Task LoadMapAsync()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                StatusMessage = "No adventure loaded";
                return;
            }

            LocationNodes.Clear();
            ConnectionLines.Clear();

            var locations = adventure.Locations.Values
                .Where(l => ShowHiddenLocations || !l.HideOnMap)
                .ToList();

            if (AutoLayout)
            {
                // Simple grid layout
                var columns = (int)Math.Ceiling(Math.Sqrt(locations.Count));
                var spacing = 150.0;

                for (int i = 0; i < locations.Count; i++)
                {
                    var loc = locations[i];
                    var row = i / columns;
                    var col = i % columns;

                    var node = new LocationNode
                    {
                        Key = loc.Key,
                        Name = loc.ShortDescription,
                        X = col * spacing,
                        Y = row * spacing,
                        IsHidden = loc.HideOnMap,
                        ExitCount = loc.Directions.Count
                    };
                    LocationNodes.Add(node);
                }
            }
            else
            {
                // Use stored positions or random placement
                var random = new Random();
                foreach (var loc in locations)
                {
                    var node = new LocationNode
                    {
                        Key = loc.Key,
                        Name = loc.ShortDescription,
                        X = random.Next(50, 800),
                        Y = random.Next(50, 600),
                        IsHidden = loc.HideOnMap,
                        ExitCount = loc.Directions.Count
                    };
                    LocationNodes.Add(node);
                }
            }

            // Create connection lines
            foreach (var loc in locations)
            {
                var sourceNode = LocationNodes.FirstOrDefault(n => n.Key == loc.Key);
                if (sourceNode == null) continue;

                foreach (var dir in loc.Directions)
                {
                    var targetNode = LocationNodes.FirstOrDefault(n => n.Key == dir.DestinationKey);
                    if (targetNode != null)
                    {
                        var line = new ConnectionLine
                        {
                            SourceX = sourceNode.X + 40, // Center of node (assuming 80px width)
                            SourceY = sourceNode.Y + 20, // Center of node (assuming 40px height)
                            TargetX = targetNode.X + 40,
                            TargetY = targetNode.Y + 20,
                            Direction = dir.DirectionName,
                            HasRestriction = !string.IsNullOrEmpty(dir.RestrictionDescription)
                        };
                        ConnectionLines.Add(line);
                    }
                }
            }

            LocationCount = LocationNodes.Count;
            ConnectionCount = ConnectionLines.Count;
            StatusMessage = $"Map loaded: {LocationCount} locations, {ConnectionCount} connections";
        }
        catch (Exception ex)
        {
            StatusMessage = $"Error loading map: {ex.Message}";
            await Shell.Current.DisplayAlert("Error", $"Failed to load map: {ex.Message}", "OK");
        }

        await Task.CompletedTask;
    }

    [RelayCommand]
    private void ZoomIn()
    {
        ZoomLevel = Math.Min(ZoomLevel + 0.25, 3.0);
        StatusMessage = $"Zoom: {ZoomLevel:P0}";
    }

    [RelayCommand]
    private void ZoomOut()
    {
        ZoomLevel = Math.Max(ZoomLevel - 0.25, 0.25);
        StatusMessage = $"Zoom: {ZoomLevel:P0}";
    }

    [RelayCommand]
    private void ResetView()
    {
        ZoomLevel = 1.0;
        PanX = 0;
        PanY = 0;
        StatusMessage = "View reset";
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadMapAsync();
    }

    [RelayCommand]
    private async Task ToggleHiddenLocations()
    {
        ShowHiddenLocations = !ShowHiddenLocations;
        await LoadMapAsync();
    }

    [RelayCommand]
    private async Task ToggleAutoLayout()
    {
        AutoLayout = !AutoLayout;
        await LoadMapAsync();
    }

    [RelayCommand]
    private async Task SelectLocation(LocationNode node)
    {
        SelectedLocation = node;
        StatusMessage = $"Selected: {node.Name}";
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task EditLocation(LocationNode node)
    {
        if (node != null)
        {
            await Shell.Current.GoToAsync($"locationeditor?key={node.Key}");
        }
    }

    [RelayCommand]
    private async Task AddLocation()
    {
        await Shell.Current.GoToAsync("locationeditor");
    }

    [RelayCommand]
    private async Task CenterOnLocation(LocationNode node)
    {
        if (node != null)
        {
            // Center the view on this location
            PanX = -node.X + 400; // Assuming viewport is 800px wide
            PanY = -node.Y + 300; // Assuming viewport is 600px tall
            StatusMessage = $"Centered on: {node.Name}";
        }
        await Task.CompletedTask;
    }

    [RelayCommand]
    private async Task ExportMap()
    {
        // TODO: Implement map export to image
        await Shell.Current.DisplayAlert("Export", "Map export feature coming soon", "OK");
    }

    [RelayCommand]
    private async Task ShowStatistics()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null) return;

        var stats = $"Map Statistics:\n\n" +
                   $"Total Locations: {adventure.Locations.Count}\n" +
                   $"Visible Locations: {adventure.Locations.Values.Count(l => !l.HideOnMap)}\n" +
                   $"Hidden Locations: {adventure.Locations.Values.Count(l => l.HideOnMap)}\n" +
                   $"Total Connections: {adventure.Locations.Values.Sum(l => l.Directions.Count)}\n" +
                   $"Isolated Locations: {adventure.Locations.Values.Count(l => l.Directions.Count == 0)}";

        await Shell.Current.DisplayAlert("Map Statistics", stats, "OK");
    }
}

public partial class LocationNode : ObservableObject
{
    [ObservableProperty]
    private string key = "";

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private double x;

    [ObservableProperty]
    private double y;

    [ObservableProperty]
    private bool isHidden;

    [ObservableProperty]
    private bool isSelected;

    [ObservableProperty]
    private int exitCount;

    [ObservableProperty]
    private Color nodeColor = Colors.LightBlue;

    partial void OnIsHiddenChanged(bool value)
    {
        NodeColor = value ? Colors.Gray : Colors.LightBlue;
    }

    partial void OnIsSelectedChanged(bool value)
    {
        NodeColor = value ? Colors.Gold : (IsHidden ? Colors.Gray : Colors.LightBlue);
    }
}

public partial class ConnectionLine : ObservableObject
{
    [ObservableProperty]
    private double sourceX;

    [ObservableProperty]
    private double sourceY;

    [ObservableProperty]
    private double targetX;

    [ObservableProperty]
    private double targetY;

    [ObservableProperty]
    private string direction = "";

    [ObservableProperty]
    private bool hasRestriction;

    [ObservableProperty]
    private Color lineColor = Colors.Black;

    partial void OnHasRestrictionChanged(bool value)
    {
        LineColor = value ? Colors.Red : Colors.Black;
    }

    public string PathData
    {
        get
        {
            return $"M {SourceX},{SourceY} L {TargetX},{TargetY}";
        }
    }
}
