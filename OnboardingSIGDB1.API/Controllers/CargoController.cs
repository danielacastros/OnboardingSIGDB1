using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto.Cargo;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Cargos;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.API.Controllers;
[ApiController]
[Route("api/cargos")]
public class CargoController : ControllerBase
{

    private readonly ICargoService _cargoService;
    private readonly ICargoRepositorio _cargoRepositorio;
    private readonly INotificationContext _notificationContext;
    
    public CargoController(ICargoService cargoService,
        ICargoRepositorio cargoRepositorio,
        INotificationContext notificationContext)
    {
        _cargoService = cargoService;
        _cargoRepositorio = cargoRepositorio;
        _notificationContext = notificationContext;
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var cargos = await _cargoRepositorio.ObterTodas();

        var cargosFormatoDto = cargos.Select(c => new BuscarCargosDto
        {
            Id = c.Id,
            Descricao = c.Descricao
        });
        return Ok(cargosFormatoDto);
    }
    
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CargoDto cargoDto)
    {
        if (cargoDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyCargo, Resource.DadosNaoFornecidos);
        }

        await _cargoService.Armazenar(cargoDto);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok();
    }

    [HttpPut]
    public async Task<IActionResult> Put(int id, CargoDto cargoDto)
    {
        if (cargoDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyCargo, Resource.DadosNaoFornecidos);
        }

        await _cargoService.Alterar(id, cargoDto);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _cargoService.Excluir(id);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok();
    }
}