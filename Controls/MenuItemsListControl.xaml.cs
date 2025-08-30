using CommunityToolkit.Mvvm.Input;
using MenuItem = RestaurantPOS.Data.MenuItem;

namespace RestaurantPOS.Controls;

public partial class MenuItemsListControl : ContentView
{
    public MenuItemsListControl()
    {
        InitializeComponent();
    }

    public static readonly BindableProperty ItemsProperty = BindableProperty.Create(
        nameof(Items),
        typeof(MenuItem[]),
        typeof(MenuItemsListControl),
        Array.Empty<MenuItem>()
    );

    public MenuItem[] Items
    {
        get => (MenuItem[])GetValue(ItemsProperty);
        set => SetValue(ItemsProperty, value);
    }

    public event Action<MenuItem> OnItemSelected;

    [RelayCommand]
    private void ItemSelected(MenuItem item) => OnItemSelected?.Invoke(item);

    public string ActionIcon { get; set; } = "shopping_bag.png";

    public bool IsEditingMode { set => ActionIcon = (value ? "edit_solid_24.png" : "shopping_bag.png"); }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);
        
        // Adjust grid span based on available width
        if (width > 0)
        {
            int span;
            if (width < 500)
                span = 1;  // Mobile portrait
            else if (width < 800)
                span = 2;  // Mobile landscape or small tablet
            else if (width < 1200)
                span = 3;  // Medium screen
            else
                span = 4;  // Large screen
            
            GridLayout.Span = span;
        }
    }
}