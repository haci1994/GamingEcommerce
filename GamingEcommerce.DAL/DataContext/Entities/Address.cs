namespace GamingEcommerce.DAL.DataContext.Entities
{
    public class Address : TimestampedEntity
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string AddressLine { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string Province { get; set; }
        public required string PostalZipCode { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }

        // Navigation property
        public string UserId { get; set; } = null!;
        public AppUser? User { get; set; }
    }
}