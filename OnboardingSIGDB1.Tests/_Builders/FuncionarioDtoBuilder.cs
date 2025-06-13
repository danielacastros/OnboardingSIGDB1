using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Dto.Funcionario;

namespace OnboardingSIGDB1.Tests._Builders;

public class FuncionarioDtoBuilder
{
    private Faker _faker;
    private string _nome;
    private string _cpf;
    private DateTime? _dataContratacao;

    public FuncionarioDtoBuilder()
    {
        _faker = new Faker();

        _nome = _faker.Person.FullName;
        _cpf = _faker.Person.Cpf();
        _dataContratacao = _faker.Date.Past(10, DateTime.Today);
    }

    public static FuncionarioDtoBuilder Novo()
    {
        return new FuncionarioDtoBuilder();
    }

    public FuncionarioDtoBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public FuncionarioDtoBuilder ComCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }

    public FuncionarioDtoBuilder ComDataDeContratacao(DateTime dataContratacao)
    {
        _dataContratacao = dataContratacao;
        return this;
    }

    public FuncionarioDto Build()
    {
        return new FuncionarioDto
        {
            Nome = _nome,
            Cpf = _cpf,
            DataContratacao = _dataContratacao
        };
    }
}