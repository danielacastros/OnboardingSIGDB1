using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Cargo;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.API.Profiles;

public class CargoProfile : Profile
{
    public CargoProfile()
    {
        CreateMap<CargoDto, Cargo>();
    }
}