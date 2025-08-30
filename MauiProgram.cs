using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;
using RestaurantPOS.Data;
using RestaurantPOS.Pages;
using RestaurantPOS.ViewModels;
#if WINDOWS
using RestaurantPOS.Platforms.Windows;
#endif

namespace RestaurantPOS
{
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
                    fonts.AddFont("Poppins-Regular.ttf", "PoppinsRegular");
                    fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif

#if WINDOWS
            // Initialize Windows-specific configuration
            WindowsInitializer.Initialize(builder);
#endif

            builder.Services
                .AddSingleton<DatabaseService>()
                .AddSingleton<HomeViewModel>()
                .AddSingleton<MainPage>()
                .AddSingleton<OrdersViewModel>()
                .AddSingleton<OrdersPage>()
                .AddTransient<ManageMenuItemsViewModel>()
                .AddTransient<ManageMenuItemPage>()
                .AddSingleton<SettingsViewModel>();

            return builder.Build();
        }
    }
}
