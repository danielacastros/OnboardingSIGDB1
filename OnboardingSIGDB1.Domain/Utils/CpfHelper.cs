using System;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain.Utils;

public class CpfHelper
{
    public static string FormatarCpf(string cpf)
    {
        // Remove qualquer caractere que não seja dígito
        cpf = Regex.Replace(cpf, @"[^\d]", "");
        
        // Formata: 000.000.000-00
        return Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");
    }
}