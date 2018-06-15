using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthenticationServer.Data;
using AuthenticationServer.Domain;


namespace AuthenticationServer.WebApi.Services
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
        User GetUserById(int userId);
        bool UserExists(int userId);
        bool EmailExists(string email);
        void AddUser(User user);
        void AddUsers(IEnumerable<User> usersList);
        void DeleteUser(User user);
        bool Save();
    }
}