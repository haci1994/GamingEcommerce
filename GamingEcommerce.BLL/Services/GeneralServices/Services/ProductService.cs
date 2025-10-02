using AutoMapper;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using GamingEcommerce.DAL.DataContext.Repositories;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GamingEcommerce.BLL.Services.GeneralServices
{
    public class ProductService : GenericService<Product, CreateProductViewModel, UpdateProductViewModel, ProductViewModel>, IProductService
    {
        private readonly IProductInterface _productRepository;
        private readonly ICategoryService _categoryService;
        private readonly IMapper _mapper;
        public ProductService(IRepository<Product> repository, IMapper mapper, IProductInterface productRepository, ICategoryService categoryService) : base(repository, mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _categoryService = categoryService;
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

        public override async Task<ProductViewModel?> GetByIdAsync(int id)
        {
            var productFromDb = await _productRepository.GetByIdAsync(id);

            if(productFromDb == null)
            {
                return null;
            }

            var product = _mapper.Map<ProductViewModel>(productFromDb);

            return product;
        }

        public override async Task<List<ProductViewModel>> GetAllAsync(Expression<Func<Product, bool>>? predicate = null, Func<IQueryable<Product>, IIncludableQueryable<Product, object>>? include = null, Func<IQueryable<Product>, IOrderedQueryable<Product>>? orderBy = null, bool asnotracking = false)
        {
            var productsFromDb = await _productRepository.GetAllAsync(predicate, include, orderBy, asnotracking);            

            var products = _mapper.Map<List<ProductViewModel>>(productsFromDb);

            return products;
        }

        public async Task<CreateProductViewModel> GetCreateProductModelAsync()
        {
            var categories = await _categoryService.GetAllAsync(predicate: x=> !x.IsDeleted);

            var selectList = new List<SelectListItem>();

            foreach (var category in categories)
            {
                var option = new SelectListItem { Text = category.Name, Value= category.Id.ToString()};
                selectList.Add(option);
            }

            var productModel = new CreateProductViewModel
            {
                CategoryList = selectList
            };

            return productModel;
        }
    }
}
