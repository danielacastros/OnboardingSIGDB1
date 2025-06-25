using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Tests._Builders;

public class FuncionarioDtoBuilder
{
    private Faker _faker;
    private string _nome;
    private string _cpf;
    private DateTime? _dataContratacao;
    private int? _empresaId;

    public FuncionarioDtoBuilder()
    {
        _faker = new Faker();

        _nome = _faker.Person.FullName;
        _cpf = _faker.Person.Cpf();
        _dataContratacao = _faker.Date.Past(10, DateTime.Today);
        _empresaId = null;
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
    
    public FuncionarioDtoBuilder ComEmpresaId(int? empresaId)
    {
        _empresaId = _empresaId;
        return this;
    }

    public FuncionarioDto Build()
    {
        
        var funcionario = new FuncionarioDto
        {
            Nome = _nome,
            Cpf = _cpf,
            DataContratacao = _dataContratacao,
            EmpresaId = _empresaId
        };

        return funcionario;
    }
}