using System.Threading.Tasks;
using AutoMapper;
using OnboardingSIGDB1.Domain.Base;
using OnboardingSIGDB1.Domain.Dto.Funcionario;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services;

public class ArmazenadorDeFuncionario
{
    private readonly IFuncionarioRepositorio _funcionarioRepositorio;
    private readonly INotificationContext _notificationContext;
    private readonly IMapper _mapper;
    
    public ArmazenadorDeFuncionario(IFuncionarioRepositorio funcionarioRepositorio, INotificationContext notificationContext, IMapper mapper)
    {
        _funcionarioRepositorio = funcionarioRepositorio;
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
}