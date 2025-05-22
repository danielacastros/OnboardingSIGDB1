using System.Collections.Generic;
using System.Linq;
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

    public TEntidade ObterPorId(int id)
    {
        return Context.Set<TEntidade>()
            .FirstOrDefault(e => e.Id == id);
    }

    public List<TEntidade> ObterTodas()
    {
        var entidades = Context.Set<TEntidade>().ToList();
        return entidades.Any() ? entidades : new List<TEntidade>();
    }
    
    public void Adicionar(TEntidade entity)
    {
        Context.Set<TEntidade>().Add(entity);
        Context.SaveChanges();
    }

    public void Alterar(TEntidade entity)
    {
        Context.Set<TEntidade>().Update(entity);
        Context.SaveChanges();
    }

    public void Excluir(TEntidade entity)
    {
        Context.Set<TEntidade>().Remove(entity);
    }
}