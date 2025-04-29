using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using Store_Sys.Models;

namespace Store_Sys.Controllers
{
    public class ArchiveController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ArchiveController(AppDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index(string searchDocumentNum, DateTime? startDate, DateTime? endDate, int? operationTypeId)
        {
            ViewData["searchDocumentNum"] = searchDocumentNum;
            ViewData["startDate"] = startDate;
            ViewData["endDate"] = endDate;
            ViewData["operationTypeId"] = operationTypeId;
            ViewData["OperationTypes"] = new SelectList(_context.TypeOfOperation, "Id", "Name");

            var archivesQuery = _context.Archive.Include(a => a.OperationType).AsQueryable();

            if (!string.IsNullOrEmpty(searchDocumentNum))
                archivesQuery = archivesQuery.Where(a => a.DocumentNum.ToString().Contains(searchDocumentNum));

            if (startDate.HasValue)
                archivesQuery = archivesQuery.Where(a => a.Documentdate >= startDate.Value);

            if (endDate.HasValue)
                archivesQuery = archivesQuery.Where(a => a.Documentdate <= endDate.Value);

            if (operationTypeId.HasValue)
                archivesQuery = archivesQuery.Where(a => a.TypeOfOperationId == operationTypeId.Value);

            var archives = await archivesQuery.ToListAsync();
            return View(archives);
        }





        [HttpGet]
        public IActionResult Add()
        {
            ViewBag.Operations = _context.TypeOfOperation.ToList();
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Add(Archive model, IFormFile FilePath)
        {
            ViewBag.Operations = _context.TypeOfOperation.ToList();

            if (FilePath == null || FilePath.Length == 0)
            {
                ModelState.AddModelError("FilePath", "يجب رفع ملف المستند");
                return View(model);
            }

            // توليد اسم عشوائي للملف وتخزينه داخل wwwroot/uploads
            string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(FilePath.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await FilePath.CopyToAsync(fileStream);
            }

            model.FilePath = "/uploads/" + uniqueFileName;

            _context.Archive.Add(model);
            await _context.SaveChangesAsync();

            TempData["Success"] = "تمت إضافة المستند بنجاح";
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var archive = await _context.Archive.FindAsync(id);
            if (archive == null)
                return NotFound();

            ViewBag.Operations = _context.TypeOfOperation.ToList();
            return View(archive);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Archive model, IFormFile FilePath)
        {
            ViewBag.Operations = _context.TypeOfOperation.ToList();

           

            var existing = await _context.Archive.FindAsync(model.Id);
            if (existing == null)
                return NotFound();

            // تحديث البيانات
            existing.DocumentNum = model.DocumentNum;
            existing.Documentdate = model.Documentdate;
            existing.TypeOfOperationId = model.TypeOfOperationId;

            if (FilePath != null && FilePath.Length > 0)
            {
                // حذف الملف القديم (اختياري)
                if (!string.IsNullOrEmpty(existing.FilePath))
                {
                    var oldPath = Path.Combine(_webHostEnvironment.WebRootPath, existing.FilePath.TrimStart('/'));
                    if (System.IO.File.Exists(oldPath))
                    {
                        System.IO.File.Delete(oldPath);
                    }
                }

                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                    Directory.CreateDirectory(uploadsFolder);

                string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(FilePath.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);

                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await FilePath.CopyToAsync(fileStream);
                }

                existing.FilePath = "/uploads/" + uniqueFileName;
            }

            await _context.SaveChangesAsync();

            TempData["Success"] = "تم تعديل المستند بنجاح";
            return RedirectToAction("Index");

        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var archive = await _context.Archive.FindAsync(id);
            if (archive == null)
                return Json(new { success = false });

            // حذف الملف من السيرفر إن وُجد
            if (!string.IsNullOrEmpty(archive.FilePath))
            {
                var filePath = Path.Combine(_webHostEnvironment.WebRootPath, archive.FilePath.TrimStart('/'));
                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            _context.Archive.Remove(archive);
            await _context.SaveChangesAsync();
            return Json(new { success = true });
        }

    }
}
