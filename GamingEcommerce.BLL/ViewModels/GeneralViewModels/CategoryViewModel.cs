using GamingEcommerce.DAL.DataContext.Entities;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }

        // Navigation property
        public List<Product> Products { get; set; } = [];
    }

    public class CreateCategoryViewModel
    {
    }

    public class UpdateCategoryViewModel
    {
    }
}
