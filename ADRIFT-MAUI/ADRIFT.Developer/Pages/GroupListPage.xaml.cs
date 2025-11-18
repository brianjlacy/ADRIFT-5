using ADRIFT.Developer.ViewModels;
namespace ADRIFT.Developer.Views;
public partial class GroupListPage : ContentPage
{
    public GroupListPage(GroupListViewModel viewModel) { InitializeComponent(); BindingContext = viewModel; }
    protected override void OnAppearing() { base.OnAppearing(); if (BindingContext is GroupListViewModel vm) _ = vm.LoadGroupsAsync(); }
}
