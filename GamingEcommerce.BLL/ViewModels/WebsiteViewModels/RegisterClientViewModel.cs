using System.ComponentModel.DataAnnotations;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class RegisterClientViewModel
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string UserName { get; set; }

        [DataType(DataType.EmailAddress)]
        public required string Email { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
        
        [DataType(DataType.Password)]
        [Compare("Password")]
        public required string ConfirmPassword { get; set; }

    }

    public class UserLoginViewModel
    {
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
    }
}
