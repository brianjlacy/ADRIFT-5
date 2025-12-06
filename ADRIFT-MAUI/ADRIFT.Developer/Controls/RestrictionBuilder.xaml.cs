using ADRIFT.Core.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace ADRIFT.Developer.Controls;

public partial class RestrictionBuilder : ContentView
{
    public static readonly BindableProperty RestrictionsProperty =
        BindableProperty.Create(nameof(Restrictions), typeof(ObservableCollection<RestrictionDisplayModel>),
            typeof(RestrictionBuilder), null, propertyChanged: OnRestrictionsChanged);

    public ObservableCollection<RestrictionDisplayModel> Restrictions
    {
        get => (ObservableCollection<RestrictionDisplayModel>)GetValue(RestrictionsProperty);
        set => SetValue(RestrictionsProperty, value);
    }

    public IRelayCommand<RestrictionDisplayModel> RemoveRestrictionCommand { get; }

    public RestrictionBuilder()
    {
        InitializeComponent();
        Restrictions = new ObservableCollection<RestrictionDisplayModel>();
        RemoveRestrictionCommand = new RelayCommand<RestrictionDisplayModel>(OnRemoveRestriction);
    }

    private static void OnRestrictionsChanged(BindableObject bindable, object oldValue, object newValue)
    {
        if (bindable is RestrictionBuilder control && newValue is ObservableCollection<RestrictionDisplayModel> restrictions)
        {
            control.RestrictionsCollectionView.ItemsSource = restrictions;
        }
    }

    private async void OnAddRestrictionClicked(object? sender, EventArgs e)
    {
        var restrictionType = await DisplayActionSheet("Select Restriction Type",
            "Cancel", null,
            "Location", "Object", "Character", "Task", "Variable", "Property", "Direction", "Expression");

        if (restrictionType == null || restrictionType == "Cancel")
            return;

        var must = await DisplayActionSheet("Must or Must Not?", "Cancel", null, "Must", "Must Not");
        if (must == null || must == "Cancel")
            return;

        var model = new RestrictionDisplayModel
        {
            TypeText = restrictionType,
            MustText = must,
            DetailsText = "Click Edit to configure",
            Restriction = new Restriction
            {
                Type = Enum.Parse<RestrictionType>(restrictionType),
                Must = must == "Must" ? MustEnum.Must : MustEnum.MustNot
            }
        };

        Restrictions.Add(model);
    }

    private async void OnEditRestrictionClicked(object? sender, EventArgs e)
    {
        if (sender is not Button button || button.BindingContext is not RestrictionDisplayModel model)
            return;

        // Simple prompt-based editing for now
        // Full implementation would show type-specific editor dialogs
        var details = await DisplayPromptAsync("Edit Restriction",
            $"Enter details for {model.TypeText} restriction:",
            initialValue: model.DetailsText == "Click Edit to configure" ? "" : model.DetailsText);

        if (details != null)
        {
            model.DetailsText = details;
            model.HasDetails = !string.IsNullOrWhiteSpace(details);
        }

        var failMsg = await DisplayActionSheet("Add fail message?", "Skip", null, "Yes");
        if (failMsg == "Yes")
        {
            var message = await DisplayPromptAsync("Fail Message",
                "Enter message to show if restriction fails:");
            if (!string.IsNullOrWhiteSpace(message))
            {
                model.FailMessageText = message;
                model.HasFailMessage = true;
                model.Restriction.FailMessage = message;
            }
        }
    }

    private void OnRemoveRestriction(RestrictionDisplayModel? model)
    {
        if (model != null)
        {
            Restrictions.Remove(model);
        }
    }

    private Task<string> DisplayActionSheet(string title, string cancel, string? destruction, params string[] buttons)
    {
        if (Application.Current?.MainPage != null)
        {
            return Application.Current.MainPage.DisplayActionSheet(title, cancel, destruction, buttons);
        }
        return Task.FromResult(cancel);
    }

    private Task<string?> DisplayPromptAsync(string title, string message, string? initialValue = null)
    {
        if (Application.Current?.MainPage != null)
        {
            return Application.Current.MainPage.DisplayPromptAsync(title, message, initialValue: initialValue);
        }
        return Task.FromResult<string?>(null);
    }
}

/// <summary>
/// Display model for a restriction in the UI
/// </summary>
public partial class RestrictionDisplayModel : ObservableObject
{
    [ObservableProperty]
    private string typeText = "";

    [ObservableProperty]
    private string mustText = "Must";

    [ObservableProperty]
    private string detailsText = "";

    [ObservableProperty]
    private bool hasDetails;

    [ObservableProperty]
    private string failMessageText = "";

    [ObservableProperty]
    private bool hasFailMessage;

    /// <summary>
    /// The actual restriction model
    /// </summary>
    public Restriction Restriction { get; set; } = new();
}
