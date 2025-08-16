using BlogAPI.Core.Entities.DTOs;

namespace BlogAPI.Services
{
    public interface IBlogService
    {
        Task<BlogPostDto> CreateBlogPostAsync(CreateBlogPostDto blogPostDto, string userId);
        Task<PaginatedResult<BlogPostDto>> GetBlogPostsAsync(PaginationFilter filter);
        Task<BlogPostDto?> GetBlogPostByIdAsync(int id);
        Task UpdateBlogPostAsync(UpdateBlogPostDto blogPostDto, string userId, bool isAdmin);
        Task DeleteBlogPostAsync(int id, string userId, bool isAdmin);
    }
}