using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;
using static GamingEcommerce.BLL.ViewModels.GeneralViewModels.WebsiteInfoViewModel;

namespace GamingEcommerce.BLL.Services.Contracts
{
    public interface IWebsiteInfoService : IGenericService<WebsiteInfo, CreateWebsiteInfoViewModel, UpdateWebsiteInfoViewModel, WebsiteInfoViewModel> { }
}
