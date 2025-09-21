using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class ProductColorImageRepository : EfCoreRepository<ProductColorImage>, IProductColorImageInterface
    {
        public ProductColorImageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
