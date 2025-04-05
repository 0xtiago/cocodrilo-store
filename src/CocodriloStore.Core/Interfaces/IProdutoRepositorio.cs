using CocodriloStore.Core.Models;

namespace CocodriloStore.Core.Interfaces;

public interface IProdutoRepositorio : IRepositorio<Produto>
{
    Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(Guid categoriaId);
    Task<IEnumerable<Produto>> ObterProdutosPorVendedorAsync(Guid vendedorId);
    Task<Produto?> ObterProdutoComCategoriaAsync(Guid id);
}