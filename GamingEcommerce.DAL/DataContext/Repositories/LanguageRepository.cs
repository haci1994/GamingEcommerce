using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class LanguageRepository : EfCoreRepository<Language>, ILanguageInterface
    {
        public LanguageRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
