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
    /// Cadastro de empresa.
    /// </summary>
    /// <param name="dto"></param>
    /// <returns></returns>
    [HttpPost]
    public IActionResult Salvar(EmpresaDto empresaDto)
    {
        _armazenadorDeEmpresa.Armazenar(empresaDto);
        return Ok();
    }
}