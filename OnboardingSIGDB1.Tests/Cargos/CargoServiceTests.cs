using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Cargos;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Cargo;

public class CargoServiceTests
{
    private readonly Faker _faker;
    private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
    private readonly CargoService _cargoService;
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<IMapper> _mapperMock;
    public CargoServiceTests()
    {
        _cargoRepositorioMock = new Mock<ICargoRepositorio>();
        _notificationContextMock = new Mock<INotificationContext>();
        _mapperMock = new Mock<IMapper>();
        _cargoService = new CargoService(_cargoRepositorioMock.Object, _notificationContextMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task QuandoDadosValidos_DeveArmazenarCargo()
    {
        // arrange
        var cargoDto = CargoDtoBuilder.Novo().Build();
        var cargo = CargoBuilder.Novo().ComDescricao(cargoDto.Descricao).Build();

        _mapperMock.Setup(m => m.Map<Domain.Entity.Cargo>(cargoDto))
            .Returns(cargo);
        
        // act
        await _cargoService.Armazenar(cargoDto);

        // assert
        _cargoRepositorioMock.Verify(r => r.Adicionar(It.Is<Domain.Entity.Cargo>(x => 
            x.Descricao == cargo.Descricao)), Times.Once);
        _notificationContextMock.Verify(x => x.AddNotification(It.IsAny<Notification>()), Times.Never);
    }
}