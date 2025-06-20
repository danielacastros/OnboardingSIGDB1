using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.API.Profiles;

public class FuncionarioCargoProfile : Profile
{
    public FuncionarioCargoProfile()
    {
        CreateMap<FuncionarioCargoDto, FuncionarioCargo>();
    }
}