using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace RestaurantPOS.Platforms.Windows;

public static class WindowsInitializer
{
    public static void Initialize(MauiAppBuilder builder)
    {
        // Configure logging
        builder.Services.AddLogging(logging =>
        {
            logging.AddDebug();
            logging.SetMinimumLevel(LogLevel.Warning);
        });
        
        // Detect if we're running in a CI environment or lacking WinAppSDK dependencies
        bool isCiEnvironment = Environment.GetEnvironmentVariable("CI") != null || 
                              Environment.GetEnvironmentVariable("GITHUB_ACTIONS") != null;
        
        try
        {
            if (isCiEnvironment)
            {
                // Log the detection of CI environment
                Debug.WriteLine("Running in CI environment - Windows App SDK initialization may be limited");
                
                // In CI environments, we can skip certain initializations that might fail
                // This allows tests to run without crashing the application
            }
            else
            {
                // In production environments, we'll attempt Windows App SDK initialization
                // but wrap it in try/catch to handle missing dependencies gracefully
                try
                {
                    // Try to initialize Windows App SDK components
                    // If this fails, we'll catch the exception and provide a more useful error message
                    Debug.WriteLine("Initializing Windows App SDK components...");
                    
                    // You can add specific Windows initialization code here
                }
                catch (System.TypeInitializationException ex) when (ex.InnerException is System.Runtime.InteropServices.COMException comEx && 
                                                                  (uint)comEx.HResult == 0x80040154) // REGDB_E_CLASSNOTREG
                {
                    // Handle missing Windows App SDK runtime components
                    Debug.WriteLine($"Windows App SDK initialization failed: {ex.Message}");
                    Debug.WriteLine("This usually indicates that Windows App Runtime is not installed.");
                    
                    // We could show a user-friendly message here in a real app
                    // but we want to continue application execution rather than crashing
                }
                catch (Exception ex)
                {
                    // Handle other initialization failures
                    Debug.WriteLine($"Windows initialization error: {ex.Message}");
                }
            }
        }
        catch (Exception ex)
        {
            // Catch any exceptions to prevent app startup failures
            Debug.WriteLine($"Error during Windows platform initialization: {ex}");
        }
    }
}
