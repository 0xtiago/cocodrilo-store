using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CocodriloStore.Web.ViewModels
{
    public class ProdutoViewModel
    {
        public int? Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string? Descricao { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Preco { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }

        public IFormFile? Imagem { get; set; }

        [Required]
        public int CategoriaId { get; set; }

        public List<SelectListItem>? Categorias { get; set; }
    }
}