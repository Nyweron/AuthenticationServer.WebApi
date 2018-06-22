using System.Collections.Generic;
namespace AuthenticationServer.WebApi.Repository.User
{
    public interface IUserRepository : IRepository<Entities.User>
    {
        Entities.Password GetPasswordByUserId(int userId);
        Entities.User GetUserByEmail(string email);
        bool UserExists(int userId);
        bool EmailExists(string email);
        void UpdateUser(Entities.User user);
        bool Save();
    }
}