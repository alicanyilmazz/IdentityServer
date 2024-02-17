using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Client3.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {
            ViewBag.IsActive = "active";
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "User");
            }
            return View();
        }
        public IActionResult SignIn()
        {
            return RedirectToAction("Index", "User");
        }
        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.url = ReturnUrl;
            return View();
        }
    }
}
