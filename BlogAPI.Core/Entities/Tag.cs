namespace BlogAPI.Core.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }

        public ICollection<BlogPostTag> BlogPosts { get; set; }
    }
}
