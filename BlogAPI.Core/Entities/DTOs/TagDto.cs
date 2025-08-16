using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs
{
    public class TagDto
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Slug { get; set; }

        public ICollection<BlogPostDto> BlogPosts { get; set; } = new List<BlogPostDto>();
    }
}
