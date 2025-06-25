using System;
using System.Collections;
using System.Collections.Generic;
using OnboardingSIGDB1.Domain.Notifications.Validators;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Entity;

public class Empresa : Entidade
{
    public string Nome { get; private set; }
    public string Cnpj { get; private set; }
    public DateTime? DataFundacao { get; private set; }
    public virtual ICollection<Funcionario> Funcionarios { get; private set; }
    public Empresa(string cnpj, string nome, DateTime? dataFundacao)
    {
        Cnpj =  CnpjHelper.FormatarCnpj(cnpj);
        Nome = nome;
        DataFundacao = dataFundacao;

        Validar(this, new EmpresaValidator());
    }

    public void AlterarCnpj(string cnpj)
    {
        Cnpj = CnpjHelper.FormatarCnpj(cnpj);
    }

    public void AlterarNome(string nome)
    {
        Nome = nome;
    }

    public void AlterarDataFundacao(DateTime? fundacao)
    {
        DataFundacao = fundacao;
    }

    public void Alterar(Empresa empresa)
    {
        Nome = empresa.Nome;
        Cnpj = CnpjHelper.FormatarCnpj(empresa.Cnpj);
        DataFundacao = empresa.DataFundacao;
    }
}