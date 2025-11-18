using ADRIFT.Developer.ViewModels;
namespace ADRIFT.Developer.Pages;
namespace ADRIFT.Developer.Pages;
public partial class VariableListPage : ContentPage
{
    public VariableListPage(VariableListViewModel viewModel) { InitializeComponent(); BindingContext = viewModel; }
    protected override void OnAppearing() { base.OnAppearing(); if (BindingContext is VariableListViewModel vm) _ = vm.LoadVariablesAsync(); }
}
