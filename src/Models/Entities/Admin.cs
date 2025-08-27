using System.ComponentModel.DataAnnotations;

namespace Aaron.Models.Entities
{
    public class Admin
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(20)]
        public string UserName { get; set; } = string.Empty;
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        [MaxLength(20)]
        public string? Role { get; set; } = string.Empty;
        public bool IsDefaultCredentials { get; set; } = true;
    }
}
