using lab_entity_framework_core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace lab_entity_framework_core_fluent_api.Data
{
    public class ApplicationContextFluentApi : DbContext
    {
        public DbSet<Pessoa> Pessoa { get; set; }

        //Criando log
        private static readonly ILoggerFactory _loggerFactory = LoggerFactory.Create(p =>
        {
            p.AddFilter("Microsoft.EntityFrameworkCore", LogLevel.Information)
             .AddConsole();
        });

        // Este exemplo mostra como adicionar a entidade ao contexto do banco de dados de forma automática
        // somente declarando o tipo public DbSet<Pessoa> Pessoa { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Configurando builder de conexão com log
            optionsBuilder
                .UseLoggerFactory(_loggerFactory)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=DbLabEntityFrameworkFluentApi;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Neste método eu deixo explicíto como serão minhas propriedades no meu contexto
            modelBuilder.Entity<Pessoa>(p =>
            {
                p.ToTable("Pessoa");
                p.HasKey(p => p.Id);
                p.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                p.Property(p => p.Sobrenome).HasColumnType("VARCHAR(80)");
                p.Property(p => p.Cpf).HasColumnType("CHAR(14)").IsRequired();
                p.Property(p => p.TipoPessoa).HasMaxLength(1).IsRequired();
                p.HasIndex(i => i.Cpf).HasName("idx_pessoa_cpf");
            });

            modelBuilder.Entity<Endereco>(e =>
            {
                e.ToTable("Endereco");
                e.HasKey(e => e.Id);
                e.Property(e => e.Logradouro).HasColumnType("VARCHAR(80)").IsRequired();
                e.Property(e => e.Nome).HasColumnType("VARCHAR(80)");
                e.Property(e => e.Numero).HasColumnType("VARCHAR(10)").IsRequired();
                e.Property(e => e.Complemento).HasColumnType("VARCHAR(80)").IsRequired();
            });

            modelBuilder.Entity<Cidade>(c =>
            {
                c.ToTable("Cidade");
                c.HasKey(c => c.Id);
                c.Property(c => c.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            });

            modelBuilder.Entity<Estado>(e =>
            {
                e.ToTable("Estado");
                e.HasKey(e => e.Id);
                e.Property(e => e.Nome).HasColumnType("VARCHAR(80)").IsRequired();
                e.Property(e => e.NomeCidade);
            });
        }
    }
}
