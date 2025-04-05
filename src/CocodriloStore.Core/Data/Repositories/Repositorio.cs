using System.Linq.Expressions;
using CocodriloStore.Core.Data.Context;
using CocodriloStore.Core.Interfaces;
using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Core.Data.Repositories;

public class Repositorio<T> : IRepositorio<T> where T : Entidade
{
    
    protected readonly AppDbContext _context;
    protected readonly DbSet<T> _dbSet;

    public Repositorio(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<T>();
    }
    
    public virtual async Task<IEnumerable<T>> ObterTodosAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<T> ObterPorIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<T>> ObterAsync(Expression<Func<T, bool>> predicado)
    {
        return await _dbSet.Where(predicado).ToListAsync();
    }

    public virtual async Task AdicionarAsync(T entidade)
    {
        await _dbSet.AddAsync(entidade);
    }

    public virtual async Task AtualizarAsync(T entidade)
    {
        _dbSet.Update(entidade);
        await Task.CompletedTask;
    }

    public virtual async Task RemoverAsync(Guid id)
    {
        var entidade = await ObterPorIdAsync(id);
        if (entidade != null)
        {
            _dbSet.Remove(entidade);
        }
    }

    public async Task<int> SalvarMudancasAsync()
    {
        return await _context.SaveChangesAsync();
    }
}