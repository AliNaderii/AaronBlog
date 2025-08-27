using System.ComponentModel.DataAnnotations;
namespace Aaron.Models.Entities
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string CoverImagePath { get; set; } = string.Empty;

        public List<Book> Books { get; set; } = new();
    }
}
