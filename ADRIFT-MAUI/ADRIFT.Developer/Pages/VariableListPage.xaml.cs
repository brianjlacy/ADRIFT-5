using ADRIFT.ViewModels;
namespace ADRIFT.Views;
public partial class VariableListPage : ContentPage
{
    public VariableListPage(VariableListViewModel viewModel) { InitializeComponent(); BindingContext = viewModel; }
    protected override void OnAppearing() { base.OnAppearing(); if (BindingContext is VariableListViewModel vm) _ = vm.LoadVariablesAsync(); }
}
