using GamingEcommerce.BLL.ViewModels.GeneralViewModels;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class AddressPageViewModel
    {
        public AddressViewModel DefaultAddress { get; set; } = null!;
        public List<AddressViewModel> OtherAddresses { get; set; } = [];
    }
}
