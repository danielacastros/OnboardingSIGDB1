using System.Threading.Tasks;
using AutoMapper;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services;

public class ArmazenadorDeEmpresa  
{
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly INotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public ArmazenadorDeEmpresa(IEmpresaRepositorio empresaRepositorio, 
        INotificationContext notificationContext, 
        IMapper mapper)
    {
        _empresaRepositorio = empresaRepositorio;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task Armazenar(EmpresaDto empresaDto)
    {
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresaDto.Cnpj);
        var empresaExistente = await _empresaRepositorio.BuscarPorCnpj(cnpjFormatado);
        
        if (empresaExistente == null)
        {
            Empresa empresa = _mapper.Map<Empresa>(empresaDto);
            
            if (empresa.Invalid)
            {
                _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.DadosInvalidos);
                return;
            }
            
            await _empresaRepositorio.Adicionar(empresa);
        }else
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.CnpjCadastrado);
        }
    }

    public async Task Alterar(int id, AlterarEmpresaDto alterarEmpresaDto)
    {
        var empresaAlterar = await _empresaRepositorio.ObterPorId(id);
        
        if (empresaAlterar == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.EmpresaNaoEncontrada);
        }
        else
        {
            Empresa empresa = _mapper.Map<Empresa>(alterarEmpresaDto);
            empresaAlterar.Alterar(empresa);
            await _empresaRepositorio.Alterar(empresaAlterar);
        }
    }

    public async Task Excluir(int id)
    {
        var empresa = await _empresaRepositorio.ObterPorId(id);
        
        if (empresa == null)
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.EmpresaNaoEncontrada);
        else
            await _empresaRepositorio.Excluir(empresa);
        
    }
}