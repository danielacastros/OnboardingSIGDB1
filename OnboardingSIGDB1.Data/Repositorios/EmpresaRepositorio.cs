using System;
using System.Linq;
using System.Threading;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositorios;

public class EmpresaRepositorio : RepositorioBase<Empresa>, IEmpresaRepositorio
{
    public EmpresaRepositorio(ApplicationDbContext context) : base(context)
    {
        
    }

    public Empresa BuscarPorCnpj(string cnpj)
    {
        return Context.Set<Empresa>()
            .AsNoTracking()
            .FirstOrDefault(e => e.Cnpj == cnpj);
    }

    public Empresa BuscarPorIntervaloDataFundacao(DateTime fundacao)
    {
        throw new NotImplementedException();
    }
}