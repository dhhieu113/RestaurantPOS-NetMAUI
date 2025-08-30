using Microsoft.Extensions.Logging;

namespace RestaurantPOS.Platforms.Windows;

public static class WindowsInitializer
{
    public static void Initialize(MauiAppBuilder builder)
    {
        // Detect if we're running in a CI environment or lacking WinAppSDK dependencies
        bool isCiEnvironment = Environment.GetEnvironmentVariable("CI") != null || 
                              Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null;
        
        if (isCiEnvironment)
        {
            // Log the detection of CI environment
            builder.Services.AddLogging(logging =>
            {
                logging.AddDebug();
                logging.SetMinimumLevel(LogLevel.Warning);
            });
            
            // In CI environments, we can modify app behavior as needed
            // For example, avoid using features that require full WinAppSDK initialization
        }
    }
}
