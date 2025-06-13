using System.Runtime.InteropServices.JavaScript;
using System.Threading.Tasks;
using AutoMapper;
using FluentValidation.Results;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services;

public class ArmazenadorDeFuncionario
{
    private readonly IFuncionarioRepositorio _funcionarioRepositorio;
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly INotificationContext _notificationContext;
    private readonly IMapper _mapper;
    
    public ArmazenadorDeFuncionario(IFuncionarioRepositorio funcionarioRepositorio, 
        IEmpresaRepositorio empresaRepositorio,
        INotificationContext notificationContext, 
        IMapper mapper)
    {
        _funcionarioRepositorio = funcionarioRepositorio;
        _empresaRepositorio = empresaRepositorio;
        _notificationContext = notificationContext;
        _mapper = mapper;
    }

    public async Task Armazenar(FuncionarioDto funcionarioDto)
    {
        if (funcionarioDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.DadosNaoFornecidos);
            return;
        }
        
        var cpfFormatado = CpfHelper.FormatarCpf(funcionarioDto.Cpf);
        if (string.IsNullOrWhiteSpace(cpfFormatado))
        {
            _notificationContext.AddNotification(nameof(Funcionario.Cpf), Resource.CpfObrigatorio);
            return;
        }

        var funcionarioExistente = await _funcionarioRepositorio.BuscarPorCpf(cpfFormatado);
        if (funcionarioExistente != null)
        {
            _notificationContext.AddNotification(nameof(Funcionario.Cpf), Resource.CpfJaCadastrado);
            return;
        }

        Funcionario funcionario = _mapper.Map<Funcionario>(funcionarioDto);
        
        if (funcionario.Invalid)
        {
            foreach (var erro in funcionario.ValidationResult.Errors)
            {
                _notificationContext.AddNotification(erro.PropertyName, erro.ErrorMessage);
            }
            return;
        }
        
        await _funcionarioRepositorio.Adicionar(funcionario);
    }

    public async Task Alterar(int id, FuncionarioDto funcionarioDto)
    {
        var funcionario = await _funcionarioRepositorio.ObterPorId(id);
        if (funcionario == null)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.FuncionarioNaoEncontrado);
            return;
        }

        funcionario.AlterarNome(funcionarioDto.Nome);
        funcionario.AlterarCpf(funcionarioDto.Cpf);
        funcionario.AlterarDataContratacao(funcionarioDto.DataContratacao);

        if (funcionario.Invalid)
        {
            foreach (var erro in funcionario.ValidationResult.Errors)
            {
                _notificationContext.AddNotification(erro.PropertyName, erro.ErrorMessage);
            }

            return;
        }
        await _funcionarioRepositorio.Alterar(funcionario);
    }

    public async Task Excluir(int id)
    {
        var funcionario = await _funcionarioRepositorio.ObterPorId(id);

        if (funcionario == null)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.FuncionarioNaoEncontrado);
            return;
        }

        await _funcionarioRepositorio.Excluir(funcionario);
    }

    public async Task VincularEmpresa(VinculoEmpresaDto vinculoEmpresaDto)
    {
        if (vinculoEmpresaDto == null)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.DadosNaoFornecidos);
            return;
        }

        var funcionario = await _funcionarioRepositorio.ObterPorId(vinculoEmpresaDto.FuncionarioId);
        if (funcionario == null)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.FuncionarioNaoEncontrado);
            return;
        }
        
        var empresa = await _empresaRepositorio.ObterPorId(vinculoEmpresaDto.EmpresaId);
        if (empresa == null)
        {
            _notificationContext.AddNotification(Resource.KeyEmpresa, Resource.EmpresaNaoEncontrada);
            return;
        }
        
        var vinculoFuncionarioEmpresa = funcionario.VincularEmpresa(empresa);
        if (!vinculoFuncionarioEmpresa)
        {
            _notificationContext.AddNotification(Resource.KeyFuncionario, Resource.VinculoJaCadastrado);
            return;
        }

        await _empresaRepositorio.Alterar(empresa);
    }
}