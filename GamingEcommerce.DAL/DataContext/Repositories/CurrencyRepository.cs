using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class CurrencyRepository : EfCoreRepository<Currency>, ICurrencyInterface
    {
        public CurrencyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
