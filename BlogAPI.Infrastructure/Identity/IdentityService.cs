using BlogAPI.Core.Entities;
using BlogAPI.Core.Entities.DTOs.Identity;
using BlogAPI.Infrastructure.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.Rest;
using System.Net;

namespace BlogAPI.Infrastructure.Identity
{
    public class IdentityService : IIdentityService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IJwtGenerator _jwtGenerator;

        public IdentityService(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IJwtGenerator jwtGenerator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _jwtGenerator = jwtGenerator;
        }

        public async Task<ApplicationUserDto> Login(string email, string password)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null) throw new RestException(HttpStatusCode.Unauthorized.ToString());

            var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);

            if (result.Succeeded)
            {
                return new ApplicationUserDto
                {
                    DisplayName = user.DisplayName,
                    Token = _jwtGenerator.CreateToken(user),
                    Username = user.UserName,
                    Email = user.Email
                };
            }

            throw new RestException(HttpStatusCode.Unauthorized.ToString());
        }

        public async Task<ApplicationUserDto> Register(RegisterDto registerDto)
        {
            if (await _userManager.FindByEmailAsync(registerDto.Email) != null)
            {
                throw new RestException(HttpStatusCode.BadRequest.ToString(), new Exception("Email already exists"));
            }

            if (await _userManager.FindByNameAsync(registerDto.Username) != null)
            {
                throw new RestException(HttpStatusCode.BadRequest.ToString(), new Exception("Username already exists"));
            }

            var user = new ApplicationUser
            {
                DisplayName = registerDto.DisplayName,
                Email = registerDto.Email,
                UserName = registerDto.Username
            };

            var result = await _userManager.CreateAsync(user, registerDto.Password);

            if (result.Succeeded)
            {
                return new ApplicationUserDto
                {
                    DisplayName = user.DisplayName,
                    Token = _jwtGenerator.CreateToken(user),
                    Username = user.UserName,
                    Email = user.Email
                };
            }

            throw new Exception("Problem creating user");
        }

        public async Task<ApplicationUserDto> GetCurrentUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            return new ApplicationUserDto
            {
                DisplayName = user.DisplayName,
                Token = _jwtGenerator.CreateToken(user),
                Username = user.UserName,
                Email = user.Email
            };
        }
    }
}
