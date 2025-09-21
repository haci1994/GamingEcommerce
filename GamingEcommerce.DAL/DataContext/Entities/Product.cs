namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class Product : TimestampedEntity
    {
        public decimal Price { get; set; }
        public decimal? DiscountPrice { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public string? AdditionalInformation { get; set; }

        // Navigation property
        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<OrderItem> OrderItems { get; set; } = [];
        public List<ProductColor> ProductColors { get; set; } = [];
    }
}