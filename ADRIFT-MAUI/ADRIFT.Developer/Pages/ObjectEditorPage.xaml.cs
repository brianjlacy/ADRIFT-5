using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class ObjectEditorPage : ContentPage
{
    public ObjectEditorPage(ObjectEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ObjectEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }
}
