using IdentityServer.AuthServer.Seeds;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.AuthServer.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ConfigsController : ControllerBase
    {
        [HttpGet]
        public async Task SetInitialConfigs()
        {
 
        }
    }
}
