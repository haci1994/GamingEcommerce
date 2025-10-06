using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.GeneralServices;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace GamingEcommerce.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly IMapper _mapper;
        private readonly IProductColorService _productColorService;
        private readonly IProductColorImageService _productColorImageService;
        private readonly IProductSizeService _productSizeService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, IMapper mapper, IProductColorService productColorService, IProductColorImageService productColorImageService, IProductSizeService productSizeService, IWebHostEnvironment webHostEnvironment, ICategoryService categoryService)
        {
            _productService = productService;
            _mapper = mapper;
            _productColorService = productColorService;
            _productColorImageService = productColorImageService;
            _productSizeService = productSizeService;
            _webHostEnvironment = webHostEnvironment;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> Index()
        {
            var allProducts = await _productService.GetAllAsync(include: x=> x.Include(x=> x.ProductColors).ThenInclude(z=>z.ProductColorImages).Include(p => p.ProductColors)
                .ThenInclude(pc => pc.ProductSizes));

            return View(allProducts);
        }

        public async Task<IActionResult> Create()
        {
            var model = await _productService.GetCreateProductModelAsync();

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                var categories = await _categoryService.GetAllAsync(predicate: x => !x.IsDeleted);

                var selectList = new List<SelectListItem>();

                foreach (var category in categories)
                {
                    var option = new SelectListItem { Text = category.Name, Value = category.Id.ToString() };
                    selectList.Add(option);
                }

                model.CategoryList = selectList;

                return View(model);
            }
            var existProduct = await _productService.GetAsync(x => x.Name.ToLower() == model.Name.ToLower(), asnotracking: true);
            if (existProduct != null)
            {
                ModelState.AddModelError("Name", "Product with this name already exists.");

                var categories = await _categoryService.GetAllAsync(predicate: x => !x.IsDeleted);

                var selectList = new List<SelectListItem>();

                foreach (var category in categories)
                {
                    var option = new SelectListItem { Text = category.Name, Value = category.Id.ToString() };
                    selectList.Add(option);
                }

                model.CategoryList = selectList;

                return View(model);
            }

            var createdProduct = await _productService.AddAsync(model);

            if (createdProduct == null)
            {
                var categories = await _categoryService.GetAllAsync(predicate: x => !x.IsDeleted);

                var selectList = new List<SelectListItem>();

                foreach (var category in categories)
                {
                    var option = new SelectListItem { Text = category.Name, Value = category.Id.ToString() };
                    selectList.Add(option);
                }

                model.CategoryList = selectList;

                return View(model);
            }
            return RedirectToAction("Index", "Product");
        }


        public async Task<IActionResult> Restore(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            product.IsDeleted = false;
            var updateProduct = _mapper.Map<UpdateProductViewModel>(product);

            await _productService.UpdateAsync(updateProduct);

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            product.IsDeleted = true;
            var updateProduct = _mapper.Map<UpdateProductViewModel>(product);

            await _productService.UpdateAsync(updateProduct);

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model  = await _productService.GetUpdateProductModelAsync(id);
            return View(model);
        }
    }
}
