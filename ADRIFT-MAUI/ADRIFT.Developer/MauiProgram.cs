using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace ADRIFT.Developer;

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

        // Register ViewModels - List Pages
        builder.Services.AddTransient<MainViewModel>();
        builder.Services.AddTransient<LocationListViewModel>();
        builder.Services.AddTransient<ObjectListViewModel>();
        builder.Services.AddTransient<TaskListViewModel>();
        builder.Services.AddTransient<CharacterListViewModel>();
        builder.Services.AddTransient<EventListViewModel>();
        builder.Services.AddTransient<VariableListViewModel>();
        builder.Services.AddTransient<GroupListViewModel>();
        builder.Services.AddTransient<MapViewModel>();

        // Register ViewModels - Editor Pages
        builder.Services.AddTransient<LocationEditorViewModel>();
        builder.Services.AddTransient<ObjectEditorViewModel>();
        builder.Services.AddTransient<TaskEditorViewModel>();
        builder.Services.AddTransient<CharacterEditorViewModel>();
        builder.Services.AddTransient<EventEditorViewModel>();
        builder.Services.AddTransient<VariableEditorViewModel>();
        builder.Services.AddTransient<SynonymEditorViewModel>();
        builder.Services.AddTransient<GroupEditorViewModel>();
        builder.Services.AddTransient<HintEditorViewModel>();

        // Register Views - Main & List Pages
        builder.Services.AddTransient<MainPage>();
        builder.Services.AddTransient<LocationListPage>();
        builder.Services.AddTransient<ObjectListPage>();
        builder.Services.AddTransient<TaskListPage>();
        builder.Services.AddTransient<CharacterListPage>();
        builder.Services.AddTransient<EventListPage>();
        builder.Services.AddTransient<VariableListPage>();
        builder.Services.AddTransient<GroupListPage>();
        builder.Services.AddTransient<MapPage>();

        // Register Views - Editor Pages
        builder.Services.AddTransient<LocationEditorPage>();
        builder.Services.AddTransient<ObjectEditorPage>();
        builder.Services.AddTransient<TaskEditorPage>();
        builder.Services.AddTransient<CharacterEditorPage>();
        builder.Services.AddTransient<EventEditorPage>();
        builder.Services.AddTransient<VariableEditorPage>();
        builder.Services.AddTransient<SynonymEditorPage>();
        builder.Services.AddTransient<GroupEditorPage>();
        builder.Services.AddTransient<HintEditorPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}
