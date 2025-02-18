using lab_entity_framework_core.ValueObjects;

namespace lab_entity_framework_core.Domain
{
    public class Produto
    {
        public int Id { get; set; }
        public string CodigoBarras { get; set; }
        public string Descricao { get; set; }
        public decimal Valor {  get; set; }
        public TipoProduto TipoProduto { get; set; }
        public bool Ativo {  get; set; }
    }
}
