using Microsoft.EntityFrameworkCore;

namespace Store_Sys.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Materials> Materials { get; set; } 
        public DbSet<YearsDate> YearsDates { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }
        public DbSet<Source> Source { get; set; }
        public DbSet<Units> Units { get; set; }
        public DbSet<InFiles> InFiles { get; set; }
        public DbSet<InMaterialsFile> InMaterialsFiles { get; set; }
        public DbSet<OutputTypes> OutputTypes { get; set; }
        public DbSet<OutFiles> OutFiles { get; set; }
        public DbSet<OutMaterialsFile> OutMaterialsFile { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=185.140.192.61\r\n;Database=StoreDatabase;User Id=team1;Password=Team@1234;TrustServerCertificate=true;MultipleActiveResultSets=True");
            }
        }
    }
}
