using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.Contracts
{

    public interface IProductService : IGenericService<Product, CreateProductViewModel, UpdateProductViewModel, ProductViewModel> { }
}
