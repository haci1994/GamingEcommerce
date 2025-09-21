using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class SocialViewService : GenericService<Social, CreateSocialViewModel, UpdateSocialViewModel, SocialViewModel>, ISocialService
    {
        public SocialViewService(IRepository<Social> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
