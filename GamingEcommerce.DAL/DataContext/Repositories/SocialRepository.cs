using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class SocialRepository : EfCoreRepository<Social>, ISocialInterface
    {
        public SocialRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
