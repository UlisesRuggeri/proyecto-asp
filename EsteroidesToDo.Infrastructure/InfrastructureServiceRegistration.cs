using EseroidesToDo.Infrastructure.Repositories;
using EsteroidesToDo.Domain.Interfaces;
using EsteroidesToDo.Infrastructure.Repositories;  
using Microsoft.Extensions.DependencyInjection;

namespace EsteroidesToDo.Infrastructure.DependencyInjection
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddInfrastructureServiceRegistration(this IServiceCollection services)
        {
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IEmpresaRepository, EmpresaRepository>();
            services.AddScoped<IVacanteRepository, VacanteRepository>();


            return services;
        }
    }
}
