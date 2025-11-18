using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.ViewModels;

public partial class ObjectListViewModel : ObservableObject
{
    private readonly IAdventureService _adventureService;

    public ObjectListViewModel(IAdventureService adventureService)
    {
        _adventureService = adventureService;
        FilteredObjects = new ObservableCollection<ObjectItemViewModel>();
    }

    [ObservableProperty]
    private ObservableCollection<ObjectItemViewModel> filteredObjects;

    [ObservableProperty]
    private string searchText = "";

    [ObservableProperty]
    private int objectCount;

    [ObservableProperty]
    private bool isRefreshing;

    public async Task LoadObjectsAsync()
    {
        try
        {
            IsRefreshing = true;
            var objects = await _adventureService.GetObjectsAsync();

            FilteredObjects.Clear();
            foreach (var obj in objects)
            {
                FilteredObjects.Add(new ObjectItemViewModel(obj));
            }

            ObjectCount = FilteredObjects.Count;
        }
        finally
        {
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task AddObject()
    {
        await Shell.Current.GoToAsync("objecteditor");
    }

    [RelayCommand]
    private async Task EditObject(ObjectItemViewModel obj)
    {
        await Shell.Current.GoToAsync($"objecteditor?key={obj.Key}");
    }

    [RelayCommand]
    private async Task DeleteObject(ObjectItemViewModel obj)
    {
        var result = await Shell.Current.DisplayAlert(
            "Delete Object",
            $"Are you sure you want to delete '{obj.FullName}'?",
            "Delete", "Cancel");

        if (result)
        {
            FilteredObjects.Remove(obj);
            ObjectCount = FilteredObjects.Count;
        }
    }

    [RelayCommand]
    private async Task Refresh()
    {
        await LoadObjectsAsync();
    }

    [RelayCommand]
    private async Task Sort()
    {
        await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Name (A-Z)", "Name (Z-A)", "Location", "Recently Modified");
    }

    [RelayCommand]
    private async Task Filter()
    {
        await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Objects", "Static Only", "Dynamic Only", "Library Only");
    }

    [RelayCommand]
    private Task Search() => Task.CompletedTask;
}

public partial class ObjectItemViewModel : ObservableObject
{
    public ObjectItemViewModel()
    {
        Key = "obj" + Guid.NewGuid().ToString("N").Substring(0, 8);
        FullName = "Sample Object";
        Description = "A sample object";
        LocationName = "Unknown";
        IsStatic = false;
        IsLibrary = false;
    }

    public ObjectItemViewModel(object obj) : this()
    {
        // TODO: Map from object to view model properties
    }

    [ObservableProperty]
    private string key = "";

    [ObservableProperty]
    private string fullName = "";

    [ObservableProperty]
    private string description = "";

    [ObservableProperty]
    private string locationName = "";

    [ObservableProperty]
    private bool isStatic;

    [ObservableProperty]
    private bool isLibrary;
}
