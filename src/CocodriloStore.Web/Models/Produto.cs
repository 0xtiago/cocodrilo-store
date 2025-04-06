using System.ComponentModel.DataAnnotations;

namespace CocodriloStore.Web.Models
{
    public class Produto
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal Preco { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int Estoque { get; set; }

        public string ImagemUrl { get; set; }

        [Required]
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; }

        [Required]
        public string VendedorId { get; set; }
        public Vendedor Vendedor { get; set; }
    }
}