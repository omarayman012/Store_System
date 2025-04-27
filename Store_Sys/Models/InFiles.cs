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
        [Range(1, int.MaxValue, ErrorMessage = "يجب إدخال اسم المستلم")]
        public string? TokenName { get; set; }




        public virtual ICollection<InMaterialsFile>? Items { get; set; } = new List<InMaterialsFile>();
        





    }
}

        //[Required(ErrorMessage = "يجب اختيار اسم المادة")]
        //[Display(Name = "اسم المادة")]
        //[Range(1, int.MaxValue, ErrorMessage = "يجب اختيار اسم المادة")]
        //public int MaterialId { get; set; }



        //[ForeignKey("MaterialId")]
        //public Materials? Material { get; set; }   
        
        
        
        
        //[Required(ErrorMessage = "يجب اختيار  الوحدة")]
        //[Display(Name = " الوحدة")]
        //[Range(1, int.MaxValue, ErrorMessage = "يجب اختيار الوحدة ")]
        //public int UnitsId { get; set; }



        //[ForeignKey("UnitsId")]
        //public Units? Units { get; set; }



        //[Required(ErrorMessage = "يجب إدخال العدد")]
        //[Display(Name = "العدد")]
        //[Range(1, int.MaxValue, ErrorMessage = "العدد يجب أن يكون أكبر من صفر")]
        //public int Quantity { get; set; } // 👈 العدد المدخل




        //[Required(ErrorMessage = "يجب إدخال السعر")]
        //[Display(Name = "السعر")]
        //[Range(1, int.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        //public double Price { get; set; }




        //[Required(ErrorMessage = "يجب إدخال الثمن الاجمالى")]
        //[Display(Name = "الثمن الاجمالى")]
        //[Range(1, int.MaxValue, ErrorMessage = "الثمن الاجمالى يجب أن يكون أكبر من صفر")]
        //public double Total { get; set; }




        //[Required(ErrorMessage = "يجب إدخال تاريخ الإدخال")]
        //[DataType(DataType.Date)]
        //public DateTime EntryDate { get; set; }




        //[Required(ErrorMessage = "يجب اختيار سنة التجهيز")]
        //[Display(Name = "سنة التجهيز")]
        //[Range(1, int.MaxValue, ErrorMessage = "يرجى اختيار سنة التجهيز")]
        //public int YearDateId { get; set; }



        //[ForeignKey("YearDateId")]
        //public YearsDate? YearDate { get; set; }




        // 👇 العلاقة مع المواد المدخلة في المستند
