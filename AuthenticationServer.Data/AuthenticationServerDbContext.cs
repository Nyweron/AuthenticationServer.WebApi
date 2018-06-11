using System;
using Microsoft.EntityFrameworkCore;
using AuthenticationServer.Domain;

namespace AuthenticationServer.Data
{
    public class AuthenticationServerDbContext:DbContext
    {
        public DbSet<User> Users { get; set; }
    }
}
