using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

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
            var products = await _productService.GetAllAsync(predicate: x => !x.IsDeleted,
                include:
                x => x.Include(z => z.ProductColors)
                    .ThenInclude(h=> h.ProductColorImages)
                .Include(z=> z.ProductColors).ThenInclude(h=> h.ProductSizes));
            var colors = await _productColorService.GetAllAsync();
            var total = products.Count();

            products = products.Take(1).ToList();

            var model = new ShopPageViewModel
            {
                Products = products,
                Categories = categories,
                Colors = colors,
                TotalProductsCount = total
            };

            return View(model);
        }

        public async Task<IActionResult> LoadMore(int skip)
        {
            var products = await _productService.GetAllAsync(include:
                x => x.Include(z => z.ProductColors)
                    .ThenInclude(h => h.ProductColorImages)
                .Include(z => z.ProductColors).ThenInclude(h => h.ProductSizes));
            products = products.Skip(skip).Take(1).ToList();

            var data = JsonConvert.SerializeObject(products);

            return Content(data, "application/json");
        }
    }
}
