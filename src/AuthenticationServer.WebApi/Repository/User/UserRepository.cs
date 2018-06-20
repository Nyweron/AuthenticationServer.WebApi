using System.Collections.Generic;
using System.Linq;
using AuthenticationServer.WebApi.Data;
using AuthenticationServer.WebApi.Entities;

namespace AuthenticationServer.WebApi.Repository.User
{
    public class UserRepository : IUserRepository
    {
        private AuthenticationServerDbContext _context;

        public UserRepository(AuthenticationServerDbContext context)
        {
            _context = context;
        }

        public Entities.User GetUserById(int userId)
        {
            return _context.Users.Where(obj => obj.Id == userId).FirstOrDefault();
        }

        public IEnumerable<Entities.User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public Password GetPasswordByUserId(int userId)
        {
            return _context.Passwords.Where(obj => obj.UserId == userId).FirstOrDefault();
        }

        public Entities.User GetUserByEmail(string email)
        {
            return  _context.Users.Where(c => c.Email == email).FirstOrDefault();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(c => c.Id == userId);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(c => c.Email == email);
        }

        public void AddUser(Entities.User user)
        {
            _context.Users.Add(user);
        }

        public void AddUsers(IEnumerable<Entities.User> usersList)
        {
            _context.Users.AddRange(usersList);
        }

        public void DeleteUser(Entities.User user)
        {
            _context.Users.Remove(user);
        }

        public void UpdateUser(Entities.User user)
        {
            _context.Update(user);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}