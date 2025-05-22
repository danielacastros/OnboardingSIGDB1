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

    public void Armazenar(Empresa empresa)
    {
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresa.Cnpj);
        var empresaExistente = _empresaRepositorio.BuscarPorCnpj(cnpjFormatado);
        
        if (empresaExistente == null)
        {
            var empresaSalvar = new Empresa(cnpjFormatado, empresa.Nome, empresa.DataFundacao);
            if (empresaSalvar.Invalid)
            {
                _notificationContext.AddNotifications(empresaSalvar.ValidationResult);
                return;
            }
            
            _empresaRepositorio.Adicionar(empresa);
        }else if (empresaExistente != null)
        {
            _notificationContext.AddNotification("Empresa", "Esse CNPJ já foi cadastrado.");
        }
    }

    public void Alterar(Empresa empresa)
    {
        var cnpjFormatado = CnpjHelper.FormatarCnpj(empresa.Cnpj);
        var empresaAlterar = _empresaRepositorio.ObterPorId(empresa.Id);
        
        if (empresaAlterar == null)
        {
            _notificationContext.AddNotification("Empresa", "O CNPJ não foi encontrado.");
        }
        else
        {
            empresaAlterar.Alterar(empresa, cnpjFormatado);
            _empresaRepositorio.Alterar(empresaAlterar);
        }
    }

    public void Excluir(int id)
    {
        var empresa = _empresaRepositorio.ObterPorId(id);
        if (empresa == null)
        {
            _notificationContext.AddNotification("Empresa", "Erro ao excluir. Empresa não encontrada.");
        }
        else
        {
            _empresaRepositorio.Excluir(empresa);
        }
    }
}