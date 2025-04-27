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
                    InCount = m.InMaterialsFile.Sum(x => (int?)x.Quantity) ?? 0, // لاحظ استخدمنا (int?) لتفادي المشاكل لو ما في بيانات
                    OutCount = m.OutMaterialsFile.Sum(x => (int?)x.Quantity) ?? 0,
                  
                });

            // هنا الفلترة
            if (!string.IsNullOrEmpty(searchName))
            {
                materials = materials.Where(m => m.Name.Contains(searchName));
            }

            if (!string.IsNullOrEmpty(searchCode))
            {
                materials = materials.Where(m => m.Code.ToString().Contains(searchCode));
               
            }

            return View(materials.ToList());
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