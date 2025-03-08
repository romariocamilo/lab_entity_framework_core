using lab_entity_framework_core.Utils;

namespace lab_entity_framework_core_data_annotations.Domain
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Nome { get; set; }
        public string Numero { get; set; }
        public string Complemento { get; set; }
        public Cidade Cidade { get; set; }

        public Endereco()
        {
            Logradouro = "Rua";
            Nome = new BogusFakerCustom().Faker.Address.StreetName();
            Numero = new BogusFakerCustom().Faker.Address.BuildingNumber();
            Complemento = new BogusFakerCustom().Faker.Address.ZipCode();
            Cidade = new Cidade();
        }
    }
}
