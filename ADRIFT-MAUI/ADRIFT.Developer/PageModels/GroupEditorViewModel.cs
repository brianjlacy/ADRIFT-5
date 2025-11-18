using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.ViewModels;

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
        if (!string.IsNullOrEmpty(GroupKey))
        {
            // Edit mode - load existing group
            IsEditMode = true;
            PageTitle = "Edit Group";

            // TODO: Load group from adventure service
            // For now, using sample data
            GroupName = "Sample Group";
            SelectedGroupType = "Characters";
            Description = "This is a sample group for demonstration";

            LoadAvailableMembers();

            // Mark some members as selected
            if (_allMembers.Count > 0)
            {
                _allMembers[0].IsSelected = true;
                if (_allMembers.Count > 2)
                    _allMembers[2].IsSelected = true;
            }
        }
        else
        {
            // New group mode
            IsEditMode = false;
            PageTitle = "New Group";
            GroupName = "";
            SelectedGroupType = "Characters";
            Description = "";

            LoadAvailableMembers();
        }

        await Task.CompletedTask;
    }

    private void LoadAvailableMembers()
    {
        // TODO: Load actual items from adventure service based on SelectedGroupType
        // For now, generate sample data

        _allMembers.Clear();

        switch (SelectedGroupType)
        {
            case "Characters":
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "char1",
                    Name = "Guard Captain",
                    Description = "The leader of the city guard"
                });
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "char2",
                    Name = "Merchant",
                    Description = "Sells various goods"
                });
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "char3",
                    Name = "Villager",
                    Description = "A typical village resident"
                });
                break;

            case "Objects":
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "obj1",
                    Name = "Golden Key",
                    Description = "Opens the treasure room"
                });
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "obj2",
                    Name = "Ancient Sword",
                    Description = "A powerful weapon"
                });
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "obj3",
                    Name = "Health Potion",
                    Description = "Restores health"
                });
                break;

            case "Locations":
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "loc1",
                    Name = "Castle Courtyard",
                    Description = "The main entrance area"
                });
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "loc2",
                    Name = "Throne Room",
                    Description = "Where the king holds court"
                });
                break;

            case "Tasks":
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "task1",
                    Name = "Open Door",
                    Description = "Opens the wooden door"
                });
                break;

            case "Events":
                _allMembers.Add(new GroupMemberItemViewModel(this)
                {
                    Key = "event1",
                    Name = "Timer Event",
                    Description = "Triggers after 10 turns"
                });
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
        // TODO: Save group to adventure service
        // Get selected members
        var selectedMembers = _allMembers.Where(m => m.IsSelected).Select(m => m.Key).ToList();

        // For now, just a placeholder
        await Task.Delay(100);

        // If this was a new group, generate a key
        if (string.IsNullOrEmpty(GroupKey))
        {
            GroupKey = "grp_" + Guid.NewGuid().ToString("N")[..8];
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
