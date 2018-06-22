using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AuthenticationServer.WebApi.Data;
using AuthenticationServer.WebApi.Entities;

namespace AuthenticationServer.WebApi.Repository.User
{
    public class UserRepository : Repository<Entities.User>, IUserRepository
    {
        public AuthenticationServerDbContext AuthenticationServerDbContext
        {
            get { return Context as AuthenticationServerDbContext; }
        }
        public UserRepository(AuthenticationServerDbContext context) : base(context)
        {
        }



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

    }
}