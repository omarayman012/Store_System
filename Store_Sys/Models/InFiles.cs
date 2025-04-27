using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Store_Sys.Models
{
    public class InFiles
    {
        [Key]
        public int Id { get; set; }


        [Required(ErrorMessage = "يجب إدخال رقم المستند")]
        [Display(Name = "رقم المستند")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ان يكون رقم المستند اكبر من الصفر")]
        public int DocumentNum { get; set; }



        [Required(ErrorMessage = "يجب إدخال تاريخ المستند")]
        [Display(Name = "تاريخ المستند")]
        public DateTime? Documentdate { get; set; }



        [Required(ErrorMessage = "يجب إدخال اسم الجهة المجهزة")]
        [Display(Name = "اسم الجهة المجهزة")]
        public string NameSupplier { get; set; }



        [Required(ErrorMessage = "يجب إدخال رقم الطلب")]
        [Display(Name = "رقم الطلب")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ان يكون رقم الطلب اكبر من الصفر")]

        public int OrderNum { get; set; }



        [Required(ErrorMessage = "يجب إدخال تاريخ الطلب")]
        [Display(Name = "تاريخ الطلب")]
        public DateTime? OrderDate { get; set; }



        [Required(ErrorMessage = "يجب إدخال رقم الموافقة على التجهيز")]
        [Display(Name = "رقم الموافقة على التجهيز")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ان يكون رقم الموافقة على التجهيز اكبر من الصفر")]
        public int ApprovalNum { get; set; }



        [Required(ErrorMessage = "يجب إدخال تاريخ الموافقة على التجهيز")]
        [Display(Name = "تاريخ الموافقة على التجهيز")]
        public DateTime? ApprovalDate { get; set; }



        [Required(ErrorMessage = "يجب اختيار  مصدر الصرف")]
        [Display(Name = " مصدر الصرف")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب اختيار  مصدر الصرف")]
        public int SourceId { get; set; }



        [ForeignKey("SourceId")]
        public Source? Source { get; set; }




        [Required(ErrorMessage = "يجب إدخال اسم المستلم")]
        [Display(Name = " اسم المستلم")]
       
        public string? TokenName { get; set; }




        public virtual ICollection<InMaterialsFile>? Items { get; set; } = new List<InMaterialsFile>();
        





    }
}
