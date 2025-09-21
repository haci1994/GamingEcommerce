namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class OrderItem : TimestampedEntity
    {
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        
        // Navigation properties
        public int OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}