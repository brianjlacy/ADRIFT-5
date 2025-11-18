using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class LocationEditorPage : ContentPage
{
    public LocationEditorPage(LocationEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
