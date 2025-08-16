using BlogAPI.Core.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Infrastructure.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Comment> Comments { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            builder.Entity<BlogPostCategory>().HasKey(bc => new { bc.BlogPostId, bc.CategoryId });

            builder.Entity<BlogPostTag>().HasKey(bt => new { bt.BlogPostId, bt.TagId });

            builder.Entity<Category>().HasIndex(c => c.Slug).IsUnique();

            builder.Entity<Tag>().HasIndex(t => t.Slug).IsUnique();
        }
    }
}
