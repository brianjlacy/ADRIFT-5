using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class ObjectListPage : ContentPage
{
    public ObjectListPage(ObjectListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is ObjectListViewModel vm)
        {
            _ = vm.LoadObjectsAsync();
        }
    }
}
