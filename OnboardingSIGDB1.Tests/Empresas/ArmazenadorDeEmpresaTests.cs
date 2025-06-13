using AutoMapper;
using Bogus;
using Moq;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Utils;
using OnboardingSIGDB1.Tests._Builders;
using Xunit.Abstractions;

namespace OnboardingSIGDB1.Tests.Empresas;

public class ArmazenadorDeEmpresaTests
{
    private readonly ITestOutputHelper _output;
    private readonly Faker _faker;
    private readonly Mock<IEmpresaRepositorio> _empresaRepositorioMock;
    private readonly Mock<IFuncionarioRepositorio> _funcionarioRepositorioMock;
    private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
    private readonly Mock<INotificationContext> _notificationContextMock;
    private readonly Mock<IMapper> _mapperMock;

    public ArmazenadorDeEmpresaTests(ITestOutputHelper output)
    {
        _output = output;
        _faker = new Faker();
        _empresaRepositorioMock = new Mock<IEmpresaRepositorio>();
        _funcionarioRepositorioMock = new Mock<IFuncionarioRepositorio>();
        _notificationContextMock = new Mock<INotificationContext>();
        _mapperMock = new Mock<IMapper>();
        _armazenadorDeEmpresa = new ArmazenadorDeEmpresa(_empresaRepositorioMock.Object, _funcionarioRepositorioMock.Object, _notificationContextMock.Object, _mapperMock.Object);
    }

    [Fact]
    public async Task QuandoDadosValidos_DeveArmazenarEmpresa()
    {
        //arrange
        var empresaDto = EmpresaDtoBuilder.Novo().Build();
        
        var empresa = EmpresaBuilder
            .Nova()
            .ComNome(empresaDto.Nome)
            .ComCnpj(empresaDto.Cnpj)
            .ComDataFundacao(empresaDto.DataFundacao)
            .Build();
        
        _mapperMock.Setup(m => m.Map<Empresa>(empresaDto))
            .Returns(empresa);
        
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresaDto.Cnpj);
        
        _empresaRepositorioMock
            .Setup(r => r.BuscarPorCnpj(cnpjFormatado))
            .ReturnsAsync((Empresa)null);
        
        //act 
        await _armazenadorDeEmpresa.Armazenar(empresaDto);

        //assert
        _empresaRepositorioMock.Verify(r => r.Adicionar(It.Is<Empresa>(x =>
            x.Nome == empresaDto.Nome &&
            x.Cnpj == CnpjHelper.FormatarCnpj(empresaDto.Cnpj) &&
            x.DataFundacao == empresaDto.DataFundacao)), Times.Once());

        _notificationContextMock.Verify(
            x => x.AddNotification(It.IsAny<Notification>()),
            Times.Never
        );
    }

    [Fact]
    public async Task QuandoCnpjJaCadastrado_NaoDeveArmazenarEmpresa()
    {
        //arrange
        var empresaEsperada = EmpresaBuilder.Nova()
            .ComCnpj("08914016000111")
            .Build();

        var empresaDto = EmpresaDtoBuilder.Novo().ComCnpj("08914016000111").Build();

        _empresaRepositorioMock
            .Setup(r => r.BuscarPorCnpj(empresaEsperada.Cnpj))
            .ReturnsAsync(empresaEsperada);
        
        //act
        await _armazenadorDeEmpresa.Armazenar(empresaDto);
        
        //assert
        _empresaRepositorioMock.Verify(e => 
            e.Adicionar(It.IsAny<Empresa>()), Times.Never);
        _notificationContextMock.Verify(x =>
                x.AddNotification(Resource.KeyEmpresa, Resource.CnpjCadastrado), Times.Once);
    }

    [Fact]
    public async Task QuandoCnpjNaoInformado_NaoDeveArmazenar()
    {
        //arrange
        var empresaDto = EmpresaDtoBuilder.Novo().ComCnpj("").Build();
        var empresa = EmpresaBuilder
            .Nova()
            .ComNome(empresaDto.Nome)
            .ComCnpj("")
            .ComDataFundacao(empresaDto.DataFundacao)
            .Build();
        
        _mapperMock.Setup(m => m.Map<Empresa>(empresaDto))
            .Returns(empresa);
        
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresaDto.Cnpj);
        
        _empresaRepositorioMock
            .Setup(r => r.BuscarPorCnpj(cnpjFormatado))
            .ReturnsAsync((Empresa)null);
        
        //act
        await _armazenadorDeEmpresa.Armazenar(empresaDto);

        //assert
        _empresaRepositorioMock.Verify(e => 
            e.Adicionar(It.IsAny<Empresa>()), Times.Never);
        //_notificationContextMock.Verify(x => 
         //   x.AddNotification(Resource.KeyEmpresa, Resource.CnpjInvalido), Times.Once);
    }

    [Fact]
    public async Task QuandoNomeNaoInformado_NaoDeveArmazenar()
    {
        //arrange
        var empresaDto = EmpresaDtoBuilder.Novo().ComNome("").Build();
        var empresa = EmpresaBuilder
            .Nova()
            .ComNome("")
            .ComCnpj(empresaDto.Cnpj)
            .ComDataFundacao(empresaDto.DataFundacao)
            .Build();
        
        _mapperMock.Setup(m => m.Map<Empresa>(empresaDto))
            .Returns(empresa);
        _output.WriteLine("empresaDto ==>" + empresaDto.Nome.ToString(), empresaDto.DataFundacao.ToString(), empresaDto.Cnpj.ToString());
        _output.WriteLine("empresa ==>" + empresa.Nome.ToString(), empresa.DataFundacao.ToString(), empresa.Cnpj.ToString());
        //act
        await _armazenadorDeEmpresa.Armazenar(empresaDto);

        //assert
        _empresaRepositorioMock.Verify(e => 
            e.Adicionar(It.IsAny<Empresa>()), Times.Never);
        _notificationContextMock.Verify(x => 
            x.AddNotification("Nome", "'Nome' deve ser informado."), Times.Once);
    }
    
    [Fact]
    public async Task QuandoDataDeFundacaoMenorQueValorMinimo_NaoDeveArmazenarEmpresa()
    {
        //arrange
        var empresaDto = EmpresaDtoBuilder.Novo().ComDataFundacao(DateTime.MinValue).Build();
        var empresa = EmpresaBuilder
            .Nova()
            .ComNome(empresaDto.Nome)
            .ComCnpj(empresaDto.Cnpj)
            .ComDataFundacao(DateTime.MinValue)
            .Build();
        
        _mapperMock.Setup(m => m.Map<Empresa>(empresaDto))
            .Returns(empresa);
        
        //act
        await _armazenadorDeEmpresa.Armazenar(empresaDto);

        //assert
        _empresaRepositorioMock.Verify(e => 
            e.Adicionar(It.IsAny<Empresa>()), Times.Never);
        _notificationContextMock.Verify(x => 
            x.AddNotification(nameof(Empresa.DataFundacao), Resource.DataInvalida), Times.Once);
    }

    [Fact]
    public async Task QuandoDadosValidos_DeveAlterarEmpresa()
    {
        // arrange
        var empresaAlterarDto = AlterarEmpresaDtoBuilder.Novo().Build();
        var empresa = EmpresaBuilder.Nova().Build();
        _empresaRepositorioMock.Setup(r => r.ObterPorId(empresaAlterarDto.Id)).ReturnsAsync(empresa);
        
        // act 
        await _armazenadorDeEmpresa.Alterar(empresaAlterarDto.Id, empresaAlterarDto);
        
        // assert
        _empresaRepositorioMock.Verify(r =>  r.Alterar(It.IsAny<Empresa>()), Times.Once());
        _empresaRepositorioMock.Verify(r => r.Alterar(It.Is<Empresa>(x =>
            x.Id == empresa.Id &&
            x.Nome == empresa.Nome &&
            x.Cnpj == CnpjHelper.FormatarCnpj(empresa.Cnpj) &&
            x.DataFundacao == empresa.DataFundacao)), Times.Once());
        _notificationContextMock.Verify(
            x => x.AddNotification(It.IsAny<Notification>()), Times.Never);
    }

    [Fact]
    public async Task QuandoEmpresaExistir_DeveExcluirEmpresa()
    {
        // arrange
        var empresa = EmpresaBuilder.Nova().Build();
        _empresaRepositorioMock.Setup(r => r.ObterPorId(empresa.Id))
            .ReturnsAsync(empresa);
        
        // act 
        await _armazenadorDeEmpresa.Excluir(empresa.Id);
        // assert
        _empresaRepositorioMock.Verify(r => r.Excluir(It.IsAny<Empresa>()), Times.Once);
        _notificationContextMock.Verify(
            x => x.AddNotification(It.IsAny<Notification>()), Times.Never);
    }
    
    [Fact]
    public async Task QuandoEmpresaNaoExistir_NaoDeveExcluirEmpresa()
    {
        // arrange
        var id = -1;
        
        // act 
        await _armazenadorDeEmpresa.Excluir(id);
        
        // assert
        _empresaRepositorioMock.Verify(r => r.Excluir(It.IsAny<Empresa>()), Times.Never);
        _notificationContextMock.Verify(
            x => x.AddNotification(Resource.KeyEmpresa, Resource.EmpresaNaoEncontrada), Times.Once);
    }
    
}