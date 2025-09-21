namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class WishlistItem : TimestampedEntity
    {
        public int ProductId { get; set; }

        // Navigation properties
        public string AppUserId { get; set; } = null!;
        public AppUser? AppUser { get; set; }
    }
}