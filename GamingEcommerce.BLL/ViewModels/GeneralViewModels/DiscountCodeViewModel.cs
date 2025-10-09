using GamingEcommerce.DAL.DataContext.Entities;
using System.ComponentModel.DataAnnotations;

namespace GamingEcommerce.BLL.ViewModels.GeneralViewModels
{
    public class DiscountCodeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal Percentage { get; set; }
        public int MaxUsageCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Used { get; set; }

    }

    public class CreateDiscountCodeViewModel
    {
        public string Name { get; set; } = null!;
        public string? Code { get; set; } = null!;
        public decimal Percentage { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "MaxUsageCount must be at least 1.")]
        public int MaxUsageCount { get; set; }
        public bool IsActive { get; set; } = true;
    }

    public class UpdateDiscountCodeViewModel
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Code { get; set; }
        public decimal Percentage { get; set; }
        public int MaxUsageCount { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public int Used { get; set; }
    }
}
