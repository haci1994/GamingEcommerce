using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class ProductColorImageViewModel
    {
        public int Id { get; set; }
        public string ImageName { get; set; } = null!;
        public int ProductColorId { get; set; }
    }

    public class CreateProductColorImageViewModel
    {
        public string? ImageName { get; set; }

        // Navigation property
        public int ProductColorId { get; set; }
        
    }

    public class UpdateProductColorImageViewModel
    {
    }
}
