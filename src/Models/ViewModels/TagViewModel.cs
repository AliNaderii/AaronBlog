using System.ComponentModel.DataAnnotations;
using Aaron.Models.Entities;

namespace Aaron.Models.ViewModels
{
    public class TagViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام برچسب الزامیست")]
        [StringLength(50, MinimumLength = 5, ErrorMessage = "نام برچسب باید بین 5 تا 50 کاراکتر باشد.")]
        public Tag NewTag { get; set; } = new();
        public PaginatedList<Tag>? Tags { get; set; }
    }
}
