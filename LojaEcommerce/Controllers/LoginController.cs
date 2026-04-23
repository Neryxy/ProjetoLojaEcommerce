using Microsoft.AspNetCore.Mvc;

namespace LojaEcommerce.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
