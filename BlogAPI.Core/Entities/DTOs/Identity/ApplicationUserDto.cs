using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs.Identity
{
    public class ApplicationUserDto
    {
        public string DisplayName { get; set; }
        public string Token { get; set; }

        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }
    }
}