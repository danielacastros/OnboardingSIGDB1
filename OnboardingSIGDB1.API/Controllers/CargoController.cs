using Microsoft.AspNetCore.Mvc;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto.Cargo;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.API.Controllers;
[ApiController]
[Route("api/cargos")]
public class CargoController : ControllerBase
{

    private readonly ArmazenadorDeCargo _armazenadorDeCargo;
    private readonly ICargoRepositorio _cargoRepositorio;
    private readonly INotificationContext _notificationContext;
    
    public CargoController(ArmazenadorDeCargo armazenadorDeCargo,
        ICargoRepositorio cargoRepositorio,
        INotificationContext notificationContext)
    {
        _armazenadorDeCargo = armazenadorDeCargo;
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

        await _armazenadorDeCargo.Armazenar(cargoDto);
        
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

        await _armazenadorDeCargo.Alterar(id, cargoDto);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok();
    }

    [HttpDelete]
    public async Task<IActionResult> Delete(int id)
    {
        await _armazenadorDeCargo.Excluir(id);
        
        if (_notificationContext.HasNotifications)
        {
            return BadRequest(_notificationContext.Notifications);
        }

        return Ok();
    }
}