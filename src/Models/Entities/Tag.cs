using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.Entities
{
    public class Tag
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(50)]
        public string Name { get; set; } = string.Empty;

        //Navigation
        public List<Book> Books { get; set; } = new();
    }
}
