using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;
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
            f => f.PropertyName == nameof(Funcionario.Nome) && f.ErrorMessage == Resource.NomeObrigatorio);
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
            f => f.PropertyName == nameof(Funcionario.Cpf) && f.ErrorMessage == Resource.CpfObrigatorio);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == nameof(Funcionario.Cpf) && f.ErrorMessage == Resource.CpfInvalido);
    }

    [Theory]
    [InlineData("123456789101")]
    public void NaoDeveCpfTerMaisQueOnzeCaracteres(string cpfInvalido)
    {
        // arrange 
        var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();
        
        // assert
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors, 
            f => f.PropertyName == nameof(Funcionario.Cpf) && f.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }

    [Theory]
    [InlineData("111.222.333-44")]
    public void NaoDeveFuncionarioTerUmCpfInvalido(string cpfInvalido)
    {
        var funcionario = FuncionarioBuilder.Novo().ComCpf(cpfInvalido).Build();
        
        Assert.False(funcionario.Valid);
        Assert.Contains(funcionario.ValidationResult.Errors,
            f => f.PropertyName == nameof(Funcionario.Cpf) && f.ErrorMessage == Resource.CpfInvalido);
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
            f => f.PropertyName == nameof(Funcionario.Nome) && f.ErrorMessage == Resource.QuantidadeDeCaracteresInvalida);
    }
}