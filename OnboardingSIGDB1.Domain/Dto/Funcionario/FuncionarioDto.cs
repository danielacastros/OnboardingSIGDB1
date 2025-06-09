using System;

namespace OnboardingSIGDB1.Domain.Dto.Funcionario;

public class CriarFuncionarioDto
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime? DataContratacao { get; set; }
}