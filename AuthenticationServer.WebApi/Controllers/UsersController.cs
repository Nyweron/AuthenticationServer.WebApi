using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.WebApi.Controllers
{

    public class UsersController : Controller
    {
        [HttpGet("api/users")]
        public JsonResult GetUsers()
        {
            return new JsonResult(new List<object>(){
                new {id=1,FirstName="Pierwsze Imie", LastName="Nazwisko", Login="Login1", Email="email@wp.pl", IsActive=1, LastLogin="null"}
            });
        }
    }
}