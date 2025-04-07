using CocodriloStore.Web.Data;
using CocodriloStore.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using CocodriloStore.Api.Dtos;


namespace CocodriloStore.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProdutosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetTodos()
        {
            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .ToListAsync();

            var resultado = produtos.Select(p => new
            {
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco,
                p.Estoque,
                Categoria = new { p.Categoria?.Id, p.Categoria?.Nome },
                Vendedor = new { p.Vendedor?.Id, p.Vendedor?.Nome }
            });

            return Ok(resultado);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPorId(int id)
        {
            var produto = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .FirstOrDefaultAsync(p => p.Id == id);

            if (produto == null)
                return NotFound();

            var resultado = new
            {
                produto.Id,
                produto.Nome,
                produto.Descricao,
                produto.Preco,
                produto.Estoque,
                Categoria = new { produto.Categoria?.Id, produto.Categoria?.Nome },
                Vendedor = new { produto.Vendedor?.Id, produto.Vendedor?.Nome }
            };

            return Ok(resultado);
        }

        [HttpGet("categoria/{categoriaId}")]
        public async Task<IActionResult> GetPorCategoria(int categoriaId)
        {
            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Include(p => p.Vendedor)
                .Where(p => p.CategoriaId == categoriaId)
                .ToListAsync();

            var resultado = produtos.Select(p => new
            {
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco,
                p.Estoque,
                Categoria = new { p.Categoria?.Id, p.Categoria?.Nome },
                Vendedor = new { p.Vendedor?.Id, p.Vendedor?.Nome }
            });

            return Ok(resultado);
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CriarProduto([FromBody] ProdutoRequest request)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == request.CategoriaId);
            if (!categoriaExiste)
                return BadRequest(new { message = "Categoria inválida." });

            var produto = new Produto
            {
                Nome = request.Nome,
                Descricao = request.Descricao,
                Preco = request.Preco,
                Estoque = request.Estoque,
                ImagemUrl = request.ImagemUrl,
                CategoriaId = request.CategoriaId,
                VendedorId = userId
            };

            _context.Produtos.Add(produto);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPorId), new { id = produto.Id }, new { produto.Id });
        }
        
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarProduto(int id, [FromBody] ProdutoRequest request)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
                return NotFound();

            if (produto.VendedorId != userId)
                return Forbid();

            var categoriaExiste = await _context.Categorias.AnyAsync(c => c.Id == request.CategoriaId);
            if (!categoriaExiste)
                return BadRequest(new { message = "Categoria inválida." });

            produto.Nome = request.Nome;
            produto.Descricao = request.Descricao;
            produto.Preco = request.Preco;
            produto.Estoque = request.Estoque;
            produto.ImagemUrl = request.ImagemUrl;
            produto.CategoriaId = request.CategoriaId;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarProduto(int id)
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var produto = await _context.Produtos.FirstOrDefaultAsync(p => p.Id == id);
            if (produto == null)
                return NotFound();

            if (produto.VendedorId != userId)
                return Forbid();

            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        [HttpGet("meus")]
        [Authorize]
        public async Task<IActionResult> GetMeusProdutos()
        {
            var userId = User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userId))
                return Unauthorized();

            var produtos = await _context.Produtos
                .Include(p => p.Categoria)
                .Where(p => p.VendedorId == userId)
                .ToListAsync();

            var resultado = produtos.Select(p => new
            {
                p.Id,
                p.Nome,
                p.Descricao,
                p.Preco,
                p.Estoque,
                p.ImagemUrl,
                Categoria = new { p.Categoria?.Id, p.Categoria?.Nome }
            });

            return Ok(resultado);
        }
    }
}