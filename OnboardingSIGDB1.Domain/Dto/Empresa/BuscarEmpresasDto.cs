using System;

namespace OnboardingSIGDB1.Domain.Dto;

public class BuscarEmpresasDto
{
    public int Id { get; set; }
    public string Nome { get; set; }

    public string Cnpj { get; set; }

    public string DataFundacao { get; set; }
}