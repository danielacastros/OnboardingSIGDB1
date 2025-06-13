using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Data.Repositorios;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Notifications;
using OnboardingSIGDB1.Domain.Services;

namespace OnboardingSIGDB1.IOC
{
    public class StartupIoc
    {
        public static void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            
            services.AddScoped(typeof(IRepositorio<>), typeof(RepositorioBase<>));
            services.AddScoped(typeof(IEmpresaRepositorio), typeof(EmpresaRepositorio));
            services.AddScoped(typeof(IFuncionarioRepositorio), typeof(FuncionarioRepositorio));
            services.AddScoped(typeof(ICargoRepositorio), typeof(CargoRepositorio));
            services.AddScoped(typeof(INotificationContext), typeof(NotificationContext));
            services.AddScoped<ArmazenadorDeEmpresa>();
            services.AddScoped<ArmazenadorDeFuncionario>();
            services.AddScoped<ArmazenadorDeCargo>();
            services.AddScoped<NotificationContext>();
            
        }
    }
}