namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class ProductColor : TimestampedEntity
    {
        public required string Name { get; set; }
        public string? HexCode { get; set; }

        // Navigation property
        public int ProductId { get; set; }
        public Product? Product { get; set; }
        public List<ProductColorImage> ProductColorImages { get; set; } = [];
        public List<ProductSize> ProductSizes { get; set; } = [];
    }
}