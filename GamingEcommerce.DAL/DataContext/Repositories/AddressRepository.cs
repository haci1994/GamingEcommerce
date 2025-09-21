using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class AddressRepository : EfCoreRepository<Address>, IAddressInterface
    {
        public AddressRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }

}
