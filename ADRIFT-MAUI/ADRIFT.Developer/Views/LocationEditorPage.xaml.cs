using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class LocationEditorPage : ContentPage
{
    public LocationEditorPage(LocationEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
