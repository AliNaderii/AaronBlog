using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.ViewModels
{
    public class EditBookViewModel
    {
        public int Id { get; set; }
        [MaxLength(200)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [Required]
        public string Summary { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty;
        [Required]
        public int AuthorId { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public List<int> TagIds { get; set; } = new();
        public string CoverImagePath { get; set; } = string.Empty;
        public IFormFile? ImageFile { get; set; }
    }

}
