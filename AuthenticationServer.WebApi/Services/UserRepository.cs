using System.Collections.Generic;
using System.Linq;
using AuthenticationServer.Data;
using AuthenticationServer.Domain;

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
    }
}