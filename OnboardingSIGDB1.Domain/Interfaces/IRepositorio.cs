using System.Collections.Generic;

namespace OnboardingSIGDB1.Domain.Interfaces;

public interface IRepositorio<TEntidade>
{
    TEntidade ObterPorId(int id);
    List<TEntidade> ObterTodas();
    void Adicionar(TEntidade entity);
    void Alterar(TEntidade entity);
    void Excluir(TEntidade entity);
}