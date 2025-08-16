using BlogAPI.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Infrastructure.Data
{
    public static class DataSeeder
    {
        public static async Task SeedDataAsync(ApplicationDbContext context, UserManager<ApplicationUser> userManager)
        {
            // Ensure database is created
            await context.Database.EnsureCreatedAsync();

            // Seed Categories
            if (!await context.Categories.AnyAsync())
            {
                var categories = new List<Category>
                {
                    new Category { Name = "Technology", Description = "Technology related posts", Slug = "technology" },
                    new Category { Name = "Programming", Description = "Programming tutorials and tips", Slug = "programming" },
                    new Category { Name = "Web Development", Description = "Web development articles", Slug = "web-development" }
                };

                await context.Categories.AddRangeAsync(categories);
                await context.SaveChangesAsync();
            }

            // Seed Tags
            if (!await context.Tags.AnyAsync())
            {
                var tags = new List<Tag>
                {
                    new Tag { Name = "ASP.NET Core", Slug = "aspnet-core" },
                    new Tag { Name = "C#", Slug = "csharp" },
                    new Tag { Name = "Entity Framework", Slug = "entity-framework" },
                    new Tag { Name = "API", Slug = "api" }
                };

                await context.Tags.AddRangeAsync(tags);
                await context.SaveChangesAsync();
            }

            // Seed default admin user
            if (!await userManager.Users.AnyAsync())
            {
                var adminUser = new ApplicationUser
                {
                    UserName = "admin@blogapi.com",
                    Email = "admin@blogapi.com",
                    EmailConfirmed = true,
                    DisplayName = "Administrator",
                    Bio = "System Administrator"
                };

                await userManager.CreateAsync(adminUser, "Admin123!");
            }
        }
    }
}
