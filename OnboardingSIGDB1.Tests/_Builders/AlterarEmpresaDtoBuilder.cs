using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Dto;

namespace OnboardingSIGDB1.Tests._Builders;

public class AlterarEmpresaDtoBuilder
{
    private int _id;
    private string _nome;
    private string _cnpj;
    private DateTime _dataFundacao;
    private Faker _faker;

    public AlterarEmpresaDtoBuilder()
    {
        _faker = new Faker();

        _id = _faker.UniqueIndex;
        _nome = _faker.Company.CompanyName();
        _cnpj = _faker.Company.Cnpj();
        _dataFundacao = _faker.Date.Past(15, DateTime.Today);
    }

    public static AlterarEmpresaDtoBuilder Novo()
    {
        return new AlterarEmpresaDtoBuilder();
    }

    public AlterarEmpresaDtoBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }
    
    public AlterarEmpresaDtoBuilder ComCnpj(string cnpj)
    {
        _cnpj = cnpj;
        return this;
    }

    public AlterarEmpresaDtoBuilder ComDataFundacao(DateTime dataFundacao)
    {
        _dataFundacao = dataFundacao;
        return this;
    }

    public AlterarEmpresaDto Build()
    {
        var alterarEmpresaDto = new AlterarEmpresaDto
        {
           Cnpj = _cnpj,
           Nome = _nome,
           DataFundacao = _dataFundacao
        };
        return alterarEmpresaDto;
    }
}
