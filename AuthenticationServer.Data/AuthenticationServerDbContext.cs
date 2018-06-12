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

        public static readonly LoggerFactory MyConsoleLoggerFactory
            = new LoggerFactory(new []
            {
                new ConsoleLoggerProvider((category, level) => category == DbLoggerCategory.Database.Command.Name &&
                    level == LogLevel.Information, true)
            });

        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthToken> UsersAuthTokens { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupsPermissions { get; set; }
        public DbSet<UserGroup> UsersGroups { get; set; }

        // protected override void OnModelCreating(ModelBuilder modelBuilder)
        // {
        //     modelBuilder.Entity<UserAuthTokens>()
        //         .HasMany(c => c.Users)
        //         .WithOne(e => e.UsersAuthTokens)
        //         .IsRequired();
        // }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=DESKTOP-NJESQAR;Database=AuthenticationServer;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder
                .UseLoggerFactory(MyConsoleLoggerFactory)
                .UseSqlServer(connection);
        }
    }
}