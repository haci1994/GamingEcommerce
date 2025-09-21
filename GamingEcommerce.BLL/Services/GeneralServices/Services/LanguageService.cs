using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class LanguageService : GenericService<Language, CreateLanguageViewModel, UpdateLanguageViewModel, LanguageViewModel>, ILanguageService
    {
        public LanguageService(IRepository<Language> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
