using Bogus.Extensions.Brazil;
using lab_entity_framework_core.Utils;

namespace lab_entity_framework_core.Domain
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public TipoPessoa TipoPessoa { get; set; }

        // Construtor que preenche os dados automaticamente
        public Pessoa()
        {
            Random rand = new Random();

            Nome = new BogusFakerCustom().Faker.Person.FirstName;
            Sobrenome = new BogusFakerCustom().Faker.Person.LastName;
            Cpf = new BogusFakerCustom().Faker.Person.Cpf();
            Endereco = new Endereco();
            TipoPessoa = TipoPessoa.Fisica;
        }
    }
}
