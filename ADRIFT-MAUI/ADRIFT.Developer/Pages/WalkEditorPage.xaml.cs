using ADRIFT.Developer.PageModels;

namespace ADRIFT.Developer.Pages;

public partial class WalkEditorPage : ContentPage
{
    public WalkEditorPage(WalkEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
