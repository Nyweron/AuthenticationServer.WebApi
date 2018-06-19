using System;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthenticationServer.WebApi.Models;
using AuthenticationServer.Domain.Entities;
using AuthenticationServer.Repository.User;

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
            try
            {
                if (!_userRepository.UserExists(userId))
                {
                    _logger.LogInformation($"User with id {userId} wasn't found when accessing to UsersController/Get(int userId).");
                    return NotFound();
                }

                var userEntities = _userRepository.GetUserById(userId);
                return Ok(userEntities);
            }
            catch (Exception ex)
            {
                _logger.LogCritical($"Exception {userId}.", ex);
                return StatusCode(500, "Problem with your request.");
            }
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
                _logger.LogInformation($"The Email {user.Email} exist in database, use other email. UsersController/Post(UserDto user).");
                return BadRequest($"The Email {user.Email} exist, user other email.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userEntity = Mapper.Map<User>(user);
            _userRepository.AddUser(userEntity);

            if (!_userRepository.Save())
            {
                return StatusCode(500, "A problem happend while handling your request.");
            }

            //TODO: Implement Realistic Implementation
            return Created("", null);
        }

        [HttpPut("api/users/{userId}")]
        public IActionResult Put([FromBody] UserDto user, int userId)
        {
            if (user == null)
            {
                return BadRequest();
            }

            if (!_userRepository.UserExists(userId))
            {
                return NotFound();
            }

            //When user change own password.
            // if (user.Password1 != user.Password2)
            // {
            //     ModelState.AddModelError("Description", "Pasword has not the same value. Change it.");
            // }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = Mapper.Map<User>(user);
            _userRepository.UpdateUser(updatedUser);

            if (!_userRepository.Save())
            {
                return StatusCode(500, "A problem happend while handling your request.");
            }
            //TODO: Implement Realistic Implementation
            return Ok();
        }

        [HttpDelete("api/users/{id}")]
        public IActionResult Delete(int id)
        {
            if (!_userRepository.UserExists(id))
            {
                return NotFound();
            }

            var user = _userRepository.GetUserById(id);
            _userRepository.DeleteUser(user);

            if (!_userRepository.Save())
            {
                return StatusCode(500, "A problem happend while handling your request.");
            }
            //TODO: Implement Realistic Implementation
            return Ok();
        }

    }
}