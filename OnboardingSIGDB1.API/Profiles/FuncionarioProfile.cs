using AutoMapper;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.API.Profiles;

public class FuncionarioProfile : Profile
{
    public FuncionarioProfile()
    {
        CreateMap<FuncionarioDto, Funcionario>();
        
        CreateMap<Funcionario, BuscarFuncionarioDto>();
        CreateMap<Funcionario, BuscarFuncionariosComEmpresaDto>()
            .ForMember(funcDto => funcDto.Empresa, opt => opt.MapFrom(funcionario => funcionario.Empresa));
    }
    
}