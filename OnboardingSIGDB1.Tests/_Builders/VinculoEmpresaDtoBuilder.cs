using Bogus;
using OnboardingSIGDB1.Domain.Dto.Funcionario;

namespace OnboardingSIGDB1.Tests._Builders;

public class VinculoEmpresaDtoBuilder
{
    private Faker _faker;
    private int _funcionarioId;
    private int _empresaId;

    public VinculoEmpresaDtoBuilder()
    {
        _faker = new Faker();

        _funcionarioId = _faker.IndexFaker;
        _empresaId = _faker.IndexFaker;
    }

    public static VinculoEmpresaDtoBuilder Novo()
    {
        return new VinculoEmpresaDtoBuilder();
    }

    public VinculoEmpresaDto Build()
    {
        return new VinculoEmpresaDto()
        {
            FuncionarioId = _funcionarioId,
            EmpresaId = _empresaId
        };
    }
}