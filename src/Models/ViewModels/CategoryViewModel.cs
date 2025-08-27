using System.ComponentModel.DataAnnotations;
using Aaron.Models.Entities;

namespace Aaron.Models.ViewModels
{
    public class CategoryViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام دسته‌بندی الزامیست")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "نام دسته بندی باید بین 5 تا ۱۰۰ کاراکتر باشد.")]
        public Category NewCategory { get; set; } = new();
        public PaginatedList<Category>? Categories { get; set; }
    }
}
