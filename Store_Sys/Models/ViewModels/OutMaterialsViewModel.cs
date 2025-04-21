namespace Store_Sys.Models.ViewModels
{
    public class OutMaterialsViewModel
    {
        public OutMaterials? OutMaterial { get; set; }
        public List<Materials> Materials { get; set; } = new();
        public List<Department>? Departments { get; set; } = new();
        public List<Person>? Persons { get; set; } = new();
    }
}
