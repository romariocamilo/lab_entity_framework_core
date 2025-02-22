using lab_entity_framework_core.Data;
using lab_entity_framework_core_automatico.Domain;
using Microsoft.EntityFrameworkCore;

namespace lab_entity_framework_core_automatico
{
    internal class Program
    {
        public static ApplicationContext db = new ApplicationContext();

        static void Main(string[] args)
        {
            // Criação do contexto
            using var db = new ApplicationContext();


            //ExecutaMigrations();
            //ExecutaMigrationsPendentesCasoExista();
            //InseriDados();
            //InseriDadosEmLote();
            //ConsultaDadosPorSintaxe();
            //ConsultaDadosPorMetodoLambda();

            Console.WriteLine("Hello, World!");
        }

        public static void ExecutaMigrations()
        {
            // Essa forma aplica as migrations sempre que a aplicação roda
            db.Database.Migrate();
        }

        public static void ExecutaMigrationsPendentesCasoExista()
        {
            // Esse comando verifica se tem alguma migraçãp pendente
            var existeMigracaoPendente = db.Database.GetPendingMigrations().Any();

            if (existeMigracaoPendente)
            {
                Console.WriteLine("Existe migrações pendentes");
                Console.ReadKey();
            }
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

            // Várias formas de add o objeto no banco
            db.Pessoa.Add(pessoa);
            db.Set<Pessoa>().Add(pessoa);
            db.Entry(pessoa).State = EntityState.Added;
            db.Add(pessoa);

            // Salvando registros
            var registros = db.SaveChanges();
        }

        public static void InseriDadosEmLote()
        {
            // Criando os dois objetos de pessoa
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
            var pessoaDois = new Pessoa
            {
                Nome = "Romário 2",
                Sobrenome = "Camilo 2",
                Cpf = "10528785630 2",
                Endereco = new Endereco
                {
                    Logradouro = "Rua 2",
                    Nome = "Aragon 2",
                    Numero = 300,
                    Complemento = "Bloco C, Apto 706 2",
                    Cidade = new Cidade
                    {
                        Nome = "Uberlândia 2",
                        Estado = new Estado
                        {
                            Nome = "Minas Gerais 2",
                            NomeCidade = "Erro 2"
                        }
                    },

                },
                TipoPessoa = TipoPessoa.Fisica
            };

            // Criando a lista e inserindo as pessoas nela
            var listaPessoas = new List<Pessoa>
            {
                pessoa,
                pessoaDois
            };

            // Várias formas de add a lista no banco
            db.Pessoa.AddRange(listaPessoas);
            db.Set<Pessoa>().AddRange(listaPessoas);
            db.AddRange(listaPessoas);


            // Salvando registros
            db.SaveChanges();
        }

        public static void ConsultaDadosPorSintaxe()
        {
            // Consulta por sintaxe, o include serve para o objeto endereço não voltar null
            // os then include é para os objetos cidade e estado não voltarem null
            var consultaPessoas = (from pe in db.Pessoa
                                   .Include(p => p.Endereco)
                                   .ThenInclude(p => p.Cidade)
                                   .ThenInclude(p => p.Estado)
                                   where pe.Nome == "Romário 2"
                                   select pe).ToList();

 

            foreach (var pessoa in consultaPessoas)
            {
                Console.WriteLine(pessoa);

                // Busca cliente no cache
                var teste = db.Pessoa.Find(pessoa.Id);
            }

            // O AsNoTracking força a consulta no banco de dados e não em cache
            // var consultaPessoas2 = db.Pessoa.AsNoTracking().Where(p => p.Nome == "Romário 2").ToList();
        }

        public static void ConsultaDadosPorMetodoLambda()
        {
            // Consulta por método com lambda, o include serve para o objeto endereço não voltar null
            // os then include é para os objetos cidade e estado não voltarem null
            var consultaPessoas2 = db.Pessoa.Include(p => p.Endereco)
                .ThenInclude(p => p.Cidade)
                .ThenInclude(p => p.Estado)
                .Where(p => p.Nome == "Romário 2")
                .ToList();

            foreach (var pessoa in consultaPessoas2)
            {
                Console.WriteLine(pessoa);

                // Busca cliente no cache
                var teste = db.Pessoa.Find(pessoa.Id);
            }

            // O AsNoTracking força a consulta no banco de dados e não em cache
            // var consultaPessoas2 = db.Pessoa.AsNoTracking().Where(p => p.Nome == "Romário 2").ToList();
        }
    }
}
