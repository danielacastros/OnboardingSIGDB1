using System;

namespace OnboardingSIGDB1.Domain.Dto.Funcionario;

public class FuncionarioDto
{
    public string Nome { get; set; }
    public string Cpf { get; set; }
    public DateTime? DataContratacao { get; set; }
    public int? EmpresaId { get; set; }
}