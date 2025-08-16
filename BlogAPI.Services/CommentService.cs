using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Infrastructure.Data;
using BlogAPI.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class CommentService : ICommentService
    {
        private readonly IRepository<Comment> _commentRepository;
        private readonly IRepository<BlogPost> _blogPostRepository;
        private readonly IMapper _mapper;

        public CommentService(
            IRepository<Comment> commentRepository,
            IRepository<BlogPost> blogPostRepository,
            IMapper mapper)
        {
            _commentRepository = commentRepository;
            _blogPostRepository = blogPostRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CommentDto>> GetCommentsByBlogPostIdAsync(int blogPostId)
        {
            var comments = await _commentRepository.GetAll()
                .Where(c => c.BlogPostId == blogPostId)
                .Include(c => c.Author)
                .OrderByDescending(c => c.CreatedAt)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CommentDto>>(comments);
        }

        public async Task<CommentDto?> GetCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetAll()
                .Where(c => c.Id == id)
                .Include(c => c.Author)
                .FirstOrDefaultAsync();

            return comment == null ? null : _mapper.Map<CommentDto>(comment);
        }

        public async Task<CommentDto> CreateCommentAsync(CreateCommentDto commentDto, string userId)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(commentDto.BlogPostId);
            if (blogPost == null)
            {
                throw new NotFoundException(nameof(BlogPost), commentDto.BlogPostId);
            }

            var comment = _mapper.Map<Comment>(commentDto);
            comment.AuthorId = userId;

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            // Return the created comment with author info
            var createdComment = await GetCommentByIdAsync(comment.Id);
            return createdComment!;
        }

        public async Task UpdateCommentAsync(UpdateCommentDto commentDto, string userId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentDto.Id);
            if (comment == null)
            {
                throw new NotFoundException(nameof(Comment), commentDto.Id);
            }

            if (comment.AuthorId != userId)
            {
                throw new UnauthorizedAccessException("You can only update your own comments");
            }

            _mapper.Map(commentDto, comment);
            comment.UpdatedAt = DateTime.UtcNow;

            _commentRepository.Update(comment);
            await _commentRepository.SaveChangesAsync();
        }

        public async Task DeleteCommentAsync(int commentId, string userId)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);
            if (comment == null)
            {
                throw new NotFoundException(nameof(Comment), commentId);
            }

            if (comment.AuthorId != userId)
            {
                throw new UnauthorizedAccessException("You can only delete your own comments");
            }

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }

        // Legacy methods - keeping for backward compatibility
        public async Task<CreateCommentDto> AddCommentAsync(CreateCommentDto commentDto, string userId)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(commentDto.BlogPostId);
            if (blogPost == null)
            {
                throw new NotFoundException(nameof(BlogPost), commentDto.BlogPostId);
            }

            var comment = _mapper.Map<Comment>(commentDto);
            comment.AuthorId = userId;

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return _mapper.Map<CreateCommentDto>(comment);
        }

        public async Task DeleteCommentAsync(int commentId, string userId, bool isAdmin)
        {
            var comment = await _commentRepository.GetByIdAsync(commentId);

            if (comment == null)
            {
                throw new NotFoundException(nameof(Comment), commentId);
            }

            if (comment.AuthorId != userId && !isAdmin)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this comment");
            }

            _commentRepository.Delete(comment);
            await _commentRepository.SaveChangesAsync();
        }
    }
}
