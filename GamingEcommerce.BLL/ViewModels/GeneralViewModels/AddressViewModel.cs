using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class AddressViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string AddressLine { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string Province { get; set; }
        public required string PostalZipCode { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
        public bool IsDeleted { get; set; }
        public string UserId { get; set; } = null!;
    }

    public class CreateAddressViewModel
    {
        public string FirstName { get; set; } = null!;
        public  string LastName { get; set; } = null!;
        public  string AddressLine { get; set; } = null!;
        public  string City { get; set; } = null!;
        public  string Country { get; set; } = null!;
        public string Province { get; set; } = null!;
        public  string PostalZipCode { get; set; } = null!;
        public  string PhoneNumber { get; set; } = null!;
        public bool IsDefault { get; set; }
        public string? UserId { get; set; }

        public bool IsDeleted { get; set; }

    }

    public class UpdateAddressViewModel
    {
        public int Id { get; set; }
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string AddressLine { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string Province { get; set; }
        public required string PostalZipCode { get; set; }
        public required string PhoneNumber { get; set; }
        public bool IsDefault { get; set; }
        public string? UserId { get; set; }
        public bool IsDeleted { get; set; }


    }
}
