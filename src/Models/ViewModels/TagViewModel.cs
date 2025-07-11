using System.ComponentModel.DataAnnotations;
using Aaron.Models.Entities;

namespace Aaron.Models.ViewModels
{
    public class TagViewModel
    {
        public PaginatedList<Tag>? Tags { get; set; }
        [Required]
        public Tag NewTag { get; set; } = new();
    }
}
