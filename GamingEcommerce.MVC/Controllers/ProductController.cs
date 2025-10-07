using GamingEcommerce.BLL.Services.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace GamingEcommerce.MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Details(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            return View(product);
        }
    }
}
