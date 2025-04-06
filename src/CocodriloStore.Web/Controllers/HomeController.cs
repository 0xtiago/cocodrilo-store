using CocodriloStore.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class HomeController : Controller
{
    private readonly AppDbContext _context;

    public HomeController(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var produtos = await _context.Produtos
            .Include(p => p.Categoria)
            .Include(p => p.Vendedor)
            .OrderByDescending(p => p.Id)
            .Take(6) // mostra os 6 mais recentes (opcional)
            .ToListAsync();

        return View(produtos);
    }
}