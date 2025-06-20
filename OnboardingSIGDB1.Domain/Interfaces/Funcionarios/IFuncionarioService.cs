using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Dto.Funcionario;

namespace OnboardingSIGDB1.Domain.Interfaces.Funcionarios;

public interface IFuncionarioService
{
    Task Armazenar(FuncionarioDto funcionarioDto);
    Task Alterar(int id, FuncionarioDto funcionarioDto);
    Task Excluir(int id);
    Task VincularEmpresa(VinculoEmpresaDto vinculoEmpresaDto);
    Task VincularCargo(FuncionarioCargoDto funcionarioCargoDto);
}