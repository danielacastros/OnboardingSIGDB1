using System.Collections.Generic;
using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IRepositorio<TEntidade>
{
    Task<TEntidade> ObterPorId(int id);
    Task<List<TEntidade>> ObterTodas();
    Task Adicionar(TEntidade entity);
    Task Alterar(TEntidade entity);
    Task Excluir(TEntidade entity);
}