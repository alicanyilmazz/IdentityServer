using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Client3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.IsActive = "active";
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
