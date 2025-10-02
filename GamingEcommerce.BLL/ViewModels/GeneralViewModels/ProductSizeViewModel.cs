namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class ProductSizeViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
    }

    public class CreateProductSizeViewModel
    {
        public string? Name { get; set; }
        public int Stock { get; set; }
        public int ProductColorId { get; set; }
    }

    public class UpdateProductSizeViewModel
    {
        public int Id { get; set; }   // mövcud size-i tapmaq üçün
        public string Name { get; set; } = null!;
        public int Stock { get; set; }
    }
}
