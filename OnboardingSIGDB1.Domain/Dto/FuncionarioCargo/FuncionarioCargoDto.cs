using System;

namespace OnboardingSIGDB1.Domain.Dto.Funcionario;

public class FuncionarioCargoDto
{
    public int FuncionarioId { get; set; }
    public int CargoId { get; set; }
    public DateTime dataVinculo { get; set; }
}