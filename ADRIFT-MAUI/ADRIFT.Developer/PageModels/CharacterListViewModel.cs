using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class CharacterListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public CharacterListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredCharacters = new ObservableCollection<CharacterItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<CharacterItemViewModel> filteredCharacters;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int characterCount;

    [ObservableProperty]
    private bool isRefreshing;

    partial void OnSearchTextChanged(string value)
    {
        FilterCharacters();
    }

    public async Task LoadCharactersAsync()
    {
        try
        {
            IsRefreshing = true;
            var characters = await _adventureService.GetCharactersAsync();

            FilteredCharacters.Clear();
            foreach (var character in characters)
            {
                FilteredCharacters.Add(new CharacterItemViewModel(character));
            }

            CharacterCount = FilteredCharacters.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async void FilterCharacters()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadCharactersAsync();
            return;
        }

        try
        {
            var allCharacters = await _adventureService.GetCharactersAsync();
            var filtered = allCharacters.Where(c =>
                c.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                c.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredCharacters.Clear();
            foreach (var character in filtered)
            {
                FilteredCharacters.Add(new CharacterItemViewModel(character));
            }

            CharacterCount = FilteredCharacters.Count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter characters: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddCharacter() => await Shell.Current.GoToAsync("charactereditor");

    [RelayCommand]
    private async Task EditCharacter(CharacterItemViewModel character) =>
        await Shell.Current.GoToAsync($"charactereditor?key={character.Key}");

    [RelayCommand]
    private async Task DeleteCharacter(CharacterItemViewModel character)
    {
        var result = await Shell.Current.DisplayAlert(
            "Delete Character",
            $"Are you sure you want to delete '{character.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Characters.ContainsKey(character.Key))
                {
                    adventure.Characters.Remove(character.Key);
                    FilteredCharacters.Remove(character);
                    CharacterCount = FilteredCharacters.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete character: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Refresh() => await LoadCharactersAsync();

    [RelayCommand]
    private async Task Sort()
    {
        var action = await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Name (A-Z)", "Name (Z-A)", "Type", "Location");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Name (A-Z)" => FilteredCharacters.OrderBy(c => c.Name).ToList(),
                "Name (Z-A)" => FilteredCharacters.OrderByDescending(c => c.Name).ToList(),
                "Type" => FilteredCharacters.OrderBy(c => c.CharacterType).ToList(),
                "Location" => FilteredCharacters.OrderBy(c => c.Location).ToList(),
                _ => FilteredCharacters.ToList()
            };

            FilteredCharacters.Clear();
            foreach (var item in sorted)
            {
                FilteredCharacters.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Characters", "NPCs", "Companions", "Enemies", "Merchants");

        if (action != null && action != "Cancel")
        {
            try
            {
                var allCharacters = await _adventureService.GetCharactersAsync();
                IEnumerable<Character> filtered = action switch
                {
                    "NPCs" => allCharacters.Where(c => c.Type == CharacterType.NPC),
                    "Companions" => allCharacters.Where(c => c.Type == CharacterType.Companion),
                    "Enemies" => allCharacters.Where(c => c.Type == CharacterType.Enemy),
                    "Merchants" => allCharacters.Where(c => c.Type == CharacterType.Merchant),
                    _ => allCharacters
                };

                FilteredCharacters.Clear();
                foreach (var character in filtered)
                {
                    FilteredCharacters.Add(new CharacterItemViewModel(character));
                }

                CharacterCount = FilteredCharacters.Count;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter characters: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterCharacters();
        await Task.CompletedTask;
    }
}

public partial class CharacterItemViewModel : ObservableObject
{
    public CharacterItemViewModel()
    {
        Key = "char" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Name = "Sample Character";
        Description = "A sample character";
        Location = "Unknown";
        CharacterType = "NPC";
    }

    public CharacterItemViewModel(Character character) : this()
    {
        Key = character.Key;
        Name = character.FullName;
        Description = character.Description;
        Location = character.InitialLocationKey ?? "Unknown";
        CharacterType = character.Type.ToString();
    }

    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private string location = "";
    [ObservableProperty] private string characterType = "";
}
