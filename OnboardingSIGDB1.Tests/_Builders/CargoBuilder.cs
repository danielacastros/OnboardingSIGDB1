using Bogus;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Tests._Builders;

public class CargoBuilder
{
    private Faker _faker;
    private string _descricao;
    
    public CargoBuilder()
    {
        _faker = new Faker();

        _descricao = _faker.Name.JobTitle();
    }

    public static CargoBuilder Novo()
    {
        return new CargoBuilder();
    }

    public CargoBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }

    public Domain.Entity.Cargo Build()
    {
        return new Domain.Entity.Cargo(_descricao);
    }
}