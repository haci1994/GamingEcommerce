using GamingEcommerce.BLL.Mapping;
using GamingEcommerce.BLL.Services.Contracts;
using GamingEcommerce.BLL.Services.GeneralServices;
using GamingEcommerce.BLL.Services.GeneralServices.Services;
using GamingEcommerce.BLL.Services.WebsiteServices;
using GamingEcommerce.DAL.DataContext;
using GamingEcommerce.DAL.DataContext.Contracts;
using GamingEcommerce.DAL.DataContext.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace GamingEcommerce.BLL
{
    public static class BLLServiceRegistration
    {
        public static IServiceCollection BllServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(opt =>
               opt.UseSqlServer(
                   configuration.GetConnectionString("DefaultConnection"),
                   sql => sql.MigrationsAssembly(typeof(AppDbContext).Assembly.FullName)));

            services.AddScoped<DataInitializer>();

            services.AddScoped(typeof(IRepository<>), typeof(EfCoreRepository<>));

            services.AddAutoMapper(cfg => { }, typeof(MappingProfile));

            services.AddScoped(typeof(IGenericService<,,,>), typeof(GenericService<,,,>));

            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IAddressService, AddressService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<IWebsiteInfoService, WebsiteInfoService>();
            services.AddScoped<ILanguageService, LanguageService>();
            services.AddScoped<ICurrencyService, CurrencyService>();
            services.AddScoped<IDiscountCodeService, DiscountCodeService>();
            services.AddScoped<IProductColorService, ProductColorService>();
            services.AddScoped<IProductSizeService, ProductSizeService>();
            services.AddScoped<IProductColorImageService, ProductColorImageService>();
            services.AddScoped<IOrderItemService, OrderItemService>();
            services.AddScoped<ISocialService, SocialViewService>();
            services.AddScoped<IWishlistItemService, WishlistItemService>();
            services.AddScoped<HomeLayoutService>();

            return services;
        }
    }
}
