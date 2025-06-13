using AutoMapper;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;

namespace OnboardingSIGDB1.API.Profiles;

public class EmpresaProfile : Profile
{
    public EmpresaProfile()
    {
        CreateMap<EmpresaDto, Empresa>();
        CreateMap<Empresa, EmpresaDto>();
        CreateMap<AlterarEmpresaDto, Empresa>();
        CreateMap<BuscarEmpresasDto, Empresa>();
        CreateMap<Empresa, BuscarEmpresasDto>();
        CreateMap<Empresa, EmpresaDto>();
        CreateMap<Empresa, EmpresaDtoSimplificado>();
    }
}