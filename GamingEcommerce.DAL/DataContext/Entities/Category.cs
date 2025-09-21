namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class Category : TimestampedEntity
    {
        public required string Name { get; set; }
        public required string ImageName { get; set; }

        // Navigation property
        public List<Product> Products { get; set; } = [];
    }
}