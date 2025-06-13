using System;
using System.Text.Json.Serialization;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Dto.Funcionario;

public class BuscarFuncionariosComEmpresaDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Cpf { get; set; }
    [JsonIgnore]
    public DateTime? DataContratacao { get; set; }
    public string? DataFormatada => 
        DataContratacao?.ToString("dd/MM/yyyy");
    public virtual EmpresaDtoSimplificado? Empresa { get; set; }
}