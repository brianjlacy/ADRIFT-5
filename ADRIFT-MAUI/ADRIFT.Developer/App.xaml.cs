namespace ADRIFT;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new AppShell();
    }

    protected override Window CreateWindow(IActivationState? activationState)
    {
        var window = base.CreateWindow(activationState);

        // Set default window size for desktop platforms
        window.Width = 1400;
        window.Height = 900;
        window.Title = "ADRIFT 5 Developer - Interactive Fiction Development Environment";

        return window;
    }
}
