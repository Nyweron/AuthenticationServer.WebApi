using AuthenticationServer.WebApi.Services;
using AuthenticationServer.Data;
using AuthenticationServer.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using NLog.Web;
using Microsoft.Extensions.Logging;

namespace AuthenticationServer.WebApi.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;
        private ILogger<UsersController> _logger;

        public UsersController(IUserRepository userRepository, ILogger<UsersController> logger)
        {
            _userRepository = userRepository;
            _logger = logger;
        }

        [HttpGet("api/users")]
        public IActionResult GetUsers()
        {
            var userEntities = _userRepository.GetUsers();
            return Ok(userEntities);
        }

        [HttpGet("api/users/{userId}")]
        public IActionResult Get(int userId)
        {
            if (!_userRepository.UserExists(userId))
            {
                _logger.LogInformation($"User with id {userId} wasn't found when accessing to UsersController/Get(int userId).");
                return NotFound();
            }

            var userEntities = _userRepository.GetUserById(userId);
            return Ok(userEntities);
        }

        [HttpPost("api/users")]
        public IActionResult Post([FromBody] UserDto user)
        {
            if (user == null)
            {
                _logger.LogInformation($"User is empty when accessing to UsersController/Post(UserDto user).");
                return BadRequest();
            }

            if (_userRepository.EmailExists(user.Email))
            {
                _logger.LogInformation($"The Email {user.Email} exist in database, email must be uniqe. UsersController/Post(UserDto user).");
                return BadRequest($"The Email {user.Email} exist, email must be uniqe.");
            }

            //TODO: Implement Realistic Implementation
            return Created("", null);
        }

        [HttpPut("api/users/userId")]
        public IActionResult Put(int userId, [FromBody] UserDto user)
        {
            //TODO: Implement Realistic Implementation
            return Ok();
        }

        [HttpDelete]
        public IActionResult Delete(UserDto id)
        {
            //TODO: Implement Realistic Implementation
            return Ok();
        }

    }
}