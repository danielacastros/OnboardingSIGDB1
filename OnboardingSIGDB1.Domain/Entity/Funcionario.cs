#nullable enable
using System;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Notifications.Validators;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Entity;

public class Funcionario : Entidade
{
    public string Nome { get; private set; }
    public string Cpf { get; private set; }
    public DateTime? DataContratacao { get; private set; }
    public int? EmpresaId { get; private set; }
    public virtual Empresa? Empresa { get; private set; }
    public virtual ICollection<FuncionarioCargo> Cargos { get; private set; }
    public Funcionario(string nome, string cpf, DateTime? dataContratacao)
    {
        Nome = nome;
        Cpf = CpfHelper.FormatarCpf(cpf);
        DataContratacao = dataContratacao;

        Validar(this, new FuncionarioValidator());
    }

    public void AlterarNome(string nome)
    {
        Nome = nome;
        Validar(this, new FuncionarioValidator());
    }

    public void AlterarCpf(string cpf)
    {
        Cpf = cpf;
        Validar(this, new FuncionarioValidator());
    }

    public void AlterarDataContratacao(DateTime? dataContratacao)
    {
        DataContratacao = dataContratacao;
        Validar(this, new FuncionarioValidator());
    }

    public bool VincularEmpresa(Empresa empresa)
    {
        if (EmpresaId != null)
            return false;
        
        EmpresaId = empresa.Id;
        Empresa = empresa;
        return true;
    }
}