using BlogAPI.Core.Entities.DTOs.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs
{
    public class CommentDto
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public BlogPostDto BlogPost { get; set; }
        public ApplicationUserDto Author { get; set; }
    }
}
