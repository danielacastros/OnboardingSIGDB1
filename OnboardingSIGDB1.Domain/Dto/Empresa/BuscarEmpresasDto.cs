using System;
using System.Text.Json.Serialization;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Dto;

public class BuscarEmpresasDto
{
    public int Id { get; set; }
    public string Nome { get; set; }
    
    private string _cnpj;
    public string Cnpj
    {
        get => CnpjHelper.FormatarCnpjFormatoPadrao(_cnpj);
        set => _cnpj = value;
    }
    
    [JsonIgnore]
    public DateTime? DataFundacao { get; set; }

    public string? DataFormatada => DataFundacao?.ToString("dd/MM/yyyy");

}