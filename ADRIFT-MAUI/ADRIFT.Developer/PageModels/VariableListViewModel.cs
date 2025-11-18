using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class VariableListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public VariableListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredVariables = new ObservableCollection<VariableItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<VariableItemViewModel> filteredVariables;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int variableCount;

    [ObservableProperty]
    private bool isRefreshing;

    partial void OnSearchTextChanged(string value)
    {
        FilterVariables();
    }

    public async Task LoadVariablesAsync()
    {
        try
        {
            IsRefreshing = true;
            var variables = await _adventureService.GetVariablesAsync();

            FilteredVariables.Clear();
            foreach (var variable in variables)
            {
                FilteredVariables.Add(new VariableItemViewModel(variable));
            }

            VariableCount = FilteredVariables.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    private async void FilterVariables()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadVariablesAsync();
            return;
        }

        try
        {
            var allVariables = await _adventureService.GetVariablesAsync();
            var filtered = allVariables.Where(v =>
                v.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                v.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                v.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredVariables.Clear();
            foreach (var variable in filtered)
            {
                FilteredVariables.Add(new VariableItemViewModel(variable));
            }

            VariableCount = FilteredVariables.Count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter variables: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddVariable() => await Shell.Current.GoToAsync("variableeditor");

    [RelayCommand]
    private async Task EditVariable(VariableItemViewModel variable) =>
        await Shell.Current.GoToAsync($"variableeditor?key={variable.Key}");

    [RelayCommand]
    private async Task DeleteVariable(VariableItemViewModel variable)
    {
        var result = await Shell.Current.DisplayAlert(
            "Delete Variable",
            $"Are you sure you want to delete '{variable.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Variables.ContainsKey(variable.Key))
                {
                    adventure.Variables.Remove(variable.Key);
                    FilteredVariables.Remove(variable);
                    VariableCount = FilteredVariables.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete variable: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Refresh() => await LoadVariablesAsync();

    [RelayCommand]
    private async Task Sort()
    {
        var action = await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Name (A-Z)", "Name (Z-A)", "Type", "Value");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Name (A-Z)" => FilteredVariables.OrderBy(v => v.Name).ToList(),
                "Name (Z-A)" => FilteredVariables.OrderByDescending(v => v.Name).ToList(),
                "Type" => FilteredVariables.OrderBy(v => v.VariableType).ToList(),
                "Value" => FilteredVariables.OrderBy(v => v.CurrentValue).ToList(),
                _ => FilteredVariables.ToList()
            };

            FilteredVariables.Clear();
            foreach (var item in sorted)
            {
                FilteredVariables.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Variables", "Integer", "Text", "Boolean");

        if (action != null && action != "Cancel")
        {
            try
            {
                var allVariables = await _adventureService.GetVariablesAsync();
                IEnumerable<Variable> filtered = action switch
                {
                    "Integer" => allVariables.Where(v => v.Type == VariableType.Integer),
                    "Text" => allVariables.Where(v => v.Type == VariableType.Text),
                    "Boolean" => allVariables.Where(v => v.Type == VariableType.Boolean),
                    _ => allVariables
                };

                FilteredVariables.Clear();
                foreach (var variable in filtered)
                {
                    FilteredVariables.Add(new VariableItemViewModel(variable));
                }

                VariableCount = FilteredVariables.Count;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter variables: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterVariables();
        await Task.CompletedTask;
    }
}

public partial class VariableItemViewModel : ObservableObject
{
    public VariableItemViewModel()
    {
        Key = "var" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Name = "Sample Variable";
        VariableType = "Integer";
        CurrentValue = "0";
        InitialValue = "0";
    }

    public VariableItemViewModel(Variable variable) : this()
    {
        Key = variable.Key;
        Name = variable.Name;
        VariableType = variable.Type.ToString();
        CurrentValue = variable.CurrentValue;
        InitialValue = variable.InitialValue;
    }

    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string variableType = "";
    [ObservableProperty] private string currentValue = "";
    [ObservableProperty] private string initialValue = "";
}
