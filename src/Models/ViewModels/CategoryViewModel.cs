using System.ComponentModel.DataAnnotations;
using Aaron.Models.Entities;

namespace Aaron.Models.ViewModels
{
    public class CategoryViewModel
    {
        public PaginatedList<Category>? Categories { get; set; }
        [Required]
        public Category NewCategory { get; set; } = new();
    }
}
