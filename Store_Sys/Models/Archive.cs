using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Store_Sys.Models
{
    public class Archive
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "يجب إدخال رقم المستند")]
        [Display(Name = "رقم المستند")]
        [Range(1, int.MaxValue, ErrorMessage = "يجب ان يكون رقم المستند اكبر من الصفر")]
        public int DocumentNum { get; set; }


        [Required(ErrorMessage = "يجب إدخال تاريخ المستند")]
        [Display(Name = "تاريخ المستند")]
        public DateTime Documentdate { get; set; }



        [Required(ErrorMessage = "يجب اختيار نوع العملية")]
        [Display(Name = " نوع العملية")]
        public int TypeOfOperationId { get; set; }

        [ForeignKey ("TypeOfOperationId")]
        public TypeOfOperation? OperationType { get; set; }



        [Required(ErrorMessage = "يجب إدخال ملف المستند")]
        [Display(Name = "ملف المستند")]
        public string FilePath { get; set; }
    }
}
