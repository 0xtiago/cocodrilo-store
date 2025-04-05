using CocodriloStore.Core.Models;

namespace CocodriloStore.Core.Interfaces;

public interface ICategoriaRepositorio : IRepositorio<Categoria>
{
    Task<bool> CategoriaPossuiProdutosAsync(Guid id);
}