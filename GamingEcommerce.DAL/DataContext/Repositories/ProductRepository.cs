using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class ProductRepository : EfCoreRepository<Product>, IProductInterface
    {
        public ProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
