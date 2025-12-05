using ADRIFT.Developer.ViewModels;
using ADRIFT.Developer.Controls;
using System.ComponentModel;

namespace ADRIFT.Developer.Pages;

public partial class MapPage : ContentPage
{
    private readonly MapViewModel _viewModel;
    private readonly MapDrawable _mapDrawable;
    private double _startScale = 1;

    public MapPage(MapViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;

        // Create and assign the map drawable
        _mapDrawable = new MapDrawable { ViewModel = _viewModel };
        MapGraphicsView.Drawable = _mapDrawable;

        // Subscribe to ViewModel property changes to refresh the map
        _viewModel.PropertyChanged += OnViewModelPropertyChanged;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();

        // Load the map data
        await _viewModel.LoadMapAsync();

        // Refresh the view
        MapGraphicsView.Invalidate();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();
        _viewModel.PropertyChanged -= OnViewModelPropertyChanged;
    }

    private void OnViewModelPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Refresh the map when relevant properties change
        if (e.PropertyName == nameof(MapViewModel.ZoomLevel) ||
            e.PropertyName == nameof(MapViewModel.PanX) ||
            e.PropertyName == nameof(MapViewModel.PanY) ||
            e.PropertyName == nameof(MapViewModel.LocationNodes) ||
            e.PropertyName == nameof(MapViewModel.ConnectionLines) ||
            e.PropertyName == nameof(MapViewModel.SelectedLocation))
        {
            MapGraphicsView.Invalidate();
        }
    }

    private void OnPanUpdated(object? sender, PanUpdatedEventArgs e)
    {
        switch (e.StatusType)
        {
            case GestureStatus.Running:
                // Update pan position
                _viewModel.PanX += e.TotalX;
                _viewModel.PanY += e.TotalY;
                break;
        }
    }

    private void OnPinchUpdated(object? sender, PinchGestureUpdatedEventArgs e)
    {
        switch (e.Status)
        {
            case GestureStatus.Started:
                _startScale = _viewModel.ZoomLevel;
                break;

            case GestureStatus.Running:
                // Calculate new zoom level
                var newZoom = _startScale * e.Scale;
                _viewModel.ZoomLevel = Math.Clamp(newZoom, 0.25, 3.0);
                break;
        }
    }

    private void OnMapTapped(object? sender, TappedEventArgs e)
    {
        // Get tap position
        var point = e.GetPosition(MapGraphicsView);
        if (point == null) return;

        // Convert screen coordinates to map coordinates (accounting for zoom and pan)
        var mapX = (point.Value.X - _viewModel.PanX) / _viewModel.ZoomLevel;
        var mapY = (point.Value.Y - _viewModel.PanY) / _viewModel.ZoomLevel;

        // Check if a location was tapped
        const double boxWidth = 80;
        const double boxHeight = 40;

        foreach (var node in _viewModel.LocationNodes)
        {
            if (mapX >= node.X && mapX <= node.X + boxWidth &&
                mapY >= node.Y && mapY <= node.Y + boxHeight)
            {
                // Location tapped - select it
                if (_viewModel.SelectedLocation != null)
                {
                    _viewModel.SelectedLocation.IsSelected = false;
                }

                node.IsSelected = true;
                _viewModel.SelectLocationCommand.Execute(node);

                MapGraphicsView.Invalidate();
                return;
            }
        }

        // No location tapped - deselect
        if (_viewModel.SelectedLocation != null)
        {
            _viewModel.SelectedLocation.IsSelected = false;
            _viewModel.SelectedLocation = null;
            MapGraphicsView.Invalidate();
        }
    }
}
