using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store_Sys.Models.ViewModels
{
    public class OutFileViewModel
    {
        public OutFiles OutFile { get; set; } = new OutFiles();


        public List<OutMaterialsFile>? Items { get; set; } = new List<OutMaterialsFile>();


        // DropDowns
        public IEnumerable<SelectListItem>? SourcesList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? MaterialsList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? UnitsList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? OutputTypesList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? PersonsList { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem>? DepartmentsList { get; set; } = new List<SelectListItem>(); // لو فيه جهات
        public IEnumerable<SelectListItem>? YearsList { get; set; } = new List<SelectListItem>();


    }
}
