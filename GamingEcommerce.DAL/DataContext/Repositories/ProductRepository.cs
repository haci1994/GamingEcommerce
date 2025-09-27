using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace GamingEcommerce.DAL.DataContext.Repositories
{
    public class ProductRepository : EfCoreRepository<Product>, IProductInterface
    {
        private readonly IOrderItemInterface _orderItemService;
        public ProductRepository(AppDbContext dbContext, IOrderItemInterface orderItemService) : base(dbContext)
        {
            _orderItemService = orderItemService;
        }

        public override async Task<Product?> GetByIdAsync(int id)
        {
            var product = DbContext.Products
                .Include(x => x.ProductColors).ThenInclude(z => z.ProductColorImages)
                .Include(x => x.ProductColors).ThenInclude(z => z.ProductSizes)
                .FirstOrDefault(x => x.Id == id);

            if (product == null)
            {
                return new Product
                {
                    Id = 0,
                    Name = string.Empty,
                };
            }

            return product;
        }

        public async Task<List<Product>> GetHotDealsAsync(int count)
        {
            var list = DbContext.Products
                .Where(p => p.DiscountPrice > 0)
                .OrderBy(p => p.CreatedAt) // Stoka çoxdan artırılan məhsullar görünsün birinci
                .Take(count)
                .Include(x => x.ProductColors).ThenInclude(z => z.ProductSizes)
                .Include(x => x.ProductColors).ThenInclude(z => z.ProductColorImages);

            if (list == null || !list.Any())
            {
                return new List<Product>();
            }

            var listResult = await list.ToListAsync();

            return listResult;
        }

        public async Task<List<Product>> GetPopularProductsAsync(int count)
        {
            var list = DbContext.Products
                .OrderByDescending(p => p.ViewCount)
                .Take(count)
                .Include(x => x.ProductColors).ThenInclude(z => z.ProductSizes)
                .Include(x => x.ProductColors).ThenInclude(z => z.ProductColorImages);

            if (list == null || !list.Any())
            {
                return new List<Product>();
            }

            var listResult = await list.ToListAsync();

            return listResult;
        }

        public async Task<List<Product>> GetRecommendedProductsAsync(int count)
        {
            var recommendedProductIds = await DbContext.OrderItems
                .GroupBy(o => o.ProductId)
                .OrderByDescending(g => g.Count())
                .Select(g => g.Key)
                .Take(count)
                .ToListAsync();

            var products = await DbContext.Products
             .Where(p => recommendedProductIds.Contains(p.Id))
             .Include(x => x.ProductColors).ThenInclude(z => z.ProductSizes)
             .Include(x => x.ProductColors).ThenInclude(z => z.ProductColorImages)
             .ToListAsync();

            return products;
        }
    }

}
