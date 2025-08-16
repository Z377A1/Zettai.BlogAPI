namespace BlogAPI.Core.Entities
{
    public class BlogPost
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedAt { get; set; }

        public string AuthorId { get; set; }
        public ApplicationUser Author { get; set; }

        public ICollection<Comment> Comments { get; set; }
        public ICollection<BlogPostCategory> BlogPostCategories { get; set; }
        public ICollection<BlogPostTag> BlogPostTags { get; set; }

        public byte[] RowVersion { get; set; } = default!;
    }
}
