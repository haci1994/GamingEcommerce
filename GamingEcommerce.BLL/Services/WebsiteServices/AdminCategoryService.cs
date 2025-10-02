using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.BLL.ViewModels.WebsiteViewModels;
using Microsoft.AspNetCore.Http.Timeouts;

namespace GamingEcommerce.BLL.Services.WebsiteServices
{
   
    public class AdminCategoryService
    {
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;

        public AdminCategoryService(ICategoryService categoryService, IMapper mapper)
        {
            _categoryService = categoryService;
            _mapper = mapper;
        }

        public async Task<AdminCategoryIndexViewModel> GetTableAsync()
        {
            var categories = await _categoryService.GetAllAsync();

            return new AdminCategoryIndexViewModel { Categories = categories };
        }

        public async Task<UpdateCategoryViewModel> GetUpdateModel(int id)
        {
            var category = await _categoryService.GetByIdAsync(id);

            if (category == null) return null;

            return _mapper.Map<UpdateCategoryViewModel>(category);
        }

        
    }
}
