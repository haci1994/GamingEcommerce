using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class WishlistItemRepository : EfCoreRepository<WishlistItem>, IWishlistItemInterface
    {
        public WishlistItemRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
