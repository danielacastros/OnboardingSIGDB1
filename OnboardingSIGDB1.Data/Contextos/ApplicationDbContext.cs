using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Data.Contextos;

public class ApplicationDbContext : DbContext
{
    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Funcionario> Funcionarios { get; set; }
    
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Ignore<FluentValidation.Results.ValidationFailure>();
        modelBuilder.Ignore<FluentValidation.Results.ValidationResult>();
    }

    public async Task Commit()
    {
        await SaveChangesAsync();
    }
}