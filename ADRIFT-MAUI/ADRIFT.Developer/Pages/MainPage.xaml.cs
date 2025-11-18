using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class MainPage : ContentPage
{
    public MainPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
