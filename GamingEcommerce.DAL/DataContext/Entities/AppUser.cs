using Microsoft.AspNetCore.Identity;

namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class AppUser : IdentityUser
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }

        // Navigation property
        public List<Address> Addresses { get; set; } = [];
        public List<Order> Orders { get; set; } = [];
        public List<WishlistItem> WishlistItems { get; set; } = [];
    }
}