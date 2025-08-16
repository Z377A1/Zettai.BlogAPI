using BlogAPI.Core.Entities.DTOs.Identity;
using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs
{
    public class BlogPostDto
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public ApplicationUserDto Author { get; set; }

        public ICollection<CategoryDto> Categories { get; set; } = new List<CategoryDto>();
        public ICollection<TagDto> Tags { get; set; } = new List<TagDto>();
        public ICollection<CreateCommentDto> Comments { get; set; } = new List<CreateCommentDto>();
    }
}
