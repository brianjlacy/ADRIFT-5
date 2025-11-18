using ADRIFT.ViewModels;

namespace ADRIFT.Views;

public partial class TaskListPage : ContentPage
{
    public TaskListPage(TaskListViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is TaskListViewModel vm)
        {
            _ = vm.LoadTasksAsync();
        }
    }
}
