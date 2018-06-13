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
                _logger.LogInformation($"User with id {userId} wasn't found when accessing User.");
                return NotFound();
            }

            var userEntities = _userRepository.GetUserById(userId);
            return Ok(userEntities);
        }

        [HttpPost]
        public IActionResult Post([FromBody] UserDto user)
        {
            if (user == null)
            {
                return BadRequest();
            }
            //TODO: Implement Realistic Implementation
            return Created("", null);
        }

    }
}