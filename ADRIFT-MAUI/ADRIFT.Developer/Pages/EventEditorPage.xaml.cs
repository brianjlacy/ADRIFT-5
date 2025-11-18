using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;
namespace ADRIFT.Developer.Pages;

public partial class EventEditorPage : ContentPage
{
    public EventEditorPage(EventEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is EventEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }
}
