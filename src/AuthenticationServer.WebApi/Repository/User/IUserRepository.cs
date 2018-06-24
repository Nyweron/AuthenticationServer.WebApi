using System.Collections.Generic;
using System.Threading.Tasks;

namespace AuthenticationServer.WebApi.Repository.User
{
  public interface IUserRepository : IRepository<Entities.User>
  {
    Entities.Password GetPasswordByUserId(int userId);
    Task<Entities.Password> GetPasswordByUserIdAsync(int userId);
    Entities.User GetUserByEmail(string email);
    Task<Entities.User> GetUserByEmailAsync(string email);
    bool UserExists(int userId);
    Task<bool> UserExistsAsync(int userId);
    bool EmailExists(string email);
    Task<bool> EmailExistsAsync(string email);
    void UpdateUser(Entities.User user);
    bool Save();
    Task<bool> SaveAsync();
  }
}