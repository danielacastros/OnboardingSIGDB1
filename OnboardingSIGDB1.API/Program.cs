using Microsoft.EntityFrameworkCore;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.IOC;

var builder = WebApplication.CreateBuilder(args);

StartupIoc.ConfigureServices(builder.Services, builder.Configuration);

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

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();