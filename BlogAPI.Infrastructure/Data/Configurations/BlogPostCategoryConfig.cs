using BlogAPI.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace BlogAPI.Infrastructure.Data.Configurations
{
    public class BlogPostCategoryConfig : IEntityTypeConfiguration<BlogPostCategory>
    {
        public void Configure(EntityTypeBuilder<BlogPostCategory> builder)
        {
            builder.HasKey(bc => new { bc.BlogPostId, bc.CategoryId });
            builder.HasIndex(bc => bc.CategoryId);
        }
    }
}
