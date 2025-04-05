using CocodriloStore.Core.Data.Context;
using CocodriloStore.Core.Interfaces;
using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Core.Data.Repositories;

public class VendedorRepositorio : Repositorio<Vendedor>, IVendedorRepositorio
{
    public VendedorRepositorio(AppDbContext context) : base(context)
    {
    }

    public async Task<Vendedor> ObterVendedorPorUsuarioIdAsync(string usuarioId)
    {
        return await _dbSet
            .FirstOrDefaultAsync(v => v.UsuarioId == usuarioId);
    }

    public override async Task<IEnumerable<Vendedor>> ObterTodosAsync()
    {
        return await _dbSet
            .Include(v => v.Produtos)
            .ToListAsync();
    }
}