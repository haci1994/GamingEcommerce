using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class ProductService : GenericService<Product, CreateProductViewModel, UpdateProductViewModel, ProductViewModel>, IProductService
    {
        public ProductService(IRepository<Product> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
