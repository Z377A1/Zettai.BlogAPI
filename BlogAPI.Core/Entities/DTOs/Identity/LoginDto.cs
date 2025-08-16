using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs.Identity
{
    public class LoginDto
    {
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; } = false;
    }
}
