using GamingEcommerce.BLL.ViewModels.GeneralViewModels;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class ProductsPartialViewModel
    {
        public List<CategoryViewModel> Categories { get; set; } = [];
        public List<ProductViewModel> Products { get; set; } = [];
        public List<ProductColorViewModel> Colors { get; set; } = [];
    }
}
