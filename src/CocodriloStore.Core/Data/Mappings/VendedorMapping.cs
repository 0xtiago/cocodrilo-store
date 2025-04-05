using CocodriloStore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CocodriloStore.Core.Data.Mappings;

public class VendedorMapping : IEntityTypeConfiguration<Vendedor>
{
    public void Configure(EntityTypeBuilder<Vendedor> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Nome)
            .IsRequired()
            .HasColumnType("varchar(200)");

        builder.Property(v => v.Email)
            .IsRequired()
            .HasColumnType("varchar(200)");
                
        builder.Property(v => v.UsuarioId)
            .IsRequired()
            .HasColumnType("varchar(450)");

        // Relacionamento com Produtos
        builder.HasMany(v => v.Produtos)
            .WithOne(p => p.Vendedor)
            .HasForeignKey(p => p.VendedorId);

        builder.ToTable("Vendedores");
    }
}