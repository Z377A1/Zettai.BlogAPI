using AutoMapper;
using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace BlogAPI.Services
{
    public class BlogService : IBlogService
    {
        private readonly IRepository<BlogPost> _blogPostRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IRepository<Tag> _tagRepository;
        private readonly IMapper _mapper;

        public BlogService(
            IRepository<BlogPost> blogPostRepository,
            IRepository<Category> categoryRepository,
            IRepository<Tag> tagRepository,
            IMapper mapper)
        {
            _blogPostRepository = blogPostRepository;
            _categoryRepository = categoryRepository;
            _tagRepository = tagRepository;
            _mapper = mapper;
        }

        public async Task<BlogPostDto> CreateBlogPostAsync(CreateBlogPostDto blogPostDto, string userId)
        {
            var blogPost = _mapper.Map<BlogPost>(blogPostDto);
            blogPost.AuthorId = userId;

            // Handle categories
            if (blogPostDto.CategoryIds != null && blogPostDto.CategoryIds.Any())
            {
                blogPost.BlogPostCategories = new List<BlogPostCategory>();
                foreach (var categoryId in blogPostDto.CategoryIds)
                {
                    var category = await _categoryRepository.GetByIdAsync(categoryId);
                    if (category != null)
                    {
                        blogPost.BlogPostCategories.Add(new BlogPostCategory
                        {
                            Category = category
                        });
                    }
                }
            }

            // Handle tags
            if (blogPostDto.TagIds != null && blogPostDto.TagIds.Any())
            {
                blogPost.BlogPostTags = new List<BlogPostTag>();
                foreach (var tagId in blogPostDto.TagIds)
                {
                    var tag = await _tagRepository.GetByIdAsync(tagId);
                    if (tag != null)
                    {
                        blogPost.BlogPostTags.Add(new BlogPostTag
                        {
                            Tag = tag
                        });
                    }
                }
            }

            await _blogPostRepository.AddAsync(blogPost);
            await _blogPostRepository.SaveChangesAsync();

            return _mapper.Map<BlogPostDto>(blogPost);
        }

        public async Task<PaginatedResult<BlogPostDto>> GetBlogPostsAsync(PaginationFilter filter)
        {
            var query = _blogPostRepository.GetAll()
                .Include(b => b.Author)
                .Include(b => b.BlogPostCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.BlogPostTags)
                    .ThenInclude(bt => bt.Tag)
                .OrderByDescending(b => b.CreatedAt);

            var totalRecords = await query.CountAsync();
            var blogPosts = await query
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize)
                .ToListAsync();

            var blogPostDtos = _mapper.Map<List<BlogPostDto>>(blogPosts);

            return new PaginatedResult<BlogPostDto>(
                blogPostDtos,
                filter.PageNumber,
                filter.PageSize,
                totalRecords);
        }
        public async Task<BlogPostDto> GetBlogPostByIdAsync(int id)
        {
            var blogPost = await _blogPostRepository.GetAll()
                .Include(b => b.Author)
                .Include(b => b.BlogPostCategories)
                    .ThenInclude(bc => bc.Category)
                .Include(b => b.BlogPostTags)
                    .ThenInclude(bt => bt.Tag)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (blogPost == null)
                return null;

            return _mapper.Map<BlogPostDto>(blogPost);
        }

        public async Task UpdateBlogPostAsync(UpdateBlogPostDto blogPostDto, string userId, bool isAdmin)
        {
            var blogPost = await _blogPostRepository.GetAll()
                .Include(b => b.BlogPostCategories)
                .Include(b => b.BlogPostTags)
                .FirstOrDefaultAsync(b => b.Id == blogPostDto.Id);

            if (blogPost == null)
                throw new KeyNotFoundException("Blog post not found.");

            _mapper.Map(blogPostDto, blogPost);

            // Update categories
            if (blogPostDto.CategoryIds != null)
            {
                blogPost.BlogPostCategories.Clear();
                foreach (var categoryId in blogPostDto.CategoryIds)
                {
                    var category = await _categoryRepository.GetByIdAsync(categoryId);
                    if (category != null)
                    {
                        blogPost.BlogPostCategories.Add(new BlogPostCategory
                        {
                            Category = category
                        });
                    }
                }
            }

            // Update tags
            if (blogPostDto.TagIds != null)
            {
                blogPost.BlogPostTags.Clear();
                foreach (var tagId in blogPostDto.TagIds)
                {
                    var tag = await _tagRepository.GetByIdAsync(tagId);
                    if (tag != null)
                    {
                        blogPost.BlogPostTags.Add(new BlogPostTag
                        {
                            Tag = tag
                        });
                    }
                }
            }

            blogPost.UpdatedAt = DateTime.UtcNow;
            _blogPostRepository.Update(blogPost);
            await _blogPostRepository.SaveChangesAsync();
        }

        public async Task DeleteBlogPostAsync(int id, string _userId, bool _isAdmin)
        {
            var blogPost = await _blogPostRepository.GetByIdAsync(id);
            if (blogPost == null)
                throw new KeyNotFoundException("Blog post not found.");

            _blogPostRepository.Delete(blogPost);
            await _blogPostRepository.SaveChangesAsync();
        }
    }
}
