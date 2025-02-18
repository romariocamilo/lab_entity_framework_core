namespace lab_entity_framework_core_automatico.Domain
{
    public class Pessoa
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Cpf { get; set; }
        public Endereco Endereco { get; set; }
        public TipoPessoa TipoPessoa { get; set; }
    }
}
