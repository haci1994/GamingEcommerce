using GamingEcommerce.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GamingEcommerce.MVC.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly IWebsiteInfoService _websiteInfoService;
        public HeaderViewComponent(IWebsiteInfoService websiteInfoService)
        {
            _websiteInfoService = websiteInfoService;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var list = await _websiteInfoService.GetAllAsync();
            var model = list.First();

            return View(model);
        }
    }
}
