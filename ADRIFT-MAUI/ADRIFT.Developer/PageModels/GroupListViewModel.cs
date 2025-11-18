using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
namespace ADRIFT.Developer.ViewModels;
public partial class GroupListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    public GroupListViewModel(IAdventureService adventureService) { _adventureService = adventureService; FilteredGroups = new(); }
    [ObservableProperty] private ObservableCollection<GroupItemViewModel> filteredGroups;
    [ObservableProperty] private string searchText = "";
    public async Task LoadGroupsAsync() { await Task.CompletedTask; FilteredGroups.Clear(); }
    [RelayCommand] private async Task AddGroup() => await Shell.Current.GoToAsync("groupeditor");
    [RelayCommand] private async Task EditGroup(GroupItemViewModel g) => await Shell.Current.GoToAsync($"groupeditor?key={g.Key}");
}
public partial class GroupItemViewModel : ObservableObject
{
    public GroupItemViewModel() { Key = "grp" + Guid.NewGuid().ToString("N")[..8]; Name = "Group"; MemberCount = 0; GroupType = "Objects"; }
    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private int memberCount;
    [ObservableProperty] private string groupType = "";
}
