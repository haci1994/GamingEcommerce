using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.WebsiteServices;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GamingEcommerce.MVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly AdminCategoryService _adminCategoryService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public CategoryController(AdminCategoryService adminCategoryService, IWebHostEnvironment webHostEnvirontment, ICategoryService categoryService, IMapper mapper)
        {
            _adminCategoryService = adminCategoryService;
            _webHostEnvironment = webHostEnvirontment;
            _categoryService = categoryService;
            _mapper = mapper;
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateCategoryViewModel model)
        {
           
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error Update");
                return View(model);
            }


            if(model.Name != null)
            {
                var category = await _categoryService.GetByIdAsync(id);
                if (category == null) return NotFound();

                var mappedCategory = _mapper.Map<UpdateCategoryViewModel>(category);

                mappedCategory.Name = model.Name;
                await _categoryService.UpdateAsync(mappedCategory);
            }

            if(model.Image != null)
            {
                if (!model.Image.Name.Contains("Image"))
                {
                    ModelState.AddModelError("Image", "Select ImageFile");
                    return View(model);
                }

                if (model.Image.Length > 1024 * 1024 * 2)
                {
                    ModelState.AddModelError("Image", "Selected image size is larger than 2 MB!");
                    return View(model);
                }

                
                var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "collections");
                var imageName = $"{Path.GetFileNameWithoutExtension(model.Image.FileName)}-{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
                var totalpath = Path.Combine(path, imageName);

                if (!string.IsNullOrEmpty(model.ImageName))
                {
                    var oldPath = Path.Combine(path, model.ImageName);
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                var fs = new FileStream(totalpath, FileMode.Create);

                await model.Image.CopyToAsync(fs);
                
                fs.Close();

                model.ImageName = imageName;

                await _categoryService.UpdateAsync(model);
            }


            return RedirectToAction("Index", "Category");
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Error Adding Category");
                return View(model);
            }

            if (!model.Image.Name.Contains("Image"))
            {
                ModelState.AddModelError("Image", "Select ImageFile");
                return View(model);
            }

            if (model.Image.Length > 1024 * 1024 * 2)
            {
                ModelState.AddModelError("Image", "Selected image size is larger than 2 MB!");
                return View(model);
            }

            if (model == null)
            {

                return View(model);
            }

            var path = Path.Combine(_webHostEnvironment.WebRootPath, "images", "collections");
            var imageName = $"{Path.GetFileNameWithoutExtension(model.Image.FileName)}-{Guid.NewGuid()}{Path.GetExtension(model.Image.FileName)}";
            var totalpath = Path.Combine(path, imageName);

            var fs = new FileStream(totalpath, FileMode.Create);

            await model.Image.CopyToAsync(fs);
            fs.Close();

            model.ImageName = imageName;

            await _categoryService.AddAsync(model);


            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Restore (int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null) return NotFound();

            category.IsDeleted = false;
            var updateCategory = _mapper.Map<UpdateCategoryViewModel>(category);

            await _categoryService.UpdateAsync(updateCategory);

            return RedirectToAction("Index", "Category");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null) return NotFound();

            category.IsDeleted = true;
            var updateCategory = _mapper.Map<UpdateCategoryViewModel>(category);

            await _categoryService.UpdateAsync(updateCategory);

            return RedirectToAction("Index", "Category");
        }
    }
}
