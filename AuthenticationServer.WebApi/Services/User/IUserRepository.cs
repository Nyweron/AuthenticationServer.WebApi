using System.Collections.Generic;
using AuthenticationServer.Domain.Entities;

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
        void UpdateUser(User user);
        bool Save();
    }
}