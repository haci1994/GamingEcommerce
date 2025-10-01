using System.ComponentModel.DataAnnotations;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class UserLoginViewModel
    {
        public required string UserName { get; set; }

        [DataType(DataType.Password)]
        public required string Password { get; set; }
        public string? ReturnUrl { get; set; }
    }
}
