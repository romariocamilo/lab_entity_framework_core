using lab_entity_framework_core.Domain;
using lab_entity_framework_core.Utils;
using lab_entity_framework_core_fluent_api.Data;
using Microsoft.EntityFrameworkCore;

namespace lab_entity_framework_core_fluent_api
{
    internal class Program
    {
        // Criação do contexto
        public static ApplicationContextFluentApi dbFluentApi = new ApplicationContextFluentApi();

        static void Main(string[] args)
        {
            //Testando Métodos EF Core

            //ExecutaMigrations();
            //ExecutaMigrationsPendentesCasoExista();
            DeletaTodasPessoas();
            InseriDados();
            InseriDadosEmLote();
            ConsultaDadosPorSintaxe();
            ConsultaDadosPorMetodoLambda();
            AtualizaDados();
            AtualizaDadosDesconectado();
            RemoveRegistros();
            RemoveRegistrosDesconectado();

            Console.WriteLine("Teste Finalizado");
        }

        public static void ExecutaMigrations()
        {
            // Essa método aplica as migrations pendentes sempre que é chamado
            dbFluentApi.Database.Migrate();
        }

        public static void ExecutaMigrationsPendentesCasoExista()
        {
            // Esse comando verifica se tem alguma migraçãp pendente e caso tenha executa
            var existeMigracaoPendente = dbFluentApi.Database.GetPendingMigrations().Any();

            if (existeMigracaoPendente)
            {
                Console.WriteLine("Existe migrações pendentes");
                Console.ReadKey();
            }
        }

        public static void InseriDados()
        {
            // Não passei os ids porque são incrementados automaticamente
            var pessoa = new Pessoa();

            // Várias formas de add o objeto no banco
            dbFluentApi.Pessoa.Add(pessoa);
            dbFluentApi.Set<Pessoa>().Add(pessoa);
            dbFluentApi.Entry(pessoa).State = EntityState.Added;
            dbFluentApi.Add(pessoa);

            // Salvando registros
            var registros = dbFluentApi.SaveChanges();
        }

        public static void InseriDadosEmLote()
        {
            // Criando os dois objetos de pessoa
            var pessoa = new Pessoa();
            var pessoaDois = new Pessoa();

            // Criando a lista e inserindo as pessoas nela
            var listaPessoas = new List<Pessoa>
            {
                pessoa,
                pessoaDois
            };

            // Várias formas de add a lista no banco
            dbFluentApi.Pessoa.AddRange(listaPessoas);
            dbFluentApi.Set<Pessoa>().AddRange(listaPessoas);
            dbFluentApi.AddRange(listaPessoas);


            // Salvando registros
            dbFluentApi.SaveChanges();
        }

        public static void ConsultaDadosPorSintaxe()
        {
            InseriDadosEmLote();

            // Consulta todas as pessoas por sintaxe
            // O Include serve para o objeto endereço não voltar null
            // Os Then é para os objetos cidade e estado não voltarem null
            var consultaPessoas = (from pessoa in dbFluentApi.Pessoa
                                   .Include(p => p.Endereco)
                                   .ThenInclude(p => p.Cidade)
                                   .ThenInclude(p => p.Estado)
                                   where pessoa.Id > 0
                                   select pessoa).ToList();



            foreach (var pessoa in consultaPessoas)
            {
                // Busca pessoa no cache
                var pessoaCache = dbFluentApi.Pessoa.Find(pessoa.Id);

                //Escreve pessoa do cache
                Console.WriteLine(pessoaCache);
            }

            // O AsNoTracking força a consulta no banco de dados e não em cache
            var consultaPessoasDoBanco = dbFluentApi.Pessoa.AsNoTracking().Where(p => p.Id == consultaPessoas.Last().Id).ToList();
        }

        public static void ConsultaDadosPorMetodoLambda()
        {
            InseriDadosEmLote();

            // Consulta por método com lambda
            // O Include serve para o objeto endereço não voltar null
            // Os Then é para os objetos cidade e estado não voltarem null
            var consultaPessoas = dbFluentApi.Pessoa.Include(p => p.Endereco)
                .ThenInclude(p => p.Cidade)
                .ThenInclude(p => p.Estado)
                .Where(p => p.Id > 0)
                .ToList();

            foreach (var pessoa in consultaPessoas)
            {
                // Busca cliente no cache
                var pessoaCache = dbFluentApi.Pessoa.Find(pessoa.Id);

                Console.WriteLine(pessoaCache);
            }

            // O AsNoTracking força a consulta no banco de dados e não em cache
            var consultaPessoasBanco = dbFluentApi.Pessoa.AsNoTracking().Where(p => p.Id == consultaPessoas.Last().Id).ToList();
        }

        public static void AtualizaDados()
        {
            InseriDadosEmLote();

            // Não criei um método de consulta para reaproveitamento só para fins explicativos
            var consultaPessoas = dbFluentApi.Pessoa.Include(p => p.Endereco)
            .ThenInclude(p => p.Cidade)
            .ThenInclude(p => p.Estado)
            .Where(p => p.Id > 0)
            .ToList();

            // Usei o find que vai direto na chave primaria da tabela pessoa
            var pessoa = dbFluentApi.Pessoa.Find(consultaPessoas.First().Id);

            // Atualiza somente o que mudou, igual ao PATCH
            pessoa.Nome = new BogusFakerCustom().Faker.Person.FirstName + "Atualizado" + Random.Shared.Next();
            dbFluentApi.SaveChanges();

            // Atualizado todo o objeto caso a linha abaixo esteja descomentada
            pessoa.Nome = new BogusFakerCustom().Faker.Person.FirstName + "Atualizado" + Random.Shared.Next() + "Novo";
            dbFluentApi.Pessoa.Update(pessoa);
            dbFluentApi.SaveChanges();
        }

        public static void AtualizaDadosDesconectado()
        {
            InseriDadosEmLote();

            // Não criei um método de consulta para reaproveitamento só para fins explicativos
            var consultaPessoas = dbFluentApi.Pessoa.Include(p => p.Endereco)
            .ThenInclude(p => p.Cidade)
            .ThenInclude(p => p.Estado)
            .Where(p => p.Id > 0)
            .ToList();

            // Usei o find com 4, porque seu que o id do pessoal 4 existe na base
            var pessoa = dbFluentApi.Pessoa.Find(consultaPessoas.First().Id);

            var pessoaDesconectada = new
            {
                Nome = "Pessoa Desconectada"
            };

            //db.Entry(pessoa).CurrentValues.SetValues(pessoaDesconectada);
            dbFluentApi.Update(pessoa).CurrentValues.SetValues(pessoaDesconectada);
            dbFluentApi.SaveChanges();
        }

        public static void RemoveRegistros()
        {
            InseriDadosEmLote();

            // Não criei um método de consulta para reaproveitamento só para fins explicativos
            var consultaPessoas = dbFluentApi.Pessoa.Include(p => p.Endereco)
            .ThenInclude(p => p.Cidade)
            .ThenInclude(p => p.Estado)
            .Where(p => p.Id > 0)
            .ToList();

            // Usei o find para ir no cache usando o Id como chave primaria
            var pessoa = dbFluentApi.Pessoa.Find(consultaPessoas.First().Id);

            // Várias formas de remover pessoas
            dbFluentApi.Remove(pessoa);
            dbFluentApi.Pessoa.Remove(pessoa);
            dbFluentApi.Set<Pessoa>().Remove(pessoa);
            dbFluentApi.Entry(pessoa).State = EntityState.Deleted;
            dbFluentApi.SaveChanges();
        }

        public static void RemoveRegistrosDesconectado()
        {

            InseriDadosEmLote();
            Console.WriteLine("Digite o id da pessoa: " + dbFluentApi.Pessoa.First().Id + " entre " + (dbFluentApi.Pessoa.First().Id + dbFluentApi.Pessoa.Count() - 1));

            try
            {
                // Usei o find para ir no cache usando o Id como chave primaria
                var pessoa = new Pessoa
                {
                    Id = dbFluentApi.Pessoa.Find(Convert.ToInt32(Console.ReadLine())).Id
                };

                // Precisei criar um novo contexto para não conflitar a chave primária já estar sendo usada em outra instância
                using (var context = new ApplicationContextFluentApi())
                {
                    // Várias formas de remover pessoas
                    context.Remove(pessoa);
                    context.Pessoa.Remove(pessoa);
                    context.Set<Pessoa>().Remove(pessoa);
                    context.Entry(pessoa).State = EntityState.Deleted;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("\n\nID DE PESSOA INVÁLIDO\n\n");
            }
        }

        public static void DeletaTodasPessoas()
        {
            // Remove todos os registros da tabela Pessoa
            var pessoas = dbFluentApi.Pessoa.Where(p => p.Id > 0).ToList();
            dbFluentApi.RemoveRange(pessoas);
            dbFluentApi.SaveChanges();
        }
    }
}