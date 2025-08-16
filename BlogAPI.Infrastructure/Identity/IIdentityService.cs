using BlogAPI.Core.Entities.DTOs.Identity;

namespace BlogAPI.Infrastructure.Identity
{
    public interface IIdentityService
    {
        Task<ApplicationUserDto> Login(string email, string password);
        Task<ApplicationUserDto> Register(RegisterDto registerDto);
        Task<ApplicationUserDto> GetCurrentUser(string email);
    }
}