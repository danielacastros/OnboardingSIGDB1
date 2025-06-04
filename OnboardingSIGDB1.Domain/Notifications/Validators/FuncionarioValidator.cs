using System;
using System.Text.RegularExpressions;
using FluentValidation;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Domain.Notifications.Validators;

public class FuncionarioValidator : AbstractValidator<Funcionario>
{
    public FuncionarioValidator()
    {
        RuleFor(f => f.Nome)
            .NotEmpty().WithMessage(Resource.NomeObrigatorio)
            .MaximumLength(150).WithMessage(Resource.QuantidadeDeCaracteresInvalida);

        RuleFor(f => f.Cpf)
            .NotEmpty().WithMessage(Resource.CpfObrigatorio)
            .MaximumLength(11).WithMessage(Resource.QuantidadeDeCaracteresInvalida)
            .Must(ValidarCpf).WithMessage(Resource.CpfInvalido);

        RuleFor(f => f.DataContratacao)
            .GreaterThan(DateTime.MinValue)
            .WithMessage(Resource.DataInvalida);
    }

    private static bool ValidarCpf(string cpf)
    {
        if (string.IsNullOrWhiteSpace(cpf))
            return false;
        
        // Remove caracteres não numéricos
        cpf = Regex.Replace(cpf, @"[^\d]", "");

        // Verifica se tem 11 dígitos
        if (cpf.Length != 11)
            return false;

        // Verifica se todos os dígitos são iguais (ex: 111.111.111-11)
        if (new string(cpf[0], cpf.Length) == cpf)
            return false;

        // Calcula o primeiro dígito verificador
        int soma = 0;
        for (int i = 0; i < 9; i++)
            soma += (cpf[i] - '0') * (10 - i);

        int resto = soma % 11;
        int digito1 = (resto < 2) ? 0 : 11 - resto;

        // Calcula o segundo dígito verificador
        soma = 0;
        for (int i = 0; i < 10; i++)
            soma += (cpf[i] - '0') * (11 - i);

        resto = soma % 11;
        int digito2 = (resto < 2) ? 0 : 11 - resto;

        // Verifica se os dígitos calculados são iguais aos informados
        return cpf[9] - '0' == digito1 && cpf[10] - '0' == digito2;
    }
}