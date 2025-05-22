using AutoMapper;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.API.Controllers;

[ApiController]
[Route("[controller]")]
public class EmpresaController : ControllerBase
{
    private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
    private readonly IRepositorio<Empresa> _empresaRepositorio;
    private IMapper _mapper;

    public EmpresaController(ArmazenadorDeEmpresa armazenadorDeEmpresa, IRepositorio<Empresa> empresaRepositorio, IMapper mapper)
    {
        _armazenadorDeEmpresa = armazenadorDeEmpresa;
        _empresaRepositorio = empresaRepositorio;
        _mapper = mapper;
    }

    /// <summary>
    /// Busca todas as empresas cadastradas no banco de dados.
    /// </summary>
    /// <returns>IActionResult</returns>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpGet]
    public IActionResult ObterTodas()
    {
        var empresas = _empresaRepositorio.ObterTodas();
        return Ok(empresas);
    }
    
    /// <summary>
    /// Adiciona os dados de uma empresa ao banco de dados.
    /// </summary>
    /// <param name="criarEmpresaDto"></param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPost]
    public IActionResult Salvar([FromBody] CriarEmpresaDto criarEmpresaDto)
    {
        Empresa empresa = _mapper.Map<Empresa>(criarEmpresaDto);
        _armazenadorDeEmpresa.Armazenar(empresa);
        return Ok();
    }

    /// <summary>
    /// Altera os dados de uma empresa existente.
    /// </summary>
    /// <param name="alterarEmpresaDto">Objeto contendo os dados atualizados da empresa.</param>
    /// <returns>IActionResult</returns>
    /// <response code="201">Caso inserção seja feita com sucesso</response>
    [HttpPut]
    public IActionResult Alterar([FromBody] AlterarEmpresaDto alterarEmpresaDto)
    {
        var empresa = _mapper.Map<Empresa>(alterarEmpresaDto);
        _armazenadorDeEmpresa.Alterar(empresa);
        
        return Ok();
    }

    /// <summary>
    /// Exclui os dados de uma empresa do banco de dados.
    /// </summary>
    /// <param name="id">Id da empresa.</param>
    /// <response code="200">Caso inserção seja feita com sucesso</response>
    [HttpDelete("{id}")]
    public IActionResult Excluir([FromQuery] int id)
    {
        _armazenadorDeEmpresa.Excluir(id);
        return Ok();
    }
}