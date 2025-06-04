using System.Collections.Generic;
using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IFuncionarioRepositorio : IRepositorio<Funcionario>
{
    Task<Funcionario> BuscarPorCpf(string cpf);
    Task<List<Funcionario>> BuscarPorNome(string nome);
}