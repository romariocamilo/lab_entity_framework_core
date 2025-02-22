using lab_entity_framework_core_automatico.Domain;
using Microsoft.EntityFrameworkCore;

namespace lab_entity_framework_core.Data
{
    public class ApplicationContext : DbContext
    {
        // Este exemplo mostra como adicionar a entidade ao contexto do banco de dados de forma automática
        // somente declarando o tipo public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbLabEntityFrameworkAutomatico;Integrated Security=True");
        }
    }
}
