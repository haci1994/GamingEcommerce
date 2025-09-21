namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class DiscountCode : TimestampedEntity
    {
        public required string Name { get; set; }
        public required string Code { get; set; }
        public decimal Percentage { get; set; }
        public int MaxUsageCount { get; set; }
        public bool IsActive { get; set; } = true;

        //Navigation property
        public List<Order> Orders { get; set; } = [];
    }
}