using CocodriloStore.Core.Data.Context;
using CocodriloStore.Core.Interfaces;
using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Core.Data.Repositories;

public class ProdutoRepositorio : Repositorio<Produto>, IProdutoRepositorio
{
    public ProdutoRepositorio(AppDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPorCategoriaAsync(Guid categoriaId)
    {
        return await _dbSet
            .Where(p => p.CategoriaId == categoriaId)
            .Include(p => p.Categoria)
            .ToListAsync();
    }

    public async Task<IEnumerable<Produto>> ObterProdutosPorVendedorAsync(Guid vendedorId)
    {
        return await _dbSet
            .Where(p => p.VendedorId == vendedorId)
            .Include(p => p.Categoria)
            .ToListAsync();
    }

    public async Task<Produto?> ObterProdutoComCategoriaAsync(Guid id)
    {
        return await _dbSet
            .Include(p => p.Categoria)
            .FirstOrDefaultAsync(p => p.Id == id);
    }
    
    public override async Task<IEnumerable<Produto>> ObterTodosAsync()
    {
        return await _dbSet
            .Include(p => p.Categoria)
            .ToListAsync();
    }
}