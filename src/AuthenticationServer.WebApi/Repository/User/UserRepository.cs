using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AuthenticationServer.WebApi.Data;
using AuthenticationServer.WebApi.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationServer.WebApi.Repository.User
{
  public class UserRepository : Repository<Entities.User>, IUserRepository
  {

    public AuthenticationServerDbContext AuthenticationServerDbContext
    {
      get { return Context as AuthenticationServerDbContext; }
    }
    public UserRepository(AuthenticationServerDbContext context) : base(context) { }

    public Password GetPasswordByUserId(int userId)
    {
      return AuthenticationServerDbContext.Passwords.Where(obj => obj.UserId == userId).FirstOrDefault();
    }

    public Entities.User GetUserByEmail(string email)
    {
      return AuthenticationServerDbContext.Users.Where(c => c.Email == email).FirstOrDefault();
    }

    public bool UserExists(int userId)
    {
      return AuthenticationServerDbContext.Users.Any(c => c.Id == userId);
    }

    public bool EmailExists(string email)
    {
      return AuthenticationServerDbContext.Users.Any(c => c.Email == email);
    }

    public void UpdateUser(Entities.User user)
    {
      AuthenticationServerDbContext.Update(user);
    }

    public bool Save()
    {
      return (AuthenticationServerDbContext.SaveChanges() >= 0);
    }

    public async Task<Password> GetPasswordByUserIdAsync(int userId)
    {
      //Is this good way?
      return await AuthenticationServerDbContext.Passwords.Where(obj => obj.UserId == userId).FirstOrDefaultAsync();
    }

    public async Task<Entities.User> GetUserByEmailAsync(string email)
    {
      //Is this good way?
      return await AuthenticationServerDbContext.Users.Where(c => c.Email == email).FirstOrDefaultAsync();
    }

    public async Task<bool> UserExistsAsync(int userId)
    {
      //Is this good way?
      var userExist = await AuthenticationServerDbContext.Users.FindAsync(userId);
      return userExist != null ? true : false;
    }

    public async Task<bool> EmailExistsAsync(string email)
    {
      //Is this good way?
      return await AuthenticationServerDbContext.Users.AnyAsync(c => c.Email == email);
    }

    public async Task<bool> SaveAsync()
    {
      var result = await AuthenticationServerDbContext.SaveChangesAsync();
      return (result >= 0);
    }
  }
}