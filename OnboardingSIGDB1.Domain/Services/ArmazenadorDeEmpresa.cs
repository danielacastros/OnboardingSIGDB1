using System.Threading.Tasks;
using OnboardingSIGDB1.Domain.Dto;
using OnboardingSIGDB1.Domain.Entity;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Utils;

namespace OnboardingSIGDB1.Domain.Services;

public class ArmazenadorDeEmpresa  
{
    private readonly IEmpresaRepositorio _empresaRepositorio;
    private readonly NotificationContext _notificationContext;

    public ArmazenadorDeEmpresa(IEmpresaRepositorio empresaRepositorio, NotificationContext notificationContext)
    {
        _empresaRepositorio = empresaRepositorio;
        _notificationContext = notificationContext;
    }

    public async Task Armazenar(Empresa empresa)
    {
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresa.Cnpj);
        var empresaExistente = await _empresaRepositorio.BuscarPorCnpj(cnpjFormatado);
        
        if (empresaExistente == null)
        {
            var empresaSalvar = new Empresa(cnpjFormatado, empresa.Nome, empresa.DataFundacao);
            if (empresaSalvar.Invalid)
            {
                _notificationContext.AddNotifications(empresaSalvar.ValidationResult);
                return;
            }
            
            await _empresaRepositorio.Adicionar(empresaSalvar);
        }else
        {
            _notificationContext.AddNotification("Empresa", "Esse CNPJ já foi cadastrado.");
        }
    }

    public async Task Alterar(Empresa empresa)
    {
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresa.Cnpj);
        var empresaAlterar = await _empresaRepositorio.ObterPorId(empresa.Id);
        
        if (empresaAlterar == null)
        {
            _notificationContext.AddNotification("Empresa", "O CNPJ não foi encontrado.");
        }
        else
        {
            empresaAlterar.Alterar(empresa, cnpjFormatado);
            await _empresaRepositorio.Alterar(empresaAlterar);
        }
    }

    public async Task Excluir(int id)
    {
        var empresa = await _empresaRepositorio.ObterPorId(id);
        
        if (empresa == null)
            _notificationContext.AddNotification("Empresa", "Erro ao excluir. Empresa não encontrada.");
        else
            await _empresaRepositorio.Excluir(empresa);
        
    }
}