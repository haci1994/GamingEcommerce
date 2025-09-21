using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.Contracts
{
    public interface ICategoryService : IGenericService<Category, CreateCategoryViewModel, UpdateCategoryViewModel, CategoryViewModel> { }
}
