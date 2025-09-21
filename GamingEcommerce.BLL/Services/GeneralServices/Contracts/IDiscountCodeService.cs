using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.Contracts
{
    public interface IDiscountCodeService : IGenericService<DiscountCode, CreateDiscountCodeViewModel, UpdateDiscountCodeViewModel, DiscountCodeViewModel> { }
}
