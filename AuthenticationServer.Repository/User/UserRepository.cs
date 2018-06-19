using System.Collections.Generic;
using System.Linq;
using AuthenticationServer.Data;

namespace AuthenticationServer.Repository.User
{
 public class UserRepository : IUserRepository
    {
        private AuthenticationServerDbContext _context;

        public UserRepository(AuthenticationServerDbContext context)
        {
            _context = context;
        }

        public AuthenticationServer.Domain.Entities.User GetUserById(int userId)
        {
            return _context.Users.Where(obj => obj.Id == userId).FirstOrDefault();
        }

        public IEnumerable<AuthenticationServer.Domain.Entities.User> GetUsers()
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

        public void AddUser(AuthenticationServer.Domain.Entities.User user)
        {
            _context.Users.Add(user);
        }

        public void AddUsers(IEnumerable<AuthenticationServer.Domain.Entities.User> usersList)
        {
            _context.Users.AddRange(usersList);
        }

        public void DeleteUser(AuthenticationServer.Domain.Entities.User user)
        {
            _context.Users.Remove(user);
        }

        public void UpdateUser(AuthenticationServer.Domain.Entities.User user)
        {
            _context.Update(user);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

    }
}