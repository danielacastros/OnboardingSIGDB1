using System.Threading.Tasks;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces.Cargos;

namespace OnboardingSIGDB1.Data.Repositorios;

public class CargoRepositorio : RepositorioBase<Cargo>, ICargoRepositorio
{
    public CargoRepositorio(ApplicationDbContext context) : base(context)
    {
    }

    
}