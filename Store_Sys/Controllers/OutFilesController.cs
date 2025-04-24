using Microsoft.AspNetCore.Mvc;

namespace Store_Sys.Controllers
{
    public class OutFilesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult AddFile()
        {
            return View("AddFile");
        }
        public IActionResult EditFile()
        {
            return View("EditFile");

        }
    }
}
