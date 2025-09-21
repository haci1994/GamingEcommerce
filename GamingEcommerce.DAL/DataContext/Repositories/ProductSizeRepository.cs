using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class ProductSizeRepository : EfCoreRepository<ProductSize>, IProductSizeInterface
    {
        public ProductSizeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
