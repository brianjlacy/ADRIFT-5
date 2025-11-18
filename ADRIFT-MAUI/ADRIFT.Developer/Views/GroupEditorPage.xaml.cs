using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class GroupEditorPage : ContentPage
{
    public GroupEditorPage(GroupEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is GroupEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }
}
