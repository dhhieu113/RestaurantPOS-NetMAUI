using RestaurantPOS.ViewModels;

namespace RestaurantPOS.Pages;

public partial class OrdersPage : ContentPage
{
    private readonly OrdersViewModel _ordersViewModel;
    private bool _isOrderDetailsVisible = false;

    public OrdersPage(OrdersViewModel ordersViewModel)
	{
		InitializeComponent();
        _ordersViewModel = ordersViewModel;
        BindingContext = _ordersViewModel;
        InitializeViewModelAsync();
    }

    private async void InitializeViewModelAsync() => await _ordersViewModel.InitializeAsync();

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        // Determine if we should use mobile or desktop layout
        // Consider mobile if width is less than 800 pixels
        bool isMobile = width < 800;

        DesktopLayout.IsVisible = !isMobile;
        MobileLayout.IsVisible = isMobile;

        // Reset order details visibility when switching layouts
        if (isMobile && _isOrderDetailsVisible)
        {
            _isOrderDetailsVisible = false;
            UpdateMobileOrderDetailsVisibility();
        }
    }

    private void OnOrderDetailsToggleClicked(object sender, EventArgs e)
    {
        _isOrderDetailsVisible = !_isOrderDetailsVisible;
        UpdateMobileOrderDetailsVisibility();
    }

    private void UpdateMobileOrderDetailsVisibility()
    {
        OrdersListContent.IsVisible = !_isOrderDetailsVisible;
        OrderDetailsContent.IsVisible = _isOrderDetailsVisible;

        // Update button text
        OrderDetailsToggleButton.Text = _isOrderDetailsVisible ? "Back to Orders" : "Order Details";
    }
}