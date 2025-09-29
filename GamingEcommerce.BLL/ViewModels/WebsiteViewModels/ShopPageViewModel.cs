using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using System.Data.Common;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class ShopPageViewModel
    {
        public List<CategoryViewModel> Categories { get; set; } = [];
        public List<ProductViewModel> Products { get; set; } = [];
        public List<ProductColorViewModel> Colors { get; set; } = [];

        public int TotalProductsCount { get; set; }

    }
}
