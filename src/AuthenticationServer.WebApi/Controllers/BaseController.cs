using Microsoft.AspNetCore.Mvc;

namespace AuthenticationServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    public abstract class BaseController : Controller
    {
    }
}