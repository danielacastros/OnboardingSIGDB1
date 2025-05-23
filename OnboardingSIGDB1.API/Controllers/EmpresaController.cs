using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Services;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.API.Controllers;

[ApiController]
[Route("api/empresas")]
public class EmpresaController : ControllerBase
{
    private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private IMapper _mapper;

    public EmpresaController(ArmazenadorDeEmpresa armazenadorDeEmpresa, IEmpresaRepositorio empresaRepositorio, IMapper mapper)
    {
        _armazenadorDeEmpresa = armazenadorDeEmpresa;
        _empresaRepositorio = empresaRepositorio;
        _mapper = mapper;
    }

    /// <summary>
    /// Busca todas as empresas cadastradas no banco de dados.
    /// </summary>
    /// <param name="BuscarEmpresasDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    public async Task<IActionResult> ObterTodas()
    {
        var empresas = await _empresaRepositorio.ObterTodas();
        
        var empresaFormatoDtos = empresas.Select(e => new BuscarEmpresasDto
        {
            Id = e.Id,
            Nome = e.Nome,
            Cnpj = CnpjHelper.FormatarCnpjFormatoPadrao(e.Cnpj),
            DataFundacao = e.DataFundacao.ToString("dd/MM/yyyy")
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
    public async Task<IActionResult> ObterPorIntervaloDeDataFundacao([FromQuery] DateTime dataInicial, [FromQuery] DateTime dataFinal)
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
            DataFundacao = e.DataFundacao.ToString("dd/MM/yyyy")
        }).ToList();
            
        return Ok(listaDeEmpresasRetornada);
        
    }

    [HttpGet("pesquisar")]
    public async Task<IActionResult> ObterEmpresaPorNome([FromQuery] string? nome)
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
            DataFundacao = e.DataFundacao.ToString("dd/MM/yyyy")
        }).ToList();
        
        return Ok(empresaOk);
    }
    
    /// <summary>
    /// Adiciona os dados de uma empresa ao banco de dados.
    /// </summary>
    /// <param name="criarEmpresaDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public async Task<IActionResult> Salvar([FromBody] CriarEmpresaDto criarEmpresaDto)
    {
        Empresa empresa = _mapper.Map<Empresa>(criarEmpresaDto);
        await _armazenadorDeEmpresa.Armazenar(empresa);
        return Ok();
    }

    /// <summary>
    /// Altera os dados de uma empresa existente.
    /// </summary>
    /// <param name="alterarEmpresaDto">Objeto contendo os dados atualizados da empresa.</param>
    /// <returns>IActionResult</returns>
    /// <response code="204">Caso inserção seja feita com sucesso</response>
    [HttpPut("{id}")]
    public async Task<IActionResult> Alterar(int id, [FromBody] AlterarEmpresaDto alterarEmpresaDto)
    {
        var empresa = _mapper.Map<Empresa>(alterarEmpresaDto);
        await _armazenadorDeEmpresa.Alterar(empresa);
        
        return Ok();
    }

    /// <summary>
    /// Exclui os dados de uma empresa do banco de dados.
    /// </summary>
    /// <param name="id">Id da empresa.</param>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        await _armazenadorDeEmpresa.Excluir(id);
        return Ok();
    }
}