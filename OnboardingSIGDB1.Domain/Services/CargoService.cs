using System.Threading.Tasks;
using AutoMapper;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto.Cargo;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Cargos;

namespace OnboardingSIGDB1.Domain.Services;

public class CargoService : ICargoService
{
    private readonly ICargoRepositorio _cargoRepositorio;
    private readonly INotificationContext _notificationContext;
    private readonly IMapper _mapper;
    
    public CargoService(ICargoRepositorio cargoRepositorio,
        INotificationContext notificationContext,
        IMapper mapper)
    {
        _cargoRepositorio = cargoRepositorio;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task Armazenar(CargoDto cargoDto)
    {
        Cargo cargo = _mapper.Map<Cargo>(cargoDto);
        if (cargo == null || cargo.Invalid)
        {
            foreach (var erro in cargo.ValidationResult.Errors)
            {
                _notificationContext.AddNotification(erro.PropertyName, erro.ErrorMessage);
            }
            
            return;
        }

        await _cargoRepositorio.Adicionar(cargo);
    }

    public async Task Alterar(int id, CargoDto cargoDto)
    {
        var cargo = await _cargoRepositorio.ObterPorId(id);
        
        if(cargo == null)
        {
            _notificationContext.AddNotification(Resource.KeyCargo, Resource.CargoNaoEncontrado);
            return;
        }

        cargo.AlterarDescricao(cargoDto.Descricao);

        if (cargo.Invalid)
        {
            foreach (var erro in cargo.ValidationResult.Errors)
            {
                _notificationContext.AddNotification(erro.PropertyName, erro.ErrorMessage);
            }
            
            return;
        }

        await _cargoRepositorio.Alterar(cargo);
    }

    public async Task Excluir(int id)
    {
        var cargo = await _cargoRepositorio.ObterPorId(id);

        if (cargo == null)
        {
            _notificationContext.AddNotification(Resource.KeyCargo, Resource.CargoNaoEncontrado);
            return;
        }

        await _cargoRepositorio.Excluir(cargo);
    }
}