using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces.Funcionarios;

namespace OnboardingSIGDB1.Data.Repositorios;

public class FuncionarioRepositorio : RepositorioBase<Funcionario>, IFuncionarioRepositorio
{
    public FuncionarioRepositorio(ApplicationDbContext context) : base(context)
    {
        
    }

    public async Task<Funcionario> BuscarPorCpf(string cpf)
    {
        return await Context.Set<Funcionario>()
            .AsNoTracking()
            .FirstOrDefaultAsync(f => f.Cpf == cpf);
    }

    public async Task<List<Funcionario>> BuscarPorNome(string nome)
    {
        List<Funcionario> funcionarios = await Context.Set<Funcionario>()
            .Where(f => EF.Functions.Like(f.Nome, $"%{nome}%"))
            .ToListAsync();
        
        return funcionarios.Count > 0 ? funcionarios : new List<Funcionario>();
    }

    public async Task<List<Funcionario>> ObterTodosPorIntervaloDataContratacao(DateTime dataInicial, DateTime dataFinal)
    {
        List<Funcionario> funcionarios = await Context.Set<Funcionario>()
            .Where(f => f.DataContratacao >= dataInicial && f.DataContratacao <= dataFinal)
            .ToListAsync();

        return funcionarios.Count > 0 ? funcionarios : new List<Funcionario>();
    }

    public async Task<List<Funcionario>> ObterTodosComEmpresa()
    {
        List<Funcionario> funcionarios = await Context.Set<Funcionario>()
            .Include(f => f.Empresa).ToListAsync();
        return funcionarios.Count > 0 ? funcionarios : new List<Funcionario>();
    }
    
    public async Task<List<Funcionario>> VerificarSePossuiFuncionarioVinculado(int id)
    {
        var funcionarios = await Context.Set<Funcionario>()
            .Where(e => e.EmpresaId == id).ToListAsync();
        return funcionarios .Count > 0 ? funcionarios : new List<Funcionario>();
    }
}