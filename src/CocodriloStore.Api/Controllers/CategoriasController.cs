using CocodriloStore.Web.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CocodriloStore.Api.Dtos;
using CocodriloStore.Web.Models;
using Microsoft.AspNetCore.Authorization;

namespace CocodriloStore.Api.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriasController(AppDbContext context)
        {
            _context = context;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetTodas()
        {
            var categorias = await _context.Categorias
                .OrderBy(c => c.Nome)
                .Select(c => new
                {
                    c.Id,
                    c.Nome,
                    c.Descricao
                })
                .ToListAsync();

            return Ok(categorias);
        }
        
        
        [HttpPost]
        public async Task<IActionResult> CriarCategoria([FromBody] CategoriaRequest request)
        {
            var categoria = new Categoria
            {
                Nome = request.Nome,
                Descricao = request.Descricao
            };

            _context.Categorias.Add(categoria);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTodas), new { categoria.Id }, new { categoria.Id });
        }
        
        
        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarCategoria(int id, [FromBody] CategoriaRequest request)
        {
            var categoria = await _context.Categorias.FindAsync(id);
            if (categoria == null)
                return NotFound();

            categoria.Nome = request.Nome;
            categoria.Descricao = request.Descricao;

            await _context.SaveChangesAsync();
            return NoContent();
        }
        
        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarCategoria(int id)
        {
            var categoria = await _context.Categorias
                .Include(c => c.Produtos)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (categoria == null)
                return NotFound();

            if (categoria.Produtos.Any())
                return BadRequest(new { message = "Não é possível excluir categorias com produtos associados." });

            _context.Categorias.Remove(categoria);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}