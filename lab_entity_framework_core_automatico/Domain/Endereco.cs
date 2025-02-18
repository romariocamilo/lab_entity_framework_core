namespace lab_entity_framework_core_automatico.Domain
{
    public class Endereco
    {
        public int Id { get; set; }
        public string Logradouro { get; set; }
        public string Nome { get; set; }
        public int Numero { get; set; }
        public string Complemento { get; set; }
        public Cidade Cidade { get; set; }

    }
}
