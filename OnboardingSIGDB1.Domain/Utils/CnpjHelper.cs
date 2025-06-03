using System;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain.Utils;

public class CnpjHelper
{
    public static string FormatarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return cnpj;
        
        return cnpj.Replace(".", "").Replace("-", "").Replace("/", "");
    }
    
    public static string FormatarCnpjFormatoPadrao(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return cnpj;

        cnpj = Regex.Replace(cnpj, @"\D", ""); // Remove tudo que não for número

        if (cnpj.Length != 14)
            return cnpj; // Retorna como está se não tiver 14 dígitos

        return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
    }
}