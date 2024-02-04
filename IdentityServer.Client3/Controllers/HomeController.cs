using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Client3.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult AccessDenied(string ReturnUrl)
        {
            ViewBag.url = ReturnUrl;
            return View();
        }
    }
}
