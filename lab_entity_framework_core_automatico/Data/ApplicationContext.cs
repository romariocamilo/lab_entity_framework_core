using lab_entity_framework_core_automatico.Domain;
using Microsoft.EntityFrameworkCore;

namespace lab_entity_framework_core.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pessoa> Pessoas { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbLabEntityFrameworkAutomatico;Integrated Security=True");
        }
    }
}
