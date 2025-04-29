using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store_Sys.Models
{
    public class InMaterialsFile
    {

        [Key]
        public int Id { get; set; }




        [Required]
        public int InFileId { get; set; }

        [ForeignKey("InFileId")]
        public InFiles InFile { get; set; }




        [Required(ErrorMessage = "يجب اختيار اسم المادة")]
        [Display(Name = "اسم المادة")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب اختيار اسم المادة")]
        public int MaterialId { get; set; }

        [ForeignKey("MaterialId")]
        public Materials Material { get; set; }





        [Required(ErrorMessage = "يجب اختيار الوحدة")]
        [Display(Name = "الوحدة")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب اختيار الوحدة")]
        public int UnitsId { get; set; }

        [ForeignKey("UnitsId")]
        public Units Units { get; set; }




        [Required(ErrorMessage = "يجب إدخال العدد")]
        [Display(Name = "العدد")]
        [Range(1, int.MaxValue, ErrorMessage = "العدد يجب أن يكون أكبر من صفر")]
        public int Quantity { get; set; }





        [Required(ErrorMessage = "يجب إدخال السعر")]
        [Display(Name = "السعر")]
        [Range(1, double.MaxValue, ErrorMessage = "السعر يجب أن يكون أكبر من صفر")]
        public double Price { get; set; }





        [Required(ErrorMessage = "يجب إدخال الثمن الإجمالي")]
        [Display(Name = "الثمن الإجمالي")]
        [Range(1, double.MaxValue, ErrorMessage = "الثمن الإجمالي يجب أن يكون أكبر من صفر")]
        public double Total { get; set; }










        [Display(Name = "تفاصيل إضافية")]
        public string? Details { get; set; }
    }
}
