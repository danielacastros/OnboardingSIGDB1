using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Funcionarios;
using OnboardingSIGDB1.Domain.Notifications;

namespace OnboardingSIGDB1.API.Controllers;

[ApiController]
[Route("api/funcionarios")]
public class FuncionarioController : ControllerBase
{
    private readonly IFuncionarioService _funcionarioService;
    private readonly IFuncionarioRepositorio _funcionarioRepositorio;
    private readonly INotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public FuncionarioController(IFuncionarioService funcionarioService,
        IFuncionarioRepositorio funcionarioRepositorio,
        INotificationContext notificationContext,
        IMapper mapper)
    {
        _funcionarioService = funcionarioService;
        _funcionarioRepositorio = funcionarioRepositorio;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var funcionarios = await _funcionarioRepositorio.ObterTodas();

        var funcionariosFormatoDtos = funcionarios.Select(f => new BuscarFuncionarioDto
        {
            Id = f.Id,
            Nome = f.Nome,
            Cpf = f.Cpf,
            DataContratacao = f.DataContratacao,
            EmpresaId = f.EmpresaId
        });

        return Ok(funcionariosFormatoDtos);
    }
    
    /// <summary>
    /// Retorna os dados dos funcionários incluindo os respectivos vínculos com a empresa.
    /// </summary>
    /// <returns>IActionResult</returns>
    [HttpGet("todos-com-empresa")]
    public async Task<IActionResult> GetComEmpresa()
    {
        var funcionarios = await _funcionarioRepositorio.ObterTodosComEmpresa();
        var listaDeFuncionariosFormatoDto = _mapper.Map<List<BuscarFuncionariosComEmpresaDto>>(funcionarios);
        
        return Ok(listaDeFuncionariosFormatoDto);
    }
    
    [HttpGet("todos-com-empresa-e-cargo")]
    public async Task<IActionResult> GetCompleto(int id)
    {
        var funcionario = await _funcionarioRepositorio.ObterTodosComEmpresaECargo(id);
        var cargoMaisRecente = funcionario.Cargos
            .OrderByDescending(f => f.DataVinculo)
            .FirstOrDefault();
        //var listaDeFuncionarios = _mapper.Map<List<BuscarFuncionariosComEmpresaECargoDto>>(funcionarios);
        var funcionarioDto = new BuscarFuncionariosComEmpresaECargoDto
        {
            Id = funcionario.Id,
            Nome = funcionario.Nome,
            Cpf = funcionario.Cpf,
            DataContratacao = funcionario.DataContratacao,
            Empresa = new EmpresaDtoSimplificado
            {
                Nome = funcionario.Empresa.Nome,
                Cnpj = funcionario.Empresa.Cnpj
            },
            VinculoFuncionarioCargo = new VinculoFuncionarioCargoDto
            {
                Descricao = cargoMaisRecente.Cargo.Descricao
            }
            
        };
        return Ok(funcionarioDto);
    }

    [HttpGet("id")]
    public async Task<IActionResult> Get(int id)
    {
        var funcionario = await _funcionarioRepositorio.ObterPorId(id);
        var funcionarioFormatoDto = _mapper.Map<BuscarFuncionarioDto>(funcionario);
        /*var funcionarioDto = new BuscarFuncionarioDto
        {
            Id = funcionario.Id,
            Nome = funcionario.Nome,
            Cpf = funcionario.Cpf,
            DataContratacao = funcionario.DataContratacao,
            EmpresaId = funcionario.EmpresaId
        };*/
        return Ok(funcionarioFormatoDto);
    }

    [HttpGet("pesquisar")]
    public async Task<IActionResult> Get([FromQuery] string? nome)
    {
        List<Funcionario> funcionarios;
        if (nome.IsNullOrEmpty())
            funcionarios = await _funcionarioRepositorio.ObterTodas();
        else
            funcionarios = await _funcionarioRepositorio.BuscarPorNome(nome);

        var funcionarioLista = funcionarios.Select(f => new BuscarFuncionarioDto
        {
            Id = f.Id,
            Nome = f.Nome,
            Cpf = f.Cpf,
            DataContratacao = f.DataContratacao,
            EmpresaId = f.EmpresaId
        });
        
        return Ok(funcionarioLista);
    }

    [HttpGet("intervalo-datas")]
    public async Task<ResultadoDaConsultaBase> Get([FromQuery] FuncionarioFiltro filtro)
    {
        List<Funcionario> funcionarios;

        if (filtro.DataInicial != DateTime.MinValue && filtro.DataFinal != DateTime.MinValue)
            funcionarios = await _funcionarioRepositorio.ObterTodosPorIntervaloDataContratacao(filtro.DataInicial, filtro.DataFinal);
        else
            funcionarios = await _funcionarioRepositorio.ObterTodas(); 
        
        var listaDeFuncionariosRetornada = funcionarios.Select(f => new BuscarFuncionarioDto
        {
            Id = f.Id,
            Nome = f.Nome,
            Cpf = f.Cpf,
            DataContratacao = f.DataContratacao,
            EmpresaId = f.EmpresaId
        });

        return new ResultadoDaConsultaBase(){Lista = listaDeFuncionariosRetornada, Total = listaDeFuncionariosRetornada.Count()};
    }
    
    /// <summary>
    /// Adiciona os dados de um funcionário ao banco de dados.
    /// </summary>
    /// <param name="funcionarioDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] FuncionarioDto funcionarioDto)
    {
        if (funcionarioDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.DadosNaoFornecidos);
            return BadRequest(_notificationContext);
        }

        await _funcionarioService.Salvar(funcionarioDto);

        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok(funcionarioDto);
    }

    [HttpPut("vincular-empresa")]
    public async Task<IActionResult> VincularFuncionarioEmpresa([FromBody] VinculoEmpresaDto vinculoEmpresaDto)
    {
        await _funcionarioService.VincularEmpresa(vinculoEmpresaDto);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }
        
        return Ok(vinculoEmpresaDto);
    }

    [HttpPost("vincular-cargo")]
    public async Task<IActionResult> VincularCargo([FromBody] FuncionarioCargoDto funcionarioCargoDto)
    {
        await _funcionarioService.VincularCargo(funcionarioCargoDto);

        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok(funcionarioCargoDto);
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, [FromBody] FuncionarioDto funcionarioDto)
    {
        if (funcionarioDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.DadosNaoFornecidos);
            return BadRequest(_notificationContext);
        }

        await _funcionarioService.Alterar(id, funcionarioDto);
        return Ok(funcionarioDto);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _funcionarioService.Excluir(id);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }
        
        return Ok(id);
    }
}