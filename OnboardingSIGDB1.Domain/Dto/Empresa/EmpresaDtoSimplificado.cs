using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Dto;

public class EmpresaDtoSimplificado
{
    public string Nome { get; set; }

    private string _cnpj;

    public string Cnpj
    {
        get => CnpjHelper.FormatarCnpjFormatoPadrao(_cnpj);
        set => _cnpj = value;
    }
    
}