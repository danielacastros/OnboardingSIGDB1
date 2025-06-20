using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositorios;

public class FuncionarioCargoRepositorio : IFuncionarioCargoRepositorio
{
    protected readonly ApplicationDbContext Context;
    
    public FuncionarioCargoRepositorio(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<List<FuncionarioCargo>> ObterTodos()
    {
        var entidades = await Context.Set<FuncionarioCargo>().ToListAsync();
        return entidades.Any() ? entidades : new List<FuncionarioCargo>();
    }

    public async Task Adicionar(FuncionarioCargo funcionarioCargo)
    {
        Context.Set<FuncionarioCargo>().Add(funcionarioCargo);
    }

    public async Task Alterar(FuncionarioCargo funcionarioCargo)
    {
        Context.Set<FuncionarioCargo>().Update(funcionarioCargo);
    }

    public async Task Excluir(FuncionarioCargo funcionarioCargo)
    {
        Context.Set<FuncionarioCargo>().Remove(funcionarioCargo);
    }
    
}