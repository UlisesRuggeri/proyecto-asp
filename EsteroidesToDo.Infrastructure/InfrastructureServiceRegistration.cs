using EseroidesToDo.Infrastructure.Repositories;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Domain.Interfaces.Notificaciones;
using EsteroidesToDo.Infrastructure.Notifications;
using EsteroidesToDo.Infrastructure.Repositories;  
using Microsoft.Extensions.DependencyInjection;

namespace EsteroidesToDo.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServiceRegistration(this IServiceCollection services)
        {
            //Repos
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IVacanteRepository, VacanteRepository>();

            //Observator Pattern

            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<INotificacionObserver, DbNotifier>();
            return services;
        }
    }
}
