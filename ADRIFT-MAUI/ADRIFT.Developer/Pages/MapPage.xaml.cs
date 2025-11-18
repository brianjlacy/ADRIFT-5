using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Views;

public partial class MapPage : ContentPage
{
    public MapPage()
    {
        InitializeComponent();
        BindingContext = new MapViewModel();
    }
}
