using Microsoft.AspNetCore.Mvc;

namespace Store_Sys.Controllers
{
    public class MaterialsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddMaterial()
        {
            return View("AddMaterial");
        }
        public IActionResult EditMaterial()
        {
            return View("EditMaterial");
        }
    }
}
