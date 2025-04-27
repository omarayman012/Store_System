using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Sys.Models;
using Store_Sys.Models.ViewModels;

namespace Store_Sys.Controllers
{
    public class InFilesController : Controller
    {

        private readonly AppDbContext _context;

        public InFilesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchDocumentNum, string searchOrderNum, DateTime? startDate, DateTime? endDate)
        {
            var filesQuery = _context.InFiles.AsQueryable();

            // إذا تم إدخال رقم المستند
            if (!string.IsNullOrEmpty(searchDocumentNum))
            {
                filesQuery = filesQuery.Where(f => f.DocumentNum.ToString().Contains(searchDocumentNum));
            }

            // إذا تم إدخال رقم الطلب
            if (!string.IsNullOrEmpty(searchOrderNum))
            {
                filesQuery = filesQuery.Where(f => f.OrderNum.ToString().Contains(searchOrderNum));
            }

            // إذا تم إدخال تاريخ البداية
            if (startDate.HasValue)
            {
                filesQuery = filesQuery.Where(f => f.Documentdate >= startDate);
            }

            // إذا تم إدخال تاريخ النهاية
            if (endDate.HasValue)
            {
                filesQuery = filesQuery.Where(f => f.Documentdate <= endDate);
            }

            var files = filesQuery.ToList();
            // تحقق إذا كانت النتائج فارغة
            if (files.Count == 0)
            {
                ViewData["NoResultsMessage"] = "لا توجد  نتائج  تطابق المعايير المدخلة.";  // رسالة للإظهار في الـ View
            }
           

            // تمرير المتغيرات إلى الـ ViewData
            ViewData["searchDocumentNum"] = searchDocumentNum;
            ViewData["searchOrderNum"] = searchOrderNum;
            ViewData["startDate"] = startDate?.ToString("yyyy-MM-dd");
            ViewData["endDate"] = endDate?.ToString("yyyy-MM-dd");

            return View(files);
        }




        public IActionResult AddFile()
        {

            var viewModel = new InFileViewModel
            {
                SourcesList = _context.Source
                     .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }),

                MaterialsList = _context.Materials
                     .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }),

                UnitsList = _context.Units
                     .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }),

                YearsList = _context.YearsDates
                     .Select(y => new SelectListItem { Value = y.id.ToString(), Text = y.Year.ToString() })
            };
            ViewBag.Message = "برجاء ملء جميع الحقول قبل الحفظ";
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFile(InFileViewModel viewModel)
        {
            
                // إضافة ملف (InFile)
                var newInFile = new InFiles
                {
                    DocumentNum = viewModel.InFile.DocumentNum,
                    Documentdate = viewModel.InFile.Documentdate,
                    NameSupplier = viewModel.InFile.NameSupplier,
                    OrderNum = viewModel.InFile.OrderNum,
                    OrderDate = viewModel.InFile.OrderDate,
                    ApprovalNum = viewModel.InFile.ApprovalNum,
                    ApprovalDate = viewModel.InFile.ApprovalDate,
                    SourceId = viewModel.InFile.SourceId,
                    TokenName = viewModel.InFile.TokenName
                    // يمكنك إضافة المزيد من الحقول إذا كان هناك حاجة
                };

                _context.InFiles.Add(newInFile);
                _context.SaveChanges(); // حفظ الملف في قاعدة البيانات

                // إضافة المواد الخاصة بالملف
                foreach (var item in viewModel.Items)
                {
                    var newMaterialFile = new InMaterialsFile
                    {
                        InFileId = newInFile.Id,
                        MaterialId = item.MaterialId,
                        UnitsId = item.UnitsId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Total = item.Total,
                        EntryDate = item.EntryDate,
                        YearDateId = item.YearDateId,
                        Details = item.Details
                    };

                    _context.InMaterialsFiles.Add(newMaterialFile);
                }

                _context.SaveChanges(); // حفظ المواد في قاعدة البيانات

                TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح!";
                return RedirectToAction("Index"); // إعادة التوجيه إلى الصفحة الرئيسية
            

        }

        [HttpGet]
        public IActionResult EditFile(int id)
        {
            // استرجاع الملف من قاعدة البيانات بناءً على الـ id
            var inFile = _context.InFiles
                  .Include(f => f.Items)
                  .FirstOrDefault(f => f.Id == id);

            if (inFile == null)
            {
                return NotFound();
            }



            var viewModel = new InFileViewModel
            {
                InFile = inFile,  // تحميل بيانات المستند
                Items = inFile.Items.ToList(),  // تحميل المواد المرفقة بالمستند
                SourcesList = _context.Source
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }),
                MaterialsList = _context.Materials
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }),
                UnitsList = _context.Units
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }),
                YearsList = _context.YearsDates
                    .Select(y => new SelectListItem { Value = y.id.ToString(), Text = y.Year.ToString() })
            };

            return View(viewModel);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFile(InFileViewModel viewModel)
        {
            // استرجاع الملف من قاعدة البيانات بناءً على الـ id
            var inFile = _context.InFiles.Include(f => f.Items)
                                         .FirstOrDefault(f => f.Id == viewModel.InFile.Id);

            if (inFile == null)
            {
                return NotFound();
            }

            // تحديث بيانات المستند
            inFile.DocumentNum = viewModel.InFile.DocumentNum;
            inFile.Documentdate = viewModel.InFile.Documentdate;
            inFile.NameSupplier = viewModel.InFile.NameSupplier;
            inFile.OrderNum = viewModel.InFile.OrderNum;
            inFile.OrderDate = viewModel.InFile.OrderDate;
            inFile.TokenName = viewModel.InFile.TokenName;
            inFile.ApprovalNum = viewModel.InFile.ApprovalNum;
            inFile.ApprovalDate = viewModel.InFile.ApprovalDate;
            inFile.SourceId = viewModel.InFile.SourceId;


            var existingItems = inFile.Items.ToList();

            // تحديث المواد المرفقة
            foreach (var item in viewModel.Items)
            {
                var materialFile = _context.InMaterialsFiles
                    .FirstOrDefault(m => m.Id == item.Id);

                if (materialFile != null)
                {
                    materialFile.MaterialId = item.MaterialId;
                    materialFile.UnitsId = item.UnitsId;
                    materialFile.Quantity = item.Quantity;
                    materialFile.Price = item.Price;
                    materialFile.Total = item.Total;
                    materialFile.EntryDate = item.EntryDate;
                    materialFile.YearDateId = item.YearDateId;
                    materialFile.Details = item.Details;
                }
                else
                {
                    var newMaterialFile = new InMaterialsFile
                    {
                        InFileId = inFile.Id,
                        MaterialId = item.MaterialId,
                        UnitsId = item.UnitsId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Total = item.Total,
                        EntryDate = item.EntryDate,
                        YearDateId = item.YearDateId,
                        Details = item.Details
                    };
                    _context.InMaterialsFiles.Add(newMaterialFile);
                }
            }
            // إذا كانت هناك أخطاء في النموذج، يجب إعادة تحميل القوائم
            viewModel.SourcesList = _context.Source
                .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name })
                .ToList();
            viewModel.MaterialsList = _context.Materials
                .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name })
                .ToList();
            viewModel.UnitsList = _context.Units
                .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name })
                .ToList();
            viewModel.YearsList = _context.YearsDates
                .Select(y => new SelectListItem { Value = y.id.ToString(), Text = y.Year.ToString() })
                .ToList();
            // في حالة وجود أخطاء في النموذج، يتم عرض نفس الصفحة مع البيانات المدخلة.
            _context.SaveChanges();
            TempData["SuccessMessage"] = "تم تعديل البيانات بنجاح!";
            return RedirectToAction("Index");
        }


            // في حال وجود أخطاء في النموذج
         

        [HttpPost]
        public IActionResult DeleteFile(int id)
        {
            var fileToDelete = _context.InFiles.FirstOrDefault(f => f.Id == id);

            if (fileToDelete != null)
            {
                _context.InFiles.Remove(fileToDelete);
                _context.SaveChanges(); // حفظ التغيير في قاعدة البيانات
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }


    }
}
