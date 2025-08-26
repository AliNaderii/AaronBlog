using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class CreateBookViewModel
    {
        [MaxLength(200, ErrorMessage ="عنوان نمی‌تواند از 200 کاراکتر بیشتر باشد")]
        [Required(ErrorMessage="عنوان نمی‌تواند خالی باشد")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "خلاصه کتاب نمی‌تواند خالی باشد")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات کتاب نمی‌تواند خالی باشد")]
        [MinLength(50, ErrorMessage = "حداقل کاراکتر برای قسمت توضیحات 50 است")]
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
