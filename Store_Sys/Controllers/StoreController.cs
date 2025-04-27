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

        public IActionResult Index()
        {
            var materials = _context.Materials
                .Select(m => new MaterialViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    Code = m.Code,
                    InCount = _context.InMaterialsFiles
                        .Where(im => im.MaterialId == m.Id)
                        .Sum(im => (int?)im.Quantity) ?? 0,
                    OutCount = _context.OutMaterialsFile
                        .Where(om => om.MaterialId == m.Id)
                        .Sum(om => (int?)om.Quantity) ?? 0
                })
                .ToList();

            return View(materials);
        }
    }

    // ViewModel خاص للعرض
    public class MaterialViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public int InCount { get; set; }
        public int OutCount { get; set; }
        public int Remaining => InCount - OutCount; // خاصية لحساب المتبقي تلقائياً
    }
}