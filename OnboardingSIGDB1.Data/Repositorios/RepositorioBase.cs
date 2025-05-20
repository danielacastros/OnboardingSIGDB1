using System.Collections.Generic;
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
        throw new System.NotImplementedException();
    }

    public List<TEntidade> Consultar()
    {
        throw new System.NotImplementedException();
    }

    public void Adicionar(TEntidade entity)
    {
        Context.Set<TEntidade>().Add(entity);
        Context.SaveChanges();
    }
}