using EseroidesToDo.Infrastructure.Repositories;
using EsteroidesToDo.Application.Interfaces.Usuario;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Domain.Interfaces.Notificaciones;
using EsteroidesToDo.Infrastructure.Notifications;
using EsteroidesToDo.Infrastructure.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using StackExchange.Redis;

namespace EsteroidesToDo.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServiceRegistration(
            this IServiceCollection services,
            IConfiguration config)
        {
            // Repos
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IVacanteRepository, VacanteRepository>();

            var redisConn = config.GetConnectionString("Redis");
            if (!string.IsNullOrEmpty(redisConn))
            {
                services.AddSingleton<IConnectionMultiplexer>(
                    _ => ConnectionMultiplexer.Connect(redisConn));
                services.AddScoped<IEmailRateLimiter, EmailRateLimiter>();
            }

            // Observator Pattern
            services.AddScoped<INotificacionRepository, NotificacionRepository>();
            services.AddScoped<INotificacionObserver, DbNotifier>();

            return services;
        }

    }
}
