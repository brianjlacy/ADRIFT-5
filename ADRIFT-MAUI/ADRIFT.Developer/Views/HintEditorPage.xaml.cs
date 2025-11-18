using ADRIFT.ViewModels;

namespace ADRIFT.Views;

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
