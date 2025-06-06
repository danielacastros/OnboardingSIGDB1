using System;
using System.Text.RegularExpressions;

namespace OnboardingSIGDB1.Domain.Utils;

public class CpfHelper
{
    public static string FormatarCpf(string cpf)
    {
        if (!string.IsNullOrWhiteSpace(cpf))
            cpf = Regex.Replace(cpf, @"[^\d]", "");

        return cpf;
    }

    public static string AplicarMascaraCpf(string cpf)
    {
        if (!string.IsNullOrWhiteSpace(cpf))
            cpf = Convert.ToUInt64(cpf).ToString(@"000\.000\.000\-00");

        return cpf;
    }
}