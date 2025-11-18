using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class LocationListPage : ContentPage
{
    public LocationListPage(LocationListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // Refresh list when page appears
        if (BindingContext is LocationListViewModel vm)
        {
            _ = vm.LoadLocationsAsync();
        }
    }
}
