using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Dto;

namespace OnboardingSIGDB1.Domain.Interfaces.Empresas;

public interface IEmpresaService
{
    Task Salvar(EmpresaDto empresaDto);
    Task Alterar(int id, AlterarEmpresaDto alterarEmpresaDto);
    Task Excluir(int id);
}