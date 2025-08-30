using CommunityToolkit.Mvvm.ComponentModel;
using RestaurantPOS.Data;

namespace RestaurantPOS.Models
{
    public partial class MenuCategoryModel : ObservableObject
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;

        [ObservableProperty]
        private bool _isSelected;

        public static MenuCategoryModel FromEntity(MenuCategory entity) => new()
        {
            Id = entity.Id,
            Name = entity.Name,
            Icon = entity.Icon
        };
    }
}
