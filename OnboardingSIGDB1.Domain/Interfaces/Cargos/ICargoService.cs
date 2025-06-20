using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Dto.Cargo;

namespace OnboardingSIGDB1.Domain.Interfaces.Cargos;

public interface ICargoService
{
    Task Armazenar(CargoDto cargoDto);
    Task Alterar(int id, CargoDto cargoDto);
    Task Excluir(int id);
}