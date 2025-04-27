using System.ComponentModel.DataAnnotations;

namespace Store_Sys.Models
{
    public class Materials
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int Code { get; set; }

        public string? Details { get; set; }
        public ICollection<InMaterialsFile> InMaterialsFile { get; set; }
        public ICollection<OutMaterialsFile> OutMaterialsFile { get; set; }
    }
}
