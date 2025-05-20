using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Domain.Services;

public class ArmazenadorDeEmpresa  
{
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public ArmazenadorDeEmpresa(IEmpresaRepositorio empresaRepositorio)
    {
        _empresaRepositorio = empresaRepositorio;
    }

    public void Armazenar(EmpresaDto empresaDto)
    {
        var empresaExistente = _empresaRepositorio.BuscarPorCnpj(empresaDto.Cnpj);

        if (empresaDto.Id == 0)
        {
            var empresa = new Empresa(empresaDto.Cnpj, empresaDto.Nome, empresaDto.DataFundacao);
            _empresaRepositorio.Adicionar(empresa);
        }
        else
        {
            var empresa = _empresaRepositorio.BuscarPorCnpj(empresaDto.Cnpj);
            empresa.Alterar(empresaDto);
            //_empresaRepositorio.Alterar(empresa);
        }
    }
}