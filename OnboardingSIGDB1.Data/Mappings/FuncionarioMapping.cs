using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Data.Mappings;

public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
{
    public void Configure(EntityTypeBuilder<Funcionario> builder)
    {
        builder.ToTable("Funcionarios");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Nome)
            .IsRequired()
            .HasMaxLength(150);

        builder.Property(x => x.Cpf)
            .IsRequired()
            .HasMaxLength(11);

        builder.Property(x => x.DataContratacao)
            .HasMaxLength(7);
        
        builder.HasOne(f => f.Empresa)
            .WithMany(e => e.Funcionarios)
            .HasForeignKey(f => f.EmpresaId)
            .IsRequired(false);
    }
}