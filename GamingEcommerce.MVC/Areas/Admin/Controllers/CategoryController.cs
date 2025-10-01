using GamingEcommerce.BLL.Services.WebsiteServices;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamingEcommerce.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AdminCategoryService _adminCategoryService;

        public CategoryController(AdminCategoryService adminCategoryService)
        {
            _adminCategoryService = adminCategoryService;
        }

        public async Task<IActionResult> Index()
        {
            var model = await _adminCategoryService.GetTableAsync();

            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _adminCategoryService.GetUpdateModel(id);

            if (model == null) return NotFound();

            return View(model);
        }
    }
}
