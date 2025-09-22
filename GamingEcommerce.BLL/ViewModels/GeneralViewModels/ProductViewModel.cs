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
        public int ViewCount { get; set; }

        // Category info
        public int CategoryId { get; set; }
        public string? CategoryName { get; set; }

        // Nested collections
        public List<ProductColorViewModel> ProductColors { get; set; } = [];
    }

    public class CreateProductViewModel
    {

    }

    public class UpdateProductViewModel
    {

    }
}
