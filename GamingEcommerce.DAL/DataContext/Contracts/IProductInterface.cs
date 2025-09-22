using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Contracts
{
    public interface IProductInterface : IRepository<Product>
    {
        Task<List<Product>> GetHotDealsAsync(int count);
        Task<List<Product>> GetPopularProductsAsync(int count);
        Task<List<Product>> GetRecommendedProductsAsync(int count);
    }
}
