namespace ADRIFT.Developer.Pages;

public partial class TaskEditorPage : ContentPage
{
    public TaskEditorPage(TaskEditorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
