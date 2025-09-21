using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.Contracts
{
    public interface ISocialService : IGenericService<Social, CreateSocialViewModel, UpdateSocialViewModel, SocialViewModel> { }
}
