using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class WebsiteInfoRepository : EfCoreRepository<WebsiteInfo>, IWebsiteInfoInterface
    {
        public WebsiteInfoRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
