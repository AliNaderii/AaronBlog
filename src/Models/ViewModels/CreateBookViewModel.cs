using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class CreateBookViewModel
    {
        [StringLength(100, MinimumLength =3, ErrorMessage ="عنوان باید بین 3 تا 100 کاراکتر باشد")]
        [Required(ErrorMessage="عنوان نمی‌تواند خالی باشد")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "خلاصه کتاب نمی‌تواند خالی باشد")]
        [MinLength(50, ErrorMessage = "خلاصه کتاب باید حداقل 50 کاراکتر باشد")]
        [MaxLength(500, ErrorMessage = "خلاصه کتاب نمی‌تواند بیشتر از 500 کاراکتر باشد")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات کتاب نمی‌تواند خالی باشد")]
        [MinLength(200, ErrorMessage = "توضیحات باید حداقل 200 کاراکتر باشد")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "عکس کتاب نمی‌تواند خالی باشد")]
        public IFormFile ImageFile { get; set; } = default!;

        [Required(ErrorMessage = "نویسنده کتاب نمی‌تواند خالی باشد")]
        public int AuthorId { get; set; }

        [Required(ErrorMessage = "دسته‌بندی کتاب نمی‌تواند خالی باشد")]
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "برچسب نمی‌تواند خالی باشد")]
        public List<int> SelectedTags { get; set; } = new();
    }

}
