using AutoMapper;
using GamingEcommerce.BLL.ViewModels.GeneralViewModels;
using GamingEcommerce.DAL.DataContext.Entities;
using static GamingEcommerce.BLL.ViewModels.GeneralViewModels.WebsiteInfoViewModel;

namespace GamingEcommerce.BLL.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Address, AddressViewModel>().ReverseMap();
            CreateMap<Address, CreateAddressViewModel>().ReverseMap();
            CreateMap<Address, UpdateAddressViewModel>().ReverseMap();

            CreateMap<Category, CategoryViewModel>().ReverseMap();
            CreateMap<Category, CreateCategoryViewModel>().ReverseMap();
            CreateMap<Category, UpdateCategoryViewModel>().ReverseMap();

            CreateMap<DiscountCode, DiscountCodeViewModel>().ReverseMap();
            CreateMap<DiscountCode, CreateDiscountCodeViewModel>().ReverseMap();
            CreateMap<DiscountCode, UpdateDiscountCodeViewModel>().ReverseMap();

            CreateMap<Language, LanguageViewModel>().ReverseMap();
            CreateMap<Language, CreateLanguageViewModel>().ReverseMap();
            CreateMap<Language, UpdateLanguageViewModel>().ReverseMap();

            CreateMap<Currency, CurrencyViewModel>().ReverseMap();
            CreateMap<Currency, CreateCurrencyViewModel>().ReverseMap();
            CreateMap<Currency, UpdateCurrencyViewModel>().ReverseMap();

            CreateMap<OrderItem, OrderItemViewModel>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemViewModel>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemViewModel>().ReverseMap();

            CreateMap<Order, OrderViewModel>().ReverseMap();
            CreateMap<Order, CreateOrderViewModel>().ReverseMap();
            CreateMap<Order, UpdateOrderViewModel>().ReverseMap();

            CreateMap<ProductColorImage, ProductColorImageViewModel>().ReverseMap();
            CreateMap<ProductColorImage, CreateProductColorImageViewModel>().ReverseMap();
            CreateMap<ProductColorImage, UpdateProductColorImageViewModel>().ReverseMap();

            CreateMap<ProductColor, ProductColorViewModel>().ReverseMap();
            CreateMap<ProductColor, CreateProductColorViewModel>().ReverseMap();
            CreateMap<ProductColor, UpdateProductColorViewModel>().ReverseMap();

            CreateMap<ProductSize, ProductSizeViewModel>().ReverseMap();
            CreateMap<ProductSize, CreateProductSizeViewModel>().ReverseMap();
            CreateMap<ProductSize, UpdateProductSizeViewModel>().ReverseMap();

            CreateMap<Product, ProductViewModel>().ReverseMap();
            CreateMap<Product, CreateProductViewModel>().ReverseMap();
            CreateMap<Product, UpdateProductViewModel>().ReverseMap();

            CreateMap<Social, SocialViewModel>().ReverseMap();
            CreateMap<Social, CreateSocialViewModel>().ReverseMap();
            CreateMap<Social, UpdateSocialViewModel>().ReverseMap();

            CreateMap<WebsiteInfo, WebsiteInfoViewModel>().ReverseMap();
            CreateMap<WebsiteInfo, CreateWebsiteInfoViewModel>().ReverseMap();
            CreateMap<WebsiteInfo, UpdateWebsiteInfoViewModel>().ReverseMap();

            CreateMap<WishlistItem, WishlistItemViewModel>().ReverseMap();
            CreateMap<WishlistItem, CreateWishlistItemViewModel>().ReverseMap();
            CreateMap<WishlistItem, UpdateWishlistItemViewModel>().ReverseMap();
        }
    }
}
