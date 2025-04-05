using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocodriloStore.Core.Data.Mappings;

public class ProdutoMapping : IEntityTypeConfiguration<Produto> 
{
    public void Configure(EntityTypeBuilder<Produto> builder)
    {
        builder.HasKey(p => p.Id);

        builder.Property(p => p.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(p => p.Descricao)
            .HasColumnType("varchar(1000)");

        builder.Property(p => p.Preco)
            .IsRequired()
            .HasColumnType("decimal(18,2)");

        builder.Property(p => p.Estoque)
            .IsRequired();

        builder.Property(p => p.ImagePath)
            .HasColumnType("varchar(500)");

        builder.ToTable("Produtos");
    }
}