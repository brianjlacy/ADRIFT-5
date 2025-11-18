using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

namespace ADRIFT.Developer.ViewModels;

[QueryProperty(nameof(GroupKey), "key")]
public partial class GroupEditorViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;
    private ObservableCollection<GroupMemberItemViewModel> _allMembers;

    public GroupEditorViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        GroupTypes = new ObservableCollection<string> { "Characters", "Objects", "Locations", "Tasks", "Events" };
        SelectedGroupType = "Characters";
        _allMembers = new ObservableCollection<GroupMemberItemViewModel>();
        FilteredMembers = new ObservableCollection<GroupMemberItemViewModel>();
    }

    [ObservableProperty]
    private string groupKey = "";

    [ObservableProperty]
    private string pageTitle = "New Group";

    [ObservableProperty]
    private string groupName = "";

    [ObservableProperty]
    private ObservableCollection<string> groupTypes;

    [ObservableProperty]
    private string selectedGroupType = "Characters";

    [ObservableProperty]
    private ObservableCollection<GroupMemberItemViewModel> filteredMembers;

    [ObservableProperty]
    private string memberSearchText = "";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private bool isEditMode = false;

    [ObservableProperty]
    private bool hasMembers = false;

    [ObservableProperty]
    private bool hasNoMembers = true;

    [ObservableProperty]
    private string memberCountText = "0 members";

    [ObservableProperty]
    private string selectedMemberCountText = "(0 selected)";

    [ObservableProperty]
    private string memberSelectionHint = "Select items to include in this group";

    partial void OnSelectedGroupTypeChanged(string value)
    {
        // Reload available members when group type changes
        LoadAvailableMembers();
        UpdateMemberSelectionHint();
    }

    partial void OnMemberSearchTextChanged(string value)
    {
        FilterMembers();
    }

    private void UpdateMemberSelectionHint()
    {
        MemberSelectionHint = SelectedGroupType switch
        {
            "Characters" => "Select characters to include in this group",
            "Objects" => "Select objects to include in this group",
            "Locations" => "Select locations to include in this group",
            "Tasks" => "Select tasks to include in this group",
            "Events" => "Select events to include in this group",
            _ => "Select items to include in this group"
        };
    }

    public async Task InitializeAsync()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null) return;

            if (!string.IsNullOrEmpty(GroupKey) && adventure.Groups.TryGetValue(GroupKey, out var existingGroup))
            {
                // Edit mode - load existing group
                IsEditMode = true;
                PageTitle = "Edit Group";

                GroupName = existingGroup.Name;
                SelectedGroupType = existingGroup.Type.ToString();
                Description = existingGroup.Description;

                // Load available members first
                LoadAvailableMembers();

                // Mark selected members
                foreach (var memberKey in existingGroup.MemberKeys)
                {
                    var member = _allMembers.FirstOrDefault(m => m.Key == memberKey);
                    if (member != null)
                    {
                        member.IsSelected = true;
                    }
                }
            }
            else
            {
                // New group mode
                GroupKey = "grp_" + Guid.NewGuid().ToString("N").Substring(0, 8);
                IsEditMode = false;
                PageTitle = "New Group";
                GroupName = "";
                SelectedGroupType = "Characters";
                Description = "";

                LoadAvailableMembers();
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to initialize: {ex.Message}", "OK");
        }
    }

    private void LoadAvailableMembers()
    {
        var adventure = _adventureService.CurrentAdventure;
        if (adventure == null) return;

        _allMembers.Clear();

        switch (SelectedGroupType)
        {
            case "Characters":
                foreach (var character in adventure.Characters.Values)
                {
                    _allMembers.Add(new GroupMemberItemViewModel(this)
                    {
                        Key = character.Key,
                        Name = character.FullName,
                        Description = character.Description
                    });
                }
                break;

            case "Objects":
                foreach (var obj in adventure.Objects.Values)
                {
                    _allMembers.Add(new GroupMemberItemViewModel(this)
                    {
                        Key = obj.Key,
                        Name = obj.FullName,
                        Description = obj.ShortDescription
                    });
                }
                break;

            case "Locations":
                foreach (var location in adventure.Locations.Values)
                {
                    _allMembers.Add(new GroupMemberItemViewModel(this)
                    {
                        Key = location.Key,
                        Name = location.ShortDescription,
                        Description = location.LongDescription
                    });
                }
                break;

            case "Tasks":
                foreach (var task in adventure.Tasks.Values)
                {
                    _allMembers.Add(new GroupMemberItemViewModel(this)
                    {
                        Key = task.Key,
                        Name = task.Name,
                        Description = task.Description
                    });
                }
                break;

            case "Events":
                foreach (var evt in adventure.Events.Values)
                {
                    _allMembers.Add(new GroupMemberItemViewModel(this)
                    {
                        Key = evt.Key,
                        Name = evt.Name,
                        Description = evt.Description
                    });
                }
                break;
        }

        FilterMembers();
        UpdateMemberCounts();
    }

    private void FilterMembers()
    {
        FilteredMembers.Clear();

        var searchLower = MemberSearchText?.ToLowerInvariant() ?? "";

        foreach (var member in _allMembers)
        {
            if (string.IsNullOrWhiteSpace(searchLower) ||
                member.Name.ToLowerInvariant().Contains(searchLower) ||
                member.Description.ToLowerInvariant().Contains(searchLower))
            {
                FilteredMembers.Add(member);
            }
        }

        HasMembers = FilteredMembers.Count > 0;
        HasNoMembers = FilteredMembers.Count == 0;
    }

    internal void UpdateMemberCounts()
    {
        var totalCount = _allMembers.Count;
        var selectedCount = _allMembers.Count(m => m.IsSelected);

        MemberCountText = $"{totalCount} {(totalCount == 1 ? "member" : "members")}";
        SelectedMemberCountText = $"({selectedCount} selected)";
    }

    [RelayCommand]
    private void SelectAllMembers()
    {
        foreach (var member in _allMembers)
        {
            member.IsSelected = true;
        }
        UpdateMemberCounts();
    }

    [RelayCommand]
    private void ClearAllMembers()
    {
        foreach (var member in _allMembers)
        {
            member.IsSelected = false;
        }
        UpdateMemberCounts();
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
        await SaveGroup();

        await Shell.Current.DisplayAlert("Success", "Group changes applied successfully.", "OK");
    }

    [RelayCommand]
    private async Task SaveAndClose()
    {
        if (!ValidateInput())
            return;

        await SaveGroup();
        await Shell.Current.GoToAsync("..");
    }

    private bool ValidateInput()
    {
        // Validate group name
        if (string.IsNullOrWhiteSpace(GroupName))
        {
            Shell.Current.DisplayAlert("Validation Error", "Group name is required.", "OK");
            return false;
        }

        // At least one member should be selected
        var selectedCount = _allMembers.Count(m => m.IsSelected);
        if (selectedCount == 0)
        {
            Shell.Current.DisplayAlert("Validation Error", "Please select at least one member for this group.", "OK");
            return false;
        }

        return true;
    }

    private async Task SaveGroup()
    {
        try
        {
            var adventure = _adventureService.CurrentAdventure;
            if (adventure == null)
            {
                await Shell.Current.DisplayAlert("Error", "No adventure loaded", "OK");
                return;
            }

            if (string.IsNullOrEmpty(GroupKey))
            {
                GroupKey = "grp_" + Guid.NewGuid().ToString("N").Substring(0, 8);
            }

            var group = new Group
            {
                Key = GroupKey,
                Name = GroupName,
                Description = Description,
                LastModified = DateTime.Now
            };

            // Parse group type
            if (Enum.TryParse<GroupType>(SelectedGroupType, out var groupType))
            {
                group.Type = groupType;
            }

            // Get selected members
            group.MemberKeys.Clear();
            foreach (var member in _allMembers.Where(m => m.IsSelected))
            {
                group.MemberKeys.Add(member.Key);
            }

            // Add or update in adventure
            if (adventure.Groups.ContainsKey(GroupKey))
            {
                adventure.Groups[GroupKey] = group;
            }
            else
            {
                adventure.Groups.Add(GroupKey, group);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to save group: {ex.Message}", "OK");
        }
    }
}

public partial class GroupMemberItemViewModel : ObservableObject
{
    private readonly GroupEditorViewModel _parentViewModel;

    public GroupMemberItemViewModel(GroupEditorViewModel parentViewModel)
    {
        _parentViewModel = parentViewModel;
    }

    [ObservableProperty]
    private string key = "";

    [ObservableProperty]
    private string name = "";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private bool isSelected = false;

    public bool HasDescription => !string.IsNullOrWhiteSpace(Description);

    public FontAttributes NameFontAttributes => IsSelected ? FontAttributes.Bold : FontAttributes.None;

    partial void OnIsSelectedChanged(bool value)
    {
        OnPropertyChanged(nameof(NameFontAttributes));
        _parentViewModel?.UpdateMemberCounts();
    }
}
