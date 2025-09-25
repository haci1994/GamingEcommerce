namespace GamingEcommerce.BLL.ViewModels.WebsiteViewModels
{
    public class BasketItemViewModel
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Color { get; set; }
        public int Count { get; set; } = 1;
        public string ImageName { get; set; }
    }

    public class BasketViewModel
    {
        public List<BasketItemViewModel> Items { get; set; } = [];
        public decimal TotalPrice { get; set; }
    }
}
