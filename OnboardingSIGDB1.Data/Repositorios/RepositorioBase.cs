using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Data.Repositorios;

public class RepositorioBase<TEntidade> : IRepositorio<TEntidade> where TEntidade : Entidade
{
    protected readonly ApplicationDbContext Context;
    
    public RepositorioBase(ApplicationDbContext context)
    {
        Context = context;
    }

    public async Task<TEntidade> ObterPorId(int id)
    {
        return await Context.Set<TEntidade>()
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task<List<TEntidade>> ObterTodas()
    {
        var entidades = await Context.Set<TEntidade>().ToListAsync();
        return entidades.Any() ? entidades : new List<TEntidade>();
    }
    
    public async Task Adicionar(TEntidade entity)
    {
        Context.Set<TEntidade>().Add(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Alterar(TEntidade entity)
    {
        Context.Set<TEntidade>().Update(entity);
        await Context.SaveChangesAsync();
    }

    public async Task Excluir(TEntidade entity)
    {
        Context.Set<TEntidade>().Remove(entity);
        await Context.SaveChangesAsync();
    }
}