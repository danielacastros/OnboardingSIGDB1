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

        if (empresaDto.Id == 0 && empresaExistente == null)
        {
            var empresa = new Empresa(empresaDto.Cnpj, empresaDto.Nome, empresaDto.DataFundacao);
            _empresaRepositorio.Adicionar(empresa);
        }
    }

    public void Alterar(EmpresaDto empresaDto)
    {
        var empresa = _empresaRepositorio.BuscarPorCnpj(empresaDto.Cnpj);
        if (empresa == null)
            return;
        
        empresa.Alterar(empresaDto);
        _empresaRepositorio.Alterar(empresa);
    }
}