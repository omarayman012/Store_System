using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Store_Sys.Models;
using Store_Sys.Models.ViewModels;

namespace Store_Sys.Controllers
{
    public class OutFilesController : Controller
    {

        private readonly AppDbContext _context;

        public OutFilesController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchDocumentNum, string searchOrderNum, string searchApprovalNum, DateTime? startDate, DateTime? endDate, string searchPerson, string searchDepartment)
        {
            // استرجاع جميع المستندات من قاعدة البيانات مع تحميل الأقسام والأشخاص
            var filesQuery = _context.OutFiles
                                     .Include(f => f.Department)  // تحميل البيانات المرتبطة بالقسم
                                     .Include(f => f.Person)      // تحميل البيانات المرتبطة بالشخص
                                     .AsQueryable();

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

            // إذا تم إدخال رقم الموافقة
            if (!string.IsNullOrEmpty(searchApprovalNum))
            {
                filesQuery = filesQuery.Where(f => f.ApprovalNum.ToString().Contains(searchApprovalNum));
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

            // إذا تم إدخال اسم الشخص
            if (!string.IsNullOrEmpty(searchPerson))
            {
                filesQuery = filesQuery.Where(f => f.PersonPrepared.Contains(searchPerson));
            }

            // إذا تم إدخال القسم
            if (!string.IsNullOrEmpty(searchDepartment))
            {
                filesQuery = filesQuery.Where(f => f.Department.Name.Contains(searchDepartment));
            }

            // استرجاع النتائج بعد التصفية
            var files = filesQuery.ToList();

            // إذا لم توجد نتائج، يمكنك إرسال رسالة للمستخدم
            if (!files.Any())
            {
                ViewData["NoResultsMessage"] = "لا توجد نتائج تطابق معايير البحث.";
            }

            return View(files);
        }


        public IActionResult AddFile()
        {
            var viewModel = new OutFileViewModel
            {
                SourcesList = _context.Source
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }),

                MaterialsList = _context.Materials
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }),

                UnitsList = _context.Units
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }),

                OutputTypesList = _context.OutputTypes
                    .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Name }),

                PersonsList = _context.Persons
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }),

                DepartmentsList = _context.Departments
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }),

                YearsList = _context.YearsDates
                    .Select(y => new SelectListItem { Value = y.id.ToString(), Text = y.Year.ToString() })
            }; 
            ViewBag.Message = "برجاء ملء جميع الحقول قبل الحفظ";
            return View(viewModel);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddFile(OutFileViewModel viewModel)
        {
            // إضافة ملف (outfile)
            var NewOutFile = new OutFiles
            {
                DocumentNum = viewModel.OutFile.DocumentNum,
                Documentdate = viewModel.OutFile.Documentdate,
                OrderNum = viewModel.OutFile.OrderNum,
                OrderDate = viewModel.OutFile.OrderDate,
                OutputTypeId = viewModel.OutFile.OutputTypeId,
                DepartmentId = viewModel.OutFile.DepartmentId,
                PersonId = viewModel.OutFile.PersonId,
                SourceId = viewModel.OutFile.SourceId,
                ApprovalNum = viewModel.OutFile.ApprovalNum,
                ApprovalDate = viewModel.OutFile.ApprovalDate,
                PreparedPerson = viewModel.OutFile.PreparedPerson,
                PersonPrepared = viewModel.OutFile.PersonPrepared,

                // يمكنك إضافة المزيد من الحقول إذا كان هناك حاجة
            };
            _context.OutFiles.Add(NewOutFile);
            _context.SaveChanges(); // حفظ الملف في قاعدة البيانات


            // إضافة المواد الخاصة بالملف
            foreach (var item in viewModel.Items)
            {
                var newMaterialFile = new OutMaterialsFile
                {
                    OutFileId = NewOutFile.Id,
                    MaterialId = item.MaterialId,
                    UnitsId = item.UnitsId,
                    Quantity = item.Quantity,
                    Price = item.Price,
                    Total = item.Total,
                    EntryDate = item.EntryDate,
                    YearDateId = item.YearDateId,
                    Details = item.Details
                };

               _context.OutMaterialsFile.Add(newMaterialFile);
            }
            _context.SaveChanges(); // حفظ المواد في قاعدة البيانات

            TempData["SuccessMessage"] = "تم إضافة البيانات بنجاح!";
            return RedirectToAction("Index"); // إعادة التوجيه إلى الصفحة الرئيسية

        }
        public IActionResult EditFile(int id)
        {
            var outFile = _context.OutFiles
                .Include(o => o.Items)
                .FirstOrDefault(o => o.Id == id);

            if (outFile == null)
            {
                return NotFound();
            }

            var viewModel = new OutFileViewModel
            {
                OutFile = outFile,
                Items = outFile.Items.ToList(),
                SourcesList = _context.Source
                    .Select(s => new SelectListItem { Value = s.Id.ToString(), Text = s.Name }),

                MaterialsList = _context.Materials
                    .Select(m => new SelectListItem { Value = m.Id.ToString(), Text = m.Name }),

                UnitsList = _context.Units
                    .Select(u => new SelectListItem { Value = u.Id.ToString(), Text = u.Name }),

                OutputTypesList = _context.OutputTypes
                    .Select(o => new SelectListItem { Value = o.Id.ToString(), Text = o.Name }),

                PersonsList = _context.Persons
                    .Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Name }),

                DepartmentsList = _context.Departments
                    .Select(d => new SelectListItem { Value = d.Id.ToString(), Text = d.Name }),

                YearsList = _context.YearsDates
                    .Select(y => new SelectListItem { Value = y.id.ToString(), Text = y.Year.ToString() })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditFile(OutFileViewModel viewModel)
        {
     
            var outFile = _context.OutFiles.Include(f => f.Items)
                .FirstOrDefault(f => f.Id == viewModel.OutFile.Id);

            if (outFile == null)
            {
                return NotFound();
            }

            // تحديث الحقول
            outFile.DocumentNum = viewModel.OutFile.DocumentNum;
            outFile.Documentdate = viewModel.OutFile.Documentdate;
            outFile.OrderNum = viewModel.OutFile.OrderNum;
            outFile.OrderDate = viewModel.OutFile.OrderDate;
            outFile.OutputTypeId = viewModel.OutFile.OutputTypeId;
            outFile.DepartmentId = viewModel.OutFile.DepartmentId;
            outFile.PersonId = viewModel.OutFile.PersonId;
            outFile.SourceId = viewModel.OutFile.SourceId;
            outFile.ApprovalNum = viewModel.OutFile.ApprovalNum;
            outFile.ApprovalDate = viewModel.OutFile.ApprovalDate;
            outFile.PreparedPerson = viewModel.OutFile.PreparedPerson;
            outFile.PersonPrepared = viewModel.OutFile.PersonPrepared;

            // تحديث أو إضافة العناصر المرتبطة
            foreach (var item in viewModel.Items)
            {
                var existingItem = outFile.Items.FirstOrDefault(i => i.Id == item.Id);
                if (existingItem != null)
                {
                    existingItem.MaterialId = item.MaterialId;
                    existingItem.UnitsId = item.UnitsId;
                    existingItem.Quantity = item.Quantity;
                    existingItem.Price = item.Price;
                    existingItem.Total = item.Total;
                    existingItem.EntryDate = item.EntryDate;
                    existingItem.YearDateId = item.YearDateId;
                    existingItem.Details = item.Details;
                }
                else
                {
                    var newItem = new OutMaterialsFile
                    {
                        OutFileId = outFile.Id,
                        MaterialId = item.MaterialId,
                        UnitsId = item.UnitsId,
                        Quantity = item.Quantity,
                        Price = item.Price,
                        Total = item.Total,
                        EntryDate = item.EntryDate,
                        YearDateId = item.YearDateId,
                        Details = item.Details
                    };
                    _context.OutMaterialsFile.Add(newItem);
                }
            }

            _context.SaveChanges(); // حفظ التعديلات في قاعدة البيانات

            TempData["SuccessMessage"] = "تم تعديل البيانات بنجاح!";
            return RedirectToAction(nameof(Index)); // أو أي أكشن آخر تريد التوجيه إليه
        }

        [HttpPost]
        public IActionResult DeleteFile(int id)
        {
            var fileToDelete = _context.OutFiles.FirstOrDefault(f => f.Id == id);

            if (fileToDelete != null)
            {
                _context.OutFiles.Remove(fileToDelete);
                _context.SaveChanges();
                return Json(new { success = true });
            }

            return Json(new { success = false });
        }

    }
}
