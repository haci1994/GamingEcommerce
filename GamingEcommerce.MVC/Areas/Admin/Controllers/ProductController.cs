using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.GeneralServices;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
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

        public ProductController(IProductService productService, IMapper mapper, IProductColorService productColorService, IProductColorImageService productColorImageService, IProductSizeService productSizeService, IWebHostEnvironment webHostEnvironment)
        {
            _productService = productService;
            _mapper = mapper;
            _productColorService = productColorService;
            _productColorImageService = productColorImageService;
            _productSizeService = productSizeService;
            _webHostEnvironment = webHostEnvironment;
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
                ModelState.AddModelError("", "Error Adding Product");
                return View(model);
            }

            // Əsas product-u yarat
            var productVm = await _productService.AddAsync(model); // GenericService-dən
            var productId = productVm.Id;

            // Əgər rənglər boşdursa, hazırdır
            if (model.ProductColors == null || model.ProductColors.Count == 0)
                return RedirectToAction("Index", "Product");

            foreach (var c in model.ProductColors)
            {
                // Rəngi yarat
                var colorVm = await _productColorService.AddAsync(new CreateProductColorViewModel
                {
                    ProductId = productId,
                    Name = c.Name,
                    HexCode = c.HexCode
                });
                var colorId = colorVm.Id;

                // şəkillər üçün folder
                var folder = Path.Combine(_webHostEnvironment.WebRootPath,
                                          "images", "products",
                                          productId.ToString(),
                                          colorId.ToString());
                Directory.CreateDirectory(folder);

                // Şəkillər
                if (c.Images != null)
                {
                    foreach (var file in c.Images)
                    {
                        if (file == null || file.Length == 0) continue;
                        if (!file.ContentType.StartsWith("image/"))
                        {
                            ModelState.AddModelError("", "Only image files are allowed.");
                            return View(model);
                        }
                        if (file.Length > 2 * 1024 * 1024)
                        {
                            ModelState.AddModelError("", "Image must be ≤ 2MB.");
                            return View(model);
                        }

                        var ext = Path.GetExtension(file.FileName);
                        var fileName = $"{Guid.NewGuid():N}{ext}";
                        var filePath = Path.Combine(folder, fileName);

                        using (var fs = new FileStream(filePath, FileMode.Create))
                            await file.CopyToAsync(fs);

                        await _productColorImageService.AddAsync(new CreateProductColorImageViewModel
                        {
                            ProductColorId = colorId,
                            ImageName = fileName
                        });
                    }
                }

                // Ölçülər
                if (c.Sizes != null)
                {
                    foreach (var s in c.Sizes)
                    {
                        if (string.IsNullOrWhiteSpace(s.Name)) continue;

                        await _productSizeService.AddAsync(new CreateProductSizeViewModel
                        {
                            ProductColorId = colorId,
                            Name = s.Name.Trim(),
                            Stock = Math.Max(0, s.Stock)
                        });
                    }
                }
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
    }
}
