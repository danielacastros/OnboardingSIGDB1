using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using OnboardingSIGDB1.Domain.Interfaces;

namespace OnboardingSIGDB1.Domain.Notifications;

public class NotificationFilter : IAsyncResultFilter 
{
    private INotificationContext _notificationContext;
    
    public NotificationFilter(INotificationContext notificationContext)
    {
        _notificationContext = notificationContext;
    }

    public async Task OnResultExecutionAsync(ResultExecutingContext context, ResultExecutionDelegate next)
    {
        if (_notificationContext.HasNotifications)
        {
            context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            context.HttpContext.Response.ContentType= "application/json";

            var notifications = JsonConvert.SerializeObject(_notificationContext.Notifications);
            await context.HttpContext.Response.WriteAsync(notifications);

            return;
        }
        
        await next();
    }
}