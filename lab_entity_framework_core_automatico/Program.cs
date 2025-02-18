using lab_entity_framework_core.Data;
using Microsoft.EntityFrameworkCore;

namespace lab_entity_framework_core_automatico
{
    internal class Program
    {
        static void Main(string[] args)
        {
            using var db = new ApplicationContext();

            // Essa forma aplica as migrations sempre que a aplicação roda
            //db.Database.Migrate();

            // Esse comando verifica se tem alguma migraçãp pendente
            var existeMigracaoPendente = db.Database.GetPendingMigrations().Any();

            if (existeMigracaoPendente)
            {
                Console.WriteLine("Existe migrações pendentes");
                Console.ReadKey();
            }

            Console.WriteLine("Hello, World!");
        }
    }
}
