namespace CocodriloStore.Api.Dtos
{
    public class ProdutoRequest
    {
        public string Nome { get; set; } = "";
        public string Descricao { get; set; } = "";
        public decimal Preco { get; set; }
        public int Estoque { get; set; }
        public string ImagemUrl { get; set; } = "";
        public int CategoriaId { get; set; }
    }
}