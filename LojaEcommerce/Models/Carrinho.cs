namespace LojaEcommerce.Models
{
    public class Carrinho
    {
        public int Id { get; set; }
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public decimal Preco { get; set; }
        public int Quantidade { get; set; }
        public string? Imagem { get; set; }
    }
}
