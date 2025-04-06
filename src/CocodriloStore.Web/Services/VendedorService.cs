using CocodriloStore.Web.Data;
using CocodriloStore.Web.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Web.Services
{
    public class VendedorService
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public VendedorService(AppDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task CriarVendedorAsync(IdentityUser user, string nomeCompleto)
        {
            if (!await _context.Vendedores.AnyAsync(v => v.Id == user.Id))
            {
                var vendedor = new Vendedor
                {
                    Id = user.Id,
                    Nome = nomeCompleto
                };

                _context.Vendedores.Add(vendedor);
                await _context.SaveChangesAsync();
            }
        }
    }
}