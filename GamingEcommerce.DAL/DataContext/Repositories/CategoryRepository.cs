using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class CategoryRepository : EfCoreRepository<Category>, ICategoryInterface
    {
        public CategoryRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
