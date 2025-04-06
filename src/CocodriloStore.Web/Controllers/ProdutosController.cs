using CocodriloStore.Web.Data;
using CocodriloStore.Web.Models;
using CocodriloStore.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Web.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly AppDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IWebHostEnvironment _env;

        public ProdutosController(AppDbContext context, UserManager<IdentityUser> userManager, IWebHostEnvironment env)
        {
            _context = context;
            _userManager = userManager;
            _env = env;
        }

        // Acesso público à lista de produtos
        [AllowAnonymous]
        public async Task<IActionResult> Index(int? categoriaId)
        {
            var categorias = await _context.Categorias
                .OrderBy(c => c.Nome)
                .ToListAsync();

            var produtosQuery = _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .AsQueryable();

            if (categoriaId.HasValue)
            {
                produtosQuery = produtosQuery.Where(p => p.CategoriaId == categoriaId.Value);
            }

            ViewBag.Categorias = categorias;
            ViewBag.CategoriaSelecionada = categoriaId;

            var produtos = await produtosQuery.ToListAsync();
            return View(produtos);
        }

        [Authorize]
        public async Task<IActionResult> Create()
        {
            var vm = new ProdutoViewModel
            {
                Categorias = await _context.Categorias
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome })
                    .ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Create(ProdutoViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Categorias = await _context.Categorias
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome })
                    .ToListAsync();

                return View(vm);
            }

            var user = await _userManager.GetUserAsync(User);
            var vendedor = await _context.Vendedores.FirstOrDefaultAsync(v => v.Id == user.Id);

            string? imagemUrl = null;

            if (vm.Imagem != null && vm.Imagem.Length > 0)
            {
                var fileName = $"{Guid.NewGuid()}_{vm.Imagem.FileName}";
                var path = Path.Combine(_env.WebRootPath, "images", "produtos", fileName);

                using var stream = new FileStream(path, FileMode.Create);
                await vm.Imagem.CopyToAsync(stream);

                imagemUrl = $"/images/produtos/{fileName}";
            }

            var produto = new Produto
            {
                Nome = vm.Nome,
                Descricao = vm.Descricao,
                Preco = vm.Preco,
                Estoque = vm.Estoque,
                ImagemUrl = imagemUrl,
                CategoriaId = vm.CategoriaId,
                VendedorId = vendedor.Id
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        [AllowAnonymous]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            return View(produto);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null || produto.VendedorId != _userManager.GetUserId(User))
                return Unauthorized();

            var vm = new ProdutoViewModel
            {
                Id = produto.Id,
                Nome = produto.Nome,
                Descricao = produto.Descricao,
                Preco = produto.Preco,
                Estoque = produto.Estoque,
                CategoriaId = produto.CategoriaId,
                Categorias = await _context.Categorias
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome })
                    .ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, ProdutoViewModel vm)
        {
            if (id != vm.Id)
                return NotFound();

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null || produto.VendedorId != _userManager.GetUserId(User))
                return Unauthorized();

            if (!ModelState.IsValid)
            {
                vm.Categorias = await _context.Categorias
                    .Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nome })
                    .ToListAsync();

                return View(vm);
            }

            produto.Nome = vm.Nome;
            produto.Descricao = vm.Descricao;
            produto.Preco = vm.Preco;
            produto.Estoque = vm.Estoque;
            produto.CategoriaId = vm.CategoriaId;

            if (vm.Imagem != null)
            {
                var fileName = $"{Guid.NewGuid()}_{vm.Imagem.FileName}";
                var path = Path.Combine(_env.WebRootPath, "images", "produtos", fileName);
                using var stream = new FileStream(path, FileMode.Create);
                await vm.Imagem.CopyToAsync(stream);

                produto.ImagemUrl = $"/images/produtos/{fileName}";
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        
        [Authorize]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
                return NotFound();

            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null || produto.VendedorId != _userManager.GetUserId(User))
                return Unauthorized();

            return View(produto);
        }

        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);

            if (produto == null || produto.VendedorId != _userManager.GetUserId(User))
                return Unauthorized();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
        
        [Authorize]
        public async Task<IActionResult> MeusProdutos()
        {
            var userId = _userManager.GetUserId(User);

            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.VendedorId == userId)
                .ToListAsync();

            return View("Index", produtos); 
        }
    }
}