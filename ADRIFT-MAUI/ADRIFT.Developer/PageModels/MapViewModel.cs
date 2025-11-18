using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ADRIFT.Developer.ViewModels;

public partial class MapViewModel : ObservableObject
{
    [ObservableProperty]
    private double zoomLevel = 1.0;

    [RelayCommand]
    private void ZoomIn()
    {
        ZoomLevel = Math.Min(ZoomLevel + 0.25, 3.0);
    }

    [RelayCommand]
    private void ZoomOut()
    {
        ZoomLevel = Math.Max(ZoomLevel - 0.25, 0.25);
    }

    [RelayCommand]
    private void ResetView()
    {
        ZoomLevel = 1.0;
    }
}
