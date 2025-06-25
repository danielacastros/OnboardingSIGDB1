using System.Reflection;
using Azure;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.IOC;

var builder = WebApplication.CreateBuilder(args);

StartupIoc.ConfigureServices(builder.Services, builder.Configuration);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddControllers(options =>
{
    options.Filters.Add<NotificationFilter>();
});
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure middleware
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (context, next) =>
{
    await next.Invoke();
    string method = context.Request.Method;
    var allowedMethodsToCommit = new string[] { "POST", "PUT", "PATCH", "DELETE" };
    
    if (allowedMethodsToCommit.Contains(method) && context.Response.StatusCode >= 200 && context.Response.StatusCode < 300)
    {
        var unitOfWork = (IUnitOfWork)context.RequestServices.GetService(typeof(IUnitOfWork));
        
        var contextoDeNotificacaoDeDominio = context.RequestServices.GetService(typeof(INotificationContext));
        var notificacaoDeDominio = (INotificationContext)contextoDeNotificacaoDeDominio;
        if (!notificacaoDeDominio.HasNotifications)
        {
            await unitOfWork.Commit();
        }
    }
});

app.UseDeveloperExceptionPage();

app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();