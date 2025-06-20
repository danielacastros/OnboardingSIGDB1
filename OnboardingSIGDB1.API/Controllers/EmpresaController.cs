

using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Empresas;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.API.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresaController : ControllerBase
{
    private readonly IEmpresaService _empresaService;
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly INotificationContext _notificationContext;

    public EmpresaController(IEmpresaService empresaService, 
        IEmpresaRepositorio empresaRepositorio, 
        INotificationContext notificationContext)
    {
        _empresaService = empresaService;
        _empresaRepositorio = empresaRepositorio;
        _notificationContext = notificationContext;
        
    }

    /// <summary>
    /// Busca todas as empresas cadastradas no banco de dados.
    /// </summary>
    /// <param name="BuscarEmpresasDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var empresas = await _empresaRepositorio.ObterTodas();
        
        var empresaFormatoDtos = empresas.Select(e => new BuscarEmpresasDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Cnpj = CnpjHelper.FormatarCnpjFormatoPadrao(e.Cnpj),
            DataFundacao = e.DataFundacao
        });

        return Ok(empresaFormatoDtos);
    }

    /// <summary>
    /// Busca todas as empresas cadastradas com fundação dentro do intervalo de datas informadas.
    /// </summary>
    /// <param name="dataInicial">Data inicial</param>
    /// <param name="dataFinal">Data final</param>
    /// <returns>Lista de empresas no formato BuscarEmpresasDto</returns>
    /// <response code="200">Caso consulta seja feita com sucesso</response>
    [HttpGet("intervalo")]
    public async Task<IActionResult> Get([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
    {
        List<Empresa> empresas;
        
        if (dataInicial != DateTime.MinValue && dataFinal != DateTime.MinValue)
            empresas = await _empresaRepositorio.ObterTodasPorIntervaloDataFundacao(dataInicial, dataFinal);
        else 
            empresas = await _empresaRepositorio.ObterTodas();
            
        var listaDeEmpresasRetornada = empresas.Select(e => new BuscarEmpresasDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Cnpj = CnpjHelper.FormatarCnpjFormatoPadrao(e.Cnpj),
            DataFundacao = e.DataFundacao
        }).ToList();
            
        return Ok(listaDeEmpresasRetornada);
    }
    
    /// <summary>
    /// Busca todas as empresas cadastradas com fundação dentro do intervalo de datas informadas.
    /// </summary>
    /// <param name="dataInicial">Data inicial</param>
    /// <param name="dataFinal">Data final</param>
    /// <returns>Lista de empresas no formato BuscarEmpresasDto</returns>
    /// <response code="200">Caso consulta seja feita com sucesso</response>
    [HttpGet("intervalo-datas")]
    public async Task<ResultadoDaConsultaBase> Get([FromQuery] EmpresaFiltro filtro)
    {
        List<Empresa> empresas;
        
        if (filtro.DataInicial != DateTime.MinValue && filtro.DataFinal != DateTime.MinValue)
            empresas = await _empresaRepositorio.ObterTodasPorIntervaloDataFundacao(filtro.DataInicial, filtro.DataFinal);
        else 
            empresas = await _empresaRepositorio.ObterTodas();
        
        var listaDeEmpresasRetornada = empresas.Select(e => new BuscarEmpresasDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Cnpj = CnpjHelper.FormatarCnpjFormatoPadrao(e.Cnpj),
            DataFundacao = e.DataFundacao
        }).ToList();
            
        return new ResultadoDaConsultaBase(){Lista = listaDeEmpresasRetornada, Total = listaDeEmpresasRetornada.Count};
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var empresa = await _empresaRepositorio.ObterPorId(id);
        var empresaFormatoDto = new BuscarEmpresasDto
        {
            Id = empresa.Id,
            Nome = empresa.Nome,
            Cnpj = empresa.Cnpj,
            DataFundacao = empresa.DataFundacao
        };
        return Ok(empresaFormatoDto);
    }
    
    [HttpGet("pesquisar")]
    public async Task<IActionResult> Get([FromQuery] string? nome)
    {
        List<Empresa> empresas;
        if (nome == null)
            empresas = await _empresaRepositorio.ObterTodas();
        else
            empresas = await _empresaRepositorio.ObterEmpresaPeloNome(nome);
            
        var empresaOk = empresas.Select(e => new BuscarEmpresasDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Cnpj = CnpjHelper.FormatarCnpjFormatoPadrao(e.Cnpj),
            DataFundacao = e.DataFundacao
        }).ToList();
        
        return Ok(empresaOk);
    }
    
    [HttpGet("empresa-funcionarios/{id}")]
    public async Task<IActionResult> ListarFuncionariosVinculados(int id)
    {
        var empresa = await _empresaRepositorio.ListarFuncionariosVinculados(id);
        
        var dto = new ListarEmpresaComFuncionariosDto()
        {
            Nome = empresa.Nome,
            Cnpj = empresa.Cnpj,
            DataFundacao = empresa.DataFundacao,
            Funcionarios = empresa.Funcionarios.Select(f => new FuncionarioDto
            {
                Nome = f.Nome,
                Cpf = f.Cpf,
                DataContratacao = f.DataContratacao.Value
            }).ToList()
        };
        return Ok(dto);
    }
    
    /// <summary>
    /// Adiciona os dados de uma empresa ao banco de dados.
    /// </summary>
    /// <param name="criarEmpresaDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] EmpresaDto empresaDto)
    {
        if (empresaDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.DadosNaoFornecidos);
            return BadRequest(_notificationContext);
        }
        
        await _empresaService.Armazenar(empresaDto);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }
        
        return Ok();
    }

    /// <summary>
    /// Altera os dados de uma empresa existente.
    /// </summary>
    /// <param name="alterarEmpresaDto">Objeto contendo os dados atualizados da empresa.</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] AlterarEmpresaDto alterarEmpresaDto)
    {
        if (alterarEmpresaDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.DadosNaoFornecidos);
            return BadRequest(_notificationContext);
        }
        
        await _empresaService.Alterar(id, alterarEmpresaDto);
        
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        
        return Ok();
    }

    /// <summary>
    /// Exclui os dados de uma empresa do banco de dados.
    /// </summary>
    /// <param name="id">Id da empresa.</param>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _empresaService.Excluir(id);
        if (_notificationContext.HasNotifications)
            return BadRequest(_notificationContext.Notifications);
        return Ok();
    }
}