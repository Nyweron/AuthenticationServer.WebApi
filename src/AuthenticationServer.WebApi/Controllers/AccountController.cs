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

        [Authorize]
        [HttpGet("api/account/protected")]
        public async Task<object> Protected()
        {
            return "Protected area";
        }

        [HttpGet("api/account/me")]
        public ActionResult Get() => Content(User.Identity.Name);

        [HttpPost("api/account/login")]
        public async Task<object> Login([FromBody] LoginDto model)
        {
            var result = _userRepository.GetUserByEmail(model.Email);

                var appUser = _userRepository.GetUserByEmail(model.Email);
                return await GenerateJwtToken(model.Email, appUser);



            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost("api/account/register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new User
            {
                Email = model.Email
            };

            _userRepository.AddUser(user);
            return await GenerateJwtToken(model.Email, user);

            throw new ApplicationException("UNKNOWN_ERROR");
        }

        private async Task<object> GenerateJwtToken(string email, User user)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, email),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var jwtOptions = new JwtOptions();
            _configuration.GetSection("jwt").Bind(jwtOptions);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.SecretKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(jwtOptions.ExpiryMinutes));

            var token = new JwtSecurityToken(
                _jwtOptions.Issuer,
                _jwtOptions.Issuer,
                claims,
                expires : expires,
                signingCredentials : creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        public class LoginDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            public string Password { get; set; }

        }

        public class RegisterDto
        {
            [Required]
            public string Email { get; set; }

            [Required]
            [StringLength(100)]
            public string Password { get; set; }
        }
    }
}