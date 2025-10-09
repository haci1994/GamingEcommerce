using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.GeneralServices;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<AppUser> _userManager;
        private readonly IAddressService _addressService;
        private readonly SignInManager<AppUser> _signInManager;

        public ShopController(ICategoryService categoryService, IProductService productService, IProductColorService productColorService, UserManager<AppUser> userManager, IAddressService addressService, SignInManager<AppUser> signInManager)
        {
            _categoryService = categoryService;
            _productService = productService;
            _productColorService = productColorService;
            _userManager = userManager;
            _addressService = addressService;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> Index()
        {
            var categories = await _categoryService.GetAllAsync();

            var products = await _productService.GetAllAsync(predicate: x => !x.IsDeleted && x.ProductColors.Count > 0,
                include:
                x => x.Include(z => z.ProductColors)
                    .ThenInclude(h => h.ProductColorImages)
                .Include(z => z.ProductColors).ThenInclude(h => h.ProductSizes));
            var colors = await _productColorService.GetAllAsync();

            int total;

            if (products == null)
            {
                total = 0;
                products = new List<ProductViewModel>();
            }
            else
            {
                total = products.Count();
            }
            ;

            total = products.Count();

            foreach (var product in products)
            {
                product.ProductColors = product.ProductColors.Where(x => !x.IsDeleted).ToList();
            }

            products = products.Take(2).ToList();

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
                .Include(z => z.ProductColors).ThenInclude(h => h.ProductSizes), predicate: x => !x.IsDeleted);

            foreach (var product in products)
            {
                product.ProductColors = product.ProductColors.Where(x => !x.IsDeleted).ToList();
            }

            products = products.Skip(skip).Take(2).ToList();

            var data = JsonConvert.SerializeObject(products);

            return Content(data, "application/json");
        }

        public async Task<IActionResult> Checkout()
        {
            AddressViewModel address;

            if (User.Identity != null && User.Identity.IsAuthenticated) //girish edib
            {
                var user = await _userManager.GetUserAsync(User);

                if (user == null) return BadRequest();

                var defaultAddress = await _addressService.GetAsync(predicate: x => x.UserId == user.Id && x.IsDefault);

                if (defaultAddress == null)
                {
                    address = new AddressViewModel();
                }
                else
                {
                    address = defaultAddress;
                }
            }
            else
            {
                address = new AddressViewModel();
            }

            var json = Request.Cookies["GAMING_ECOMMERCE_BASKET"];

            var list = new List<BasketItemViewModel>();

            if (!string.IsNullOrEmpty(json))
            {
                list = JsonConvert.DeserializeObject<List<BasketItemViewModel>>(json) ?? [];
            }

            var model = new CheckoutPageViewModel
            {
                DefaultAddress = address,
                Products = list
            };

            return View(model);
        }
    }
}