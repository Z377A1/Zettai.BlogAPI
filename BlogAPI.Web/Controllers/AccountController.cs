using BlogAPI.Core.Entities.DTOs.Identity;
using BlogAPI.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BlogAPI.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IIdentityService _identityService;

        public AccountController(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<ApplicationUserDto>> Login(LoginDto loginDto)
        {
            return await _identityService.Login(loginDto.Email, loginDto.Password);
        }

        [HttpPost("register")]
        public async Task<ActionResult<ApplicationUserDto>> Register(RegisterDto registerDto)
        {
            return await _identityService.Register(registerDto);
        }

        [HttpGet("currentuser")]
        [Authorize]
        public async Task<ActionResult<ApplicationUserDto>> GetCurrentUser()
        {
            var email = User.FindFirstValue(ClaimTypes.Email);
            return await _identityService.GetCurrentUser(email);
        }
    }
}
