using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static IdentityServer4.IdentityServerConstants;

namespace IdentityServer.AuthServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    [Authorize(LocalApi.PolicyName)]
    public class UserController : ControllerBase
    {
        [HttpPost]
        public IActionResult SignUp()
        {
            return Ok();
        }
    }
}
