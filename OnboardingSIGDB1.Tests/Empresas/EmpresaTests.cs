using Bogus;
using ExpectedObjects;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Empresas;

public class EmpresaTests
{
    private readonly Faker _faker;

    public EmpresaTests()
    {
        _faker = new Faker();
    }
    
    [Fact]
    public void QuandoDadosValidos_DeveCriarEmpresa()
    {
        var empresaEsperada = new
        {
            Nome = "Empresa",
            Cnpj = "CNPJ",
            DataFundacao = DateTime.Today,
        };
        
        var empresa = new Empresa(empresaEsperada.Nome, empresaEsperada.Cnpj, empresaEsperada.DataFundacao);

        empresaEsperada.ToExpectedObject().ShouldMatch(empresa);
    }

    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void QuandoNomeInvalido_NaoDeveCriarEmpresa(string nomeInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComNome(nomeInvalido).Build();
        
        //Assert.False(empresa.Valid);
        //Assert.Contains();
    }
}