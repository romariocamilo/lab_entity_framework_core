using lab_entity_framework_core.Utils;

namespace lab_entity_framework_core.Domain
{
    public class Cidade
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public Estado Estado { get; set; }

        public Cidade()
        {
            Nome = new BogusFakerCustom().Faker.Address.City();
            Estado = new Estado();
        }
    }
}