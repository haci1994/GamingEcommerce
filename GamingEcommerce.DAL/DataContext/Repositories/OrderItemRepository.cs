using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class OrderItemRepository : EfCoreRepository<OrderItem>, IOrderItemInterface
    {
        public OrderItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
