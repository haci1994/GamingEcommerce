using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class ProductColorRepository : EfCoreRepository<ProductColor>, IProductColorInterface
    {
        public ProductColorRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
