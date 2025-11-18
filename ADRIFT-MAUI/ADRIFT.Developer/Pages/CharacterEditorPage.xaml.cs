using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class CharacterEditorPage : ContentPage
{
    public CharacterEditorPage(CharacterEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is CharacterEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }
}
