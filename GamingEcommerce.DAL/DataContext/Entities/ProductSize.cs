namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class ProductSize : TimestampedEntity
    {
        public required string Name { get; set; }
        public int Stock { get; set; }

        // Navigation property
        public int ProductColorId { get; set; }
        public ProductColor? ProductColor { get; set; }
    }
}