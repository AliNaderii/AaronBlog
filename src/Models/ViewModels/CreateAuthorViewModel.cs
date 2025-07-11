using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
namespace Aaron.Models.ViewModels
{
    public class CreateAuthorViewModel
    {
        [Required(ErrorMessage = "وارد کردن نام نویسنده الزامیست")]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "نام نویسنده باید بین 2 تا 50 کاراکتر باشد")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "وارد کردن توضیحات الزامیست")]
        [MinLength(50, ErrorMessage = "حداقل کاراکتر برای قسمت توضیحات 50 است")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "افزودن عکس الزامیست")]
        public IFormFile ImageFile { get; set; } = default!;
    }
}
