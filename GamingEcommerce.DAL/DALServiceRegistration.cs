using GamingEcommerce.DAL.DataContext;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Repositories;

namespace GamingEcommerce.DAL
{
    public static class DALServiceRegistration
    {
        public static IServiceCollection AddDALServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"), options =>
                {
                    options.MigrationsAssembly(typeof(DALServiceRegistration).Assembly.FullName);
                }));

            services.AddScoped<DataInitializer>();

            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));
            services.AddScoped<IAddressInterface,AddressRepository>();
            services.AddScoped<ICategoryInterface, CategoryRepository>();
            services.AddScoped<ICurrencyInterface, CurrencyRepository>();
            services.AddScoped<IDiscountCodeInterface, DiscountCodeRepository>();
            services.AddScoped<ILanguageInterface, LanguageRepository>();
            services.AddScoped<IOrderInterface, OrderRepository>();
            services.AddScoped<IOrderItemInterface, OrderItemRepository>();
            services.AddScoped<IProductColorImageInterface, ProductColorImageRepository>();
            services.AddScoped<IProductColorInterface, ProductColorRepository>();
            services.AddScoped<IProductInterface, ProductRepository>();
            services.AddScoped<IProductSizeInterface, ProductSizeRepository>();
            services.AddScoped<ISocialInterface, SocialRepository>();
            services.AddScoped<IWebsiteInfoInterface, WebsiteInfoRepository>();
            services.AddScoped<IWishlistItemInterface, WishlistItemRepository>();

            return services;
        }
    }
}
