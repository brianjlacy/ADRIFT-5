using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ADRIFT.ViewModels;
public partial class VariableListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    public VariableListViewModel(IAdventureService adventureService) { _adventureService = adventureService; FilteredVariables = new(); }
    [ObservableProperty] private ObservableCollection<VariableItemViewModel> filteredVariables;
    [ObservableProperty] private string searchText = "";
    public async Task LoadVariablesAsync() { var vars = await _adventureService.GetVariablesAsync(); FilteredVariables.Clear(); foreach (var v in vars) FilteredVariables.Add(new(v)); }
    [RelayCommand] private async Task AddVariable() => await Shell.Current.GoToAsync("variableeditor");
    [RelayCommand] private async Task EditVariable(VariableItemViewModel v) => await Shell.Current.GoToAsync($"variableeditor?key={v.Key}");
}
public partial class VariableItemViewModel : ObservableObject
{
    public VariableItemViewModel(object v) { Key = "var" + Guid.NewGuid().ToString("N")[..8]; Name = "Variable"; VariableType = "Integer"; CurrentValue = "0"; }
    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string variableType = "";
    [ObservableProperty] private string currentValue = "";
}
