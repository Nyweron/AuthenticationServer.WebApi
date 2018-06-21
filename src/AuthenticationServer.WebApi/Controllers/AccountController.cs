using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationServer.WebApi.Entities;
using AuthenticationServer.WebApi.Repository.User;
using AuthenticationServer.WebApi.Settings.Options;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationServer.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUserRepository _userRepository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public AccountController(
            IUserRepository userRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IOptions<JwtOptions> jwtOptions
        )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
        }

        [Authorize(Policy = "admin")]
        [HttpGet("api/account/Protectedadmin")]
        public async Task<object> Protectedadmin()
        {
            return "Protected area Admin";
        }

        [Authorize(Policy = "user")]
        [HttpGet("api/account/Protecteduser")]
        public async Task<object> Protecteduser()
        {
            return "Protected area User";
        }

        [Authorize]
        [HttpGet("api/account/me")]
        public ActionResult Get() => Content(User.Identity.Name);

        [HttpPost("api/account/login")]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = _userRepository.GetUserByEmail(model.Email);
            if (result != null && result.Password == model.Password)
            {
                var appUser = _userRepository.GetUserByEmail(model.Email);
                return await GenerateJwtToken(model.Email, appUser);
            }

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost("api/account/register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new User
            {
                FirstName = "test1",
                LastName = "test01",
                Login = "test001",
                IsActive = true,
                Email = model.Email,
                Password = model.Password
            };

            _userRepository.AddUser(user);
            _userRepository.Save();
            return await GenerateJwtToken(model.Email, user);

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private async Task<object> GenerateJwtToken(string email, User user)
        {
            var now = DateTime.UtcNow;
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            };

            var jwtOptions = new JwtOptions();
            _configuration.GetSection("jwt").Bind(jwtOptions);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = now.AddMinutes(_jwtOptions.ExpiryMinutes);

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                claims: claims,
                notBefore: now,
                expires: expires,
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }
            public string Role { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100)]
            public string Password { get; set; }
            public string Role { get; set; }
        }
    }
}