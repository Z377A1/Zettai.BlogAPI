using BlogAPI.Core.Entities.DTOs;

namespace BlogAPI.Services
{
    public interface ICommentService
    {
        Task<IEnumerable<CommentDto>> GetCommentsByBlogPostIdAsync(int blogPostId);
        Task<CommentDto?> GetCommentByIdAsync(int id);
        Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto, string userId);
        Task UpdateCommentAsync(UpdateCommentDto commentDto, string userId);
        Task DeleteCommentAsync(int commentId, string userId);
        Task<CreateCommentDto> AddCommentAsync(CreateCommentDto commentDto, string userId); // Legacy method
        Task DeleteCommentAsync(int commentId, string userId, bool isAdmin); // Legacy method
    }
}