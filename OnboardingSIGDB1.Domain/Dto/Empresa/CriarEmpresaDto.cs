using System;

namespace OnboardingSIGDB1.Domain.Dto;

public class CriarEmpresaDto
{
    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public DateTime DataFundacao { get; set; }
}