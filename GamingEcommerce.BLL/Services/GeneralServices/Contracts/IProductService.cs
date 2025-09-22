using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.Contracts
{

    public interface IProductService : IGenericService<Product, CreateProductViewModel, UpdateProductViewModel, ProductViewModel>
    {
        Task<List<ProductViewModel>> GetHotDealsAsync(int count);
        Task<List<ProductViewModel>> GetPopularProductsAsync(int count);
        Task<List<ProductViewModel>> GetRecommendedProductsAsync(int count);
    }
}
