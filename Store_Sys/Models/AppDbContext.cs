using Microsoft.EntityFrameworkCore;

namespace Store_Sys.Models
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Materials> Materials { get; set; } 
        public DbSet<InMaterials> InMaterials { get; set; } 
        public DbSet<OutMaterials> OutMaterials { get; set; } 
        public DbSet<YearsDate> YearsDates { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Person> Persons { get; set; }


        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server=185.140.192.61\r\n;Database=StoreDatabase;User Id=team1;Password=Team@1234;TrustServerCertificate=true;MultipleActiveResultSets=True");
            }
        }
    }
}
