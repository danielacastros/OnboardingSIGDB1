using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces.Empresas;

namespace OnboardingSIGDB1.Data.Repositorios;

public class EmpresaRepositorio : RepositorioBase<Empresa>, IEmpresaRepositorio
{
    public EmpresaRepositorio(ApplicationDbContext context) : base(context)
    {
        
    }

    public async Task<Empresa> BuscarPorCnpj(string cnpj)
    {
        return await Context.Set<Empresa>()
            .AsNoTracking()
            .FirstOrDefaultAsync(e => e.Cnpj == cnpj);
    }

    public async Task<List<Empresa>> ObterTodasPorIntervaloDataFundacao(DateTime dataInicial, DateTime dataFinal)
    {
        var empresas = await Context.Set<Empresa>()
            .Where(e => e.DataFundacao >= dataInicial && e.DataFundacao <= dataFinal)
            .ToListAsync();
        return empresas.Any() ? empresas : new List<Empresa>();
    }

    public async Task<List<Empresa>> ObterEmpresaPeloNome(string nome)
    {
        List<Empresa> empresas = await Context.Set<Empresa>()
            .Where(e => EF.Functions.Like(e.Nome, $"%{nome}%"))
            .ToListAsync();
        return empresas.Any() ? empresas : new List<Empresa>();
    }

    public async Task<Empresa> ListarFuncionariosVinculados(int id)
    {
        Empresa funcionariosVinculados = await Context.Set<Empresa>()
            .Include(e => e.Funcionarios)
            .FirstOrDefaultAsync(e => e.Id == id);

        return funcionariosVinculados;
    }
}