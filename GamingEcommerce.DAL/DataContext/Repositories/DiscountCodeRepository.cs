using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class DiscountCodeRepository : EfCoreRepository<DiscountCode>, IDiscountCodeInterface
    {
        public DiscountCodeRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
