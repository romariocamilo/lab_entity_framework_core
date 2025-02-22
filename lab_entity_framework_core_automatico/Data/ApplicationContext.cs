using lab_entity_framework_core_automatico.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lab_entity_framework_core.Data
{
    public class ApplicationContext : DbContext
    {
        //Criando log
        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(p =>
        {
            p.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Information)
             .AddConsole();
        });

        // Este exemplo mostra como adicionar a entidade ao contexto do banco de dados de forma automática
        // somente declarando o tipo public DbSet<Pessoa> Pessoa { get; set; }
        public DbSet<Pessoa> Pessoa { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Configurando builder de conexão com log
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbLabEntityFrameworkAutomatico;Integrated Security=True");
        }
    }
}
