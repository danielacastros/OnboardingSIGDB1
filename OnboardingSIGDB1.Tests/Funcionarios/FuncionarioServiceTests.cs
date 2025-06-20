using AutoMapper;
using FluentValidation.Results;
using Moq;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Cargos;
using OnboardingSIGDB1.Domain.Interfaces.Empresas;
using OnboardingSIGDB1.Domain.Interfaces.Funcionarios;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Notifications.Validators;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Tests._Builders;

namespace OnboardingSIGDB1.Tests.Funcionarios;

public class FuncionarioServiceTests
{
    private FuncionarioService _funcionarioService;
    private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
    private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
    private readonly Mock<ICargoRepositorio> _cargoRepositorioMock;
    private readonly Mock<IFuncionarioCargoRepositorio> _funcionarioCargoRepositorioMock;
    private Mock<INotificationContext> _notificationContextMock;
    private Mock<IMapper> _mapperMock;

    public FuncionarioServiceTests()
    {
        _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();
        _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        _cargoRepositorioMock = new Mock<ICargoRepositorio>();
        _funcionarioCargoRepositorioMock = new Mock<IFuncionarioCargoRepositorio>();
        _notificationContextMock = new Mock<INotificationContext>();
        _mapperMock = new Mock<IMapper>();
        _funcionarioService = new FuncionarioService(_funcionarioRepositorioMock.Object, 
            _empresaRepositorioMock.Object,
            _cargoRepositorioMock.Object,
            _funcionarioCargoRepositorioMock.Object,
            _notificationContextMock.Object, 
            _mapperMock.Object);
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
        await _funcionarioService.Armazenar(funcionarioDto);
        
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
            .ComNome("")
            .ComCpf("416.552.018-29")
            .ComDataDeContratacao(DateTime.MinValue)
            .Build();
        
        _mapperMock.Setup(m => m.Map<Funcionario>(funcionarioDto))
            .Returns(funcionario);
        
        // act
        await _funcionarioService.Armazenar(funcionarioDto);
        
        // assert
        _funcionarioRepositorioMock.Verify(f => f.Adicionar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => 
            x.AddNotification(nameof(Funcionario.Nome),Resource.NomeObrigatorio), Times.Once);
        _notificationContextMock.Verify(x => 
            x.AddNotification(nameof(Funcionario.Cpf),Resource.CpfInvalido), Times.Once);
        _notificationContextMock.Verify(x => 
            x.AddNotification(nameof(Funcionario.DataContratacao), Resource.DataInvalida), Times.Once);
    }
    
    [Fact]
    public async Task QuandoCpfJaCadastrado_NaoDeveArmazenarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().ComCpf("41655301829").Build();
        
        var funcionario = FuncionarioBuilder
            .Novo()
            .ComNome(funcionarioDto.Nome)
            .ComCpf(funcionarioDto.Cpf)
            .ComDataDeContratacao(funcionarioDto.DataContratacao)
            .Build();

        _funcionarioRepositorioMock
            .Setup(r => r.BuscarPorCpf(funcionarioDto.Cpf))
            .ReturnsAsync(funcionario);
        
        // act
        await _funcionarioService.Armazenar(funcionarioDto);
        
        // assert
        _funcionarioRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => x.AddNotification(nameof(Funcionario.Cpf), Resource.CpfJaCadastrado));
    }
    
    [Fact]
    public async Task QuandoCpfInvalido_NaoDeveArmazenarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        
        var funcionario = FuncionarioBuilder
            .Novo()
            .ComCpf("416.552.018-29")
            .Build();
        
        _mapperMock.Setup(m => m.Map<Funcionario>(funcionarioDto))
            .Returns(funcionario);

        // act
        await _funcionarioService.Armazenar(funcionarioDto);
        
        // assert
        _funcionarioRepositorioMock.Verify(f => f.Adicionar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => x.AddNotification(nameof(Funcionario.Cpf), Resource.CpfInvalido),Times.Once);
    }
    
    [Fact]
    public async Task QuandoNomeNaoInformado_NaoDeveCadastrarFuncionario()
    {
        // arrange 
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder.Novo().ComNome("").Build();

        _mapperMock.Setup(m => m.Map<Funcionario>(funcionarioDto)).Returns(funcionario);
        
        // act
        await _funcionarioService.Armazenar(funcionarioDto);

        // assert
        _funcionarioRepositorioMock.Verify(r => r.Adicionar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => x.AddNotification(nameof(Funcionario.Nome), Resource.NomeObrigatorio));
    }

    [Fact]
    public async Task QuandoCpfNaoInformado_NaoDeveCadastrarFuncionario()
    {
        //arrange
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder.Novo().ComCpf("").Build();

        _mapperMock
            .Setup(m => m.Map<Funcionario>(funcionarioDto))
            .Returns(funcionario);
        // act
        await _funcionarioService.Armazenar(funcionarioDto);
        
        // assert
        _funcionarioRepositorioMock.Verify(r => 
            r.Adicionar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => 
            x.AddNotification(nameof(Funcionario.Cpf), Resource.CpfObrigatorio));
    }

    [Fact]
    public async Task QuandoDataMenorQueValorMinimo_NaoDeveCadastrarFuncionario()
    {
        //arrange
        var funcionarioDto = FuncionarioDtoBuilder.Novo().Build();
        var funcionario = FuncionarioBuilder.Novo().ComDataDeContratacao(DateTime.MinValue).Build();

        _mapperMock
            .Setup(m => m.Map<Funcionario>(funcionarioDto))
            .Returns(funcionario);
        // act
        await _funcionarioService.Armazenar(funcionarioDto);
        
        // assert
        _funcionarioRepositorioMock.Verify(r => 
            r.Adicionar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => 
            x.AddNotification(nameof(Funcionario.DataContratacao), Resource.DataInvalida));
        
    }

    [Fact]
    public async Task QuandoIdInvalido_NaoDeveExcluirFuncionario()
    {
        // arrange
        int id = 0;
        
        //act 
        await _funcionarioService.Excluir(id);
        
        // assert
        _funcionarioRepositorioMock.Verify(r => r.Excluir(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => x.AddNotification(Resource.KeyFuncionario, Resource.FuncionarioNaoEncontrado));
    }

    [Fact]
    public async Task QuandoIdValido_DeveExcluirFuncionario()
    {
        // arrange
        var funcionario = FuncionarioBuilder.Novo().Build();
        _funcionarioRepositorioMock.Setup(r => r.ObterPorId(funcionario.Id)).ReturnsAsync(funcionario);

        //act
        await _funcionarioService.Excluir(funcionario.Id);
        
        //assert
        _funcionarioRepositorioMock.Verify(r => r.Excluir(It.IsAny<Funcionario>()), Times.Once);
        _notificationContextMock.Verify(x => x.AddNotification(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async Task QuandoFuncionarioVinculadoAEmpresa_NaoDeveAlterarVinculo()
    {
        // arrange 
        var vinculoEmpresaDto = VinculoEmpresaDtoBuilder.Novo().Build();
        var empresa = EmpresaBuilder.Nova().Build();
        var funcionario = FuncionarioBuilder.Novo().ComEmpresa(empresa).Build();
        
        _empresaRepositorioMock.Setup(r => r.ObterPorId(vinculoEmpresaDto.EmpresaId)).ReturnsAsync(empresa);
        _funcionarioRepositorioMock.Setup(r => r.ObterPorId(vinculoEmpresaDto.FuncionarioId)).ReturnsAsync(funcionario);
        
        // act
        await _funcionarioService.VincularEmpresa(vinculoEmpresaDto);
        
        // assert
        _funcionarioRepositorioMock.Verify(r => r.Alterar(It.IsAny<Funcionario>()), Times.Never);
        _notificationContextMock.Verify(x => x.AddNotification(Resource.KeyFuncionario, Resource.VinculoJaCadastrado), Times.Once);
        
    }
}