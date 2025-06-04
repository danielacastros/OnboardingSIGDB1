using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Funcionarios;

public class FuncionarioTests
{
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveFuncionarioTerUmNomeNuloOuVazio(string nomeInvalido)
    {
        // arrange 
        var funcionario = FuncionarioBuilder.Novo().ComNome(nomeInvalido).Build();
        
        // assert
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == "Nome" && f.ErrorMessage == Resource.NomeObrigatorio);
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveFuncionarioTerUmCpfNuloOuVazio(string cpfInvalido)
    {
        // arrange 
        var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();

        // assert
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == "Cpf" && f.ErrorMessage == Resource.CpfObrigatorio);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == "Cpf" && f.ErrorMessage == Resource.CpfInvalido);
    }

    [Theory]
    [InlineData("111111111111")]
    public void NaoDeveCpfTerMaisQueOnzeCaracteres(string cpfInvalido)
    {
        // arrange 
        var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();
        
        // assert
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors, 
            f => f.PropertyName == "Cpf" && f.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }

    [Theory]
    [InlineData("111.111.111-11")]
    public void NaoDeveFuncionarioTerUmCpfInvalido(string cpfInvalido)
    {
        var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();
        
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == "Cpf" && f.ErrorMessage == Resource.CpfInvalido);
    }

    [Theory]
    [InlineData("Maria Aparecida dos Anjos Oliveira Monteiro da Silva Ramos Cardoso Pereira Lima Soares Fernandes de Albuquerque Castro Melo Rodrigues Almeida Silveira Silva")]
    public void NaoDeveNomeTerMaisQueCentoECinquentaCaracteres(string nomeInvalido)
    {
        // arrange 
        var funcionario = FuncionarioBuilder.Novo().ComNome(nomeInvalido).Build();
        
        // assert
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == "Nome" && f.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }
    
    
    
}