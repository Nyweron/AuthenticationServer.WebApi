using System;
using AuthenticationServer.Domain;
using Microsoft.EntityFrameworkCore;


namespace AuthenticationServer.Data
{
    public class AuthenticationServerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthTokens> UsersAuthTokens { get; set; }
        public DbSet<AuthToken> AuthTokens { get; set; }
        public DbSet<Password> Passwords { get; set; }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermissions> GroupsPermissions { get; set; }

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
            optionsBuilder.UseSqlServer(connection);
        }
    }
}