using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Web.Models;

public class ProdutoModel
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Nome { get; set; }
    
    [MaxLength(100)]
    public string Categoria { get; set; }
    
    [Precision(16, 2)]
    public decimal Preco { get; set; }
    
    public string Descricao { get; set; }
    
    [MaxLength(100)]
    public string ImageFileName { get; set; }
    
    public DateTime CriadoEm { get; set; }
}