using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Web.Helpers;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class ProductColorService : GenericService<ProductColor, CreateProductColorViewModel, UpdateProductColorViewModel, ProductColorViewModel>, IProductColorService
    {
        private readonly IProductColorInterface _productColorServis;
        private readonly IProductSizeInterface _productSizeServis;
        private readonly IMapper _mapper;
        public ProductColorService(IRepository<ProductColor> repository, IMapper mapper, IProductColorInterface productColorServis, IProductSizeInterface productSizeServis) : base(repository, mapper)
        {
            _productColorServis = productColorServis;
            _productSizeServis = productSizeServis;
            _mapper = mapper;
        }

        public override async Task<ProductColorViewModel> AddAsync(CreateProductColorViewModel model)
        {
            // model.ProductColorImages və model.ProductSizes artıq controller-də doldurulub
            var entity = _mapper.Map<ProductColor>(model); // <-- nested list-lər map üçün 1-ci bənd şərtdir
            await _productColorServis.AddAsync(entity);    // SaveChanges içindədir

            var newColor = await _productColorServis.GetAsync(
                x => x.Id == entity.Id,
                include: q => q
                    .Include(z => z.ProductColorImages)
                    .Include(z => z.ProductSizes),
                asnotracking: true
            );

            return new ProductColorViewModel
            {
                Id = newColor.Id,
                Name = newColor.Name,
                HexCode = newColor.HexCode,
                ProductColorImages = _mapper.Map<List<ProductColorImageViewModel>>(newColor.ProductColorImages),
                ProductSizes = _mapper.Map<List<ProductSizeViewModel>>(newColor.ProductSizes)
            };
        }

    }
}
