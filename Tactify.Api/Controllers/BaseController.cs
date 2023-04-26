using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Tactify.Api.Controllers
{
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected string Username => "TactifyUser";
    }
}
