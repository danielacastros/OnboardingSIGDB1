using System;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Notifications.Validators;

namespace OnboardingSIGDB1.Domain.Entity;

public class Empresa : Entidade
{
    public string Nome { get; private set; }

    public string Cnpj { get; private set; }

    public DateTime DataFundacao { get; private set; }

    public Empresa(string cnpj, string nome, DateTime dataFundacao)
    {
        Cnpj = cnpj;
        Nome = nome;
        DataFundacao = dataFundacao;

        Validar(this, new EmpresaValidator());
    }

    public void AlterarCnpj(string cnpj)
    {
        
        Cnpj = cnpj;
    }

    public void AlterarNome(string nome)
    {
        Nome = nome;
    }

    public void AlterarDataFundacao(DateTime fundacao)
    {
        DataFundacao = fundacao;
    }

    public void Alterar(Empresa empresa, string cnpjFormatado)
    {
        Nome = empresa.Nome;
        Cnpj = cnpjFormatado;
        DataFundacao = empresa.DataFundacao;
        
        Validar(this, new EmpresaValidator());
    }
}