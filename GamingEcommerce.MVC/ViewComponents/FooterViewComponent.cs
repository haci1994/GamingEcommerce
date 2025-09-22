using GamingEcommerce.BLL.Services.WebsiteServices;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamingEcommerce.MVC.ViewComponents
{
    public class FooterViewComponent : ViewComponent
    {
        private readonly HomeLayoutService _homeLayoutService;

        public FooterViewComponent(HomeLayoutService homeLayoutService)
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

            return View(model);
        }
    }
}
