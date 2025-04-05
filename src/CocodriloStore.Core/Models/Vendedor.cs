namespace CocodriloStore.Core.Models;

public class Vendedor : Entidade
{
    public string Nome { get; set; }
    public string UsuarioId { get; set; }
    public string Email { get; set; }
    public ICollection<Produto> Produtos { get; set; }
    

}