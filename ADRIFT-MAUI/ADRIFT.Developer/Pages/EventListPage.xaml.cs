using ADRIFT.Developer.ViewModels;
namespace ADRIFT.Developer.Views;
public partial class EventListPage : ContentPage
{
    public EventListPage(EventListViewModel viewModel) { InitializeComponent(); BindingContext = viewModel; }
    protected override void OnAppearing() { base.OnAppearing(); if (BindingContext is EventListViewModel vm) _ = vm.LoadEventsAsync(); }
}
