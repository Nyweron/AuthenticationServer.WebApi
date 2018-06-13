using AuthenticationServer.WebApi.Services;
using AuthenticationServer.Data;
using AuthenticationServer.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace AuthenticationServer.WebApi.Controllers
{
    public class UsersController : Controller
    {
        private IUserRepository _userRepository;

        public UsersController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
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
            var userEntities = _userRepository.GetUserById(userId);
            return Ok(userEntities);
        }
    }
}