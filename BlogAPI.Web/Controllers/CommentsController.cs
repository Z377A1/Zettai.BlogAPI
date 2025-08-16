using BlogAPI.Core.Entities.DTOs;
using BlogAPI.Services;
using BlogAPI.Core.Exceptions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentService _commentService;

        public CommentsController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        /// <summary>
        /// Get comments for a specific blog post
        /// </summary>
        [HttpGet("blogpost/{blogPostId}")]
        public async Task<ActionResult<IEnumerable<CommentDto>>> GetCommentsByBlogPost(int blogPostId)
        {
            var comments = await _commentService.GetCommentsByBlogPostIdAsync(blogPostId);
            return Ok(comments);
        }

        /// <summary>
        /// Get a specific comment by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<ActionResult<CommentDto>> Get(int id)
        {
            var comment = await _commentService.GetCommentByIdAsync(id);
            if (comment == null) return NotFound($"Comment with ID {id} not found");
            return comment;
        }

        /// <summary>
        /// Create a new comment on a blog post
        /// </summary>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult<CommentDto>> Create(CreateCommentDto dto)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token");
            }

            var created = await _commentService.CreateCommentAsync(dto, userId);
            return CreatedAtAction(nameof(Get), new { id = created.Id }, created);
        }

        /// <summary>
        /// Update an existing comment
        /// </summary>
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> Update(int id, UpdateCommentDto dto)
        {
            if (id != dto.Id) return BadRequest("ID mismatch");

            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token");
            }

            try
            {
                await _commentService.UpdateCommentAsync(dto, userId);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"Comment with ID {id} not found");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("You can only update your own comments");
            }
        }

        /// <summary>
        /// Delete a comment
        /// </summary>
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> Delete(int id)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
            {
                return Unauthorized("User ID not found in token");
            }

            try
            {
                await _commentService.DeleteCommentAsync(id, userId);
                return NoContent();
            }
            catch (NotFoundException)
            {
                return NotFound($"Comment with ID {id} not found");
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid("You can only delete your own comments");
            }
        }
    }
}
