using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class WishlistItemService : GenericService<WishlistItem, CreateWishlistItemViewModel, UpdateWishlistItemViewModel, WishlistItemViewModel>, IWishlistItemService
    {
        public WishlistItemService(IRepository<WishlistItem> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
