using Microsoft.AspNetCore.Mvc;

namespace Store_Sys.Controllers
{
    public class UserController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View("Login");
        }
    }
}
