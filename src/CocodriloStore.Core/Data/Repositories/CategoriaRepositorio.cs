using CocodriloStore.Core.Data.Context;
using CocodriloStore.Core.Interfaces;
using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Core.Data.Repositories;

public class CategoriaRepositorio : Repositorio<Categoria>, ICategoriaRepositorio
{
    public CategoriaRepositorio(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> CategoriaPossuiProdutosAsync(Guid id)
    {
        return await _context.Produtos.AnyAsync(p => p.CategoriaId == id);
    }

    public override async Task<IEnumerable<Categoria>> ObterTodosAsync()
    {
        return await _dbSet
            .Include(c => c.Produtos)
            .ToListAsync();
    }
}

    
