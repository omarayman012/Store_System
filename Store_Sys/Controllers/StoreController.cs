using Microsoft.AspNetCore.Mvc;

namespace Store_Sys.Controllers
{
    public class StoreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
