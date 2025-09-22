using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;

namespace GamingEcommerce.BLL.Services.WebsiteServices
{
    public class HomeLayoutService
    {
        private readonly IWebsiteInfoService _websiteInfoService;
        private readonly ISocialService _socialService;
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly ILanguageService _languageService;
        private readonly ICurrencyService _currencyService;

        public HomeLayoutService(IWebsiteInfoService websiteInfoService, ISocialService socialService, ICategoryService categoryService, IProductService productService, ILanguageService languageService, ICurrencyService currencyService)
        {
            _websiteInfoService = websiteInfoService;
            _socialService = socialService;
            _categoryService = categoryService;
            _productService = productService;
            _languageService = languageService;
            _currencyService = currencyService;
        }

        public async Task<HomeLayoutViewModel> GetHomeLayoutDataAsync()
        {
            //Website Info
            var websiteInfoList = await _websiteInfoService.GetAllAsync();

            var websiteInfo = new WebsiteInfoViewModel();

            if (websiteInfoList == null || !websiteInfoList.Any())
            {
                websiteInfo.HelpPhone = "";
                websiteInfo.SupportPhone = "";
                websiteInfo.Email = "";
                websiteInfo.Address = "";
                websiteInfo.Copyright = "";
            } else
            {
                websiteInfo = websiteInfoList.First();
            }

            //Socials

            var socials = await _socialService.GetAllAsync();

            if (socials == null || !socials.Any())
            {
                socials = new List<SocialViewModel>();
            }

            //Categories

            var categories = await _categoryService.GetAllAsync();

            if (categories == null || !categories.Any())
            {
                categories = new List<CategoryViewModel>();
            }

            //Hot Deals

            var hotDeals = await _productService.GetHotDealsAsync(5);
            if (hotDeals == null || !hotDeals.Any())
            {
                hotDeals = new List<ProductViewModel>();
            }

            //Popular Products

            var popularProducts = await _productService.GetPopularProductsAsync(5);
            if (popularProducts == null || !popularProducts.Any())
            {
                popularProducts = new List<ProductViewModel>();
            }

            //Recommended Products
            var recommendedProducts = await _productService.GetRecommendedProductsAsync(5);
            if (recommendedProducts == null || !recommendedProducts.Any())
            {
                recommendedProducts = new List<ProductViewModel>();
            }

            //Languages
            var languages = await _languageService.GetAllAsync();
            if (languages == null || !languages.Any())
            {
                languages = new List<LanguageViewModel>();
            }

            //Currencies
            var currencies = await _currencyService.GetAllAsync();
            if (currencies == null || !currencies.Any())
            {
                currencies = new List<CurrencyViewModel>();
            }

            return new HomeLayoutViewModel
            {
                WebsiteInfo = websiteInfo,
                Socials = socials,
                Categories = categories,
                HotDeals = hotDeals,
                PopularProducts = popularProducts,
                RecommendedProducts = recommendedProducts,
                Languages = languages,
                Currencies = currencies
            };
        }
    }
}
