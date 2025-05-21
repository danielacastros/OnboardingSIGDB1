using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Domain.Notifications.Handlers;

public class CriarEmpresaHandler
{
    private readonly NotificationContext _notificationContext;
    private readonly IEmpresaRepositorio _empresaRepositorio;

    public CriarEmpresaHandler(NotificationContext notificationContext, IEmpresaRepositorio empresaRepositorio)
    {
        _notificationContext = notificationContext;
        _empresaRepositorio = empresaRepositorio;
    }
    
    
}