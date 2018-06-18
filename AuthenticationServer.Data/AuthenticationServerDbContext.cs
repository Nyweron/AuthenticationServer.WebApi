using AuthenticationServer.Data.EF;
using AuthenticationServer.Domain;
using AuthenticationServer.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthenticationServer.Data
{
    public class AuthenticationServerDbContext : DbContext
    {
        private readonly IOptions<SqlOptions> _sqlOptions;
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthToken> UsersAuthTokens { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupsPermissions { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }

        public AuthenticationServerDbContext(IOptions<SqlOptions> sqlOptions)
        {
            _sqlOptions = sqlOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_sqlOptions.Value.InMemory)
            {

                optionsBuilder.UseInMemoryDatabase("AuthenticationServer");

                return;
            }

            optionsBuilder.UseSqlServer(_sqlOptions.Value.ConnectionString,
                o => o.MigrationsAssembly("AuthenticationServer.WebApi"));
        }
    }
}