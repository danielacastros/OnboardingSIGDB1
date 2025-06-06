using AutoMapper;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.API.Profiles;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<FuncionarioDto, Funcionario>();
    }
}