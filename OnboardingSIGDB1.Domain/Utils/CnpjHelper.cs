namespace OnboardingSIGDB1.Domain.Utils;

public class CnpjHelper
{
    public static string FormatarCnpj(string cnpj)
    {
        return cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
    }
}