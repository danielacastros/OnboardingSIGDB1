using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Data.Mappings;

public class FuncionarioCargoMapping : IEntityTypeConfiguration<FuncionarioCargo>
{
    public void Configure(EntityTypeBuilder<FuncionarioCargo> builder)
    {
        builder.ToTable("FuncionarioCargoVinculo");
        
        builder.HasKey(vinculo => new { vinculo.FuncionarioId, vinculo.CargoId });

        builder.HasOne(vinculo => vinculo.Funcionario)
            .WithMany(funcionario => funcionario.Cargos)
            .HasForeignKey(cargo => cargo.FuncionarioId);

        builder.HasOne(vinculo => vinculo.Cargo)
            .WithMany(cargo => cargo.Funcionarios)
            .HasForeignKey(cargo => cargo.CargoId)
            .OnDelete(DeleteBehavior.Restrict);
        
        builder.Property(x => x.DataVinculo)
            .IsRequired();
    }
}