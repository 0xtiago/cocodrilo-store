namespace CocodriloStore.Core.Models;

public class Produto : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
    public decimal Preco { get; set; }
    public int Estoque { get; set; }
    public string ImagePath { get; set; }
        
    // Chaves estrangeiras e propriedades de navegação
    public Guid CategoriaId { get; set; }
    public Categoria Categoria { get; set; }
    public Guid VendedorId { get; set; }
    public Vendedor Vendedor { get; set; }
    
    
    
}