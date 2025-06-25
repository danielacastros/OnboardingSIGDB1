using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Mappings;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Contextos;

public class ApplicationDbContext : DbContext
{
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    public DbSet<Cargo> Cargos { get; set; }
    public DbSet<FuncionarioCargo> FuncionarioCargoVinculo { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Ignore<FluentValidation.Results.ValidationFailure>();
        modelBuilder.Ignore<FluentValidation.Results.ValidationResult>();
        modelBuilder.Entity<Funcionario>().Ignore(e => e.Valid);
        modelBuilder.Entity<Funcionario>().Ignore(e => e.Invalid);
        modelBuilder.Entity<Cargo>().Ignore(e => e.Valid);
        modelBuilder.Entity<Cargo>().Ignore(e => e.Invalid);
        modelBuilder.ApplyConfiguration(new CargoMapping());
        modelBuilder.ApplyConfiguration(new FuncionarioMapping());
        modelBuilder.ApplyConfiguration(new EmpresaMapping());
        modelBuilder.ApplyConfiguration(new FuncionarioCargoMapping());

    }

    public async Task Commit()
    {
        await SaveChangesAsync();
    }
}