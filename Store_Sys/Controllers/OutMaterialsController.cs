using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Sys.Models;
using Store_Sys.Models.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace Store_Sys.Controllers
{
    public class OutMaterialsController : Controller
    {

        private readonly AppDbContext _context;

        public OutMaterialsController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var outMaterials = _context.OutMaterials
               .Include(o => o.Material)
               //.Include(o => o.OutputType)
               .Include(o => o.Person)
               .Include(o => o.Department)
               .ToList();

            return View(outMaterials);
        }


        [HttpGet]
        public IActionResult Add()
        {
            var materials = _context.Materials.ToList();
            var departments = _context.Departments.ToList();
            var persons = _context.Persons.ToList();

            var viewModel = new OutMaterialsViewModel
            {
                OutMaterial = new OutMaterials(),
                Materials = materials,
                Departments = departments,
                Persons = persons,
            
            };

            ViewBag.OutputTypes = Enum.GetValues(typeof(OutputType))
                .Cast<OutputType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.GetType()
                            .GetMember(e.ToString())
                            .First()
                            .GetCustomAttributes(false)
                            .OfType<DisplayAttribute>()
                            .FirstOrDefault()?.Name ?? e.ToString()
                }).ToList();

            ViewBag.MaterialsList = new SelectList(materials, "Id", "Name");
            ViewBag.DepartmentsList = new SelectList(departments, "Id", "Name");
            ViewBag.PersonsList = new SelectList(persons, "Id", "Name");

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(OutMaterialsViewModel viewModel)
        {
            var materials = _context.Materials.ToList();
            var departments = _context.Departments.ToList();
            var persons = _context.Persons.ToList();

            // تعبئة القوائم في ViewBag لعرضها في الـ View
            ViewBag.MaterialsList = new SelectList(materials, "Id", "Name");
            ViewBag.DepartmentsList = new SelectList(departments, "Id", "Name");
            ViewBag.PersonsList = new SelectList(persons, "Id", "Name");

            // التحقق من نوع الإخراج باستخدام OutputType بدلاً من OutputTypeId
            if (viewModel.OutMaterial.OutputType == OutputType.Personal) // إخراج شخصي
            {
                // إخفاء القسم
                viewModel.OutMaterial.DepartmentId = null;
            }
            else if (viewModel.OutMaterial.OutputType == OutputType.Department) // إخراج قسم
            {
                // إخفاء الشخص
                viewModel.OutMaterial.PersonId = null;
            }
            else
            {
                // إذا لم يكن هناك نوع إخراج محدد، قم بإخفاء الحقول
                viewModel.OutMaterial.PersonId = null;
                viewModel.OutMaterial.DepartmentId = null;
            }

            // إضافة المدخل الجديد إلى قاعدة البيانات
            _context.OutMaterials.Add(viewModel.OutMaterial);
            _context.SaveChanges();

            // إعادة التوجيه إلى صفحة الـ Index بعد الحفظ
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var outMaterial = _context.OutMaterials
                .Include(o => o.Material)
                .Include(o => o.Person)
                .Include(o => o.Department)
                .FirstOrDefault(o => o.Id == id);

            if (outMaterial == null)
            {
                return NotFound();
            }

            var viewModel = new OutMaterialsViewModel
            {
                OutMaterial = outMaterial,
                Materials = _context.Materials.ToList(),
                Departments = _context.Departments.ToList(),
                Persons = _context.Persons.ToList()
            };

            ViewBag.OutputTypes = Enum.GetValues(typeof(OutputType))
                .Cast<OutputType>()
                .Select(e => new SelectListItem
                {
                    Value = ((int)e).ToString(),
                    Text = e.GetType()
                            .GetMember(e.ToString())
                            .First()
                            .GetCustomAttributes(false)
                            .OfType<DisplayAttribute>()
                            .FirstOrDefault()?.Name ?? e.ToString()
                }).ToList();

            ViewBag.MaterialsList = new SelectList(viewModel.Materials, "Id", "Name", outMaterial.MaterialId);
            ViewBag.DepartmentsList = new SelectList(viewModel.Departments, "Id", "Name", outMaterial.DepartmentId);
            ViewBag.PersonsList = new SelectList(viewModel.Persons, "Id", "Name", outMaterial.PersonId);

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, OutMaterialsViewModel viewModel)
        {
            if (id != viewModel.OutMaterial.Id)
            {
                return BadRequest();
            }

            if (!ModelState.IsValid)
            {
                // إعادة تعبئة القوائم
                viewModel.Materials = _context.Materials.ToList();
                viewModel.Departments = _context.Departments.ToList();
                viewModel.Persons = _context.Persons.ToList();

                ViewBag.MaterialsList = new SelectList(viewModel.Materials, "Id", "Name", viewModel.OutMaterial.MaterialId);
                ViewBag.DepartmentsList = new SelectList(viewModel.Departments, "Id", "Name", viewModel.OutMaterial.DepartmentId);
                ViewBag.PersonsList = new SelectList(viewModel.Persons, "Id", "Name", viewModel.OutMaterial.PersonId);

                ViewBag.OutputTypes = Enum.GetValues(typeof(OutputType))
                    .Cast<OutputType>()
                    .Select(e => new SelectListItem
                    {
                        Value = ((int)e).ToString(),
                        Text = e.GetType()
                                .GetMember(e.ToString())
                                .First()
                                .GetCustomAttributes(false)
                                .OfType<DisplayAttribute>()
                                .FirstOrDefault()?.Name ?? e.ToString()
                    }).ToList();

                return View(viewModel);
            }

            var existingOutMaterial = _context.OutMaterials.Find(id);
            if (existingOutMaterial == null)
            {
                return NotFound();
            }

            // تحديث البيانات
            existingOutMaterial.MaterialId = viewModel.OutMaterial.MaterialId;
            existingOutMaterial.Quantity = viewModel.OutMaterial.Quantity;
            existingOutMaterial.Source = viewModel.OutMaterial.Source;
            existingOutMaterial.DocumentNumber = viewModel.OutMaterial.DocumentNumber;
            existingOutMaterial.OutputType = viewModel.OutMaterial.OutputType;

            // الإخراج حسب النوع
            if (viewModel.OutMaterial.OutputType == OutputType.Personal)
            {
                existingOutMaterial.PersonId = viewModel.OutMaterial.PersonId;
                existingOutMaterial.DepartmentId = null;
            }
            else if (viewModel.OutMaterial.OutputType == OutputType.Department)
            {
                existingOutMaterial.DepartmentId = viewModel.OutMaterial.DepartmentId;
                existingOutMaterial.PersonId = null;
            }
            else
            {
                existingOutMaterial.PersonId = null;
                existingOutMaterial.DepartmentId = null;
            }

            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var outMaterial = _context.OutMaterials.FirstOrDefault(o => o.Id == id);
            if (outMaterial == null)
            {
                return Json(new { success = false, message = "العنصر غير موجود" });
            }

            _context.OutMaterials.Remove(outMaterial);
            _context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
