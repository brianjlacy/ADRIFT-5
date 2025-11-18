using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Views;

public partial class HintEditorPage : ContentPage
{
    public HintEditorPage(HintEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is HintEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }
}
