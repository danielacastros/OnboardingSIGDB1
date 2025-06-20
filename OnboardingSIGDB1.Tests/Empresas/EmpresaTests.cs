using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Empresas;

public class EmpresaTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveEmpresaTerUmNomeInvalido(string nomeInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComNome(nomeInvalido).Build();
        
        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors, 
            e => e.PropertyName == nameof(Empresa.Nome) && e.ErrorMessage == Resource.NomeObrigatorio);
    }

    [Theory]
    [InlineData("00.000.000/0000-00")]
    public void NaoDeveEmpresaTerUmCnpjInvalido(string cnpjInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComCnpj(cnpjInvalido).Build();
        
        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors, e => e.PropertyName == nameof(Empresa.Cnpj) && e.ErrorMessage == Resource.CnpjInvalido);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveEmpresaTerUmCnpjNuloOuVazio(string cnpjInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComCnpj(cnpjInvalido).Build();
        
        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors, e => e.PropertyName == nameof(Empresa.Cnpj) && e.ErrorMessage == Resource.CnpjObrigatorio);
    }

    [Theory]
    [InlineData("Instituto Internacional de Soluções Inovadoras para Desenvolvimento Sustentável, Tecnologias Avançadas e Consultoria Estratégica em Mercados Emergentes e Globais Ltda")]
    public void NaoDeveNomeTerMaisQueCentoECinquentaCaracteres(string nomeInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComNome(nomeInvalido).Build();
        
        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors,
            e => e.PropertyName == nameof(Empresa.Nome) && e.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }

    [Theory]
    [InlineData("123456789101112")]
    public void NaoDeveCnpjTerMaisQueCatorzeCaracteres(string cnpjInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComCnpj(cnpjInvalido).Build();

        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors,
            e => e.PropertyName == nameof(Empresa.Cnpj) && e.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }
}