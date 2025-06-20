using System.Collections.Generic;
using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IFuncionarioCargoRepositorio
{
    Task<List<FuncionarioCargo>> ObterTodos();
    Task Adicionar(FuncionarioCargo funcionarioCargo);
    Task Alterar(FuncionarioCargo funcionarioCargo);
    Task Excluir(FuncionarioCargo funcionarioCargo);
}