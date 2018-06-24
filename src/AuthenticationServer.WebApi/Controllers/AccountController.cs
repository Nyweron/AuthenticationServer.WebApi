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
using AuthenticationServer.WebApi.Services.Auth;
using AuthenticationServer.WebApi.Settings.Options;
using AutoMapper;
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AuthenticationServer.WebApi.Controllers
{
  public partial class AccountController : BaseController
  {
    private readonly JwtOptions _jwtOptions;
    private readonly IJwtProvider _jwtProvider;
    private readonly IUserRepository _userRepository;
    private readonly Microsoft.Extensions.Configuration.IConfiguration _configuration;
    private ILogger<AccountController> _logger;
       private IMapper _mapper;

    public AccountController(
      IUserRepository userRepository,
      Microsoft.Extensions.Configuration.IConfiguration configuration,
      IOptions<JwtOptions> jwtOptions,
      IJwtProvider jwtProvider,
      ILogger<AccountController> logger,
      IMapper mapper
    )
    {
      _userRepository = userRepository;
      _configuration = configuration;
      _jwtOptions = jwtOptions.Value;
      _jwtProvider = jwtProvider;
      _logger = logger;
      _mapper = mapper;
    }

    [Authorize(Policy = "admin")]
    [HttpGet("Protectedadmin")]
    public async Task<object> Protectedadmin()
    {
      await Task.CompletedTask;
      return "Protected area Admin";
    }

    [Authorize(Policy = "user")]
    [HttpGet("Protecteduser")]
    public async Task<object> Protecteduser()
    {
      await Task.CompletedTask;
      return "Protected area User";
    }

    [Authorize]
    [HttpGet("me")]
    public ActionResult Get() => Content(User.Identity.Name);

    [HttpPost("login")]
    public async Task<object> Login([FromBody] LoginDto model)
    {
      var result = _userRepository.GetUserByEmail(model.Email);
      if (result == null)
      {
        _logger.LogError($"User with that email {model.Email} wasn't found when accessing to AccountController/login");
        return NotFound();
      }

      if (result.Password != model.Password)
      {
        _logger.LogError($"Password is incorrect. When accessing to AccountController/login");
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        _logger.LogError($"ModelState is not valid. When accessing to AccountController/login");
        return BadRequest(ModelState);
      }

      if (result != null && result.Password == model.Password)
      {
        var appUser = _userRepository.GetUserByEmail(model.Email);
        return await _jwtProvider.GenerateJwtToken(model.Email, appUser);
      }

      throw new ApplicationException("INVALID_LOGIN_ATTEMPT");
    }

    [HttpPost("register")]
    public async Task<object> Register([FromBody] RegisterDto model)
    {
      if (model == null)
      {
        _logger.LogError($"Can not register because registerDto is null. When accessing to AccountController/register");
        return NotFound();
      }

      if (!ModelState.IsValid)
      {
        _logger.LogError($"ModelState is not valid. When accessing to AccountController/register");
        return BadRequest(ModelState);
      }

      // var user = new User
      // {
      //   FirstName = model.FirstName,
      //   LastName = model.LastName,
      //   Login = model.Login,
      //   IsActive = true,
      //   Email = model.Email,
      //   Password = model.Password
      // };

      var userEntity = _mapper.Map<User>(model);
      await _userRepository.AddAsync(userEntity);

      if (!_userRepository.SaveAsync().Result)
      {
        _logger.LogError($"Register is not valid. Error in Save().  When accessing to AccountController/register");
        return StatusCode(500, "A problem happend while handling your request.");
      }


      // _userRepository.Add(user);
      // _userRepository.Save();
      return await _jwtProvider.GenerateJwtToken(model.Email, userEntity);
    }

  }
}