using GamingEcommerce.BLL.ViewModels.GeneralViewModels;

namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class CheckoutPageViewModel
    {
        public AddressViewModel? DefaultAddress { get; set; }
        public List<BasketItemViewModel> Products { get; set; } = [];
    }
}
