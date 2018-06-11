using System;
using AuthenticationServer.Domain;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationServer.Data
{
    public class AuthenticationServerDbContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connection = @"Server=DESKTOP-NJESQAR;Database=AuthenticationServer;Trusted_Connection=True;MultipleActiveResultSets=true";
            optionsBuilder.UseSqlServer(connection);
        }
    }
}