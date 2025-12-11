using Microsoft.Extensions.Logging;
using Plugin.Maui.Audio;
using GAME.Services;
using GAME.ViewModels;
using GAME.Views;

namespace GAME
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

            // Register services
            builder.Services.AddSingleton(AudioManager.Current);
            builder.Services.AddSingleton<DatabaseService>();

            // Register ViewModels
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<CharacterSelectionViewModel>();
            builder.Services.AddSingleton<CombatViewModel>();
            builder.Services.AddSingleton<StatisticsViewModel>();

            // Register Views
            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<CharacterSelectionPage>();
            builder.Services.AddSingleton<CombatPage>();
            builder.Services.AddSingleton<StatisticsPage>();

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
