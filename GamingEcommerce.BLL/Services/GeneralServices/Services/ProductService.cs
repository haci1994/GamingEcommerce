using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using GamingEcommerce.DAL.DataContext.Repositories;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class ProductService : GenericService<Product, CreateProductViewModel, UpdateProductViewModel, ProductViewModel>, IProductService
    {
        private readonly IProductInterface _productRepository;
        private readonly IMapper _mapper;
        public ProductService(IRepository<Product> repository, IMapper mapper, IProductInterface productRepository) : base(repository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<List<ProductViewModel>> GetHotDealsAsync(int count)
        {
            var productsFromDb = await _productRepository.GetHotDealsAsync(count);

            var products = _mapper.Map<List<ProductViewModel>>(productsFromDb);

            return products;
        }

        public async Task<List<ProductViewModel>> GetPopularProductsAsync(int count)
        {
            var productsFromDb = await _productRepository.GetPopularProductsAsync(count);

            var products = _mapper.Map<List<ProductViewModel>>(productsFromDb);

            return products;
        }

        public async Task<List<ProductViewModel>> GetRecommendedProductsAsync(int count)
        {
            var productsFromDb = await _productRepository.GetRecommendedProductsAsync(count);

            var products = _mapper.Map<List<ProductViewModel>>(productsFromDb);

            return products;
        }
    }
}
