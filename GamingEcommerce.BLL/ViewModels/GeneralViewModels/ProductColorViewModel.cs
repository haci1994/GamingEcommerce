namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class ProductColorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? HexCode { get; set; }

        public List<ProductColorImageViewModel> ProductColorImages { get; set; } = [];
        public List<ProductSizeViewModel> ProductSizes { get; set; } = [];
    }

    public class CreateProductColorViewModel
    {
    }

    public class UpdateProductColorViewModel
    {
    }
}
