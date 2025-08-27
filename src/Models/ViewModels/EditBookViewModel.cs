using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class EditBookViewModel
    {
        public int Id { get; set; }

        [StringLength(100, MinimumLength = 3, ErrorMessage = "عنوان باید بین 3 تا 100 کاراکتر باشد")]
        [Required(ErrorMessage = "عنوان نمی‌تواند خالی باشد")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "خلاصه کتاب نمی‌تواند خالی باشد")]
        [MinLength(50, ErrorMessage = "خلاصه کتاب باید حداقل 50 کاراکتر باشد")]
        [MaxLength(500, ErrorMessage = "خلاصه کتاب نمی‌تواند بیشتر از 500 کاراکتر باشد")]
        public string Summary { get; set; } = string.Empty;

        [Required(ErrorMessage = "توضیحات کتاب نمی‌تواند خالی باشد")]
        [MinLength(200, ErrorMessage = "توضیحات باید حداقل 200 کاراکتر باشد")]
        public string Description { get; set; } = string.Empty;

        public string Author { get; set; } = string.Empty;

        [Required(ErrorMessage = "نویسنده کتاب نمی‌تواند خالی باشد")]
        public int AuthorId { get; set; }

        public string Category { get; set; } = string.Empty;

        [Required(ErrorMessage = "دسته‌بندی کتاب نمی‌تواند خالی باشد")]
        public int CategoryId { get; set; }

        public string CoverImagePath { get; set; } = string.Empty;

        public IFormFile? ImageFile { get; set; }

        [Required(ErrorMessage = "برچسب نمی‌تواند خالی باشد")]
        public List<int> SelectedTags { get; set; } = new();
    }
}
