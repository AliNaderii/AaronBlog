using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن نام دسته‌بندی الزامیست")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "نام باید بین 5 تا ۱۰۰ کاراکتر باشد.")]
        public string Name { get; set; } = string.Empty;
    }
}
