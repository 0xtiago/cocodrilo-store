using CocodriloStore.Core.Models;

namespace CocodriloStore.Core.Interfaces;

public interface IVendedorRepositorio : IRepositorio<Vendedor>
{
    Task<Vendedor> ObterVendedorPorUsuarioIdAsync(string usuarioId);
}