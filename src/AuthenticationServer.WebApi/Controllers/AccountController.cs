using System;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationServer.WebApi.Entities;
using AuthenticationServer.WebApi.Models;
using AuthenticationServer.WebApi.Repository.User;
using AuthenticationServer.WebApi.Security.Auth;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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


        [HttpGet("api/account/me")]
        public ActionResult Get() => Content(User.Identity.Name);

        [HttpPost("api/account/login")]
        public async Task<ActionResult> Login([FromBody] LoginDto model)
        {
            if (!_userRepository.EmailExists(model.Email))
            {
                throw new Exception("Invalid email.");
            }

            var user = _userRepository.GetUsers().Where(x => x.Email == model.Email).First();
            if (user == null)
            {
                throw new Exception("Invalid credentials.");
            }

            // var userPassword = _userRepository.GetPasswordByUserId(user.Id);

            var passwordResult = _passwordHasher.VerifyHashedPassword(user,
                user.Password, model.Password);

            if (passwordResult == PasswordVerificationResult.Failed)
            {
                throw new Exception("Invalid credentials.");
            }
            await Task.CompletedTask;

            _jwtProvider.Create(user.Email, user.FirstName);

            throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
        }

        [HttpPost("api/account/register")]
        public async Task<object> Register([FromBody] RegisterDto model)
        {
            var user = _userRepository.GetUserByEmail(model.Email);
            if (user != null)
            {
                throw new Exception("Username is in use.");
            }

            user = new User
            {
                Email = model.Email,
                Password = model.Password,
                Role = model.Role ?? "user",
                IsActive = true,
                FirstName = "John",
                LastName = "Five",
                Login = "Johny5"
            };

            var passwordHash = _passwordHasher.HashPassword(user, model.Password);
            if (string.IsNullOrWhiteSpace(passwordHash))
            {
                throw new ArgumentException("Invalid password.",
                    nameof(passwordHash));
            }

            _userRepository.AddUser(user);
            await Task.CompletedTask;

            return Created(nameof(Get), null);
        }

    }
}