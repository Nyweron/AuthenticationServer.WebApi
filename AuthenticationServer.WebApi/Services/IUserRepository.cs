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
    }
}