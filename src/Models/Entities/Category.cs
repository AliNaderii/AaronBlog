using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.Entities
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; } = string.Empty;

        //Navigation
        public List<Book> Books { get; set; } = new();
    }
}
