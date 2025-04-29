namespace Store_Sys.Models
{
    public class TypeOfOperation
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Archive> Archives { get; set; }

    }
}
