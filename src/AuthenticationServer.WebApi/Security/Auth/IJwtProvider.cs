using System;

namespace AuthenticationServer.WebApi.Security.Auth
{
    public interface IJwtProvider
    {
        JsonWebToken Create(string email, string role);
    }
}