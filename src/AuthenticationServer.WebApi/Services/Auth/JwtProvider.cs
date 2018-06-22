using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationServer.WebApi.Entities;
using AuthenticationServer.WebApi.Repository.User;
using AuthenticationServer.WebApi.Settings.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationServer.WebApi.Services.Auth
{
    public class JwtProvider : IJwtProvider
    {
        private readonly JwtOptions _jwtOptions;
        private readonly IUserRepository _userRepository;
        private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;

        public JwtProvider(
            IUserRepository userRepository,
            Microsoft.Extensions.Configuration.IConfiguration configuration,
            IOptions<JwtOptions> jwtOptions
        )
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<object> GenerateJwtToken(string email, User user)
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

            await Task.CompletedTask;
            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}