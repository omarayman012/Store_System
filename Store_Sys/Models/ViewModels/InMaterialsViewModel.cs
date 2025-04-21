using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store_Sys.Models.ViewModels
{
    public class InMaterialsViewModel
    {
        public InMaterials? InMaterial { get; set; }
        public List<Materials>? Materials { get; set; }
        public List<YearsDate>? Years { get; set; }
    }
}
