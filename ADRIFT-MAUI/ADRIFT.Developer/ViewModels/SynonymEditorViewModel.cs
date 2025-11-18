using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

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
        if (!string.IsNullOrEmpty(SynonymKey))
        {
            // Edit mode - load existing synonym set
            IsEditMode = true;
            PageTitle = "Edit Synonym";

            // TODO: Load synonym from adventure service
            // For now, using sample data
            OriginalWord = "examine";
            Synonyms.Add(new SynonymItemViewModel { Text = "look at" });
            Synonyms.Add(new SynonymItemViewModel { Text = "inspect" });
            Synonyms.Add(new SynonymItemViewModel { Text = "check" });
        }
        else
        {
            // New synonym mode
            IsEditMode = false;
            PageTitle = "New Synonym";
            OriginalWord = "";
            Synonyms.Clear();
        }

        await Task.CompletedTask;
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
        // TODO: Save synonym to adventure service
        // For now, just a placeholder
        await Task.Delay(100);

        // If this was a new synonym, generate a key
        if (string.IsNullOrEmpty(SynonymKey))
        {
            SynonymKey = "syn_" + Guid.NewGuid().ToString("N")[..8];
        }
    }
}

public partial class SynonymItemViewModel : ObservableObject
{
    [ObservableProperty]
    private string text = "";
}
