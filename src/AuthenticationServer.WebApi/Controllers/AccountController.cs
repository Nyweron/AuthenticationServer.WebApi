using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using AuthenticationServer.WebApi.Entities;
using AuthenticationServer.WebApi.Models;
using AuthenticationServer.WebApi.Repository.User;
using AuthenticationServer.WebApi.Security.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationServer.WebApi.Controllers
{
    public class AccountController : Controller
    {
        private readonly IJwtProvider _jwtProvider;
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher<User> _passwordHasher;

        public AccountController(IJwtProvider jwtProvider,
            IPasswordHasher<User> passwordHasher)
        {
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        [HttpPost("api/account/login")]
        public async Task<JsonWebToken> Login([FromBody] LoginDto model)
        {
            if(!_userRepository.EmailExists(model.Email))
            {
                throw new Exception("Invalid email.");
            }

            var user = _userRepository.GetUsers().Where(x => x.Email == model.Email).First();
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            var userPassword = _userRepository.GetPasswordByUserId(user.Id).Pwd;

            var passwordResult = _passwordHasher.VerifyHashedPassword(user,
                userPassword, model.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials.");
            }
            await Task.CompletedTask;

            return _jwtProvider.Create(user.Id, user.UsersGroups);

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost("api/account/register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = new IdentityUser
            {
                UserName = model.Email,
                Email = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return await GenerateJwtToken(model.Email, user);
            }

            throw new ApplicationException("UNKNOWN_ERROR");
        }

    }
}