using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Views;

public partial class LocationEditorPage : ContentPage
{
    public LocationEditorPage(LocationEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
