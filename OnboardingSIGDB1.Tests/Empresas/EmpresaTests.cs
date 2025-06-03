using Bogus;
using ExpectedObjects;
using Moq;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Empresas;

public class EmpresaTests
{
    private readonly Mock<INotificationContext> _notificationContextMock;

    public EmpresaTests()
    {
        _notificationContextMock = new Mock<INotificationContext>();
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveEmpresaTerUmNomeInvalido(string nomeInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComNome(nomeInvalido).Build();
        
        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors, 
            e => e.PropertyName == "Nome" && e.ErrorMessage == "'Nome' deve ser informado.");
    }

    [Theory]
    [InlineData("00.000.000/0000-00")]
    public void NaoDeveEmpresaTerUmCnpjInvalido(string cnpjInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComCnpj(cnpjInvalido).Build();
        
        Assert.False(empresa.Valid);
        Assert.Contains(empresa.ValidationResult.Errors, e => e.PropertyName == "Cnpj" && e.ErrorMessage == "'Cnpj' não atende a condição definida.");
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(null)]
    public void NaoDeveEmpresaTerUmCnpjNuloOuVazio(string cnpjInvalido)
    {
        var empresa = EmpresaBuilder.Nova().ComCnpj(cnpjInvalido).Build();
        
        Assert.False(empresa.Valid);
        //Assert.Contains(empresa.ValidationResult.Errors, e => e.PropertyName == "Cnpj" && e.ErrorMessage == "'Cnpj' deve ser informado., 'Cnpj' não atende a condição definida.");
    }
    
}