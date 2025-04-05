using System.Reflection;
using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace CocodriloStore.Core.Data.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Produto> Produtos { get; set; }
    public DbSet<Categoria> Categorias { get; set; }
    public DbSet<Vendedor> Vendedores { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            
        base.OnModelCreating(modelBuilder);
    }

    
    // protected override void OnModelCreating(ModelBuilder modelBuilder)
    // {
    //     base.OnModelCreating(modelBuilder);
    //         
    //     // Configuração do Produto
    //     modelBuilder.Entity<Produto>()
    //         .HasOne(p => p.Categoria)
    //         .WithMany(c => c.Produtos)
    //         .HasForeignKey(p => p.CategoriaId);
    //             
    //     modelBuilder.Entity<Produto>()
    //         .HasOne(p => p.Vendedor)
    //         .WithMany(s => s.Produtos)
    //         .HasForeignKey(p => p.VendedorId);
    //             
    //     modelBuilder.Entity<Produto>()
    //         .Property(p => p.Preco)
    //         .HasColumnType("decimal(18,2)");
    //             
    //     // Configurações adicionais
    //     modelBuilder.Entity<Produto>()
    //         .HasIndex(p => p.Nome);
    //             
    //     modelBuilder.Entity<Categoria>()
    //         .HasIndex(c => c.Nome)
    //         .IsUnique();
    // }
    
}