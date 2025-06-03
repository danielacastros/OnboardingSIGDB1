using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Tests._Builders;

public class EmpresaBuilder
{
    private Faker _faker;
    private string _nome;
    private string _cnpj;
    private DateTime _dataFundacao;
    private int _id;

    public EmpresaBuilder()
    {
        _faker = new Faker();
        
        _nome = _faker.Company.CompanyName();
        _cnpj = _faker.Company.Cnpj();
        _dataFundacao = _faker.Date.Past(15, DateTime.Now);
    }
    
    public static EmpresaBuilder Nova()
    {
        return new EmpresaBuilder();
    }

    public EmpresaBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public EmpresaBuilder ComCnpj(string cnpj)
    {
        _cnpj = cnpj;
        return this;
    }

    public EmpresaBuilder ComDataFundacao(DateTime dataFundacao)
    {
        _dataFundacao = dataFundacao;
        return this;
    }

    public EmpresaBuilder ComId(int id)
    {
        _id = id;
        return this;
    }

    public Empresa Build()
    {
        var empresa = new Empresa(_cnpj, _nome, _dataFundacao);

        if (_id > 0)
        {
            var propertyInfo = empresa.GetType().GetProperty("Id");
            propertyInfo.SetValue(empresa, Convert.ChangeType(_id, propertyInfo.PropertyType), null);
        }

        return empresa;
    }
    
}