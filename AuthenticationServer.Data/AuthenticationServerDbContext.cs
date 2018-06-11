using System;
using AuthenticationServer.Domain;
using Microsoft.EntityFrameworkCore;


namespace AuthenticationServer.Data
{
    public class AuthenticationServerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<UserAuthTokens> UsersAuthTokens { get; set; }

        // protected override void OnModelCreating(Modelbuilder modelBuilder)
        // {
        //     modelBuilder.Entity<UserAuthTokens>()
        //         .HasMany(c => c.User)
        //         .WithOne(e => e.UserAuthTokens)
        //         .IsRequired();
        // }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=DESKTOP-NJESQAR;Database=AuthenticationServer;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connection);
        }


    }
}