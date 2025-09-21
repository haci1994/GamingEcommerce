using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class AddressService : GenericService<Address, CreateAddressViewModel, UpdateAddressViewModel, AddressViewModel>, IAddressService
    {
        public AddressService(IRepository<Address> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
