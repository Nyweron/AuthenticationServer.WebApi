using System;
using AuthenticationServer.WebApi.Entities;
using AuthenticationServer.WebApi.Models;
using AuthenticationServer.WebApi.Repository.User;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace AuthenticationServer.WebApi.Controllers
{
  public class UsersController : BaseController
  {
    private IUserRepository _userRepository;
    private ILogger<UsersController> _logger;
    private IMapper _mapper;

    public UsersController(IUserRepository userRepository,
      ILogger<UsersController> logger,
      IMapper mapper)
    {
      _userRepository = userRepository;
      _logger = logger;
      _mapper = mapper;
    }

    [HttpGet]
    public IActionResult GetUsers()
    {
      var userEntities = _userRepository.GetAllAsync().Result;
      return Ok(userEntities);
    }

    [HttpGet("{userId}")]
    public IActionResult Get(int userId)
    {
      try
      {
        if (!_userRepository.UserExistsAsync(userId).Result)
        {
          _logger.LogInformation($"User with id {userId} wasn't found when accessing to UsersController/Get(int userId).");
          return NotFound();
        }

        var userEntities = _userRepository.GetAsync(userId).Result;
        return Ok(userEntities);
      }
      catch (Exception ex)
      {
        _logger.LogCritical($"Exception {userId}.", ex);
        return StatusCode(500, "Problem with your request.");
      }
    }

    [HttpPost]
    public IActionResult Post([FromBody] UserDto user)
    {
      if (user == null)
      {
        _logger.LogInformation($"User is empty when accessing to UsersController/Post(UserDto user).");
        return BadRequest();
      }

      if (_userRepository.EmailExistsAsync(user.Email).Result)
      {
        _logger.LogInformation($"The Email {user.Email} exist in database, use other email. UsersController/Post(UserDto user).");
        return BadRequest($"The Email {user.Email} exist, user other email.");
      }

      if (!ModelState.IsValid)
      {
        return BadRequest(ModelState);
      }

      var userEntity = _mapper.Map<User>(user);
      _userRepository.AddAsync(userEntity);

      if (!_userRepository.SaveAsync().Result)
      {
       _logger.LogError($"Add User is not valid. Error in SaveAsync(). When accessing to UserController/Post");
        return StatusCode(500, "A problem happend while handling your request.");
      }

      //TODO: Implement Realistic Implementation
      return Created("", null);
    }

    [HttpPut("{userId}")]
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

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
      if (!_userRepository.UserExistsAsync(id).Result)
      {
        _logger.LogInformation($"User with id: {id} is not exist. When accessing to UsersController/Delete(int id).");
        return NotFound();
      }

      var user = _userRepository.GetAsync(id).Result;
      _userRepository.Remove(user);

      if (!_userRepository.SaveAsync().Result)
      {
        _logger.LogError($"Delete User is not valid. Error in SaveAsync(). When accessing to UserController/Delete");
        return StatusCode(500, "A problem happend while handling your request.");
      }
      //TODO: Implement Realistic Implementation
      return Ok();
    }

  }
}