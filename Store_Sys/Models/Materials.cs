using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Store_Sys.Models
{
    public class Materials
    {
        [Key]
        public int Id { get; set; }




        [Required]
        public string Name { get; set; }



        
        [Required(ErrorMessage = "يجب كود المادة")]
        [Display(Name = "كود المادة")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ان يكون كود المادة اكبر من الصفر")]
        public int Code { get; set; }



        public string? Details { get; set; }



        [Required(ErrorMessage = "يجب اختيار الوحدة")]
        [Display(Name = "الوحدة")]
        public int? UnitsId { get; set; }

        [ForeignKey("UnitsId")]
        public Units? Units { get; set; }




        [NotMapped]
        public List<SelectListItem>? UnitsList { get; set; }

        public ICollection<InMaterialsFile>? InMaterialsFile { get; set; }
        public ICollection<OutMaterialsFile>? OutMaterialsFile { get; set; }
    }
}
