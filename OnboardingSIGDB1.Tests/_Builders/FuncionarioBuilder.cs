using Bogus;
using Bogus.Extensions.Brazil;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.Tests._Builders;

public class FuncionarioBuilder
{
    private Faker _faker;
    private int _id;
    private string _nome;
    private string _cpf;
    private DateTime? _dataContratacao;
    private int? _empresaId;
    public Empresa? _empresa;

    public FuncionarioBuilder()
    {
        _faker = new Faker();

        _id = _faker.IndexGlobal;
        _nome = _faker.Person.FullName;
        _cpf = _faker.Person.Cpf();
        _dataContratacao = _faker.Date.Past(10, DateTime.Now);
        _empresa = null;
        _empresaId = null;
    }

    public static FuncionarioBuilder Novo()
    {
        return new FuncionarioBuilder();
    }
    
    public FuncionarioBuilder ComId(int id)
    {
        _id = id;
        return this;
    }

    public FuncionarioBuilder ComNome(string nome)
    {
        _nome = nome;
        return this;
    }

    public FuncionarioBuilder ComCpf(string cpf)
    {
        _cpf = cpf;
        return this;
    }

    public FuncionarioBuilder ComDataDeContratacao(DateTime? dataContratacao)
    {
        _dataContratacao = dataContratacao;
        return this;
    }

    public FuncionarioBuilder ComEmpresaId(int? empresaId)
    {
        _empresaId = empresaId;
        return this;
    }
    
    public FuncionarioBuilder ComEmpresa(Empresa? empresa)
    {
        _empresa = empresa;
        _empresaId = empresa.Id;
        return this;
    }

    public Funcionario Build()
    {
        var funcionario = new Funcionario(_nome, _cpf, _dataContratacao, _empresaId);
        
        if (_empresa != null)
            funcionario.VincularEmpresa(_empresa);
        
        return funcionario;
    }
}