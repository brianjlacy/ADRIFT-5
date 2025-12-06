using ADRIFT.Developer.ViewModels;

namespace ADRIFT.Developer.Pages;

public partial class TaskEditorPage : ContentPage
{
    public TaskEditorPage(TaskEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    private void OnTabClicked(object? sender, EventArgs e)
    {
        if (sender is not Button clickedButton) return;

        // Hide all tab contents
        GeneralContent.IsVisible = false;
        CommandsContent.IsVisible = false;
        RestrictionsContent.IsVisible = false;
        ActionsContent.IsVisible = false;
        AdvancedContent.IsVisible = false;

        // Reset all tab button styles to default
        GeneralTab.BackgroundColor = null;
        CommandsTab.BackgroundColor = null;
        RestrictionsTab.BackgroundColor = null;
        ActionsTab.BackgroundColor = null;
        AdvancedTab.BackgroundColor = null;

        // Show clicked tab content and highlight button
        if (clickedButton.Text == "General")
        {
            GeneralContent.IsVisible = true;
            GeneralTab.BackgroundColor = Application.Current?.Resources["ADRIFTPrimary"] as Color;
        }
        else if (clickedButton.Text == "Commands")
        {
            CommandsContent.IsVisible = true;
            CommandsTab.BackgroundColor = Application.Current?.Resources["ADRIFTPrimary"] as Color;
        }
        else if (clickedButton.Text == "Restrictions")
        {
            RestrictionsContent.IsVisible = true;
            RestrictionsTab.BackgroundColor = Application.Current?.Resources["ADRIFTPrimary"] as Color;
        }
        else if (clickedButton.Text == "Actions")
        {
            ActionsContent.IsVisible = true;
            ActionsTab.BackgroundColor = Application.Current?.Resources["ADRIFTPrimary"] as Color;
        }
        else if (clickedButton.Text == "Advanced")
        {
            AdvancedContent.IsVisible = true;
            AdvancedTab.BackgroundColor = Application.Current?.Resources["ADRIFTPrimary"] as Color;
        }
    }
}
