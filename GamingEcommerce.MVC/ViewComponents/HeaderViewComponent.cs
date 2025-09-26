using GamingEcommerce.BLL.Services.WebsiteServices;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace GamingEcommerce.MVC.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly HomeLayoutService _homeLayoutService;
        

        public HeaderViewComponent(HomeLayoutService homeLayoutService)
        {
            _homeLayoutService = homeLayoutService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var model = await _homeLayoutService.GetHomeLayoutDataAsync();

            if (model == null)
            {
                model = new HomeLayoutViewModel();
            }

            var basketList = new List<BasketItemViewModel>();

            var basketListJson = Request.Cookies["GAMING_ECOMMERCE_BASKET"];

            if (string.IsNullOrEmpty(basketListJson))
            {
                model.BasketItems = basketList;
            } else
            {
                model.BasketItems = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(basketListJson);
            }

                return View(model);
        }

        
    }
}
