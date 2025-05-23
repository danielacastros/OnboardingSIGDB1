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
        
        
    }
}