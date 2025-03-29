using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Web.Models;

public class CategoriaModel
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Nome { get; set; }
    
    public string Descricao { get; set; }
    
}