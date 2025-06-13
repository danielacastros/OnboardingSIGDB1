using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Data.Mappings;

public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
{
    public void Configure(EntityTypeBuilder<Empresa> builder)
    {
        builder.ToTable("Empresas");
        
        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Cnpj)
            .IsRequired()
            .HasMaxLength(14);

        builder.Property(x => x.DataFundacao)
            .HasMaxLength(7);

        builder.HasMany(x => x.Funcionarios)
            .WithOne(f => f.Empresa)
            .HasForeignKey(f => f.EmpresaId)
            .IsRequired(false)
            .OnDelete(DeleteBehavior.Restrict);
    }
}