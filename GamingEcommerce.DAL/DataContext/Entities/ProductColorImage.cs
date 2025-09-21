namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class ProductColorImage : TimestampedEntity
    {
        public required string ImageName { get; set; }

        // Navigation property
        public int ProductColorId { get; set; }
        public ProductColor? ProductColor { get; set; }
    }
}