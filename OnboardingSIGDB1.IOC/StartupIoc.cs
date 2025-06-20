using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnboardingSIGDB1.Data.Contextos;
using OnboardingSIGDB1.Data.Repositorios;
using OnboardingSIGDB1.Domain.Interfaces;
using OnboardingSIGDB1.Domain.Interfaces.Cargos;
using OnboardingSIGDB1.Domain.Interfaces.Empresas;
using OnboardingSIGDB1.Domain.Interfaces.Funcionarios;
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
            services.AddScoped(typeof(IFuncionarioCargoRepositorio), typeof(FuncionarioCargoRepositorio));
            services.AddScoped(typeof(ICargoRepositorio), typeof(CargoRepositorio));
            services.AddScoped(typeof(INotificationContext), typeof(NotificationContext));
            services.AddScoped(typeof(IEmpresaService), typeof(EmpresaService));
            services.AddScoped(typeof(IFuncionarioService), typeof(FuncionarioService));
            services.AddScoped(typeof(ICargoService), typeof(CargoService));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<EmpresaService>();
            services.AddScoped<FuncionarioService>();
            services.AddScoped<CargoService>();
            services.AddScoped<NotificationContext>();
            
        }
        
        
    }
}