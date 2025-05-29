using System.Collections.Generic;
using System.Linq;

namespace OnboardingSIGDB1.Domain.Base;

public class ResultadoDaConsultaBase
{
    public int Total { get; set; }
    public IEnumerable<object> Lista { get; set; }

    public ResultadoDaConsultaBase()
    {
        
    }
    public ResultadoDaConsultaBase(IEnumerable<object> lista)
    {
        Lista = lista;
        Total = lista.Count();
    }
}