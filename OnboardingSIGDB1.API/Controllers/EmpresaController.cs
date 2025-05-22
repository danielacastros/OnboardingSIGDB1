using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmpresaController : Controller
{
    private readonly ArmazenadorDeEmpresa _armazenadorDeEmpresa;
    private readonly IRepositorio<Empresa> _empresaRepositorio;

    public EmpresaController(ArmazenadorDeEmpresa armazenadorDeEmpresa, IRepositorio<Empresa> empresaRepositorio)
    {
        _armazenadorDeEmpresa = armazenadorDeEmpresa;
        _empresaRepositorio = empresaRepositorio;
    }

    /// <summary>
    /// Cadastro dos dados de uma empresa.
    /// </summary>
    /// <param name="empresaDto"></param>
    /// <returns>Retorna um status HTTP 200 (OK) se a operação for bem-sucedida.</returns>
    [HttpPost]
    public IActionResult Salvar(EmpresaDto empresaDto)
    {
        _armazenadorDeEmpresa.Armazenar(empresaDto);
        return Ok();
    }

    /// <summary>
    /// Altera os dados de uma empresa existente.
    /// </summary>
    /// <param name="empresaDto">Objeto contendo os dados atualizados da empresa.</param>
    /// <returns>Retorna um status HTTP 200 (OK) se a operação for bem-sucedida.</returns>
    [HttpPut]
    public IActionResult Alterar(EmpresaDto empresaDto)
    {
        _armazenadorDeEmpresa.Alterar(empresaDto);
        return Ok();
    }
}