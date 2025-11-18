using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

[QueryProperty(nameof(SynonymKey), "key")]
public partial class SynonymEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public SynonymEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        Synonyms = new ObservableCollection<SynonymItemViewModel>();
        Synonyms.CollectionChanged += (s, e) => UpdateSynonymCounts();
    }

    [ObservableProperty]
    private string synonymKey = "";

    [ObservableProperty]
    private string pageTitle = "New Synonym";

    [ObservableProperty]
    private string originalWord = "";

    [ObservableProperty]
    private string newSynonym = "";

    [ObservableProperty]
    private ObservableCollection<SynonymItemViewModel> synonyms;

    [ObservableProperty]
    private int synonymCount = 0;

    [ObservableProperty]
    private bool hasSynonyms = false;

    [ObservableProperty]
    private bool hasNoSynonyms = true;

    [ObservableProperty]
    private bool isEditMode = false;

    private void UpdateSynonymCounts()
    {
        SynonymCount = Synonyms.Count;
        HasSynonyms = SynonymCount > 0;
        HasNoSynonyms = SynonymCount == 0;
    }

    public async Task InitializeAsync()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null) return;

            if (!string.IsNullOrEmpty(SynonymKey) && adventure.Synonyms.TryGetValue(SynonymKey, out var existingSynonym))
            {
                // Edit mode - load existing synonym set
                IsEditMode = true;
                PageTitle = "Edit Synonym";

                OriginalWord = existingSynonym.OriginalWord;

                // Load synonyms
                Synonyms.Clear();
                foreach (var syn in existingSynonym.SynonymWords)
                {
                    Synonyms.Add(new SynonymItemViewModel { Text = syn });
                }
            }
            else
            {
                // New synonym mode
                SynonymKey = "syn_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                IsEditMode = false;
                PageTitle = "New Synonym";
                OriginalWord = "";
                Synonyms.Clear();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to initialize: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private void AddSynonym()
    {
        if (string.IsNullOrWhiteSpace(NewSynonym))
            return;

        // Check for duplicates
        var normalizedNew = NewSynonym.Trim().ToLowerInvariant();
        if (Synonyms.Any(s => s.Text.ToLowerInvariant() == normalizedNew))
        {
            Shell.Current.DisplayAlert("Duplicate", "This synonym already exists.", "OK");
            return;
        }

        // Check if synonym matches original word
        if (!string.IsNullOrWhiteSpace(OriginalWord) &&
            normalizedNew == OriginalWord.Trim().ToLowerInvariant())
        {
            Shell.Current.DisplayAlert("Invalid", "A synonym cannot be the same as the original word.", "OK");
            return;
        }

        Synonyms.Add(new SynonymItemViewModel { Text = NewSynonym.Trim() });
        NewSynonym = ""; // Clear the entry field
    }

    [RelayCommand]
    private void DeleteSynonym(SynonymItemViewModel synonym)
    {
        if (synonym != null)
        {
            Synonyms.Remove(synonym);
        }
    }

    [RelayCommand]
    private async Task Cancel()
    {
        await Shell.Current.GoToAsync("..");
    }

    [RelayCommand]
    private async Task Apply()
    {
        if (!ValidateInput())
            return;

        // Save changes but stay on page
        await SaveSynonym();

        await Shell.Current.DisplayAlert("Success", "Synonym changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveSynonym();
        await Shell.Current.GoToAsync("..");
    }

    private bool ValidateInput()
    {
        // Validate original word
        if (string.IsNullOrWhiteSpace(OriginalWord))
        {
            Shell.Current.DisplayAlert("Validation Error", "Original word is required.", "OK");
            return false;
        }

        // At least one synonym should be added
        if (Synonyms.Count == 0)
        {
            Shell.Current.DisplayAlert("Validation Error", "Please add at least one synonym.", "OK");
            return false;
        }

        return true;
    }

    private async Task SaveSynonym()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
                return;
            }

            if (string.IsNullOrEmpty(SynonymKey))
            {
                SynonymKey = "syn_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            }

            var synonym = new Synonym
            {
                Key = SynonymKey,
                OriginalWord = OriginalWord.Trim(),
                LastModified = DateTime.Now
            };

            // Save synonym words
            synonym.SynonymWords.Clear();
            foreach (var syn in Synonyms)
            {
                if (!string.IsNullOrWhiteSpace(syn.Text))
                {
                    synonym.SynonymWords.Add(syn.Text.Trim());
                }
            }

            // Add or update in adventure
            if (adventure.Synonyms.ContainsKey(SynonymKey))
            {
                adventure.Synonyms[SynonymKey] = synonym;
            }
            else
            {
                adventure.Synonyms.Add(SynonymKey, synonym);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to save synonym: {ex.Message}", "OK");
        }
    }
}

public partial class SynonymItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string text = "";
}
