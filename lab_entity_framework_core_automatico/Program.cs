using lab_entity_framework_core.Data;
using lab_entity_framework_core_automatico.Domain;
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
            //var existeMigracaoPendente = db.Database.GetPendingMigrations().Any();

            //if (existeMigracaoPendente)
            //{
            //    Console.WriteLine("Existe migrações pendentes");
            //    Console.ReadKey();
            //}

            //InseriDados();
            InseriDadosEmLote();
            Console.WriteLine("Hello, World!");
        }

        public static void InseriDados()
        {
            // Não passei os ids porque são incrementados automaticamente
            var pessoa = new Pessoa
            {
                Nome = "Romário",
                Sobrenome = "Camilo",
                Cpf = "10528785630",
                Endereco = new Endereco
                {
                    Logradouro = "Rua",
                    Nome = "Aragon",
                    Numero = 300,
                    Complemento = "Bloco C, Apto 706",
                    Cidade = new Cidade
                    {
                        Nome = "Uberlândia",
                        Estado = new Estado
                        {
                            Nome = "Minas Gerais",
                            NomeCidade = "Erro"
                        }
                    },

                },
                TipoPessoa = TipoPessoa.Fisica
            };

            // Criação do contexto
            using var db = new ApplicationContext();

            // Várias formas de add o objeto no banco
            db.Pessoa.Add(pessoa);
            db.Set<Pessoa>().Add(pessoa);
            db.Entry(pessoa).State = EntityState.Added;
            db.Add(pessoa);

            // Salvando registros
            var registros = db.SaveChanges();
        }
    }
}
