using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs
{
    public class CreateBlogPostDto
    {
        [Required]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public List<int> CategoryIds { get; set; } = new List<int>();
        public List<int> TagIds { get; set; } = new List<int>();
    }
}
