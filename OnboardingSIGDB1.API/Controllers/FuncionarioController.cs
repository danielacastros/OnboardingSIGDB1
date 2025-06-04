using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.API.Controllers;

[ApiController]
[Route("api/funcionarios")]
public class FuncionarioController : ControllerBase
{
    private readonly ArmazenadorDeFuncionario _armazenadorDeFuncionario;
    private readonly IFuncionarioRepositorio _funcionarioRepositorio;
    private readonly INotificationContext _notificationContext;

    public FuncionarioController(ArmazenadorDeFuncionario armazenadorDeFuncionario,
        IFuncionarioRepositorio funcionarioRepositorio,
        INotificationContext notificationContext)
    {
        _armazenadorDeFuncionario = armazenadorDeFuncionario;
        _funcionarioRepositorio = funcionarioRepositorio;
        _notificationContext = notificationContext;
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
        }

        await _armazenadorDeFuncionario.Armazenar(funcionarioDto);

        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok();
    }
}