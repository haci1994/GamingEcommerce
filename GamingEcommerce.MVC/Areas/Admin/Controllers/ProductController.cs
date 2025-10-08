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
            var allProducts = await _productService.GetAllAsync(include: x => x.Include(x => x.ProductColors).ThenInclude(z => z.ProductColorImages).Include(p => p.ProductColors)
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
            var model = await _productService.GetUpdateProductModelAsync(id);
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(UpdateProductViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            
            var updatedProduct = await _productService.UpdateAsync(model);

            if (updatedProduct == null)
            {
                ModelState.AddModelError("", "Something went wrong");
                return View(model);
            }

            return RedirectToAction("Index", "Product");
        }

        public async Task<IActionResult> AddProductColor(int id)
        {
            var model = new CreateProductColorViewModel();
            var product = await _productService.GetByIdAsync(id);

            if (product == null) return NotFound();

            model.ProductName = product.Name;
            model.ProductId = id;

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddProductColor(CreateProductColorViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error");
                return View(model);
            }

            if (model.ProductId <= 0) return BadRequest("Invalid product id.");

            var product = await _productService.GetByIdAsync(model.ProductId);
            if (product == null) return BadRequest();

            foreach(var color in product.ProductColors)
            {
                if(color.Name == model.Name || color.HexCode == model.HexCode)
                {
                    ModelState.AddModelError("", "Color is exist!");
                    return View(model);
                }
            }

            foreach(var image in model.Images)
            {
                if (!image.ContentType.StartsWith("image/"))
                {
                    ModelState.AddModelError("", "Select ImageFile");
                    return View(model);
                }

                if (image.Length > 1024 * 1024 * 2)
                {
                    ModelState.AddModelError("", "Selected image size is larger than 2 MB!");
                    return View(model);
                }
            }

            foreach (var image in model.Images)
            {
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "products");
                var imageName = $"{Path.GetFileNameWithoutExtension(image.FileName)}-{Guid.NewGuid()}{Path.GetExtension(image.FileName)}";
                var totalpath = Path.Combine(path, imageName);

                var fs = new FileStream(totalpath, FileMode.Create);

                await image.CopyToAsync(fs);
                fs.Close();

                model.ProductColorImages.Add(new CreateProductColorImageViewModel { ImageName=imageName});
            }

            var listSizes = new List<CreateProductSizeViewModel>();

            foreach (var size in model.Sizes)
            {
                var newSize = new CreateProductSizeViewModel
                {
                    Name = size
                };
                listSizes.Add(newSize);
            }

            var newProductColor = new CreateProductColorViewModel
            {
                HexCode = model.HexCode,
                Name = model.Name,
                ProductColorImages = model.ProductColorImages,
                ProductId = model.ProductId,
                ProductSizes = listSizes                
            };

            await _productColorService.AddAsync(newProductColor);

            return RedirectToAction("Index","Product");
        }

        public async Task<IActionResult> DeleteProductColor(int id)
        {
            var existColor = await _productColorService.GetAsync(predicate: x => x.Id == id, asnotracking: false);

            if (existColor == null) return BadRequest();

            existColor.IsDeleted = true;

            var updateColor = new UpdateProductColorViewModel
            {
                HexCode = existColor.HexCode,
                Id = id,
                Name = existColor.Name,
                IsDeleted = true,
                ProductId = existColor.ProductId
            };

            await _productColorService.UpdateAsync(updateColor);

            return RedirectToAction("Edit","Product", new { id = existColor.ProductId });
        }

        public async Task<IActionResult> RestoreProductColor(int id)
        {
            var existColor = await _productColorService.GetAsync(predicate: x => x.Id == id, asnotracking: false);

            if (existColor == null) return BadRequest();

            existColor.IsDeleted = true;

            var updateColor = new UpdateProductColorViewModel
            {
                HexCode = existColor.HexCode,
                Id = id,
                Name = existColor.Name,
                IsDeleted = false,
                ProductId = existColor.ProductId
            };

            await _productColorService.UpdateAsync(updateColor);

            return RedirectToAction("Edit", "Product", new {id = existColor.ProductId });
        }
    }
}
