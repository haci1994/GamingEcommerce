namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class  Order : TimestampedEntity
    {
        public OrderStatus Status { get; set; } = OrderStatus.Pending;
        public decimal TotalAmount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal FinalAmount { get; set; }

        // Navigation property
        public string UserId { get; set; } = null!;
        public AppUser? User { get; set; }

        public int? DiscountCodeId { get; set; }
        public DiscountCode? DiscountCode { get; set; }

        public List<OrderItem> OrderItems { get; set; } = [];
    }

    public enum OrderStatus
    {
        Pending,
        Processing,
        Shipped,
        Delivered,
        Cancelled
    }
}