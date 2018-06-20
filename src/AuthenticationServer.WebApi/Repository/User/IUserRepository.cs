using System.Collections.Generic;
namespace AuthenticationServer.WebApi.Repository.User
{
    public interface IUserRepository
    {
        IEnumerable<Entities.User> GetUsers();
        Entities.User GetUserById(int userId);
        Entities.Password GetPasswordByUserId(int userId);
        Entities.User GetUserByEmail(string email);
        bool UserExists(int userId);
        bool EmailExists(string email);
        void AddUser(Entities.User user);
        void AddUsers(IEnumerable<Entities.User> usersList);
        void DeleteUser(Entities.User user);
        void UpdateUser(Entities.User user);
        bool Save();
    }
}