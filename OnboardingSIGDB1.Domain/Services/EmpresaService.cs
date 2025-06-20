using System.Threading.Tasks;
using AutoMapper;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Empresas;
using OnboardingSIGDB1.Domain.Interfaces.Funcionarios;
using OnboardingSIGDB1.Domain.Utils;
namespace OnboardingSIGDB1.Domain.Services;

public class EmpresaService : IEmpresaService
{
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly IFuncionarioRepositorio _funcionarioRepositorio;
    private readonly INotificationContext _notificationContext;
    private readonly IMapper _mapper;

    public EmpresaService(IEmpresaRepositorio empresaRepositorio, IFuncionarioRepositorio funcionarioRepositorio,
        INotificationContext notificationContext, 
        IMapper mapper)
    {
        _empresaRepositorio = empresaRepositorio;
        _funcionarioRepositorio = funcionarioRepositorio;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task Armazenar(EmpresaDto empresaDto)
    {
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresaDto.Cnpj);
        if (string.IsNullOrEmpty(cnpjFormatado))
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.CnpjInvalido);
            return;
        }
        
        var empresaExistente = await _empresaRepositorio.BuscarPorCnpj(cnpjFormatado);

        if (empresaExistente != null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.CnpjCadastrado);
            return;
        }
        
        Empresa empresa = _mapper.Map<Empresa>(empresaDto);
        
        if (empresa == null || empresa.Invalid)
        {
            foreach (var erro in empresa.ValidationResult.Errors)
            {
                _notificationContext.AddNotification(erro.PropertyName, erro.ErrorMessage);
            }
            
            return;
        }

        //empresa.Validar(empresa);
        await _empresaRepositorio.Adicionar(empresa);
    }

    public async Task Alterar(int id, AlterarEmpresaDto alterarEmpresaDto)
    {
        var empresa = await _empresaRepositorio.ObterPorId(id);
        
        if (empresa == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.EmpresaNaoEncontrada);
            return;
        }
            
        empresa.AlterarNome(alterarEmpresaDto.Nome);
        empresa.AlterarCnpj(alterarEmpresaDto.Cnpj);
        empresa.AlterarDataFundacao(alterarEmpresaDto.DataFundacao);
        
        if (empresa.Invalid)
        {
            foreach (var error in empresa.ValidationResult.Errors)
            {
                _notificationContext.AddNotification(error.PropertyName, error.ErrorMessage);
            }

            return;
        }
        //empresa.Validar(empresa);
        await _empresaRepositorio.Alterar(empresa);
    }

    public async Task Excluir(int id)
    {
        var empresa = await _empresaRepositorio.ObterPorId(id);
        
        if (empresa == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.EmpresaNaoEncontrada);
            return;
        }

        var possuiFuncionario = await _funcionarioRepositorio.VerificarSePossuiFuncionarioVinculado(empresa.Id);
        if (possuiFuncionario.Count > 0)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.NaoEPossivelExcluirEmpresa);
            return;
        }
        
        await _empresaRepositorio.Excluir(empresa);
    }
}