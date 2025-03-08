using Bogus.Extensions.Brazil;
using lab_entity_framework_core.Utils;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lab_entity_framework_core_data_annotations.Domain
{
    [Table("Tabela_Pessoa")]
    public class Pessoa
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        [Required]
        [StringLength(50)]
        public string Sobrenome { get; set; }

        [Required]
        [Column("Cpf_Pessoa")]
        public string Cpf { get; set; }

        [Required]
        public Endereco Endereco { get; set; }

        [Required]
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
