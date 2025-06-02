using AutoMapper;
using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Notifications.Validators;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Empresas;

public class ArmazenadorDeEmpresaTests
{
    private readonly Faker _faker;
    private readonly EmpresaDto _empresaDto;
    private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
    private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<IMapper> _mapperMock;

    public ArmazenadorDeEmpresaTests()
    {
        _faker = new Faker();
        _empresaDto = new EmpresaDto()
        {
            Nome = _faker.Company.CompanyName(),
            Cnpj = "26582544000100",
            DataFundacao = _faker.Date.Past(15)
        };
        _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        _notificationContextMock = new Mock<INotificationContext>();
        _mapperMock = new Mock<IMapper>();
        _armazenadorDeEmpresa = new ArmazenadorDeEmpresa(_empresaRepositorioMock.Object, _notificationContextMock.Object, _mapperMock.Object);
    }
    
    [Fact]
    public async Task QuandoDadosValidos_DeveArmazenarEmpresa()
    {
        //Arrange
        var empresa = EmpresaBuilder
            .Nova()
            .ComNome(_empresaDto.Nome)
            .ComCnpj("26582544000100")
            .ComDataFundacao(_empresaDto.DataFundacao)
            .Build();

        _mapperMock.Setup(m => m.Map<Empresa>(_empresaDto))
            .Returns(empresa);
        
        var cnpjFormatado = CnpjHelper.FormatarCnpj(_empresaDto.Cnpj);
        
        _empresaRepositorioMock
            .Setup(r => r.BuscarPorCnpj("26582544000100"))
            .ReturnsAsync((Empresa)null);
        
        //Act 
        await _armazenadorDeEmpresa.Armazenar(_empresaDto);

        //Assert
        _empresaRepositorioMock.Verify(e => e.Adicionar(It.Is<Empresa>(x =>
            x.Nome == _empresaDto.Nome && 
            x.Cnpj == _empresaDto.Cnpj && 
           x.DataFundacao == _empresaDto.DataFundacao)), Times.Once);
        
        _empresaRepositorioMock.Verify(e => e.Adicionar(It.IsAny<Empresa>()), Times.Once);

        _notificationContextMock.Verify(n =>
            n.AddNotifications(It.IsAny<IReadOnlyCollection<Notification>>()), Times.Never);
    }
}