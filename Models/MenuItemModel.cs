using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace RestaurantPOS.Models
{
    public partial class MenuItemModel : ObservableObject
    {

        public int Id { get; set; }

        [ObservableProperty]
        private string _name = string.Empty;

        [ObservableProperty]
        private decimal _price;

        [ObservableProperty]
        private string _icon = string.Empty;

        [ObservableProperty]
        private string _description = string.Empty;

        public ObservableCollection<MenuCategoryModel> Categories { get; set; } = [];

        public MenuCategoryModel[] SelectedCategories => Categories.Where(c => c.IsSelected).ToArray();
    }
}
