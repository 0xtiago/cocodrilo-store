using System.ComponentModel.DataAnnotations;

namespace CocodriloStore.Web.Models
{
    public class Vendedor
    {
        [Key]
        public string Id { get; set; } // mesmo ID do AspNetUser

        [Required]
        public string Nome { get; set; }

        public List<Produto> Produtos { get; set; } = new();
    }
}