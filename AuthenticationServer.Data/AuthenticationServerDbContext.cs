using System;
using AuthenticationServer.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace AuthenticationServer.Data
{
    public class AuthenticationServerDbContext : DbContext
    {
        public AuthenticationServerDbContext(DbContextOptions<AuthenticationServerDbContext> options) : base(options)
        {
            Database.Migrate();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthToken> UsersAuthTokens { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupsPermissions { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }
    }
}