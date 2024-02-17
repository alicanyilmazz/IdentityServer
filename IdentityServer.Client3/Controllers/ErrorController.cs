using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Client3.Controllers
{
    public class ErrorController : Controller
    {
        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "Üzgünüz, istediğiniz sayfa bulunamadı.";
                    break;
            }
            return View("NotFound");
        }
    }
}
