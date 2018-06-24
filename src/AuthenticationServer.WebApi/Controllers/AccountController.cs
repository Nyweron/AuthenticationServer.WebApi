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
using AutoMapper.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
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

    public AccountController(
      IUserRepository userRepository,
      Microsoft.Extensions.Configuration.IConfiguration configuration,
      IOptions<JwtOptions> jwtOptions,
      IJwtProvider jwtProvider
    )
    {
      _userRepository = userRepository;
      _configuration = configuration;
      _jwtOptions = jwtOptions.Value;
      _jwtProvider = jwtProvider;
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
      var user = new User
      {
        FirstName = "test1",
        LastName = "test01",
        Login = "test001",
        IsActive = true,
        Email = model.Email,
        Password = model.Password
      };

      _userRepository.Add(user);
      _userRepository.Save();
      return await _jwtProvider.GenerateJwtToken(model.Email, user);

      throw new ApplicationException("UNKNOWN_ERROR");
    }

  }
}