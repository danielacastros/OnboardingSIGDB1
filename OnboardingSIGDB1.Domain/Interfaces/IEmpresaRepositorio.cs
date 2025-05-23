using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IEmpresaRepositorio : IRepositorio<Empresa>
{
    Task<Empresa> BuscarPorCnpj(string cnpj);
    Task<List<Empresa>> ObterTodasPorIntervaloDataFundacao(DateTime dataInicial, DateTime dataFinal);
    Task<List<Empresa>> ObterEmpresaPeloNome(string nome);
}