namespace BlogAPI.Core.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public int BlogPostId { get; set; }
        public BlogPost BlogPost { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public byte[] RowVersion { get; set; } = default!;
    }
}
