using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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

        // عرض المواد
        public IActionResult Index()
        {
            var materials = _context.Materials.Include(m => m.Units).ToList();
            return View(materials);
        }

        // عرض صفحة إضافة مادة جديدة
        [HttpGet]
        public IActionResult AddMaterial()
        {
            var material = new Materials
            {
                UnitsList = _context.Units
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Name
                    })
                    .ToList()
            };

            return View(material);
        }

        // إضافة مادة جديدة
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateMaterial(Materials material)
        {
            if (!ModelState.IsValid)
            {
                // إعادة تعبئة UnitsList في حالة فشل التحقق
                material.UnitsList = _context.Units
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Name
                    })
                    .ToList();
                return View("AddMaterial", material);
            }

            var existingMaterial = _context.Materials
                .FirstOrDefault(m => m.Code == material.Code);

            if (existingMaterial != null)
            {
                ModelState.AddModelError("Code", "كود المادة موجود بالفعل.");

                material.UnitsList = _context.Units
                    .Select(u => new SelectListItem
                    {
                        Value = u.Id.ToString(),
                        Text = u.Name
                    })
                    .ToList();

                return View("AddMaterial", material);
            }

            _context.Materials.Add(material);
            _context.SaveChanges();
            TempData["Success"] = "تمت إضافة المادة بنجاح!";
            return RedirectToAction("Index");
        }

        // عرض صفحة تعديل مادة
        [HttpGet]
        public IActionResult EditMaterial(int id)
        {
            var material = _context.Materials
                .FirstOrDefault(m => m.Id == id);

            if (material == null)
            {
                return NotFound();
            }

            // إضافة قائمة الوحدات عند التعديل
            material.UnitsList = _context.Units
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                })
                .ToList();

            return View("EditMaterial", material);
        }


        // تعديل مادة
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

            // إعادة تعبئة UnitsList في حالة فشل التحقق
            material.UnitsList = _context.Units
                .Select(u => new SelectListItem
                {
                    Value = u.Id.ToString(),
                    Text = u.Name
                })
                .ToList();

            return View("EditMaterial", material);
        }

        // حذف مادة
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

        // التحقق من وجود الكود
        [HttpGet]
        public JsonResult CheckCode(int code)
        {
            var exists = _context.Materials.Any(m => m.Code == code);
            return Json(new { exists = exists });
        }
    }
}
