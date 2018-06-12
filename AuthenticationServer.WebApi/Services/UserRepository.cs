using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationServer.Data;
using AuthenticationServer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationServer.WebApi.Services
{
    public class UserRepository : IUserRepository
    {
        private AuthenticationServerDbContext _context;

        public UserRepository(AuthenticationServerDbContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetUsers()
        {
            return _context.Users.ToList();
        }
    }
}