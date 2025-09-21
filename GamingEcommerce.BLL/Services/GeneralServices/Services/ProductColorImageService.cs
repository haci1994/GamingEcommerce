using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class ProductColorImageService : GenericService<ProductColorImage, CreateProductColorImageViewModel, UpdateProductColorImageViewModel, ProductColorImageViewModel>, IProductColorImageService
    {
        public ProductColorImageService(IRepository<ProductColorImage> repository, IMapper mapper) : base(repository, mapper) { }
    }
}
