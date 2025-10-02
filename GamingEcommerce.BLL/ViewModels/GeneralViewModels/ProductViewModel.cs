using Microsoft.AspNetCore.Mvc.Rendering;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? AdditionalInformation { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }

        // Category info
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        // Nested collections
        public List<ProductColorViewModel> ProductColors { get; set; } = new();
    }

    public class CreateProductViewModel
    {
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? AdditionalInformation { get; set; }

        public int CategoryId { get; set; }
        public List<SelectListItem> CategoryList { get; set; } = new();

        // Nested collections
        public List<CreateProductColorViewModel> ProductColors { get; set; } = new();
    }

    public class UpdateProductViewModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public string? AdditionalInformation { get; set; }
        public bool IsDeleted { get; set; }
        public int ViewCount { get; set; }

        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        public List<UpdateProductColorViewModel> ProductColors { get; set; } = new();
    }
}
