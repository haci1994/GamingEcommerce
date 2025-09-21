using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class OrderRepository : EfCoreRepository<Order>, IOrderInterface
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
