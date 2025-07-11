using System.ComponentModel.DataAnnotations;
namespace Aaron.Models.ViewModels
{
    public class EditAuthorViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "وارد کردن نام نویسنده الزامیست")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "نام نویسنده باید بین 2 تا 50 کاراکتر باشد")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "وارد کردن توضیحات الزامیست")]
        [MinLength(50, ErrorMessage = "حداقل کاراکتر برای قسمت توضیحات 50 است")]
        public string Description { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

        public string CoverImagePath { get; set; } = string.Empty;

    }
}
