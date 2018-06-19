using AuthenticationServer.WebApi.Entities;
using AuthenticationServer.WebApi.Settings.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace AuthenticationServer.WebApi.Data
{
    public class AuthenticationServerDbContext : DbContext
    {
        private readonly IOptions<DatabaseOptions> _databaseOptions;
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthToken> UsersAuthTokens { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupsPermissions { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }

        public AuthenticationServerDbContext(IOptions<DatabaseOptions> databaseOptions)
        {
            _databaseOptions = databaseOptions;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured)
            {
                return;
            }

            if (_databaseOptions.Value.InMemory)
            {

                optionsBuilder.UseInMemoryDatabase("AuthenticationServer");

                return;
            }

            optionsBuilder.UseSqlServer(_databaseOptions.Value.ConnectionString,
                o => o.MigrationsAssembly("AuthenticationServer.WebApi"));
        }
    }
}