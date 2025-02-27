using Bogus;

namespace lab_entity_framework_core_automatico.Utils
{
    public class BogusFakerCustom
    {
        // Gera dados em português
        public Faker Faker {  get; private set; }

        public BogusFakerCustom()
        {
            Faker = new Faker("pt_BR");
        }
    }
}
