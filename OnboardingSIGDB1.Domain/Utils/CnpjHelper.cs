using System;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain.Utils;

public class CnpjHelper
{
    public static string FormatarCnpj(string cnpj)
    {
        if (!string.IsNullOrWhiteSpace(cnpj))
            cnpj = cnpj.Replace(".", "").Replace("-", "").Replace("/", "");

        return cnpj;
    }
    
    public static string FormatarCnpjFormatoPadrao(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            cnpj = Regex.Replace(cnpj, @"\D", "");

        if (cnpj.Length != 14)
            return cnpj; 

        return Convert.ToUInt64(cnpj).ToString(@"00\.000\.000\/0000\-00");
    }
}