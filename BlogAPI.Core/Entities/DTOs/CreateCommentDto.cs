using System.ComponentModel.DataAnnotations;

namespace BlogAPI.Core.Entities.DTOs
{
    public class CreateCommentDto
    {
        [Required]
        public string Content { get; set; }

        public int BlogPostId { get; set; }
        public int AuthorId { get; set; }
    }
}
