using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Dto;

namespace OnboardingSIGDB1.Tests._Builders;

public class EmpresaDtoBuilder
{
    private string _nome;
    private string _cnpj;
    private DateTime? _dataFundacao;
    private Faker _faker;

    public EmpresaDtoBuilder()
    {
        _faker = new Faker();

        _nome = _faker.Company.CompanyName();
        _cnpj = _faker.Company.Cnpj();
        _dataFundacao = _faker.Date.Past(10, DateTime.Now);
    }

    public static EmpresaDtoBuilder Novo()
    {
        return new EmpresaDtoBuilder();
    }

    public EmpresaDtoBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    
    public EmpresaDtoBuilder ComCnpj(string cnpj)
    {
        _cnpj = cnpj;
        return this;
    }

    public EmpresaDtoBuilder ComDataFundacao(DateTime? dataFundacao)
    {
        _dataFundacao = dataFundacao;
        return this;
    }

    public EmpresaDto Build()
    {
        var empresaDto = new EmpresaDto
        {
           Cnpj = _cnpj,
           Nome = _nome,
           DataFundacao = _dataFundacao
        };
        return empresaDto;
    }
}