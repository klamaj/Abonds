using System.ComponentModel.DataAnnotations;

namespace Core.DTOs
{
    public class LoginDto
    {
        [Required]
        public string? Email { get; set; }
        [Required]
        [StringLength(int.MaxValue, MinimumLength = 8)]
        public string? Password { get; set; }
        public string Role { get; set; } = "Client";
    }
}