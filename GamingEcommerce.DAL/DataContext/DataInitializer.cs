using Microsoft.EntityFrameworkCore;

namespace GamingEcommerce.DAL.DataContext
{
    public class DataInitializer
    {
        private readonly AppDbContext _dbContext;

        public DataInitializer(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task Initialize()
        {
            await _dbContext.Database.MigrateAsync();
        }
    }
}
