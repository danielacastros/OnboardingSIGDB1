using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IFuncionarioRepositorio : IRepositorio<Funcionario>
{
    Task<Funcionario> BuscarPorCpf(string cpf);
    Task<List<Funcionario>> BuscarPorNome(string nome);
    Task<List<Funcionario>> ObterTodosPorIntervaloDataContratacao(DateTime dataInicial, DateTime dataFinal);
    Task<List<Funcionario>> ObterTodosComEmpresa();
    Task<List<Funcionario>> VerificarSePossuiFuncionarioVinculado(int id);
}