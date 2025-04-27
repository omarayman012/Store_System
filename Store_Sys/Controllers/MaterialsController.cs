using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_Sys.Models;

namespace Store_Sys.Controllers
{
    public class MaterialsController : Controller
    {
        private readonly AppDbContext _context;
        public MaterialsController(AppDbContext context)
        {
            _context = context;

        }
        public IActionResult Index()
        {
            var Materials = _context.Materials.ToList();
            return View(Materials);
        }
        [HttpGet]
        public IActionResult AddMaterial()
        {
            return View("AddMaterial");
        } 

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaterial(Materials material)
        {
            // تحقق إذا كان الكود موجود بالفعل
            var existingMaterial = _context.Materials
                .FirstOrDefault(m => m.Code == material.Code);

            if (existingMaterial != null)
            {
                ModelState.AddModelError("Code", "كود المادة موجود بالفعل.");
                return View("AddMaterial", material);
            }
           
                _context.Materials.Add(material);
                _context.SaveChanges();
                TempData["Success"] = "تمت إضافة المادة بنجاح!";
                return RedirectToAction("Index");
            

        }

        [HttpGet]
        public IActionResult EditMaterial(int id)
        {
            var material =  _context.Materials
             .FirstOrDefault(m => m.Id == id);

            if (material == null)
            {
                return NotFound();
            }

            return View("EditMaterial", material);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditMaterial(Materials material)
        {
            if (ModelState.IsValid)
            {
                // تحقق إذا كان الكود موجود بالفعل لمادة أخرى
                var existingMaterial = _context.Materials
                    .FirstOrDefault(m => m.Code == material.Code && m.Id != material.Id);

                if (existingMaterial != null)
                {
                    ModelState.AddModelError("Code", "كود المادة موجود بالفعل.");
                    return View("EditMaterial", material);
                }

                _context.Update(material);
                _context.SaveChanges();
                TempData["Success"] = "تم تعديل المادة بنجاح!";
                return RedirectToAction("Index");
            }

            return View(material);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteMaterial(int id)
        {
            var material = _context.Materials.Find(id);
            if (material == null)
            {
                return NotFound();
            }

            _context.Materials.Remove(material);
            _context.SaveChanges();

            return Ok();
        }
        [HttpGet]
        public JsonResult CheckCode(int code)
        {
            var exists = _context.Materials.Any(m => m.Code == code);
            return Json(new { exists = exists });
        }


    }
}
