using System;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IEmpresaRepositorio : IRepositorio<Empresa>
{
    Empresa BuscarPorCnpj(string cnpj);
    Empresa BuscarPorIntervaloDataFundacao(DateTime fundacao);
    
}