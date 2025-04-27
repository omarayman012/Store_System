using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store_Sys.Models.ViewModels
{
    public class InFileViewModel
    {
        public InFiles InFile { get; set; } = new InFiles();

        public List<InMaterialsFile>? Items { get; set; } = new List<InMaterialsFile>();

        // Dropdown Lists
        public IEnumerable<SelectListItem>? SourcesList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? MaterialsList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? UnitsList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? YearsList { get; set; } = new List<SelectListItem>();
    }
}
