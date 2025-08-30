using CommunityToolkit.Maui.Views;
using RestaurantPOS.Controls;

namespace RestaurantPOS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            
            // Ensure flyout is accessible on all platforms
            ConfigureFlyoutBehavior();
        }

        private void ConfigureFlyoutBehavior()
        {
            // On mobile devices, we want the flyout to be available but hidden by default
            // On desktop/tablet, we can keep it locked open if there's enough space
            
#if ANDROID || IOS
            // Mobile platforms - always use flyout behavior
            FlyoutBehavior = FlyoutBehavior.Flyout;
#else
            // Desktop platforms - can use locked behavior
            FlyoutBehavior = FlyoutBehavior.Flyout; // Start with flyout, adapt based on size
#endif
        }

        protected override void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            
            // Adapt flyout behavior based on screen size
            if (width > 0)
            {
                if (width < 800)
                {
                    // Mobile/Small screen: Use flyout that can be toggled
                    FlyoutBehavior = FlyoutBehavior.Flyout;
                }
                else
                {
                    // Large screen: Can use locked flyout if desired
                    // For now, keep it as flyout for consistency
                    FlyoutBehavior = FlyoutBehavior.Flyout;
                }
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            // Ensure the navigation bar is visible so users can access the hamburger menu
            SetValue(Shell.NavBarIsVisibleProperty, true);
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            try
            {
                var helpPopup = new HelpPopup();
                await this.ShowPopupAsync(helpPopup);
            }
            catch
            {
                // Handle any popup errors gracefully
                await DisplayAlert("Support", "For support, please contact us at support@restaurantpos.com", "OK");
            }
        }
    }
}
