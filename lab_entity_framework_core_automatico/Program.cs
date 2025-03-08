using lab_entity_framework_core.Data;
using lab_entity_framework_core.Domain;
using lab_entity_framework_core.Utils;
using Microsoft.EntityFrameworkCore;

namespace lab_entity_framework_core_automatico
{
    internal class Program
    {
        // Criação do contexto
        public static ApplicationContextAutomatico dbAutomatico = new ApplicationContextAutomatico();

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
            dbAutomatico.Database.Migrate();
        }

        public static void ExecutaMigrationsPendentesCasoExista()
        {
            // Esse comando verifica se tem alguma migraçãp pendente e caso tenha executa
            var existeMigracaoPendente = dbAutomatico.Database.GetPendingMigrations().Any();

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
            dbAutomatico.Pessoa.Add(pessoa);
            dbAutomatico.Set<Pessoa>().Add(pessoa);
            dbAutomatico.Entry(pessoa).State = EntityState.Added;
            dbAutomatico.Add(pessoa);

            // Salvando registros
            var registros = dbAutomatico.SaveChanges();
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
            dbAutomatico.Pessoa.AddRange(listaPessoas);
            dbAutomatico.Set<Pessoa>().AddRange(listaPessoas);
            dbAutomatico.AddRange(listaPessoas);


            // Salvando registros
            dbAutomatico.SaveChanges();
        }

        public static void ConsultaDadosPorSintaxe()
        {
            InseriDadosEmLote();

            // Consulta todas as pessoas por sintaxe
            // O Include serve para o objeto endereço não voltar null
            // Os Then é para os objetos cidade e estado não voltarem null
            var consultaPessoas = (from pessoa in dbAutomatico.Pessoa
                                   .Include(p => p.Endereco)
                                   .ThenInclude(p => p.Cidade)
                                   .ThenInclude(p => p.Estado)
                                   where pessoa.Id > 0
                                   select pessoa).ToList();

 

            foreach (var pessoa in consultaPessoas)
            {
                // Busca pessoa no cache
                var pessoaCache = dbAutomatico.Pessoa.Find(pessoa.Id);

                //Escreve pessoa do cache
                Console.WriteLine(pessoaCache);
            }

            // O AsNoTracking força a consulta no banco de dados e não em cache
            var consultaPessoasDoBanco = dbAutomatico.Pessoa.AsNoTracking().Where(p => p.Id == consultaPessoas.Last().Id).ToList();
        }

        public static void ConsultaDadosPorMetodoLambda()
        {
            InseriDadosEmLote();

            // Consulta por método com lambda
            // O Include serve para o objeto endereço não voltar null
            // Os Then é para os objetos cidade e estado não voltarem null
            var consultaPessoas = dbAutomatico.Pessoa.Include(p => p.Endereco)
                .ThenInclude(p => p.Cidade)
                .ThenInclude(p => p.Estado)
                .Where(p => p.Id > 0)
                .ToList();

            foreach (var pessoa in consultaPessoas)
            {
                // Busca cliente no cache
                var pessoaCache = dbAutomatico.Pessoa.Find(pessoa.Id);

                Console.WriteLine(pessoaCache);
            }

            // O AsNoTracking força a consulta no banco de dados e não em cache
            var consultaPessoasBanco = dbAutomatico.Pessoa.AsNoTracking().Where(p => p.Id == consultaPessoas.Last().Id).ToList();
        }

        public static void AtualizaDados()
        {
            InseriDadosEmLote();

            // Não criei um método de consulta para reaproveitamento só para fins explicativos
            var consultaPessoas = dbAutomatico.Pessoa.Include(p => p.Endereco)
            .ThenInclude(p => p.Cidade)
            .ThenInclude(p => p.Estado)
            .Where(p => p.Id > 0)
            .ToList();

            // Usei o find que vai direto na chave primaria da tabela pessoa
            var pessoa = dbAutomatico.Pessoa.Find(consultaPessoas.First().Id);

            // Atualiza somente o que mudou, igual ao PATCH
            pessoa.Nome = new BogusFakerCustom().Faker.Person.FirstName + "Atualizado" + Random.Shared.Next();   
            dbAutomatico.SaveChanges();

            // Atualizado todo o objeto caso a linha abaixo esteja descomentada
            pessoa.Nome = new BogusFakerCustom().Faker.Person.FirstName + "Atualizado" + Random.Shared.Next() + "Novo";
            dbAutomatico.Pessoa.Update(pessoa);
            dbAutomatico.SaveChanges();
        }

        public static void AtualizaDadosDesconectado()
        {
            InseriDadosEmLote();

            // Não criei um método de consulta para reaproveitamento só para fins explicativos
            var consultaPessoas = dbAutomatico.Pessoa.Include(p => p.Endereco)
            .ThenInclude(p => p.Cidade)
            .ThenInclude(p => p.Estado)
            .Where(p => p.Id > 0)
            .ToList();

            // Usei o find com 4, porque seu que o id do pessoal 4 existe na base
            var pessoa = dbAutomatico.Pessoa.Find(consultaPessoas.First().Id);

            var pessoaDesconectada = new
            {
                Nome = "Pessoa Desconectada"
            };

            //db.Entry(pessoa).CurrentValues.SetValues(pessoaDesconectada);
            dbAutomatico.Update(pessoa).CurrentValues.SetValues(pessoaDesconectada);
            dbAutomatico.SaveChanges();
        }

        public static void RemoveRegistros()
        {
            InseriDadosEmLote();

            // Não criei um método de consulta para reaproveitamento só para fins explicativos
            var consultaPessoas = dbAutomatico.Pessoa.Include(p => p.Endereco)
            .ThenInclude(p => p.Cidade)
            .ThenInclude(p => p.Estado)
            .Where(p => p.Id > 0)
            .ToList();

            // Usei o find para ir no cache usando o Id como chave primaria
            var pessoa = dbAutomatico.Pessoa.Find(consultaPessoas.First().Id);

            // Várias formas de remover pessoas
            dbAutomatico.Remove(pessoa);
            dbAutomatico.Pessoa.Remove(pessoa);
            dbAutomatico.Set<Pessoa>().Remove(pessoa);
            dbAutomatico.Entry(pessoa).State = EntityState.Deleted;
            dbAutomatico.SaveChanges();
        }

        public static void RemoveRegistrosDesconectado()
        {
            InseriDadosEmLote();

            Console.WriteLine("Digite o id da pessoa: " + dbAutomatico.Pessoa.First().Id + " entre " + (dbAutomatico.Pessoa.First().Id + dbAutomatico.Pessoa.Count() - 1));

            try
            {
                // Usei o find para ir no cache usando o Id como chave primaria
                var pessoa = new Pessoa
                {
                    Id = dbAutomatico.Pessoa.Find(Convert.ToInt32(Console.ReadLine())).Id
                };

                // Precisei criar um novo contexto para não conflitar a chave primária já estar sendo usada em outra instância
                using (var context = new ApplicationContextAutomatico())
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
            var pessoas = dbAutomatico.Pessoa.Where(p => p.Id > 0).ToList();
            dbAutomatico.RemoveRange(pessoas);
            dbAutomatico.SaveChanges();
        }
    }
}
