using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.Entities
{
    public class Book
    {
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Title { get; set; } = string.Empty;
        [MaxLength(200)]
        [Required]
        public string Slug { get; set; } = string.Empty;
        [Required]
        [MaxLength(500)]
        public string Summary { get; set; } = string.Empty;
        [Required]
        public string Description { get; set; } = string.Empty;
        [Required]
        public string CoverImagePath { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        [Required]
        public int CategoryId { get; set; }
        [Required]
        public int AuthorId { get; set; }

        //Navigation
        public Author? Author { get; set; }
        public Category? Category { get; set; }
        public List<Tag> Tags { get; set; } = new();

        public string GenerateSlug(string title)
        {
            return title
                .ToLowerInvariant()
                .Replace(" ", "-")
                .Replace(".", "")
                .Replace(",", "")
                .Replace(":", "")
                .Replace(";", "")
                .Replace("?", "")
                .Replace("!", "")
                .Replace("'", "")
                .Replace("\"", "")
                .Replace("--", "-")
                .Trim('-');
        }

    }

}
