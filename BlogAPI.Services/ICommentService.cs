using BlogAPI.Core.Entities.DTOs;

namespace BlogAPI.Services
{
    public interface ICommentService
    {
        Task<CreateCommentDto> AddCommentAsync(CreateCommentDto commentDto, string userId);
        Task DeleteCommentAsync(int commentId, string userId, bool isAdmin);
    }
}