using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class CreateBookViewModel
    {
        [MaxLength(200)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Summary { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public IFormFile ImageFile { get; set; } = default!;
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int CategoryId { get; set; }
    }

}
