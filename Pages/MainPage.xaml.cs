using RestaurantPOS.ViewModels;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Pages
{
    public partial class MainPage : ContentPage
    {
        private readonly HomeViewModel _homeViewModel;
        private readonly SettingsViewModel _settingsViewModel;
        private bool _isCartVisible = false;

        public MainPage(HomeViewModel homeViewModel, SettingsViewModel settingsViewModel)
        {
            InitializeComponent();

            _homeViewModel = homeViewModel;
            _settingsViewModel = settingsViewModel;

            BindingContext = _homeViewModel;

            Initialize();
        }

        private async void Initialize()
        {
            await _homeViewModel.InitializeAsync();
        }

        protected override async void OnSizeAllocated(double width, double height)
        {
            base.OnSizeAllocated(width, height);
            await _settingsViewModel.InitializeAsync();
            
            // Determine if we should use mobile or desktop layout
            // Consider mobile if width is less than 800 pixels
            bool isMobile = width < 800;
            
            DesktopLayout.IsVisible = !isMobile;
            MobileLayout.IsVisible = isMobile;
            
            // Reset cart visibility when switching layouts
            if (isMobile && _isCartVisible)
            {
                _isCartVisible = false;
                UpdateMobileCartVisibility();
            }
        }

        private async void OnCategorySelected(Models.MenuCategoryModel category)
        {
            await _homeViewModel.SelectCategoryCommand.ExecuteAsync(category.Id);
        }

        private void OnItemSelected(MenuItem menuItem)
        {
            _homeViewModel.AddToCartCommand.Execute(menuItem);
        }

        private void OnCartToggleClicked(object sender, EventArgs e)
        {
            _isCartVisible = !_isCartVisible;
            UpdateMobileCartVisibility();
        }

        private void UpdateMobileCartVisibility()
        {
            MenuContent.IsVisible = !_isCartVisible;
            CartContent.IsVisible = _isCartVisible;
            
            // Update button text
            CartToggleButton.Text = _isCartVisible ? "Back to Menu" : $"Cart ({_homeViewModel.CartItems.Count})";
        }
    }
}
