using lab_entity_framework_core.Utils;

namespace lab_entity_framework_core_data_annotations.Domain
{
    public class Estado
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string NomeCidade { get; set; }

        public Estado()
        {
            Nome = new BogusFakerCustom().Faker.Address.State();
            NomeCidade = "ErroNomeAqui";
        }
    }
}