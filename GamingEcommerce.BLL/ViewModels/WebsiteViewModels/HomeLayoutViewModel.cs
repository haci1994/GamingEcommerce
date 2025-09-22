using GamingEcommerce.BLL.ViewModels.GeneralViewModels;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class HomeLayoutViewModel
    {
        public WebsiteInfoViewModel? WebsiteInfo { get; set; }
        public List<SocialViewModel>? Socials { get; set; } = [];
        public List<CategoryViewModel>? Categories { get; set; } = [];
        public List<ProductViewModel>? HotDeals { get; set; } = [];
        public List<ProductViewModel>? PopularProducts { get; set; } = [];
        public List<ProductViewModel>? RecommendedProducts { get; set; } = [];
        public List<LanguageViewModel>? Languages { get; set; } = [];
        public List<CurrencyViewModel>? Currencies { get; set; } = [];

    }
}
