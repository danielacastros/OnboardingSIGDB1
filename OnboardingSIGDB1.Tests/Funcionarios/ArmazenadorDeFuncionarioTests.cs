using AutoMapper;
using Moq;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Funcionarios;

public class ArmazenadorDeFuncionarioTests
{
    private ArmazenadorDeFuncionario _armazenadorDeFuncionario;
    private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
    private Mock<INotificationContext> _notificationContextMock;
    private Mock<IMapper> _mapperMock;

    public ArmazenadorDeFuncionarioTests()
    {
        _armazenadorDeFuncionario = new ArmazenadorDeFuncionario(_funcionarioRepositorioMock.Object, _notificationContextMock.Object, _mapperMock.Object);
        _notificationContextMock = new Mock<INotificationContext>();
        _mapperMock = new Mock<IMapper>();
    }
    
    [Fact]
    public async Task QuandoDadosValidos_DeveArmazenarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder
            .Novo()
            .ComNome(funcionarioDto.Nome)
            .ComCpf(funcionarioDto.Cpf)
            .ComDataDeContratacao(funcionarioDto.DataContratacao)
            .Build();

        var cpfFormatado = CpfHelper.FormatarCpf(funcionarioDto.Cpf);

        _funcionarioRepositorioMock.Setup(r => r.BuscarPorCpf(cpfFormatado)).ReturnsAsync(funcionario);
        
        // act
        await _armazenadorDeFuncionario.Armazenar(funcionarioDto);
        
        // assert
        _notificationContextMock.Verify(x => x.AddNotification(It.IsAny<Notification>()), Times.Never);
        
    }
    
    [Fact]
    public async Task QuandoDadosInvalidos_NaoDeveArmazenarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder
            .Novo()
            .ComNome(funcionarioDto.Nome)
            .ComCpf(funcionarioDto.Cpf)
            .ComDataDeContratacao(funcionarioDto.DataContratacao)
            .Build();
        // act
        await _armazenadorDeFuncionario.Armazenar(funcionarioDto);
        
        // assert

    }
    
    [Fact]
    public async Task QuandoCpfJaCadastrado_NaoDeveArmazenarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder
            .Novo()
            .ComNome(funcionarioDto.Nome)
            .ComCpf(funcionarioDto.Cpf)
            .ComDataDeContratacao(funcionarioDto.DataContratacao)
            .Build();
        // act
        await _armazenadorDeFuncionario.Armazenar(funcionarioDto);
        
        // assert

    }
    
    [Fact]
    public async Task QuandoCpfInvalido_NaoDeveArmazenarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder
            .Novo()
            .ComNome(funcionarioDto.Nome)
            .ComCpf(funcionarioDto.Cpf)
            .ComDataDeContratacao(funcionarioDto.DataContratacao)
            .Build();
        
        // act
        await _armazenadorDeFuncionario.Armazenar(funcionarioDto);
        
        // assert

    }
}