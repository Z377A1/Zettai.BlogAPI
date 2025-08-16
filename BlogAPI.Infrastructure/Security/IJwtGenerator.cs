using BlogAPI.Core.Entities;

namespace BlogAPI.Infrastructure.Security
{
    public interface IJwtGenerator
    {
        string CreateToken(ApplicationUser user);
    }
}