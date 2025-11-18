using ADRIFT.Developer.ViewModels;
namespace ADRIFT.Developer.Views;
public partial class CharacterListPage : ContentPage
{
    public CharacterListPage(CharacterListViewModel viewModel) { InitializeComponent(); BindingContext = viewModel; }
    protected override void OnAppearing() { base.OnAppearing(); if (BindingContext is CharacterListViewModel vm) _ = vm.LoadCharactersAsync(); }
}
