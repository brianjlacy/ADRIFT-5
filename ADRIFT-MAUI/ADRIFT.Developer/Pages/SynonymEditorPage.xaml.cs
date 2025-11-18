using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Views;

public partial class SynonymEditorPage : ContentPage
{
    public SynonymEditorPage(SynonymEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SynonymEditorViewModel vm)
        {
            _ = vm.InitializeAsync();
        }
    }

    private void OnNewSynonymCompleted(object sender, EventArgs e)
    {
        // When user presses Enter in the Entry, add the synonym
        if (BindingContext is SynonymEditorViewModel vm)
        {
            if (vm.AddSynonymCommand.CanExecute(null))
            {
                vm.AddSynonymCommand.Execute(null);
            }
        }
    }
}
