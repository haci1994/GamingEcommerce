using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamingEcommerce.MVC.Controllers
{
    public class ShopController : Controller
    {
        private readonly ICategoryService _categoryService;
        private readonly IProductService _productService;
        private readonly IProductColorService _productColorService;

        public ShopController(ICategoryService categoryService, IProductService productService, IProductColorService productColorService)
        {
            _categoryService = categoryService;
            _productService = productService;
            _productColorService = productColorService;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();
            var products = await _productService.GetAllAsync();
            var colors = await _productColorService.GetAllAsync();

            products = products.Take(4).ToList();

            var model = new ShopPageViewModel
            {
                Products = products,
                Categories = categories,
                Colors = colors
            };

            return View(model);
        }
    }
}
