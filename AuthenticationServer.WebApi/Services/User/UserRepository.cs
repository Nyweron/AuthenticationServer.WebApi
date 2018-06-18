using System.Collections.Generic;
using System.Linq;
using AuthenticationServer.Data;
using AuthenticationServer.Domain.Entities;

namespace AuthenticationServer.WebApi.Services
{
    public class UserRepository : IUserRepository
    {
        private AuthenticationServerDbContext _context;

        public UserRepository(AuthenticationServerDbContext context)
        {
            _context = context;
        }

        public User GetUserById(int userId)
        {
            return _context.Users.Where(obj => obj.Id == userId).FirstOrDefault();
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }

        public bool UserExists(int userId)
        {
            return _context.Users.Any(c => c.Id == userId);
        }

        public bool EmailExists(string email)
        {
            return _context.Users.Any(c => c.Email == email);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
        }

        public void AddUsers(IEnumerable<User> usersList)
        {
            _context.Users.AddRange(usersList);
        }

        public void DeleteUser(User user)
        {
            _context.Users.Remove(user);
        }

        public void UpdateUser(User user)
        {
            _context.Update(user);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}