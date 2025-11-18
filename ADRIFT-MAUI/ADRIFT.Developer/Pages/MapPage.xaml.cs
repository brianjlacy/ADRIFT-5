using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
        BindingContext = new MapViewModel();
    }
}
