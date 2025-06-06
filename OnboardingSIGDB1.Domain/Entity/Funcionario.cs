using System;
using OnboardingSIGDB1.Domain.Notifications.Validators;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Entity;

public class Funcionario : Entidade
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTime? DataContratacao { get; private set; }
    public Funcionario(string nome, string cpf, DateTime? dataContratacao)
    {
        Nome = nome;
        Cpf = CpfHelper.FormatarCpf(cpf);
        DataContratacao = dataContratacao;

        Validar(this, new FuncionarioValidator());
    }
}