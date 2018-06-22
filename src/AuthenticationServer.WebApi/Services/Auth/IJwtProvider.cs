using System.Threading.Tasks;
using AuthenticationServer.WebApi.Entities;

namespace AuthenticationServer.WebApi.Services.Auth
{
    public interface IJwtProvider
    {
        Task<object> GenerateJwtToken(string email, User user);
    }
}