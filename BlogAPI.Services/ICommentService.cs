using BlogAPI.Core.Entities.DTOs;

public interface ICommentService
{
    Task<CreateCommentDto> AddCommentAsync(CreateCommentDto commentDto, string userId);
    Task DeleteCommentAsync(int commentId, string userId, bool isAdmin);
}