using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن نام برچسب الزامیست.")]
        [StringLength(100, MinimumLength = 5, ErrorMessage = "نام باید بین ۵ تا ۱۰۰ کاراکتر باشد.")]
        public string Name { get; set; } = string.Empty;

        //Navigation
        public List<Book> Books { get; set; } = new();
    }
}
