using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store_Sys.Models
{
    public class OutMaterials
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "يجب اختيار اسم المادة")]
        [Display(Name = "اسم المادة")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب اختيار اسم المادة")]
        public int MaterialId { get; set; }


        [ForeignKey("MaterialId")]
        public Materials? Material { get; set; }




        [Required(ErrorMessage = "يجب إدخال العدد")]
        [Display(Name = "العدد")]
        [Range(1, int.MaxValue, ErrorMessage = "العدد يجب أن يكون أكبر من صفر")]
        public int Quantity { get; set; } // 👈 العدد المدخل


        [Required(ErrorMessage = "يجب اختيار نوع الإخراج")]
        [Display(Name = "نوع الإخراج")]
        public OutputType OutputType { get; set; }





        [Display(Name = " القسم")]
        [Range(1, int.MaxValue, ErrorMessage = "يرجى اختيار  القسم")]
        public int? DepartmentId { get; set; }

        [ForeignKey("DepartmentId")]
        public Department? Department { get; set; }



       

        [Display(Name = " الشخص")]
        [Range(1, int.MaxValue, ErrorMessage = "يرجى اختيار  الشخص")]
        public int? PersonId { get; set; }

        [ForeignKey("PersonId")]
        public Person? Person { get; set; }




        [Required(ErrorMessage = "يجب إدخال مصدر المادة ")]
        [Display(Name = " مصدر المادة")]
        public string Source { get; set; }





        [Required(ErrorMessage = "يجب إدخال رقم المستند")]
        [Display(Name = "رقم المستند")]
        public string DocumentNumber { get; set; }



        public virtual ICollection<Materials> Materials { get; set; } = new List<Materials>();


    }
 
    public enum OutputType
    {
        [Display(Name = "إخراج شخصي")]
        Personal = 1,

        [Display(Name = "إخراج قسم")]
        Department = 2
    }
}


