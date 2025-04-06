using System.ComponentModel.DataAnnotations;

namespace CocodriloStore.Web.Models
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required]
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public List<Produto> Produtos { get; set; } = new();
    }
}