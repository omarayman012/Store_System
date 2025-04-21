using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Sys.Models;
using Store_Sys.Models.ViewModels;

namespace Store_Sys.Controllers
{
    public class InMaterialsController : Controller
    {
        private readonly AppDbContext _context;

        public InMaterialsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var inMaterials = _context.InMaterials
               .Include(m => m.Material)
               .Include(y => y.YearDate)
               .ToList();

            return View(inMaterials);
        }

        [HttpGet]
        public IActionResult Add()
        {
            var materials = _context.Materials.ToList();
            var years = _context.YearsDates.ToList();

            var viewModel = new InMaterialsViewModel
            {
                InMaterial = new InMaterials(),
                Materials = materials.Any() ? materials : new List<Materials>(),
                Years = years.Any() ? years : new List<YearsDate>()
            };


            // تمرير القيم عبر SelectList
            ViewBag.MaterialsList = new SelectList(materials, "Id", "Name");
            ViewBag.YearsList = new SelectList(years, "id", "Year");

            return View(viewModel);

        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(InMaterialsViewModel viewModel)
        {
            var materials = _context.Materials.ToList();
            var years = _context.YearsDates.ToList();

            // تمرير القيم عبر SelectList
            ViewBag.MaterialsList = new SelectList(materials, "Id", "Name");
            ViewBag.YearsList = new SelectList(years, "id", "Year");

            // ✅ الحالة الصحيحة: لو النموذج غير صحيح
            if (!ModelState.IsValid)
            {
                // إعادة تحميل القوائم
                viewModel.Materials = materials;
                viewModel.Years = years;

                // إعادة عرض النموذج مع القوائم والبيانات المدخلة
                return View("Add", viewModel);
            }

            // ✅ الحالة السليمة: حفظ البيانات
            _context.InMaterials.Add(viewModel.InMaterial);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var inMaterial = _context.InMaterials
                .Include(m => m.Material)
                .Include(y => y.YearDate)
                .FirstOrDefault(x => x.Id == id);

            if (inMaterial == null)
                return NotFound();

            var materials = _context.Materials.ToList();
            var years = _context.YearsDates.ToList();

            var viewModel = new InMaterialsViewModel
            {
                InMaterial = inMaterial,
                Materials = materials.Any() ? materials : new List<Materials>(),
                Years = years.Any() ? years : new List<YearsDate>()
            };
            ViewBag.MaterialsList = new SelectList(materials, "Id", "Name", viewModel.InMaterial.MaterialCode);
            ViewBag.YearsList = new SelectList(years, "id", "Year", viewModel.InMaterial.YearDateId);

            // تمرير القيم عبر SelectList
            ViewBag.MaterialsList = new SelectList(materials, "Id", "Name");
            ViewBag.YearsList = new SelectList(years, "id", "Year");

            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(InMaterialsViewModel viewModel)
        {
            var materials = _context.Materials.ToList();
            var years = _context.YearsDates.ToList();

            if (!ModelState.IsValid)
            {
                ViewBag.MaterialsList = new SelectList(materials, "Id", "Name", viewModel.InMaterial.MaterialCode);
                ViewBag.YearsList = new SelectList(years, "id", "Year", viewModel.InMaterial.YearDateId);
                // إعادة تحميل القوائم
                viewModel.Materials = materials;
                viewModel.Years = years;

                return View(viewModel);
            }

            _context.InMaterials.Update(viewModel.InMaterial);
            _context.SaveChanges();

            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var inMaterial = _context.InMaterials.Find(id);

            if (inMaterial == null)
                return NotFound();

            _context.InMaterials.Remove(inMaterial);
            _context.SaveChanges();

            TempData["DeleteSuccess"] = "تم حذف المدخل بنجاح!";
            return RedirectToAction("Index");
        }
    }
}