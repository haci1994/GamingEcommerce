using Microsoft.AspNetCore.Http;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class ProductColorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? HexCode { get; set; }
        public List<ProductColorImageViewModel> ProductColorImages { get; set; } = new();
        public List<ProductSizeViewModel> Sizes { get; set; } = new();
    }

    public class CreateProductColorViewModel
    {
        public string Name { get; set; } = null!;
        public int ProductId { get; set; }
        public string? HexCode { get; set; }
        public List<IFormFile> Images { get; set; } = new();
        public List<CreateProductSizeViewModel> Sizes { get; set; } = new();
    }

    public class UpdateProductColorViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? HexCode { get; set; }
        public List<IFormFile>? NewImages { get; set; } // yeni şəkillər
        public List<ProductSizeViewModel> Sizes { get; set; } = new();
    }

}
