using Bogus;
using Bogus.Extensions.Brazil;
using Moq;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.Tests.Empresas;

public class ArmazenadorDeEmpresaTests
{
    private readonly Faker _faker;
    private readonly EmpresaDto _empresaDto;
    private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
    private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
    private readonly NotificationContext _notificationContext;

    public ArmazenadorDeEmpresaTests()
    {
        _faker = new Faker();
        _empresaDto = new EmpresaDto()
        {
            Nome = _faker.Company.CompanyName(),
            Cnpj = _faker.Company.Cnpj(),
            DataFundacao = _faker.Date.Past(15)
        };
        _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        _notificationContext = new NotificationContext();
        _armazenadorDeEmpresa = new ArmazenadorDeEmpresa(_empresaRepositorioMock.Object, _notificationContext);
    }
    
    [Fact]
    public void QuandoDadosValidos_DeveArmazenarEmpresa()
    {
        _armazenadorDeEmpresa.Armazenar(_empresaDto);
    }
}