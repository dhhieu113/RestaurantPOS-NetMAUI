using RestaurantPOS.ViewModels;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Pages;

public partial class ManageMenuItemPage : ContentPage
{
    private readonly ManageMenuItemsViewModel _manageMenuItemViewModel;
    private bool _isFormVisible = false;

    public ManageMenuItemPage(ManageMenuItemsViewModel manageMenuItemViewModel)
    {
        InitializeComponent();
        _manageMenuItemViewModel = manageMenuItemViewModel;
        BindingContext = _manageMenuItemViewModel;
        InitializeAsync();
    }

    private async void InitializeAsync()
    {
        await _manageMenuItemViewModel.InitializeAsync();
    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        // Determine if we should use mobile or desktop layout
        // Consider mobile if width is less than 800 pixels
        bool isMobile = width < 800;

        DesktopLayout.IsVisible = !isMobile;
        MobileLayout.IsVisible = isMobile;

        // Reset form visibility when switching layouts
        if (isMobile && _isFormVisible)
        {
            _isFormVisible = false;
            UpdateMobileFormVisibility();
        }
    }

    private async void OnCategorySelected(Models.MenuCategoryModel category) => await _manageMenuItemViewModel.SelectCategoryCommand.ExecuteAsync(category.Id);

    private async void OnItemSelected(MenuItem menuItem)
    {
        await _manageMenuItemViewModel.EditMenuItemCommand.ExecuteAsync(menuItem);

        // On mobile, automatically show the form when an item is selected for editing
        if (MobileLayout.IsVisible)
        {
            _isFormVisible = true;
            UpdateMobileFormVisibility();
        }
    }

    private void SaveMenuItemFormControl_OnCancel()
    {
        _manageMenuItemViewModel.CancelCommand.Execute(null);

        // On mobile, hide the form when canceled
        if (MobileLayout.IsVisible)
        {
            _isFormVisible = false;
            UpdateMobileFormVisibility();
        }
    }

    private async void SaveMenuItemFormControl_OnSaveItem(Models.MenuItemModel menuItemModel)
    {
        await _manageMenuItemViewModel.SaveMenuItemCommand.ExecuteAsync(menuItemModel);

        // On mobile, hide the form after saving
        if (MobileLayout.IsVisible)
        {
            _isFormVisible = false;
            UpdateMobileFormVisibility();
        }
    }

    private void OnFormToggleClicked(object sender, EventArgs e)
    {
        _isFormVisible = !_isFormVisible;
        UpdateMobileFormVisibility();
    }

    private void OnAddItemFabClicked(object sender, EventArgs e)
    {
        // Clear any existing item and show the form for adding a new item
        _manageMenuItemViewModel.CancelCommand.Execute(null);
        _isFormVisible = true;
        UpdateMobileFormVisibility();
    }

    private void UpdateMobileFormVisibility()
    {
        MenuItemsContent.IsVisible = !_isFormVisible;
        FormContent.IsVisible = _isFormVisible;

        // Update button text
        FormToggleButton.Text = _isFormVisible ? "Back to Items" : "Add/Edit Item";
    }
}