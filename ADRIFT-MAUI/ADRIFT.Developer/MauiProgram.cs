using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ADRIFT;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        // Register Syncfusion license
        // Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("YOUR_LICENSE_KEY_HERE");

        // Register services
        builder.Services.AddSingleton<IAdventureService, AdventureService>();
        builder.Services.AddSingleton<IFileService, FileService>();

        // Register ViewModels
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<LocationEditorViewModel>();
        builder.Services.AddTransient<ObjectEditorViewModel>();
        builder.Services.AddTransient<TaskEditorViewModel>();
        builder.Services.AddTransient<CharacterEditorViewModel>();
        builder.Services.AddTransient<EventEditorViewModel>();

        // Register Views (Pages)
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<LocationEditorPage>();
        builder.Services.AddTransient<ObjectEditorPage>();
        builder.Services.AddTransient<TaskEditorPage>();
        builder.Services.AddTransient<CharacterEditorPage>();
        builder.Services.AddTransient<EventEditorPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
