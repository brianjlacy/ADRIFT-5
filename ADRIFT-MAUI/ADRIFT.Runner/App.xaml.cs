namespace ADRIFT;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new MainPage();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        // Set default window size for desktop platforms
        window.Width = 1000;
        window.Height = 700;
        window.Title = "ADRIFT 5 Runner - Interactive Fiction Player";

        return window;
    }
}
