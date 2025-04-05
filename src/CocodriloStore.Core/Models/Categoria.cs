namespace CocodriloStore.Core.Models;

public class Categoria : Entidade
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
        
    // Relação com produtos
    public ICollection<Produto> Produtos { get; set; }
    
}