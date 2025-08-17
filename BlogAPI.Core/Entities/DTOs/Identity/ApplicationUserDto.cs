using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs.Identity
{
    public class ApplicationUserDto
    {
        public string DisplayName { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
        public string? Bio { get; set; } // Optional field

        [Required]
        public string Username { get; set; } = string.Empty;

        [Required]
        public string Email { get; set; } = string.Empty;
    }
}