using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Infrastructure.Data;

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
