using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Store_Sys.Models;

namespace Store_Sys.Controllers
{
    public class StoreController : Controller
    {
        private readonly AppDbContext _context;

        public StoreController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(string searchName, string searchCode)
        {
            var materials = _context.Materials
                .Select(m => new MaterialViewModel
                {
                    Name = m.Name,
                    Code = m.Code,
                    InCount = m.InMaterialsFile.Sum(x => (int?)x.Quantity) ?? 0,
                    OutCount = m.OutMaterialsFile.Sum(x => (int?)x.Quantity) ?? 0,
                });

            // فلترة حسب الاسم باستخدام EF.Functions.Like
            if (!string.IsNullOrEmpty(searchName))
            {
                materials = materials.Where(m => EF.Functions.Like(m.Name, $"%{searchName}%"));
            }

            // فلترة حسب الكود
            if (!string.IsNullOrEmpty(searchCode))
            {
                materials = materials.Where(m => m.Code.ToString().Contains(searchCode));
            }

            // تحقق إذا كانت النتيجة فارغة
            var materialsList = materials.ToList();
            if (materialsList.Count == 0)
            {
                ViewData["NoResultsMessage"] = "لا توجد  نتائج  تطابق المعايير المدخلة.";  // رسالة للإظهار في الـ View
            }
            // تمرير القيم إلى ViewData
            ViewData["SearchName"] = searchName;
            ViewData["SearchCode"] = searchCode;

            return View(materialsList);
        }




    }

    // ViewModel خاص للعرض
    public class MaterialViewModel
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int InCount { get; set; }
        public int OutCount { get; set; }
        public int Remaining => InCount - OutCount; // خاصية لحساب المتبقي تلقائياً
    }
}