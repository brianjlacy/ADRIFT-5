using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using ADRIFT.Core.Models;

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

    partial void OnSearchTextChanged(string value)
    {
        FilterObjects();
    }

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

    private async void FilterObjects()
    {
        if (string.IsNullOrWhiteSpace(SearchText))
        {
            await LoadObjectsAsync();
            return;
        }

        try
        {
            var allObjects = await _adventureService.GetObjectsAsync();
            var filtered = allObjects.Where(o =>
                o.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                o.ShortDescription.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                o.Key.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                .ToList();

            FilteredObjects.Clear();
            foreach (var obj in filtered)
            {
                FilteredObjects.Add(new ObjectItemViewModel(obj));
            }

            ObjectCount = FilteredObjects.Count;
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", $"Failed to filter objects: {ex.Message}", "OK");
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
            try
            {
                var adventure = _adventureService.CurrentAdventure;
                if (adventure != null && adventure.Objects.ContainsKey(obj.Key))
                {
                    adventure.Objects.Remove(obj.Key);
                    FilteredObjects.Remove(obj);
                    ObjectCount = FilteredObjects.Count;
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to delete object: {ex.Message}", "OK");
            }
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
        var action = await Shell.Current.DisplayActionSheet("Sort By", "Cancel", null,
            "Name (A-Z)", "Name (Z-A)", "Location", "Recently Modified");

        if (action != null && action != "Cancel")
        {
            var sorted = action switch
            {
                "Name (A-Z)" => FilteredObjects.OrderBy(o => o.FullName).ToList(),
                "Name (Z-A)" => FilteredObjects.OrderByDescending(o => o.FullName).ToList(),
                "Location" => FilteredObjects.OrderBy(o => o.LocationName).ToList(),
                "Recently Modified" => FilteredObjects.OrderByDescending(o => o.Key).ToList(),
                _ => FilteredObjects.ToList()
            };

            FilteredObjects.Clear();
            foreach (var item in sorted)
            {
                FilteredObjects.Add(item);
            }
        }
    }

    [RelayCommand]
    private async Task Filter()
    {
        var action = await Shell.Current.DisplayActionSheet("Filter", "Cancel", null,
            "All Objects", "Static Only", "Dynamic Only", "Library Only");

        if (action != null && action != "Cancel")
        {
            try
            {
                var allObjects = await _adventureService.GetObjectsAsync();
                IEnumerable<AdriftObject> filtered = action switch
                {
                    "Static Only" => allObjects.Where(o => o.IsStatic),
                    "Dynamic Only" => allObjects.Where(o => !o.IsStatic),
                    "Library Only" => allObjects.Where(o => o.IsLibrary),
                    _ => allObjects
                };

                FilteredObjects.Clear();
                foreach (var obj in filtered)
                {
                    FilteredObjects.Add(new ObjectItemViewModel(obj));
                }

                ObjectCount = FilteredObjects.Count;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", $"Failed to filter objects: {ex.Message}", "OK");
            }
        }
    }

    [RelayCommand]
    private async Task Search()
    {
        FilterObjects();
        await Task.CompletedTask;
    }
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

    public ObjectItemViewModel(AdriftObject obj) : this()
    {
        Key = obj.Key;
        FullName = obj.FullName;
        Description = obj.ShortDescription;
        LocationName = obj.LocationKey ?? "Unknown";
        IsStatic = obj.IsStatic;
        IsLibrary = obj.IsLibrary;
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
