using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class ProductColorService : GenericService<ProductColor, CreateProductColorViewModel, UpdateProductColorViewModel, ProductColorViewModel>, IProductColorService
    {
        public ProductColorService(IRepository<ProductColor> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
