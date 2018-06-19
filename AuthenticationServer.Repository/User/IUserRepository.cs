using System.Collections.Generic;

namespace AuthenticationServer.Repository.User
{
    public interface IUserRepository
    {
        IEnumerable<AuthenticationServer.Domain.Entities.User> GetUsers();
        AuthenticationServer.Domain.Entities.User GetUserById(int userId);
        bool UserExists(int userId);
        bool EmailExists(string email);
        void AddUser(AuthenticationServer.Domain.Entities.User user);
        void AddUsers(IEnumerable<AuthenticationServer.Domain.Entities.User> usersList);
        void DeleteUser(AuthenticationServer.Domain.Entities.User user);
        void UpdateUser(AuthenticationServer.Domain.Entities.User user);
        bool Save();
    }
}