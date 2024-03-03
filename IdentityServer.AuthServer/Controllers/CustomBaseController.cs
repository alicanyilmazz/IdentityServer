using IdentityServer.SharedLibrary.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.AuthServer.Controllers
{
    public class CustomBaseController : ControllerBase
    {
        public IActionResult ActionResultInstance<T>(Response<T> response) where T : class
        {
            return new ObjectResult(response)
            {
                StatusCode = response.StatusCode
            };
        }
    }
}
