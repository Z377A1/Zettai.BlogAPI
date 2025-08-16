using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs.Identity
{
    public class RegisterDto
    {
        public string DisplayName { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}