using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Views;

public partial class VariableEditorPage : ContentPage
{
    public VariableEditorPage(VariableEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is VariableEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }
}
