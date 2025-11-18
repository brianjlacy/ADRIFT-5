using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ADRIFT.ViewModels;
public partial class CharacterListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    public CharacterListViewModel(IAdventureService adventureService) { _adventureService = adventureService; FilteredCharacters = new(); }
    [ObservableProperty] private ObservableCollection<CharacterItemViewModel> filteredCharacters;
    [ObservableProperty] private string searchText = "";
    [ObservableProperty] private bool isRefreshing;
    public async Task LoadCharactersAsync() { try { IsRefreshing = true; var chars = await _adventureService.GetCharactersAsync(); FilteredCharacters.Clear(); foreach (var c in chars) FilteredCharacters.Add(new(c)); } finally { IsRefreshing = false; } }
    [RelayCommand] private async Task AddCharacter() => await Shell.Current.GoToAsync("charactereditor");
    [RelayCommand] private async Task EditCharacter(CharacterItemViewModel c) => await Shell.Current.GoToAsync($"charactereditor?key={c.Key}");
    [RelayCommand] private async Task Refresh() => await LoadCharactersAsync();
}
public partial class CharacterItemViewModel : ObservableObject
{
    public CharacterItemViewModel(object c) { Key = "char" + Guid.NewGuid().ToString("N")[..8]; Name = "Character"; Description = "Description"; Location = "Location"; }
    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private string location = "";
}
