using System.Linq.Expressions;
using CocodriloStore.Core.Models;

namespace CocodriloStore.Core.Interfaces;

public interface IRepositorio<T> where T : Entidade
{
    Task<IEnumerable<T>> ObterTodosAsync();
    Task<T> ObterPorIdAsync(Guid id);
    Task<IEnumerable<T>> ObterAsync(Expression<Func<T, bool>> predicado);
    Task AdicionarAsync(T entidade);
    Task AtualizarAsync(T entidade);
    Task RemoverAsync(Guid id);
    Task<int> SalvarMudancasAsync();
}