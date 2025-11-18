using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
        BindingContext = new MapViewModel();
    }
}
