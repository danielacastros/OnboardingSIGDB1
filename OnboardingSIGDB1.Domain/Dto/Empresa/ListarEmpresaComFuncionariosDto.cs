using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Dto.Funcionario;

namespace OnboardingSIGDB1.Domain.Dto;

public class ListarEmpresaComFuncionariosDto
{
    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public DateTime? DataFundacao { get; set; }
    public virtual List<FuncionarioDto> Funcionarios { get; set; }
}