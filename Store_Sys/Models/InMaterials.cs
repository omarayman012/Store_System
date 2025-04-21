using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store_Sys.Models
{
    public class InMaterials
    {
        [Key]
        public int Id { get; set; }




        [Required(ErrorMessage = "يجب اختيار اسم المادة")]
        [Display(Name = "اسم المادة")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب اختيار اسم المادة")]
        public int MaterialCode { get; set; }




        [ForeignKey("MaterialCode")]
        public Materials? Material { get; set; }



        [Required(ErrorMessage = "يجب إدخال العدد")]
        [Display(Name = "العدد")]
        [Range(1, int.MaxValue, ErrorMessage = "العدد يجب أن يكون أكبر من صفر")]
        public int Quantity { get; set; } // 👈 العدد المدخل



        [Required(ErrorMessage = "يجب إدخال مصدر المادة")]
        [Display(Name = "مصدر المادة")]
        public string Source { get; set; }




        [Required(ErrorMessage = "يجب إدخال رقم المستند")]
        [Display(Name = "رقم المستند")]
        public string DocumentNumber { get; set; }



        
        [Required(ErrorMessage = "يجب إدخال تاريخ الإدخال")]
        [DataType(DataType.Date)]
        public DateTime EntryDate { get; set; }




        [Required(ErrorMessage = "يجب اختيار سنة التجهيز")]
        [Display(Name = "سنة التجهيز")]
        [Range(1, int.MaxValue, ErrorMessage = "يرجى اختيار سنة التجهيز")]
        public int YearDateId { get; set; }



        [ForeignKey("YearDateId")]
        public YearsDate? YearDate { get; set; }



        [Required(ErrorMessage = "يجب إدخال اسم الجهة المحجوزة لها")]
        public string ReservedFor { get; set; }



        public virtual ICollection<Materials> Materials { get; set; } = new List<Materials>();

    }
}
