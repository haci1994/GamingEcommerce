using GamingEcommerce.DAL.DataContext.Entities;
using Microsoft.AspNetCore.Http;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        public bool IsDeleted { get; set; }

        // Navigation property
        public List<Product> Products { get; set; } = [];
    }

    public class CreateCategoryViewModel
    {
        public string Name { get; set; } = null!;
        public string? ImageName { get; set; }
        public IFormFile Image { get; set; } = null!;
    }

    public class UpdateCategoryViewModel
    {
        public int Id { get; set;}
        public string? Name { get; set; }
        public string? ImageName { get; set; }
        public IFormFile? Image {  get; set; }
        public bool IsDeleted { get; set; }
    }
}
