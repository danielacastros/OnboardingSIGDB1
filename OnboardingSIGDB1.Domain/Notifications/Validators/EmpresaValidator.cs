using System;
using System.Text.RegularExpressions;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Notifications.Validators;

public class EmpresaValidator : AbstractValidator<Empresa>
{
    public EmpresaValidator()
    {
        RuleFor(e => e.Nome)
            .NotEmpty()
            .MaximumLength(150);

        RuleFor(e => e.Cnpj)
            .NotEmpty()
            .MaximumLength(14)
            .Must(ValidarCnpj);

        RuleFor(e => e.DataFundacao)
            .GreaterThan(DateTime.MinValue)
            .WithMessage(Resource.DataInvalida);
    }
    
    private bool ValidarCnpj(string cnpj)
    {
        if (string.IsNullOrWhiteSpace(cnpj))
            return false;

        cnpj = Regex.Replace(cnpj, @"[^\d]", "");

        if (cnpj.Length != 14)
            return false;

        if (new string(cnpj[0], cnpj.Length) == cnpj)
            return false;

        int[] multiplicador1 = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        int[] multiplicador2 = { 6, 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCnpj = cnpj.Substring(0, 12);
        int soma = 0;

        for (int i = 0; i < 12; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador1[i];

        int resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;

        tempCnpj += resto;
        soma = 0;

        for (int i = 0; i < 13; i++)
            soma += int.Parse(tempCnpj[i].ToString()) * multiplicador2[i];

        resto = (soma % 11);
        resto = resto < 2 ? 0 : 11 - resto;

        return cnpj.EndsWith(resto.ToString());
    }
}