using Bogus;
using OnboardingSIGDB1.Domain.Dto.Cargo;

namespace OnboardingSIGDB1.Tests._Builders;

public class CargoDtoBuilder
{
    private Faker _faker;
    private string _descricao;
    
    public CargoDtoBuilder()
    {
        _faker = new Faker();

        _descricao = _faker.Name.JobTitle();
    }

    public static CargoDtoBuilder Novo()
    {
        return new CargoDtoBuilder();
    }

    public CargoDtoBuilder ComDescricao(string descricao)
    {
        _descricao = descricao;
        return this;
    }

    public CargoDto Build()
    {
        var cargoDto = new CargoDto
        {
            Descricao = _descricao
        };
        return cargoDto;
    }
}