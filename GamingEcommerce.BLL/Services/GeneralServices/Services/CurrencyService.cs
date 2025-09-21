using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class CurrencyService : GenericService<Currency, CreateCurrencyViewModel, UpdateCurrencyViewModel, CurrencyViewModel>, ICurrencyService
    {
        public CurrencyService(IRepository<Currency> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
