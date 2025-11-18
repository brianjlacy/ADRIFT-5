using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

public partial class GroupListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public GroupListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredGroups = new ObservableCollection<GroupItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<GroupItemViewModel> filteredGroups;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int groupCount;

    [ObservableProperty]
    private bool isRefreshing;

    partial void OnSearchTextChanged(string value)
    {
        FilterGroups();
    }

    public async Task LoadGroupsAsync()
    {
        try
        {
            IsRefreshing = true;
            var adventure = _adventureService.CurrentAdventure;
            if (adventure != null)
            {
                FilteredGroups.Clear();
                foreach (var group in adventure.Groups.Values)
                {
                    FilteredGroups.Add(new GroupItemViewModel(group));
                }

                GroupCount = FilteredGroups.Count;
            }
        }
        finally
        {
            IsRefreshing = false;
        }

        await Task.CompletedTask;
    }

    private async void FilterGroups()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadGroupsAsync();
            return;
        }

        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure != null)
            {
                var filtered = adventure.Groups.Values.Where(g =>
                    g.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    g.Description.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                    g.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                    .ToList();

                FilteredGroups.Clear();
                foreach (var group in filtered)
                {
                    FilteredGroups.Add(new GroupItemViewModel(group));
                }

                GroupCount = FilteredGroups.Count;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter groups: {ex.Message}", "OK");
        }
    }

    [RelayCommand]
    private async Task AddGroup() => await Shell.Current.GoToAsync("groupeditor");

    [RelayCommand]
    private async Task EditGroup(GroupItemViewModel group) =>
        await Shell.Current.GoToAsync($"groupeditor?key={group.Key}");

    [RelayCommand]
    private async Task DeleteGroup(GroupItemViewModel group)
    {
        var result = await Shell.Current.DisplayAlert(
            "Delete Group",
            $"Are you sure you want to delete '{group.Name}'?",
            "Delete", "Cancel");

        if (result)
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Groups.ContainsKey(group.Key))
                {
                    adventure.Groups.Remove(group.Key);
                    FilteredGroups.Remove(group);
                    GroupCount = FilteredGroups.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete group: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Refresh() => await LoadGroupsAsync();

    [RelayCommand]
    private async Task Sort()
    {
        var action = await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Name (A-Z)", "Name (Z-A)", "Type", "Member Count");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Name (A-Z)" => FilteredGroups.OrderBy(g => g.Name).ToList(),
                "Name (Z-A)" => FilteredGroups.OrderByDescending(g => g.Name).ToList(),
                "Type" => FilteredGroups.OrderBy(g => g.GroupType).ToList(),
                "Member Count" => FilteredGroups.OrderByDescending(g => g.MemberCount).ToList(),
                _ => FilteredGroups.ToList()
            };

            FilteredGroups.Clear();
            foreach (var item in sorted)
            {
                FilteredGroups.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Groups", "Characters", "Objects", "Locations", "Tasks", "Events");

        if (action != null && action != "Cancel")
        {
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null)
                {
                    IEnumerable<Group> filtered = action switch
                    {
                        "Characters" => adventure.Groups.Values.Where(g => g.Type == GroupType.Characters),
                        "Objects" => adventure.Groups.Values.Where(g => g.Type == GroupType.Objects),
                        "Locations" => adventure.Groups.Values.Where(g => g.Type == GroupType.Locations),
                        "Tasks" => adventure.Groups.Values.Where(g => g.Type == GroupType.Tasks),
                        "Events" => adventure.Groups.Values.Where(g => g.Type == GroupType.Events),
                        _ => adventure.Groups.Values
                    };

                    FilteredGroups.Clear();
                    foreach (var group in filtered)
                    {
                        FilteredGroups.Add(new GroupItemViewModel(group));
                    }

                    GroupCount = FilteredGroups.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter groups: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterGroups();
        await Task.CompletedTask;
    }
}

public partial class GroupItemViewModel : ObservableObject
{
    public GroupItemViewModel()
    {
        Key = "grp" + Guid.NewGuid().ToString("N").Substring(0, 8);
        Name = "Sample Group";
        Description = "A sample group";
        MemberCount = 0;
        GroupType = "Objects";
    }

    public GroupItemViewModel(Group group) : this()
    {
        Key = group.Key;
        Name = group.Name;
        Description = group.Description;
        MemberCount = group.MemberKeys.Count;
        GroupType = group.Type.ToString();
    }

    [ObservableProperty] private string key = "";
    [ObservableProperty] private string name = "";
    [ObservableProperty] private string description = "";
    [ObservableProperty] private int memberCount;
    [ObservableProperty] private string groupType = "";
}
