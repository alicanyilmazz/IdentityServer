using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace IdentityServer.Client3.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        public async Task<IActionResult> Index()
        {
            var authenticationValues = await HttpContext.AuthenticateAsync();
            if (authenticationValues != null)
            {
                var properties = authenticationValues.Properties.Items;
                foreach (var property in properties)
                {
                    Debug.WriteLine($"{property.Key} - {property.Value}");
                }
            }
            foreach (var claim in User.Claims)
            {
                Debug.WriteLine($"{claim.Type} - {claim.Value}");
            }
            var authenticatedUserClaim = User.Claims.Where(x=>x.Type == ClaimTypes.NameIdentifier).FirstOrDefault();
            var authenticatedUserCountry = User.Claims.Where(x => x.Type == ClaimTypes.Country).FirstOrDefault();
            var authenticatedUserId = authenticatedUserClaim.Value;
            Debug.WriteLine($"{authenticatedUserId}");
            return View();
        }
    }
}
