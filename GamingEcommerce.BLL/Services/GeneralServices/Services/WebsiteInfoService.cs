using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using static GamingEcommerce.BLL.ViewModels.GeneralViewModels.WebsiteInfoViewModel;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class WebsiteInfoService : GenericService<WebsiteInfo, CreateWebsiteInfoViewModel, UpdateWebsiteInfoViewModel, WebsiteInfoViewModel>, IWebsiteInfoService
    {
        public WebsiteInfoService(IRepository<WebsiteInfo> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
