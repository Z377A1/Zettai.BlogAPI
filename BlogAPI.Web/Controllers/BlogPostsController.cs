using BlogAPI.Core.Entities.DTOs;
using Microsoft.AspNetCore.Authorization;
using BlogAPI.Services;
using BlogAPI.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogPostsController : ControllerBase
    {
        private readonly IBlogService _blogService;

        public BlogPostsController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<ActionResult<PaginatedResult<BlogPostDto>>> GetBlogPosts(
            [FromQuery] PaginationFilter filter)
        {
            return await _blogService.GetBlogPostsAsync(filter);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BlogPostDto>> GetBlogPost(int id)
        {
            var blogPost = await _blogService.GetBlogPostByIdAsync(id);

            if (blogPost == null)
            {
                return NotFound();
            }

            return blogPost;
        }

        [HttpPost]
        [Authorize]
        public async Task<ActionResult<BlogPostDto>> CreateBlogPost(CreateBlogPostDto blogPostDto)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();
            var blogPost = await _blogService.CreateBlogPostAsync(blogPostDto, userId);

            return CreatedAtAction(nameof(GetBlogPost), new { id = blogPost.Id }, blogPost);
        }

        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateBlogPost(int id, UpdateBlogPostDto blogPostDto)
        {
            if (id != blogPostDto.Id)
            {
                return BadRequest();
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();
            var isAdmin = User.IsInRole("Admin");

            try
            {
                await _blogService.UpdateBlogPostAsync(blogPostDto, userId, isAdmin);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteBlogPost(int id)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null) return Unauthorized();
            var isAdmin = User.IsInRole("Admin");

            try
            {
                await _blogService.DeleteBlogPostAsync(id, userId, isAdmin);
            }
            catch (NotFoundException)
            {
                return NotFound();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }

            return NoContent();
        }
    }
}
