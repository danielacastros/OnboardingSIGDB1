using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IRepositorio<TEntidade>
{
    TEntidade ObterPorId(int id);
    List<TEntidade> Consultar();
    void Adicionar(TEntidade entity);
    void Alterar(TEntidade entity);
    void Remover(TEntidade entity);
}